using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using GitHubUpdate.Services;

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
            int evening_hour = 23;
            int morning_hour = 1;
            GitHubUpdateClass runner = new GitHubUpdateClass();
            runner.set_evening_hour(9);
            runner.set_morning_hour(11);
            runner.set_file(file_path);
            runner.run();
        }
    }
}
