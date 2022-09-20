using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GitHubUpdate.Services
{
    public class GitHubUpdateClass
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
        static void run_git_command(Process cmdProcess, string command)
        {
            StreamWriter writer = cmdProcess.StandardInput;
            writer.WriteLine(command);
        }
        public string file_path = @".\Paths.txt";
        public int evening_hour = 23;
        public int morning_hour = 1;
        private bool ran_evening = false;
        private bool ran_morning = false;
        public GitHubUpdateClass()
        {
        }
        public void set_evening_hour(int hour)
        {
            evening_hour = hour;
        }
        public void set_morning_hour(int hour)
        {
            morning_hour = hour;
        }
        public void set_file(string file)
        {
            file_path = file;
        }
        public void build()
        {
            if (!File.Exists(file_path))
            {
                File.CreateText(file_path);
            }
        }
        public void run()
        {
            DateTime now;
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = "C:\\Windows\\System32\\cmd.exe";
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardInput = true;
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
                                foreach (string p in Directory.EnumerateDirectories(path, "*", SearchOption.AllDirectories))
                                {
                                    if (Directory.Exists(Path.Combine(p, ".git")))
                                    {
                                        Process cmdProcess = new Process();
                                        processStartInfo.WorkingDirectory = p;
                                        cmdProcess.StartInfo = processStartInfo;
                                        cmdProcess.Start();
                                        run_git_command(cmdProcess, "git add .");
                                        run_git_command(cmdProcess, $"git commit -m 'evening_update'");
                                        run_git_command(cmdProcess, "git push");
                                        //run_git_command(p, "git add .");
                                        //Thread.Sleep(30 * 1000);
                                        //run_git_command(p, $"git commit -m 'evening_update'");
                                        //Thread.Sleep(5 * 1000);
                                        //run_git_command(p, "git push");
                                        //Thread.Sleep(30 * 1000);
                                        cmdProcess.Dispose();
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
                                foreach (string p in Directory.EnumerateDirectories(path, "*", SearchOption.AllDirectories))
                                {
                                    if (Directory.Exists(Path.Combine(p, ".git")))
                                    {
                                        run_git_command(p, "git pull");
                                        Thread.Sleep(3500);
                                    }
                                }
                            }
                            ran_evening = false;
                            ran_morning = true;
                        }
                    }

                    Thread.Sleep(60 * 60 * 1000); // 60 seconds * 60 minutes in an hour * 1000 milliseconds
                }
            }
        }
    }
}
