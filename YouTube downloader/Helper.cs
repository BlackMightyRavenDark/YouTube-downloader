using System.Drawing;
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

        public static Rectangle ResizeTo(this Rectangle source, Size newSize)
        {
            float aspectSource = source.Height / (float)source.Width;
            float aspectDest = newSize.Height / (float)newSize.Width;
            int w = newSize.Width;
            int h = newSize.Height;
            if (aspectSource > aspectDest)
            {
                w = (int)(newSize.Height / aspectSource);
            }
            else if (aspectSource < aspectDest)
            {
                h = (int)(newSize.Width * aspectSource);
            }
            return new Rectangle(0, 0, w, h);
        }

        public static Rectangle CenterIn(this Rectangle source, Rectangle dest)
        {
            int x = dest.Width / 2 - source.Width / 2;
            int y = dest.Height / 2 - source.Height / 2;
            return new Rectangle(x, y, source.Width, source.Height);
        }
    }
}
