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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
/* =========================================================================================== */



namespace SHELLBUILDER
{
    class Builder
    {

/* ===================================== GLOBAL OBJECTS ====================================== */
        private static string GET_SHELLCODE = null;
        private static string GET_FILE_PATH = null;
        private static string GET_SHELL_LOG = null;
        private static FileInfo GET_FILE_INFO = null;
        private static StreamWriter GET_WRITE = null;
/* =========================================================================================== */




/* ===================================== EXIT FUNCTION ======================================= */
        private static void __EXIT()
        {
            Thread.Sleep(1500);
            Console.Clear();
            Environment.Exit(0);
        }
/* =========================================================================================== */




/* ======================================= SIMPLE UI ========================================= */
        private static void __INIT()
        {
            try
            {
                Console.WriteLine("--------------------------------------------------------------");
                Console.WriteLine(">>>>>>>>>>>>>>>>>>>>> SHELLCODE BUILDER <<<<<<<<<<<<<<<<<<<<<<");
                Console.WriteLine("--------------------------------------------------------------");
                Console.Write("[INPUT] Enter the file path: ");
                GET_FILE_PATH = Convert.ToString(Console.ReadLine());
                Console.WriteLine("[STATUS] Process is started...");
                Thread.Sleep(1000);

                if (GET_FILE_PATH == null)
                {
                    Console.WriteLine("[ERROR] Please specify path to the file!");
                    __EXIT();
                }
                else
                {
                    Console.WriteLine("[STATUS] Checking file...");
                    Thread.Sleep(1000);

                    if (File.Exists(GET_FILE_PATH))
                    {
                        GET_SHELL_LOG = "SHELLCODE.DARXIS";
                        GET_FILE_INFO = new FileInfo(GET_FILE_PATH);
                        GET_SHELLCODE = Convert.ToBase64String(File.ReadAllBytes(GET_FILE_PATH));
                        GET_WRITE = new StreamWriter(new FileStream(GET_SHELL_LOG, FileMode.Create, FileAccess.Write));

                        GET_WRITE.Write(GET_SHELLCODE);
                        GET_WRITE.Close();

                        Console.WriteLine("[STATUS] Creating log-file...");
                        Thread.Sleep(1000);
                        
                        if (File.Exists(GET_SHELL_LOG))
                        {
                            Console.WriteLine("[STATUS] Converting...");
                            Thread.Sleep(1000);
                            Console.WriteLine("--------------------------------------------------------------");
                            Console.WriteLine("[STATUS] File name: " + GET_FILE_INFO.Name.ToString());
                            Console.WriteLine("[STATUS] File path: " + GET_FILE_INFO.DirectoryName.ToString());
                            Console.WriteLine("[STATUS] File extension: " + GET_FILE_INFO.Extension.ToString());
                            Console.WriteLine("[STATUS] File attributes: " + GET_FILE_INFO.Attributes.ToString());
                            Console.WriteLine("[STATUS] File creation time: " + GET_FILE_INFO.CreationTime.ToString());
                            Console.WriteLine("[STATUS] File size: " + GET_FILE_INFO.Length.ToString());
                            Console.WriteLine("--------------------------------------------------------------");
                            Thread.Sleep(2000);
                            Console.WriteLine("[RESULT] Successfully! Your shellcode:");
                            Console.WriteLine(" ");
                            Console.WriteLine(GET_SHELLCODE);
                            Console.WriteLine(" ");
                            Console.WriteLine("[RESULT] Shellcode has been saved at: .\\" + GET_SHELL_LOG);
                            Thread.Sleep(50000);
                            Console.WriteLine("[STATUS] Closing...");
                            __EXIT();
                        }
                        else
                        {
                            Console.WriteLine("[ERROR] Log-file is missing!");
                            __EXIT();
                        }
                    }
                    else
                    {
                        Console.WriteLine("[ERROR] File is missing!");
                        __EXIT();
                    }
                }
            }
            catch
            {
                Console.WriteLine("[ERROR] Exception error!");
                __EXIT();
            }
        }
/* =========================================================================================== */





/* ===================================== EXECUTION ============================================ */
        static void Main(string[] args)
        {
            try
            {
                __INIT();
            }
            catch
            {
                Console.WriteLine("[ERROR] Global exception error!");
                __EXIT();
            }
        }
/* ============================================================================================ */
    }
}
