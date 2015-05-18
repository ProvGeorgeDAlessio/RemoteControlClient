using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
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
        public ViscaService()
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

        public void ClosePort()
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
                serialPort = null;
            }
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

        public void Left()
        {
            byte[] left = new byte[] { 0x81, 0x01, 0x06, 0x03, 0x0C, 0x00,  0x00, 0x00, 0x0E, 0x0C,0x00,0x00,0x00,0x00,0xFF };
            //byte[] left = new byte[] { 0x81, 0x01, 0x06, 0x01, 0x0C, 0x0C, 0x01,0x03,0xFF }; // works
            SerialCmdSend(left);
        }
        public void Right()
        {
            byte[] right = new byte[] { 0x81, 0x01, 0x06, 0x03, 0x0C, 0x00, 0x0F, 0x06, 0x0C, 0x08, 0x00, 0x00, 0x00, 0x00, 0xFF };
            //byte[] off = new byte[] { 0x81, 0x01, 0x04, 0x0, 0x02, 0xFF };
            SerialCmdSend(right);
        }
        public void Up()
        {
            byte[] up = new byte[] { 0x81, 0x01, 0x06, 0x03, 0x0C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0E, 0x0C, 0xFF };
            SerialCmdSend(up);
        }
        public void Down()
        {
            byte[] down = new byte[] { 0x81, 0x01, 0x06, 0x03, 0x0C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0F, 0x06, 0x0C, 0x08, 0xFF };
            SerialCmdSend(down);
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
