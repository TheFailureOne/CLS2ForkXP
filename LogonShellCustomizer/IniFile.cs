using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

// Change this to match your program's normal namespace
namespace LogonShellCustomizer
{
    public class IniFile   // Rev.11 Customized for LogonShell
    {
        public string Path { get; }
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;
        public List<IniKey> Default { get; }

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public IniFile(IniKey[] Default, string IniPath = null)
        {
            Path = new FileInfo(IniPath ?? EXE + ".ini").FullName;
            this.Default = new List<IniKey>(Default);
        }

        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }
        public string Read(IniKey iniKey)
        {
            return Read(iniKey.Key, iniKey.Section);
        }
        public string Read(int KeyId)
        {
            return Read(Default[KeyId]);
        }
        public void ReadKey(IniKey iniKey)
        {
            iniKey.Value = Read(iniKey.Key, iniKey.Section);
        }
        public void ReadKey(int KeyId)
        {
            Default[KeyId].Value = Read(Default[KeyId]);
        }

        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
        }
        public void Write(IniKey iniKey)
        {
            Write(iniKey.Key, iniKey.Value, iniKey.Section);
        }
        public void Write(IniKey iniKey, string Value)
        {
            Write(iniKey.Key, Value, iniKey.Section);
        }
        public void Write(int KeyId)
        {
            Write(Default[KeyId]);
        }
        public void Write(int KeyId, string Value)
        {
            Write(Default[KeyId], Value);
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }

        public void Delete(int KeyId)
        {
            Write(Default[KeyId].Key, null, Default[KeyId].Section ?? EXE);
        }

        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }
        public bool KeyExists(IniKey iniKey)
        {
            return KeyExists(iniKey.Key, iniKey.Section);
        }
        public bool KeyExists(int KeyId)
        {
            return KeyExists(Default[KeyId]);
        }

        public void convertFile()
        {
            Encoding enc = GetEncoding(Path);
            if (enc != Encoding.Unicode)
            {
                var myBytes = File.ReadAllBytes(Path);
                var utf16Bytes = Encoding.Convert(enc, Encoding.Unicode, myBytes);
                var addBom = new byte[utf16Bytes.Length + 2];
                addBom[0] = 0xff;
                addBom[1] = 0xfe;
                for (int i = 0; i < utf16Bytes.Length; i++)
                    addBom[i+2] = (byte)utf16Bytes[i];
                File.WriteAllBytes(Path, addBom);
            }
        }

        public static Encoding GetEncoding(string filename)
        {
            // Read the BOM
            var bom = new byte[4];
            using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }

            // Analyze the BOM
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
            if (bom[0] == 0xff && bom[1] == 0xfe && bom[2] == 0 && bom[3] == 0) return Encoding.UTF32; //UTF-32LE
            if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
            if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return new UTF32Encoding(true, true);  //UTF-32BE

            // Assume default encoding if we reach this point
            return Encoding.Default;
        }
    }

    public class IniKey
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Section { get; set; }

        public IniKey(string Key, string Value = null, string Section = null)
        {
            this.Key = Key;
            this.Value = Value;
            this.Section = Section;
        }
    }
}