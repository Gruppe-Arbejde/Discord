using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace Discord
{
    class Program
    {
        static void Main(string[] args)
        {

            string parentFolder = AppDomain.CurrentDomain.BaseDirectory; //locates the folder path in which the program is located
            //Console.WriteLine(parentFolder);

            bool active = true;
            while (active == true)
            {
                try //error handling
                {
                //    string[] ports;
                //    ports = new string[8] { "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", };

                //    string[] portNames = SerialPort.GetPortNames();
                //    foreach (array_combine (portName, portNames) as port => ports);
                //    {
                //        SerialPort port = new SerialPort(portName, 9600); //creates a new instance of the SerialPort Class
                //        port.Open();
                //    }

                    SerialPort Srp = new SerialPort("COM4", 9600); //creates a new instance of the SerialPort Class
                    Srp.Open();

                    int input = Srp.ReadChar(); //reads incoming siganl from serial
                    if (input >= 0) //if a signal is read then it will execute the following code
                    {
                        Console.WriteLine("Distress signal detected"); //output to user that the signal has been detected
                        var proc = new Process(); //creates a new instance of the Process class
                        proc.StartInfo.FileName = $@"{parentFolder}\\Discord Arduino Comms App.exe"; //sets the path to the discord comms app
                        proc.StartInfo.Arguments = "-v -s -a"; 
                        proc.Start(); //starts the application
                        proc.WaitForExit(); //instruct the process to wait indefinitely for the asssociated process to exit
                        var exitCode = proc.ExitCode;
                        proc.Close(); //closes the prgrogram

                    }
                }
                catch (Exception) { } //error handling
            }
            Console.ReadLine();
        }
    }
}
