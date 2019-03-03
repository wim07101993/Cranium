using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace Cranium.WPF.Helpers.Extensions
{
    public static class ImageExtensions
    {
        public static byte[] ToBytes(this BitmapImage image)
        {
            byte[] data;
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (var stream = new MemoryStream())
            {
                encoder.Save(stream);
                data = stream.ToArray();
            }

            return data;
        }

        public static Bitmap ToBitmap(this BitmapImage img)
        {
            using (var outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(img));
                enc.Save(outStream);
                var bitmap = new Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

        public static System.Drawing.Color GetAverageColorAsync(this BitmapImage img)
        {
            var bitmap = img.ToBitmap();

            var srcData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            var stride = srcData.Stride;
            var scan0 = srcData.Scan0;

            long[] totals = {0, 0, 0};

            var width = bitmap.Width;
            var height = bitmap.Height;

            unsafe
            {
                var p = (byte*) (void*) scan0;

                for (var y = 0; y < height; y++)
                {
                    for (var x = 0; x < width; x++)
                    {
                        for (var color = 0; color < 3; color++)
                        {
                            var idx = y * stride + x * 4 + color;
                            totals[color] += p[idx];
                        }
                    }
                }
            }

            var avgB = (int) totals[0] / (width * height);
            var avgG = (int) totals[1] / (width * height);
            var avgR = (int) totals[2] / (width * height);

            return System.Drawing.Color.FromArgb(255, avgR, avgG, avgB);
        }
    }
}