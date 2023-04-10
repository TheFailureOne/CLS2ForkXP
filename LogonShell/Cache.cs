using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace LogonShell
{
    internal class Cache
    {
        public static string file;
        public static string[] args;
        public static string[] Config;
        public static Image[] Images;

        public static void Initialize(string[] argsv)
        {
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            file = AppDomain.CurrentDomain.BaseDirectory + "\\cache.bin";
            if (File.Exists(file))
                Read();
            if (!File.Exists(file) || Config[0] != Settings.ProgramVersion)
            {
                Refresh();
            }
            args = argsv;
        }
        public static void Refresh()
        {
            IniFile SettingsFile = new IniFile(Settings.Default);
            if (!File.Exists(SettingsFile.Path) || (SettingsFile.KeyExists(1) && Boolean.Parse(SettingsFile.Read(1))))
                SettingsFile.Reset();
            else if (!SettingsFile.KeyExists(0) || SettingsFile.Read(0) != Settings.ProgramVersion)
                SettingsFile.Update();
            IniFile LocaleFile = new IniFile(Settings.Locale, SettingsFile.Read(4));
            if (SettingsFile.KeyExists(2) && Boolean.Parse(SettingsFile.Read(2)) || !File.Exists(LocaleFile.Path))
                LocaleFile.Reset();
            else if (!LocaleFile.KeyExists(0) || LocaleFile.Read(0) != Settings.ProgramVersion)
                LocaleFile.Update();
            Config = new string[Settings.Default.Length + Settings.Locale.Length];
            for (int i = 0; i < Settings.Default.Length; i++)
                Config[i] = SettingsFile.Read(i);
            for (int i = 0; i < Settings.Locale.Length; i++)
                Config[i + Settings.Default.Length] = LocaleFile.Read(i);
            Images = new Image[Settings.Images.Length];
            for (int i = 0; i < Settings.Images.Length; i++)
            {
                Images[i] = Image.FromStream(new MemoryStream(File.ReadAllBytes(Environment.CurrentDirectory + "\\" + Config[Settings.Images[i]])));
                if (Settings.ImageSizes[i] != null && Images[i].Size != Settings.ImageSizes[i])
                    Images[i] = ResizeImage(Images[i], (Size)Settings.ImageSizes[i]);
            }
            File.WriteAllBytes(file,ConvertObjectToByteArray(new CacheFile(Config, Images)));
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Lsa", true);
            if (key.GetValue("LimitBlankPasswordUse") == null || Convert.ToInt32(key.GetValue("LimitBlankPasswordUse")) != 0)
                key.SetValue("LimitBlankPasswordUse", 0, RegistryValueKind.DWord);
        }

        public static void Read()
        {
            CacheFile cf = (CacheFile)ConvertByteArrayToObject(File.ReadAllBytes(file));
            Config = cf.Config;
            Images = cf.Images;

        }

        public static byte[] ConvertObjectToByteArray(object ob)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, ob);
            return ms.ToArray();
        }

        public static object ConvertByteArrayToObject(byte[] ba)
        {
            BinaryFormatter bf = new BinaryFormatter();
            Stream stream = new MemoryStream(ba);
            return bf.Deserialize(stream);
        }
        public static Bitmap ResizeImage(Image image, Size size)
        {
            var destRect = new Rectangle(0, 0, size.Width, size.Height);
            var destImage = new Bitmap(size.Width, size.Height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
    [Serializable]
    public class CacheFile
    {
        public string[] Config;
        public Image[] Images;
        public CacheFile(string[] config, Image[] images)
        {
            Config = config;
            Images = images;
        }
    }
}
