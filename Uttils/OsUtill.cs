using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CCRATestAutomation.Uttils
{
    internal class OsUtill
    {

        public static string GetOperatingSystem()
        {
            string os = RuntimeInformation.OSDescription;
            return os;
        }

        public static void KillAllProcesses(string appName)
        {
            string[] cmdString = null;

            switch (appName.ToLower())
            {
                case "chrome":
                    cmdString = new string[] { "chrome.exe", "chromedriver.exe" };
                    break;
                case "ie":
                    cmdString = new string[] { "iexplore.exe", "IEDriverServer.exe" };
                    break;
                case "microsoftedge":
                    cmdString = new string[] { "msedge.exe", "msedgedriver.exe" };
                    break;
                case "provider98":
                case "reportworx":
                case "eligible2000":
                case "smsportal":
                case "cignaportal":
                    cmdString = new string[] { "mstsc.exe" };
                    break;
                case "notepad":
                    cmdString = new string[] { "notepad.exe", "Winium.Desktop.Driver.exe" };
                    break;
                default:
                    cmdString = new string[] { appName };
                    break;
            }

            foreach (string cmd in cmdString)
            {
                string runCMD = $"taskkill /f /im {cmd} /t";
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe", $"/c {runCMD}");
                    startInfo.RedirectStandardOutput = true;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;

                    Process process = new Process();
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();
                    process.Close();

                    Console.WriteLine($"Closing {cmd}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
