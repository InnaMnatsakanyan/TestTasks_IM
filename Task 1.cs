using System;
using System.Diagnostics;

namespace GetProcess
{
    class Program
    {
        static void Main(String[] args)
        {
            // receive input
            Console.WriteLine("Enter splitted by space:\r\nThe process name\r\nMaximum lifetime(in minutes)\r\nMonitoring frequency(in minutes)");
            string input = Console.ReadLine();
            string[] split = input.Split(' ');
            string ProcessName = split[0];
            int MaxLifetime = Convert.ToInt32(split[1]);
            int Frequency = Convert.ToInt32(split[2]);

            // set timer
            var timer = new Timer(MonitorProcesses, null, 0, Frequency * 60000);
            Console.ReadKey();

            void MonitorProcesses(object o)
            {
                // store the processes in list
                Process[] list = Process.GetProcessesByName(ProcessName);

                // output the processes and their duration times
                foreach (var process in list)
                {
                    Console.WriteLine(process.ProcessName + " " + process.TotalProcessorTime.TotalMinutes);
                }
                Console.ReadLine();

                // check lifetime, and if exceeds, kill
                for (int i = 0; i < list.Length; i++)
                {
                    if (list[i].TotalProcessorTime.TotalMinutes > MaxLifetime)
                        list[i].Kill();
                }
            }
        }
    }
}