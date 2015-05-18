/* ------------------------------------------------------------------------ --
--                                                                          --
--                        PC serial port connection object                  --
--                           for  event-driven programs                     --
--                                                                          --
--                                                                          --
--                                                                          --
--  Copyright @ 2001-2002     Thierry Schneider                             --
--                            thierry@tetraedre.com                         --
--                                                                          --
--                                                                          --
--                                                                          --
-- ------------------------------------------------------------------------ --
--                                                                          --
--  Filename : Tserial_event.cpp                                            --
--  Author   : Thierry Schneider                                            --
--  Created  : April 4th 2000                                               --
--  Modified : June 22nd 2002                                               --
--  Plateform: Windows 95, 98, NT, 2000, XP (Win32)                         --
-- ------------------------------------------------------------------------ --
--                                                                          --
--  This software is given without any warranty. It can be distributed      --
--  free of charge as long as this header remains, unchanged.               --
--                                                                          --
-- ------------------------------------------------------------------------ --
--                                                                          --
-- 01.04.24      Comments added                                             --
-- 01.04.28      Bug 010427 corrected. OnDisconnectedManager was not        --
--                initialized                                               --
-- 01.04.28      connect() function prototype modified to handle 7-bit      --
--                communication                                             --
-- 01.04.29      "ready" field added to remove a bug that occured during    --
--                 reconnect (event manager pointers cleared)               --
--                 I removed the "delete" in Tserial_event_thread_start     --
--                 because it was destroying the object even if we would    --
--                 use it again                                             --
--                                                                          --
-- 02.01.30      Version 2.0 of the serial event object                     --
--                                                                          --
--                                                                          --
-- 02.06.22      - wait for the thread termination before                   --
--                 quiting or restarting                                    --
--               - "owner" field added to be able to call C++ object from   --
--                  the event manager routine                               --
--               - Correction of a bug that occured when receiving data     --
--                 (setting twice the SIG_READ_DONE event)                  --
--                                                                          --
--                                                                          --
--                                                                          --
-- ------------------------------------------------------------------------ --
--                                                                          --
--    Note to Visual C++ users:  Don't forget to compile with the           --
--     "Multithreaded" option in your project settings                      --
--                                                                          --
--         See   Project settings                                           --
--                   |                                                      --
--                   *--- C/C++                                             --
--                          |                                               --
--                          *--- Code generation                            --
--                                       |                                  --
--                                       *---- Use run-time library         --
--                                                     |                    --
--                                                     *---- Multithreaded  --
--                                                                          --
--                                                                          --
--                                                                          --
-- ------------------------------------------------------------------------ */





//public delegate void type_myCallBack(uint @object, uint Event);

//#if ! __BORLANDC__
////C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
////ORIGINAL LINE: #define bool BOOL
//#define bool
////C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
////ORIGINAL LINE: #define true TRUE
//#define true
////C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
////ORIGINAL LINE: #define false FALSE
//#define false
//#endif


/* -------------------------------------------------------------------- */
/* -----------------------------  Tserial  ---------------------------- */
/* -------------------------------------------------------------------- */
public class Tserial_event
{
	// -------------------------------------------------------- //
	protected int ready;
	protected int check_modem;
	protected string port = new string(new char[10]); // port name "com1",...
	protected int rate; // baudrate
	protected int parityMode;

	protected System.IntPtr[] serial_events = new System.IntPtr[DefineConstants.SERIAL_SIGNAL_NBR]; // events to wait on
	protected uint threadid; // ...
	protected System.IntPtr serial_handle; // ...
	protected System.IntPtr thread_handle; // ...
    //protected OVERLAPPED ovReader = new OVERLAPPED(); // Overlapped structure for ReadFile
    //protected OVERLAPPED ovWriter = new OVERLAPPED(); // Overlapped structure for WriteFile
    //protected OVERLAPPED ovWaitEvent = new OVERLAPPED(); // Overlapped structure for WaitCommEvent
	protected sbyte tx_in_progress; // BOOL indicating if a WriteFile is
													 // in progress
	protected sbyte rx_in_progress; // BOOL indicating if a ReadFile is
													 // in progress
	protected sbyte WaitCommEventInProgress;
	protected string rxBuffer = new string(new char[DefineConstants.SERIAL_MAX_RX]);
	protected int max_rx_size;
	protected int received_size;
	protected string txBuffer = new string(new char[DefineConstants.SERIAL_MAX_TX]);
	protected int tx_size;
	protected uint dwCommEvent; // to store the result of the wait

	// ............................................................
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void OnCharArrival(sbyte c);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void OnEvent(uint events);

	// ++++++++++++++++++++++++++++++++++++++++++++++
	// .................. EXTERNAL VIEW .............
	// ++++++++++++++++++++++++++++++++++++++++++++++

	public int instance; // used to tell which cam!
	public object owner; // do what you want with this
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void run();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//				  Tserial_event();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//				 public void Dispose();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	int connect(ref string port, int rate, int parity, sbyte ByteSize, int modem_events);

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void setManager(type_myCallBack manager);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void setRxSize(int size);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void sendData(ref string buffer, int size);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	int getNbrOfBytes();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	int getDataInSize();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	string getDataInBuffer();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void dataHasBeenRead();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void disconnect();
	public type_myCallBack manager;
}
/* -------------------------------------------------------------------- */





internal static partial class DefineConstants
{
	public const int SERIAL_PARITY_NONE = 0;
	public const int SERIAL_PARITY_ODD = 1;
	public const int SERIAL_PARITY_EVEN = 2;
	public const int SERIAL_CONNECTED = 0;
	public const int SERIAL_DISCONNECTED = 1;
	public const int SERIAL_DATA_SENT = 2;
	public const int SERIAL_DATA_ARRIVAL = 3;
	public const int SERIAL_RING = 4;
	public const int SERIAL_CD_ON = 5;
	public const int SERIAL_CD_OFF = 6;
	public const int SERIAL_SIGNAL_NBR = 7;
	public const int SERIAL_MAX_RX = 256;
	public const int SERIAL_MAX_TX = 256;
	public const int timer_interval = 18;
	public const int VISCA_MAX_NAME_LEN = 30;
	public const int VISCA_DEV_TYPE_CAMERA = 1;
	public const int VISCA_DEV_TYPE_VCR = 2;
	public const int VISCA_TILT_BASE = 3;
	public const int VISCA_LIB_MAXCAMS = 4;
	public const int VISCA_COMMAND_HEADER = 0x01;
	public const int VISCA_INQUIRY_CHEADER = 0x09;
	public const int VISCA_DATA_HEADER = 0x04;
	public const int VISCA_PAN_DATA_HEADER = 0x06;
	public const int VISCA_SEND_HEADER = 0x80;
	public const int VISCA_BROADCAST_HEADER = 0x88;
	public const int VISCA_CAM_ADDR = 0x01;
	public const int VISCA_NULL = 0x00;
	public const int VISCA_ERROR_HEADER = 0x06;
	public const int VISCA_INQUIRY_HEADER = 0x05;
	public const int VISCA_ACK = 0x04;
	public const int VISCA_COMPLETE = 0x05;
	public const int VISCA_INQ_COMPLETE = 0x50;
	public const int VISCA_LENGTH_ERROR = 0x01;
	public const int VISCA_SYNTAX_ERROR = 0x02;
	public const int VISCA_BUFFER_FULL = 0x03;
	public const int VISCA_CANCELLED = 0x04;
	public const int VISCA_NO_SOCKET_ERROR = 0x05;
	public const int VISCA_NOT_EXECUTABLE = 0x41;
	public const int VISCA_ON = 0x02;
	public const int VISCA_OFF = 0x03;
	public const int VISCA_UP = 0x02;
	public const int VISCA_DOWN = 0x03;
	public const int VISCA_RESET = 0x00;
	public const int VISCA_STOP = 0x00;
	public const int VISCA_EMPTY = 0x00;
	public const int VISCA_TOGGLE = 0x10;
	public const int VISCA_MANUAL = 0x03;
	public const int VISCA_LENS_INIT = 0x01;
	public const int VISCA_COMP_SCAN = 0x02;
	public const int VISCA_ENABLE = 0x01;
	public const int VIACA_DISABLE = 0x00;
	public const int VISCA_CAMERA_POWER = 0x00;
	public const int VISCA_RGAIN = 0x03;
	public const int VISCA_BGAIN = 0x04;
	public const int VISCA_SHUTTER = 0x0A;
	public const int VISCA_IRIS = 0x0B;
	public const int VISCA_GAIN = 0x0C;
	public const int VISCA_BRIGHTNESS = 0x0D;
	public const int VISCA_BACKLIGHT = 0x33;
	public const int VISCA_SPOTAE = 0x59;
	public const int VISCA_REVERSE = 0x61;
	public const int VISCA_FREEZE = 0x62;
	public const int VISCA_DISPLAY = 0x15;
	public const int VISCA_DATE_DISPLAY = 0x71;
	public const int VISCA_TIME_DISPLAY = 0x72;
	public const int VISCA_CAM_MUTE = 0x75;
	public const int VISCA_STABILIZER = 0x34;
	public const int VISCA_AUTOIR = 0x51;
	public const int VISCA_IR_MODE = 0x01;
	public const int VISCA_TILT_MAXSPEED_INQ = 0x11;
	public const int VISCA_TILT_MODE_INQ = 0x10;
	public const int VISCA_TILT_POS_INQ = 0x12;
	public const int VISCA_TILT_DRIVE = 0x01;
	public const int VISCA_TILT_DRIVE_ABS = 0x02;
	public const int VISCA_TILT_DRIVE_REL = 0x03;
	public const int VISCA_TILT_DRIVE_HOME = 0x04;
	public const int VISCA_TILT_DRIVE_RESET = 0x05;
	public const int TEST_VISCA_01 = 0x01;
	public const int TEST_VISCA_02 = 0x02;
	public const int TEST_VISCA_03 = 0x03;
	public const int TEST_VISCA_04 = 0x04;
	public const int TEST_VISCA_06 = 0x06;
	public const int TEST_VISCA_PAN_SLOW = 0x08;
	public const int TEST_VISCA_TILT_SLOW = 0x08;
	public const int VISCA_SPEED_TILT_MAX = 0x14;
	public const int VISCA_SPEED_PAN_MAX = 0x18;
	public const int VISCA_STD_1080I_60 = 0x01;
	public const int VISCA_STD_720P_60 = 0x02;
	public const int VISCA_STD_NTSC_CROP = 0x03;
	public const int VISCA_STD_NTSC_SQ = 0x04;
	public const int VISCA_STD_1080I_50 = 0x11;
	public const int VISCA_STD_720P_50 = 0x12;
	public const int VISCA_STD_PAL_CROP = 0x13;
	public const int VISCA_STD_PAL_SQ = 0x14;
	public const int VISCA_PAL = 0x01;
	public const int VISCA_NTSC = 0x00;
	public const int VISCA_AFSENSITIVITY = 0x58;
	public const int VISCA_INITIALIZE = 0x19;
	public const int VISCA_CTRL_LOCK = 0x17;
	public const int VISCA_PIC_EFFECT = 0x63;
	public const int VISCA_AF_MODE = 0x57;
	public const int VISCA_REGISTER = 0x24;
	public const int VISCA_REG_VID_SYS = 0x70;
	public const int VISCA_EXP_COMP_EN = 0x3E;
	public const int VISCA_EXP_COMP_RST = 0x0E;
	public const int VISCA_EXP_COMP_DCT = 0x4E;
	public const int VISCA_AE = 0x39;
	public const int VISCA_SLOW_SHUTTER = 0x5A;
	public const int VISCA_AE_AUTO = 0x00;
	public const int VISCA_AE_SHUTTER = 0x0A;
	public const int VISCA_AE_IRIS = 0x0B;
	public const int VISCA_AE_BRIGHT = 0x0D;
	public const int VISCA_AE_MANUAL = 0x03;
	public const int VISCA_SLOW_ON = 0x02;
	public const int VISCA_SLOW_OFF = 0x03;
	public const int VISCA_SHUTTER_DIRECT = 0x4A;
	public const int VISCA_IRIS_DIRECT = 0x4B;
	public const int VISCA_GAIN_DIRECT = 0x4C;
	public const int VISCA_BRIGHT_DIRECT = 0x4D;
	public const int VISCA_WB = 0x35;
	public const int VISCA_WB_AUTO = 0x00;
	public const int VISCA_WB_INDOOR = 0x01;
	public const int VISCA_WB_OUTDOOR = 0x02;
	public const int VISCA_WB_ONEPUSH = 0x03;
	public const int VISCA_WB_ATW = 0x04;
	public const int VISCA_WB_MANUAL = 0x05;
	public const int VISCA_WB_ONEPUSH_T1 = 0x10;
	public const int VISCA_WB_ONEPUSH_T2 = 0x05;
	public const int VISCA_ZOOM = 0x07;
	public const int VISCA_TELE_STANDARD = 0x02;
	public const int VISCA_WIDE_STANDARD = 0x03;
	public const int VISCA_TELE_VARIABLE = 0x20;
	public const int VISCA_WIDE_VARIABLE = 0x30;
	public const int VISCA_ZOOM_DIRECT = 0x47;
	public const int VISCA_APERTURE = 0x02;
	public const int VISCA_APERTURE_DCT = 0x42;
	public const int VISCA_TITLE_SET = 0x73;
	public const int VISCA_TITLE = 0x74;
	public const int VISCA_TITLE_ONE = 0x00;
	public const int VISCA_TITLE_TWO = 0x01;
	public const int VISCA_TITLE_THREE = 0x02;
	public const int VISCA_TITLE_CLEAR = 0x00;
	public const int VISCA_NOBLINK = 0x00;
	public const int VISCA_BLINK = 0x01;
	public const int VISCA_SPACE = 0x1B;
	public const int VISCA_WHITE = 0x00;
	public const int VISCA_YELLOW = 0x01;
	public const int VISCA_VIOLET = 0x02;
	public const int VISCA_RED = 0x03;
	public const int VISCA_CYAN = 0x04;
	public const int VISCA_GREEN = 0x05;
	public const int VISCA_BLUE = 0x06;
	public const int VISCA_SET_DATETIME = 0x70;
	public const int VISCA_INQ_BLK = 0x7e;
	public const int VISCA_INQ_LCS = 0x00;
	public const int VISCA_INQ_CCS = 0x01;
	public const int VISCA_INQ_GEN = 0x02;
	public const int VISCA_INQ_EFQ = 0x03;
	public const int VISCA_INQ_LONG = 0x01;
	public const int VISCA_INQ_INT = 0x00;
	public const int VISCA_INQ_SHORT = 0x04;
	public const int VISCA_INQ_IF = 0x00;
	public const int VISCA_INQ_TILT = 0x06;
	public const int VISCA_INQ_VERSION = 0x02;
	public const int VISCA_UNKNOWN = 0x0000;
	public const int VISCA_MFG_SONY = 0x0020;
	public const int VISCA_FCB_EX980 = 0x0430;
	public const int VISCA_FCB_EX980P = 0x0431;
	public const int VISCA_FCB_EX980S = 0x0432;
	public const int VISCA_FCB_EX980SP = 0x0433;
	public const int VISCA_FCB_H10 = 0x044a;
	public const int VISCA_FCB_H11 = 0x044b;
	public const int VISCA_EVI_D100P = 0x0402;
	public const int VISCA_EVI_D30 = 0x040d;
	public const string VISCA_NAME_UNKNOWN = "Unknown";
	public const string VISCA_NAME_MFG_SONY = "Sony";
	public const string VISCA_NAME_FCB_EX980 = "FCB-EX980";
	public const string VISCA_NAME_FCB_EX980P = "FCB-EX980P";
	public const string VISCA_NAME_FCB_EX980S = "FCB-EX980S";
	public const string VISCA_NAME_FCB_EX980SP = "FCB-EX980SP";
	public const string VISCA_NAME_FCB_H10 = "FCB-H10";
	public const string VISCA_NAME_FCB_H11 = "FCB-H11";
	public const string VISCA_NAME_EVI_D100P = "EVI-D100/P";
	public const string VISCA_NAME_EVI_D30 = "EVI-D30";
	public const int VISCA_TERMINATOR = 0xff;
}