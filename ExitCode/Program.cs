using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ExitCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            if (args?.Length != 1)
            {
                Console.WriteLine($"Drag an executable on {typeof(Program).Assembly.GetName().Name}.exe to deterine its exit code.");
                Console.ReadKey();
                return;
            }

            string file = args[0];
            if (!File.Exists(file))
            {
                Console.WriteLine("The provided file does not exist: " + file);
                Console.ReadKey();
                return;
            }

            var pi = new ProcessStartInfo(file);
            pi.RedirectStandardError = true;
            pi.RedirectStandardOutput = true;
            pi.UseShellExecute = false;

            try
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Starting process: " + Path.GetFileName(file));

                using (var proc = Process.Start(pi))
                {
                    proc.ErrorDataReceived += Proc_ErrorDataReceived;
                    proc.OutputDataReceived += Proc_OutputDataReceived;

                    proc.BeginErrorReadLine();
                    proc.BeginOutputReadLine();


                    proc.WaitForExit();

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Exit code was: " + proc.ExitCode);
                }
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An error occured:\n\n" + ex.Message);
            }


            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Press any key to exit ...");
            Console.ReadKey();
        }

        private static void Proc_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(">> " + e.Data);
        }

        private static void Proc_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(">> " + e.Data);
        }
    }
}
