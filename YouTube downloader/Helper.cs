using System.IO;

namespace YouTube_downloader
{
    public static class Helper
    {

        public static void SaveToFile(this Stream stream, string fileName)
        {
            Stream fileStream = File.OpenWrite(fileName);
            stream.Seek(0, SeekOrigin.Begin);
            MultiThreadedDownloader.AppendStream(stream, fileStream);
            fileStream.Close();
            fileStream.Dispose();
        }

    }
}
