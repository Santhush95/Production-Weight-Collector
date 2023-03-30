using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.IO.Ports;
using System.Data.SqlClient;
using System.Configuration;


namespace pp_plant_scale_reader
{
    public partial class Service1 : ServiceBase
    {
        Thread th;
        bool isRunning = false;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            th = new Thread(DoThis);
            th.Start();
            isRunning = true;
        }

        private void DoThis()
        {
            while (isRunning)
            {
                SerialPort MyCOMPort = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
                MyCOMPort.Open();
                string DataReceived = MyCOMPort.ReadLine();
             
               //File.AppendAllText(@"C:\Users\Lasith.Chandimal\Desktop\teststream.txt", DataReceived + Environment.NewLine);
               StreamWriter sw = new StreamWriter(@"C:\Users\Lasith.Chandimal\Desktop\teststream.txt", true);
               sw.WriteLine(DataReceived);
               sw.Flush();
               sw.Close();





               MyCOMPort.Close();

                

                
            }
        }



        protected override void OnStop()
        {
            isRunning = false;
            th = null;
        }
    }
}
