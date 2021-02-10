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
            Console.WriteLine(parentFolder);

            bool active = true;
            while (active == true)
            {
                try //error handling
                {

                    SerialPort Srp = new SerialPort(); //creates a new instance of the SerialPort Class
                    string[] ports = SerialPort.GetPortNames(); //to ensure that it reads from the right serial port the following will be used
                    Srp.PortName = ports[ports.Length - 1]; //we will use this because the arduino is usually connected to the highest numbered COM port 
                    Srp.BaudRate = 9600; //bits per second
                    Srp.Open(); //opens the COM port

                    int input = Srp.ReadChar(); //reads incoming siganl from serialport
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
