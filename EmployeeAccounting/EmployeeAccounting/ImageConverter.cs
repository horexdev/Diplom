using System.IO;
using System.Windows.Media.Imaging;

namespace EmployeeAccounting;

public static class ImageConverter
{
    public static byte[]? ConvertImageToBinary(string path)
    {
        if (string.IsNullOrEmpty(path))
            return null;

        using var st = new FileStream(path, FileMode.Open);

        var buffer = new byte[st.Length];
        st.Read(buffer, 0, (int)st.Length);

        return buffer;
    }

    public static BitmapImage ConvertBinaryToImage(byte[] buffer)
    {
        using var memoryStream = new MemoryStream(buffer);

        var bitmapImage = new BitmapImage();
        bitmapImage.BeginInit();
        bitmapImage.StreamSource = memoryStream;
        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        bitmapImage.EndInit();
        bitmapImage.Freeze();

        return bitmapImage;
    }
}