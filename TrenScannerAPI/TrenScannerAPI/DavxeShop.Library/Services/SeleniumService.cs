using DavxeShop.Library.Services.Interfaces;
using DavxeShop.Models;
using System.Diagnostics;

namespace DavxeShop.Library.Services
{
    public class SeleniumService : ISeleniumService
    {
        public string GenerateSeleniumScript(TrenData trenData)
        {
            try
            {
                string pythonScriptPath = @"C:\Users\Tonaxe\Desktop\TrenScanner\c#seleniumscript.py";
                //string pythonScriptPath = @"C:\Users\yassi\Desktop\DAW\TrenScannerS\c#seleniumscript.py";

                string pythonExePath = @"C:\Users\Tonaxe\AppData\Local\Microsoft\WindowsApps\python.exe";
                //string pythonExePath = @"C:\Users\yassi\AppData\Local\Microsoft\WindowsApps\python.exe";

                string arguments = $"\"{pythonScriptPath}\" " +
                                   $"\"{trenData.Origin}\" " +
                                   $"\"{trenData.Destination}\" " +
                                   $"{DateTime.Parse(trenData.DepartureDate).Day} " +
                                   $"{DateTime.Parse(trenData.ReturnDate).Day} " +
                                   $"{trenData.Adults} " +
                                   $"{trenData.Children} " +
                                   $"{trenData.Infants}";


                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = pythonExePath,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(startInfo))
                {
                    if (process != null)
                    {
                        string output = process.StandardOutput.ReadToEnd();
                        process.WaitForExit();

                        return output;
                    }
                    else
                    {
                        return "Error al ejecutar el script Python.";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}

