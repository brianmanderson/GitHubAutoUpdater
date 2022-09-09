using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GitHubUpdate
{
    public class Program
    {
        static void run_git_add(string path)
        {
            Process cmdProcess = new Process();
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = "C:\\Windows\\System32\\cmd.exe";
            processStartInfo.WorkingDirectory = path;
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardInput = true;

            cmdProcess.StartInfo = processStartInfo;
            cmdProcess.Start();
            StreamWriter writer = cmdProcess.StandardInput;
            writer.WriteLine("git add .");
            writer.WriteLine("git commit -m 'update'");
            writer.WriteLine("git push");

            cmdProcess.WaitForExit();
            Process p = new Process{
                StartInfo = 
                {
                    FileName =  "C:\\Windows\\System32\\cmd.exe",
                    WorkingDirectory = path,
                    Arguments = "git add .",
                    UseShellExecute = true
                }
            };
            p.Start();
            p.StartInfo.WorkingDirectory = path;
            p.StartInfo.FileName = "C:\\Windows\\System32\\cmd.exe";
            p.StartInfo.Arguments = "git add .";
            p.StartInfo.UseShellExecute = false;
            p.Start();
            p.StartInfo.Arguments = "git commit -m 'Evening auto-push'";
            p.Start();
            p.StartInfo.Arguments = "git push";
            p.Start();
        }
        static void Main(string[] args)
        {
            string file_path = @".\Paths.txt";
            int hour = 16;
            bool ran_evening = false;
            bool ran_morning = false;
            bool run_evening = false;
            bool run_morning = false;
            DateTime now = DateTime.Now;
            if (now.Hour < hour)
            {
                run_evening = true;
            }
            if (File.Exists(file_path))
            {
                List<string> paths = File.ReadAllLines(file_path).ToList();
                while (true)
                {
                    now = DateTime.Now;
                    if (now.Hour >= hour)
                    {
                        if (!ran_evening)
                        {
                            foreach (string path in paths)
                            {
                                if (!Directory.Exists(path))
                                {
                                    continue;
                                }
                                foreach (string p in Directory.EnumerateDirectories(path))
                                {
                                    run_git_add(@"C:\\Users\\b5anderson\\Modular_Projects\\GitHubUpdater\\");
                                }
                            }
                            ran_evening = true;
                        }
                    }

                    Thread.Sleep(10000);

                    if (now.Hour > 20)
                        if (!ran_evening)
                        {

                        }
                }
            }
        }
    }
}
