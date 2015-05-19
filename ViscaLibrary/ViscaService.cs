using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace ViscaLibrary
{
    public class ViscaService
    {
        //Serial 
        private SerialPort serialPort;
        public string ProlificPort { get; set; }
        //public ViscaService()
        //{


        //}
        public void OpenPort()
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
                Thread.Sleep(1000);
            }


            serialPort = new SerialPort();
            //Sets up serial port
            serialPort.PortName = GetProlificSerialPortName();
            serialPort.BaudRate = 9600;
            serialPort.Handshake = System.IO.Ports.Handshake.None;
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.ReadTimeout = 200;
            serialPort.WriteTimeout = 50;
            serialPort.Open();
        }
        public void ClosePort()
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
                serialPort = null;
            }
        }


        private string GetProlificSerialPortName()
        {
            //std::wstring REG_SW_GROUP_I_WANT = L"HARDWARE\\DEVICEMAP\\SERIALCOMM";
            // const std::wstring REG_KEY_I_WANT = L"\\Device\\ProlificSerial0";
            //  if (ERROR_SUCCESS != regKey.Open(HKEY_LOCAL_MACHINE, REG_SW_GROUP_I_WANT.c_str()))
            //regKey.QueryStringValue(REG_KEY_I_WANT.c_str(), serialValue, &serialValueLength);
            string portName = "Com5";
            const string userRoot = "HKEY_LOCAL_MACHINE";
            RegistryKey key = Registry.LocalMachine.OpenSubKey("HARDWARE\\DEVICEMAP\\SERIALCOMM");

            foreach (String valueName in key.GetValueNames())
            {
                if (valueName.Contains("ProlificSerial"))
                {
                    portName = key.GetValue(valueName).ToString();
                    ProlificPort = portName;
                }
            }
            return portName;
        }
        #region Sending

        public string GetProlificPort()
        {
            return ProlificPort;
        }


        public void Off()
        {
            byte[] off = new byte[] { 0x81, 0x01, 0x04, 0x0, 0x03, 0xFF };
            SerialCmdSend(off);
        }
        public void On()
        {
            byte[] on = new byte[] { 0x81, 0x01, 0x04, 0x0, 0x02, 0xFF };
            SerialCmdSend(on);
        }

        public void Left(byte[] command)
        {
            byte[] left = new byte[] { 0x81, 0x01, 0x06, 0x03, 0x0C, 0x00,  
                0x00, 0x00, 0x0E, 0x0C,
                0x00,0x00,0x00,0x00,
                0xFF };
            //byte[] left = new byte[] { 0x81, 0x01, 0x06, 0x01, 0x0C, 0x0C, 0x01,0x03,0xFF }; // works
            //SerialCmdSend(left);
            SerialCmdSend(command);
        }
        public void Right(byte[] command)
        {
            byte[] right = new byte[] { 0x81, 0x01, 0x06, 0x03, 0x0C, 0x00, 
                0x0F, 0x0F, 0x01, 0x04,
                0x00, 0x00, 0x00, 0x00, 
                0xFF };
            //byte[] off = new byte[] { 0x81, 0x01, 0x04, 0x0, 0x02, 0xFF };
            SerialCmdSend(command);
        }
        public void Up(byte[] command)
        {
            byte[] up = new byte[] { 0x81, 0x01, 0x06, 0x03, 0x0C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0E, 0x0C, 0xFF };
            SerialCmdSend(command);
        }
        public void Down(byte[] command)
        {
            byte[] down = new byte[] { 0x81, 0x01, 0x06, 0x03, 0x0C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0F, 0x06, 0x0C, 0x08, 0xFF };
            SerialCmdSend(command);
        }

        public void ZoomOut(byte[] command)
        {
            //byte[] up = new byte[] { 0x81, 0x01, 0x06, 0x03, 0x0C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0E, 0x0C, 0xFF };
            SerialCmdSend(command);
        }
        public void ZoomIn(byte[] command)
        {
            //byte[] up = new byte[] { 0x81, 0x01, 0x06, 0x03, 0x0C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0E, 0x0C, 0xFF };
            SerialCmdSend(command);
        }
        public byte[] SetPositionRelative(int pan, int tilt, int panSpeed, int tiltSpeed)
        {
            //#define VISCA_SPEED_TILT_MAX	0x14 // Max tilt speed
            //#define VISCA_SPEED_PAN_MAX		0x18 // Max pan speed

            int VISCA_SPEED_TILT_MAX = 0x14;
            int VISCA_SPEED_PAN_MAX = 0x18;
            double panFactor;
            double tiltFactor;
            int panSteps;
            int tiltSteps;

            var pspd = panSpeed & 0xff;
            var tspd = tiltSpeed & 0xff;
            if (pspd > VISCA_SPEED_PAN_MAX)
            {
                pspd = VISCA_SPEED_PAN_MAX;
            }

            if (tspd > VISCA_SPEED_TILT_MAX)
            {
                tspd = VISCA_SPEED_TILT_MAX;
            }
            // Make sure we have a pan/tilt camera, and it's okay to send

            int y1 = 0;
            int y2 = 0;
            int y3 = 0;
            int y4 = 0;
            int z1 = 0;
            int z2 = 0;
            int z3 = 0;
            int z4 = 0;

            /* Camera maximum position, can be changed */
            if (pan > 100)
            {
                pan = 100;
            }

            if (pan < -100)
            {
                pan = -100;
            }

            if (tilt > 25)
            {
                tilt = 25;
            }

            if (tilt < -25)
            {
                tilt = -25;
            }

            if (pan > 0) panFactor = 8.61;
            else panFactor = 8.6;

            if (tilt < 0) tiltFactor = 11.4;
            else tiltFactor = 11.44;

            /*set the step*/
            panSteps = 0xffff & (int)(0x10000 + pan * panFactor + 0.5);
            tiltSteps = 0xffff & (int)(0x10000 + tilt * tiltFactor + 0.5);

            /* Pan argument */
            y1 = 0x0f & (panSteps >> 12);
            y2 = 0x0f & (panSteps >> 8);
            y3 = 0x0f & (panSteps >> 4);
            y4 = 0x0f & panSteps;

            /* Tilt argument */
            z1 = 0x0f & (tiltSteps >> 12);
            z2 = 0x0f & (tiltSteps >> 8);
            z3 = 0x0f & (tiltSteps >> 4);
            z4 = 0x0f & tiltSteps;

            /* make package */
            //unsigned char cm = VISCA_TILT_DRIVE_REL;	// use the relative position method
            //unsigned char tm = VISCA_TERMINATOR;		// stop bit
            byte b = (byte)pspd;
            //byte[] command = new byte[]
            //{
            //    0x81, 0x01, 0x06, 0x03, 
            //    Convert.ToByte(pspd.ToString(), 16),
            //    Convert.ToByte(tspd.ToString(), 16),
            //    Convert.ToByte(y1.ToString(), 16),
            //    Convert.ToByte(y2.ToString(), 16),
            //    Convert.ToByte(y3.ToString(), 16),
            //    Convert.ToByte(y4.ToString(), 16),
            //    Convert.ToByte(z1.ToString(), 16),
            //    Convert.ToByte(z2.ToString(), 16),
            //    Convert.ToByte(z3.ToString(), 16),
            //    Convert.ToByte(z4.ToString(), 16),
            //    0xFF
            //};
            byte[] command = new byte[]
            {
                0x81, 0x01, 0x06, 0x03,
                (byte) pspd,
                (byte) tspd,
                (byte) y1,
                (byte) y2,
                (byte) y3,
                (byte) y4,
                (byte) z1,
                (byte) z2,
                (byte) z3,
                (byte) z4,
                0xFF
            };
            return command;

        }
        public void SerialCmdSend(byte[] data)
        {

            try
            {
                // Send the binary data out the port
                //byte[] hexstring1 = new byte[] { 0x81, 0x01, 0x04, 0x19, 0x01, 0xFF };

                //There is a intermitant problem that I came across
                //If I write more than one byte in succesion without a 
                //delay the PIC i'm communicating with will Crash
                //I expect this id due to PC timing issues ad they are
                //not directley connected to the COM port the solution
                //Is a ver small 1 millisecound delay between chracters

                foreach (byte hexval in data)
                {
                    byte[] _hexval = new byte[] { hexval }; // need to convert byte to byte[] to write
                    serialPort.Write(_hexval, 0, 1);
                    Thread.Sleep(1);
                }

            }
            catch (Exception ex)
            {

            }
        }



        #endregion
    }
}
