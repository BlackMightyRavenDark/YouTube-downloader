using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static YouTube_downloader.Utils;

namespace YouTube_downloader
{
    public sealed class MultiThreadedDownloader
    {
        public sealed class ProgressItem
        {
            public string FileName { get; set; }
            public int Id { get; }
            public long Processed { get; }
            public long Total { get; }

            public ProgressItem(string fileName, int id, long processed, long total)
            {
                FileName = fileName;
                Id = id;
                Processed = processed;
                Total = total;
            }
        }

        public const int MEGABYTE = 1048576; //1024 * 1024;

        public const int ERROR_MERGING_CHUNKS = -200;
        public const int ERROR_BAD_FILE_NAME = -201;
        public const int ERROR_DOWNLOAD_CANCELED = -202;
        public const int ERROR_ZERO_LENGTH_CONTENT = -203;

        public delegate void DownloadStartedDelegate(object sender, long fileSize);
        public delegate void DownloadProgressDelegate(object sender, long bytesTransfered);
        public delegate void DownloadFinishedDelegate(object sender, long bytesTransfered);
        public delegate void CancelTestDelegate(object sender, ref bool cancel);
        public delegate void MergingStartDelegate(object sender, int chunkCount);
        public delegate void MergingProgressDelegate(object sender, int chunkId);

        public DownloadStartedDelegate DownloadStarted;
        public DownloadProgressDelegate DownloadProgress;
        public DownloadFinishedDelegate DownloadFinished;
        public CancelTestDelegate CancelTest;
        public MergingStartDelegate MergingStart;
        public MergingProgressDelegate MergingProgress;

        public string Url { get; set; }
        public string tempDirectory = string.Empty;
        public string outputFileName = string.Empty;
        private long fileSize = 0;
        public long ContentLength => fileSize;
        private long downloadedBytes = 0;
        public long DownloadedBytes => downloadedBytes;
        public int UpdateInterval { get; set; } = 10;
        public int threadCount = 2;
        private bool aborted = false;

        public static string GetNumberedFileName(string fn)
        {
            if (File.Exists(fn))
            {
                int n = fn.LastIndexOf(".");
                string part1 = fn.Substring(0, n);
                string ext = fn.Substring(n, fn.Length - n);
                string newFileName;
                int i = 2;
                do
                {
                    newFileName = $"{part1}_{i++}{ext}";
                } while (File.Exists(newFileName));
                return newFileName;
            }
            else
            {
                return fn;
            }
        }

        public static bool AppendStream(Stream streamFrom, Stream streamTo)
        {
            long size = streamTo.Length;
            byte[] buf = new byte[4096];
            int bytesRead;
            do
            {
                bytesRead = streamFrom.Read(buf, 0, buf.Length);
                if (bytesRead > 0)
                {
                    streamTo.Write(buf, 0, bytesRead);
                }
            } while (bytesRead > 0);
            return streamTo.Length == size + streamFrom.Length;
        }

        public static int GetUrlFileSize(string url, out long contentLength)
        {
            return FileDownloader.GetContentLength(url, out contentLength);
        }

        private IEnumerable<Tuple<long, long>> Split(long fileSize, int chunkCount)
        {
            if (chunkCount <= 1 || fileSize <= MEGABYTE)
            {
                yield return new Tuple<long, long>(0, fileSize - 1);
                yield break;
            }
            long chunkSize = fileSize / chunkCount;
            for (int i = 0; i < chunkCount; i++)
            {
                long startPos = chunkSize * i;
                bool lastChunk = i == chunkCount - 1;
                long endPos = lastChunk ? (fileSize - 1) : (startPos + chunkSize - 1);
                yield return new Tuple<long, long>(startPos, endPos);
            }
        }

        public async Task<int> Download()
        {
            aborted = false;
            downloadedBytes = 0;
            if (string.IsNullOrEmpty(outputFileName) || string.IsNullOrWhiteSpace(outputFileName) ||
                string.IsNullOrEmpty(Url) || string.IsNullOrWhiteSpace(Url))
            {
                return 400;
            }
            if (GetUrlFileSize(Url, out long contentLength) != 200)
            {
                return 404;
            }
            if (contentLength == 0)
            {
                return ERROR_ZERO_LENGTH_CONTENT;
            }

            fileSize = contentLength;
            DownloadStarted?.Invoke(this, contentLength);

            Dictionary<int, ProgressItem> threadProgressDict = new Dictionary<int, ProgressItem>();
            Progress<ProgressItem> progress = new Progress<ProgressItem>();
            progress.ProgressChanged += (s, p) =>
            {
                threadProgressDict[p.Id] = p;

                downloadedBytes = threadProgressDict.Values.Select(it => it.Processed).Sum();

                DownloadProgress?.Invoke(this, downloadedBytes);
                CancelTest?.Invoke(this, ref aborted);
            };

            if (string.IsNullOrEmpty(tempDirectory) || string.IsNullOrWhiteSpace(tempDirectory))
            {
                tempDirectory = Path.GetDirectoryName(outputFileName);
            }

            if (threadCount <= 0)
            {
                threadCount = 2;
            }
            int chunkCount = contentLength > MEGABYTE ? threadCount : 1;
            var tasks = Split(contentLength, chunkCount).Select((range, id) => Task.Run(() =>
            {
                IProgress<ProgressItem> reporter = progress;

                FileDownloader downloader = new FileDownloader();
                downloader.progressUpdateInterval = UpdateInterval;

                string chunkFileName;
                if (chunkCount > 1)
                {
                    string path = Path.GetFileName(outputFileName);
                    chunkFileName = $"{path}.chunk_{id}.tmp";
                    if (!string.IsNullOrEmpty(tempDirectory))
                    {
                        chunkFileName = tempDirectory + chunkFileName;
                    }
                }
                else
                {
                    chunkFileName = outputFileName + ".tmp";
                }

                chunkFileName = GetNumberedFileName(chunkFileName);
                downloader.url = Url;
                downloader.rangeFrom = range.Item1;
                downloader.rangeTo = range.Item2;
                downloader.WorkProgress += (object sender, long transfered, ref bool stop) =>
                {
                    reporter.Report(new ProgressItem(chunkFileName, id, transfered, range.Item2));
                    stop = aborted;
                };
                downloader.WorkEnd += (s, transfered, errCode) =>
                {
                    reporter.Report(new ProgressItem(chunkFileName, id, transfered, range.Item2));
                };

                Stream stream = File.OpenWrite(chunkFileName);
                int errorCode = downloader.Download(stream);

                stream.Close();
                stream.Dispose();

                if (errorCode != 200 && errorCode != 206)
                {
                    if (aborted)
                    {
                        throw new OperationCanceledException();
                    }
                    throw new Exception($"Errorcode = {errorCode}");
                }
            }
            ));

            try
            {
                await Task.WhenAll(tasks);
            }
            catch (OperationCanceledException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return ERROR_DOWNLOAD_CANCELED;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return ex.HResult;
            }

            if (threadCount > 1)
            {
                MergingStart?.Invoke(this, threadProgressDict.Count);
                Progress<int> progressMerging = new Progress<int>();
                progressMerging.ProgressChanged += (s, n) =>
                {
                    MergingProgress?.Invoke(this, n);
                };

                int res = await Task.Run(() =>
                {
                    string tmpFileName = GetNumberedFileName(outputFileName + ".tmp");
                    Stream outputStream = null;
                    try
                    {
                        outputStream = File.OpenWrite(tmpFileName);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        return ERROR_BAD_FILE_NAME;
                    }
                    IProgress<int> reporter = progressMerging;

                    for (int i = 0; i < threadProgressDict.Count; i++)
                    {
                        ProgressItem item = threadProgressDict[i];
                        try
                        {
                            Stream s = File.OpenRead(item.FileName);
                            bool appended = AppendStream(s, outputStream);
                            s.Close();
                            s.Dispose();
                            if (!appended)
                            {
                                outputStream.Close();
                                outputStream.Dispose();
                                return ERROR_MERGING_CHUNKS;
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.Message);
                            outputStream.Close();
                            outputStream.Dispose();
                            return ERROR_MERGING_CHUNKS;
                        }
                        File.Delete(item.FileName);
                        reporter.Report(i);
                    }
                    outputStream.Close();
                    outputStream.Dispose();

                    outputFileName = GetNumberedFileName(outputFileName);
                    File.Move(tmpFileName, outputFileName);

                    return 200;
                });

                DownloadFinished?.Invoke(this, downloadedBytes);
                return res;
            }
            else
            {
                string t = threadProgressDict[0].FileName;
                if (File.Exists(t))
                {
                    outputFileName = GetNumberedFileName(outputFileName);
                    File.Move(t, outputFileName);
                }
                DownloadFinished?.Invoke(this, downloadedBytes);
            }

            return 200;
        }
    }
}
