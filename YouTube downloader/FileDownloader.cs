using System;
using System.IO;
using System.Net;

namespace YouTube_downloader
{
    public sealed class FileDownloader
    {
        public string url;
        public long streamSize = 0;
        public long bytesTransfered = 0;
        public long rangeFrom;
        public long rangeTo;
        public int progressUpdateInterval = 10;
        private bool stopped = false;

        public int lastErrorCode;
        public const int DOWNLOAD_ERROR_UNKNOWN = -1;
        public const int DOWNLOAD_ERROR_ABORTED_BY_USER = -2;
        public const int DOWNLOAD_ERROR_INCOMPLETE_DATA_READ = -3;

        public delegate void WorkStartDelegate(object sender, long fileSize);
        public delegate void WorkProgressDelegate(object sender, long bytesTransfered, ref bool stop);
        public delegate void WorkEndDelegate(object sender, long bytesTransfered, int errorCode);
        public WorkStartDelegate WorkStart;
        public WorkProgressDelegate WorkProgress;
        public WorkEndDelegate WorkEnd;
        
        public FileDownloader()
        {
            rangeFrom = 0;
            rangeTo = 0;
        }

        public int Download(Stream stream)
        {          
            stopped = false;
            lastErrorCode = DOWNLOAD_ERROR_UNKNOWN;
            streamSize = stream.Length;
            bytesTransfered = 0;
            HttpWebResponse response;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                if (rangeTo > 0)
                {
                    request.AddRange(rangeFrom, rangeTo);
                }
                response = (HttpWebResponse)request.GetResponse();
            } 
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse httpWebResponse = (HttpWebResponse)e.Response;
                    lastErrorCode = (int)httpWebResponse.StatusCode;
                    return lastErrorCode;
                }
                else
                {
                    lastErrorCode = 400;
                    return 400;
                }
            }
            catch (Exception)
            {
                lastErrorCode = 400;
                return 400;
            }

            Stream responseStream = response.GetResponseStream();
            long size = response.ContentLength;
            
            WorkStart?.Invoke(this, size);
            
            byte[] buf = new byte[4096];
            int bytesRead;
            long bytesAvaliable;
            lastErrorCode = 200;
            int iter = 0;
            try
            {
                do
                {
                    bytesAvaliable = size - bytesTransfered;
                    if (bytesAvaliable > 0)
                    {
                        bytesRead = responseStream.Read(buf, 0, buf.Length);
                        if (bytesRead > 0)
                        {
                            stream.Write(buf, 0, bytesRead);
                            bytesTransfered += bytesRead;
                            if (WorkProgress != null && (progressUpdateInterval == 0 || iter++ >= progressUpdateInterval))
                            {
                                WorkProgress.Invoke(this, bytesTransfered, ref stopped);
                                iter = 0;
                            }
                        }
                    }
                }
                while (bytesAvaliable > 0 && !stopped);
            }
            catch (Exception)
            {
                lastErrorCode = DOWNLOAD_ERROR_UNKNOWN;
            }
            response.Close();
            response.Dispose();
            if (stopped)
            {
                lastErrorCode = DOWNLOAD_ERROR_ABORTED_BY_USER;
            }
            else if (lastErrorCode != DOWNLOAD_ERROR_UNKNOWN)
            {
                if (size != 0 && bytesTransfered != size)
                {
                    lastErrorCode = DOWNLOAD_ERROR_INCOMPLETE_DATA_READ;
                }
            }
           
            WorkEnd?.Invoke(this, bytesTransfered, lastErrorCode);
            return lastErrorCode;
        }

        public static int GetContentLength(string url, out long contentLength)
        {
            HttpWebResponse response = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                response = (HttpWebResponse)request.GetResponse();
                contentLength = response.ContentLength;
                response.Close();
                response.Dispose();
                return 200;
            }
            catch (WebException e)
            {
                contentLength = 0;
                if (response != null)
                {
                    response.Close();
                    response.Dispose();
                }
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse httpWebResponse = (HttpWebResponse)e.Response;
                    url = e.Message;
                    return (int)httpWebResponse.StatusCode;
                }
                else
                {
                    return 400;
                }
            }
            catch (Exception)
            {
                if (response != null)
                {
                    response.Close();
                    response.Dispose();
                }
                contentLength = 0;
                return 400;
            }
        }

        public long GetBytesTransfered()
        {
            return bytesTransfered;
        }

        public long GetStreamSize()
        {
            return streamSize;
        }
    }
}
