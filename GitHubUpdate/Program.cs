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
        static void run_git_command(string path, string command)
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
            writer.WriteLine(command);
            cmdProcess.Dispose();
        }
        static void Main(string[] args)
        {
            string file_path = @".\Paths.txt";
            int evening_hour = 16;
            int morning_hour = 18;
            bool ran_evening = false;
            bool ran_morning = false;
            bool run_evening = false;
            bool run_morning = false;
            DateTime now = DateTime.Now;
            if (now.Hour < evening_hour)
            {
                run_evening = true;
            }
            if (File.Exists(file_path))
            {
                List<string> paths = File.ReadAllLines(file_path).ToList();
                while (true)
                {
                    now = DateTime.Now;
                    if (now.Hour >= evening_hour)
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
                                    if (Directory.Exists(Path.Combine(p, ".git")))
                                    {
                                        run_git_command(@"C:\\Users\\b5anderson\\Modular_Projects\\GitHubUpdater\\", "git add .");
                                        run_git_command(@"C:\\Users\\b5anderson\\Modular_Projects\\GitHubUpdater\\", $"git commit -m 'evening_update'");
                                        run_git_command(@"C:\\Users\\b5anderson\\Modular_Projects\\GitHubUpdater\\", "git push");
                                        break;
                                    }
                                }
                            }
                            ran_evening = true;
                            ran_morning = false;
                        }
                    }
                    if (now.Hour <= morning_hour)
                    {
                        if (!ran_morning)
                        {
                            foreach (string path in paths)
                            {
                                if (!Directory.Exists(path))
                                {
                                    continue;
                                }
                                foreach (string p in Directory.EnumerateDirectories(path))
                                {
                                    if (Directory.Exists(Path.Combine(p, ".git")))
                                    {
                                        run_git_command(@"C:\\Users\\b5anderson\\Modular_Projects\\GitHubUpdater\\", "git pull");
                                        break;
                                    }
                                }
                            }
                            ran_evening = false;
                            ran_morning = true;
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
