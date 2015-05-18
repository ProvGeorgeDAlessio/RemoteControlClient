using System.Collections.Generic;

// visca_camera.h

/*
 * VISCA(tm) Camera Control
 * Copyright (C) 2005 - 2008 MGC Works Inc.
 *
 * Written by John Mazza <maz@mgcworks.com>
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 ****************************************************************************

 This file contains definitions of VISCA camera class utilizing multi-threaded
 serial communications and outbound packet queue processing.  Inbound packets 
 are processed asychronously when terminators are recieved.
  
 Serial library (TSERIAL_EVENT) is fully freeware, so no LGPL incompatability
 results from its inclusion in this project.

 All constants are defined visca_consts.h file.

*/

// Header to define camera
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
//#pragma unmanaged


// Queue typedefs

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

public delegate void type_myCallBack(uint @object, uint Event);

/* -------------------------------------------------------------------- */
/* -----------------------------  Tserial  ---------------------------- */
/* -------------------------------------------------------------------- */
//public class Tserial_event
//{
//    // -------------------------------------------------------- //
//    protected int ready;
//    protected int check_modem;
//    protected string port = new string(new char[10]); // port name "com1",...
//    protected int rate; // baudrate
//    protected int parityMode;

//    protected System.IntPtr[] serial_events = new System.IntPtr[DefineConstants.SERIAL_SIGNAL_NBR]; // events to wait on
//    protected uint threadid; // ...
//    protected System.IntPtr serial_handle; // ...
//    protected System.IntPtr thread_handle; // ...
//    protected OVERLAPPED ovReader = new OVERLAPPED(); // Overlapped structure for ReadFile
//    protected OVERLAPPED ovWriter = new OVERLAPPED(); // Overlapped structure for WriteFile
//    protected OVERLAPPED ovWaitEvent = new OVERLAPPED(); // Overlapped structure for WaitCommEvent
//    protected sbyte tx_in_progress; // BOOL indicating if a WriteFile is
//                                                     // in progress
//    protected sbyte rx_in_progress; // BOOL indicating if a ReadFile is
//                                                     // in progress
//    protected sbyte WaitCommEventInProgress;
//    protected string rxBuffer = new string(new char[DefineConstants.SERIAL_MAX_RX]);
//    protected int max_rx_size;
//    protected int received_size;
//    protected string txBuffer = new string(new char[DefineConstants.SERIAL_MAX_TX]);
//    protected int tx_size;
//    protected uint dwCommEvent; // to store the result of the wait

//    // ............................................................
////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
////	void OnCharArrival(sbyte c);
////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
////	void OnEvent(uint events);

//    // ++++++++++++++++++++++++++++++++++++++++++++++
//    // .................. EXTERNAL VIEW .............
//    // ++++++++++++++++++++++++++++++++++++++++++++++

//    public int instance; // used to tell which cam!
//    public object owner; // do what you want with this
////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
////	void run();
////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
////				  Tserial_event();
////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
////				 public void Dispose();
////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
////	int connect(ref string port, int rate, int parity, sbyte ByteSize, int modem_events);

////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
////	void setManager(type_myCallBack manager);
////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
////	void setRxSize(int size);
////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
////	void sendData(ref string buffer, int size);
////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
////	int getNbrOfBytes();
////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
////	int getDataInSize();
////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
////	string getDataInBuffer();
////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
////	void dataHasBeenRead();
////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
////	void disconnect();
//    public type_myCallBack manager;
//}
/* -------------------------------------------------------------------- */





// 18 ms per timer click.



namespace visca_cam
{
	public class VISCA_Timer2
	{
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//			void Setup();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//			void Stop();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//			void ProcessQueue(int Iteration);

			private uint idTimerThread;
			private System.IntPtr hTimerThread;
			private System.IntPtr hWaitTimer;
	}

	// Define our packet helper class

	public class VISCA_Packet
	{
			protected byte[] packet;
			protected int length;

			public VISCA_Packet()
			{
				length = 0;
				packet = new byte [32];
				packet[0] = (byte)'\0';
			}

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//			void NextByte(byte NamelessParameter);

//C++ TO C# CONVERTER WARNING: C# has no equivalent to methods returning pointers to value types:
//ORIGINAL LINE: byte *get_Packet()
			public byte[] get_Packet()
			{
				return packet;
			}

			public int get_Length()
			{
				return length;
			}
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//			void clear();
	}

	// Inbound packet helper class

	public class VISCA_inPacket : VISCA_Packet
	{
//C++ TO C# CONVERTER TODO TASK: C# does not have an equivalent for pointers to value types:
//ORIGINAL LINE: byte *data;
		private byte data;
		private byte sender;
		private int type;
		private int parsed;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		void parse();

		public VISCA_inPacket()
		{
			length = 0;
			packet = new byte [32];
			packet [0] = (byte)'\0';
			parsed = 0;
			type = 0;
		}

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		byte get_Sender();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		byte get_Type();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		void get_Data(ref string indat); // Take bytes from inQueue and bang them into an inPacket, then process.
									 // Triggered when a VISCA_TERMINATOR is recieved. 
	}


	///////////////////////////////////////////////////////////////
	//  VISCA_Cam Class
	///////////////////////////////////////////////////////////////
	public class VISCA_Cam
	{

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		VISCA_Cam(ref string NamelessParameter1, int NamelessParameter2, int NamelessParameter3, ref uint NamelessParameter4); // Constructor
		public Tserial_event com; // Serial manager

		// Add cam functions below (ie zoom, set_date, etc.)
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int cmds_running(); // How many commands are in process
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int if_clear(); // Clear interface
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		void get_cam_type(); // Get camera type
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int power_on(int camera); // turn on
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int power_off(int camera); // turn off
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int lens_init(int camera); // init lens
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int comp_scan(int camera); // init camera
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int set_wb_auto(int camera); // Set auto white balance
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int set_wb_onepush(int camera); // Set and execute one-push WB
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int stabilizer(int camera, int onoff); // Turn stabilizer on or off
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int enable_auto_ir(int camera, int onoff); // Turn auto IR on/off
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int enable_ir_mode(int camera, int onoff); // Turn IR on/off

		// Exposure stuff 
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int set_auto_slow(int camera, int onoff);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int set_exposure_comp_onoff(int camera, int onoff);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int set_exposure_comp(int camera, int compval);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int set_ae_mode(int camera, int ae_mode);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int get_ae_mode();

		// Set shutter - only valid in bright, manual, or shutter modes
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int set_shutter(int camera, int shut_speed);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int set_zoom_direct(int zoom, int camera);

		// Set iris - only valid in bright, manual, or iris modes
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int set_iris(int camera, int iris_setting);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int set_gain(int camera, int cam_gain);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int reset_bright(int camera);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int set_bright(int camera, int cam_bright);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int set_sharpness(int camera, int sharp_val);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int translate_shutter(int shut_speed);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		double translate_iris(int iris_val);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int translate_gain(int gain_val);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		double translate_bright_iris(int bright_val);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int translate_bright_gain(int bright_val);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int set_parameter(int camera, byte parameter, byte newValue);

		// Pan/Tilt functions
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int VISCA_Cam::set_pt_home(int camera);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int VISCA_Cam::go_up(int camera);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int VISCA_Cam::go_down(int camera);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int VISCA_Cam::go_left(int camera);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int VISCA_Cam::go_right(int camera);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int VISCA_Cam::go_home(int camera);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int VISCA_Cam::set_pt_relative(int pan, int tilt, int pan_speed, int tilt_speed, int camera);

		// Packet and serial stuff
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int queue_packet();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		void send_packet();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int serial_status();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		void update_status(int FullMode); // Run status update inquiries

		// Camera state variables
		public int exp_mode; // Exposure mode  0 = Full Auto, 1 = Aperture 2 = Shutter 3= Manual
		public int shut_speed; // Shutter speed code
		public int aperture_code; // Aperture code
		public int gain_code; // Gain code
		public int brt_code; // Bright code
		public int exp_comp_pos; // Exposure compensation, 0 - F, 7 = normal.
		public int zoom_position; // Zoom Position (0x0000 - 0x4000)
		public int zoom_running; // Zoom running, 0 = false, 1 = true
		public int focus_mode; // Focus mode, 0 = Manual, 1 = Auto
		public int focus_position; // Focus position
		public int focus_sensitivity; // Focus sensitivity, 0 = normal, 1 = low
		public int af_mode; // Focus mode, 0 = Normal, 1 = Interval, 2 = Zoom Trigger
		public int af_sensitivity; // AF Sensitivity, 0 = Normal, 1 = Low
		public int af_low_contrast; // AF Low Contrast Detect, 0 = No, 1 = Yes
		public int dz_mode; // Digital zoom mode, 0 = Combine, 1 = separate
		public int focus_near_limit; // Focus near limit
		public int wb_r_gain; // White balance gain R
		public int wb_b_gain; // White balance gain B
		public int wb_mode; // White balance mode
		public int aper_gain; // Aperture Gain
		public int spot_ae; // Spot AE, 0 = off, 1 = on
		public int backlight_enabled; // Backlight mode, 0 = off, 1 = on
		public int exp_comp_enabled; // Exposure Compensation, 0 = off, 1 = on
		public int slow_shut_enabled; // Slow shutter enabled, 0 = Manual, 1 = Auto
		public int auto_icr_enabled; // IR Automatic mode enabled, 0 = off, 1 = on
		public int cam_keylock; // Keylock, 0 = off, 1 = on
		public int cam_power_state; // Power, 0 = off, 1 = on
		public int stabilizer_enabled; // Image stabilizer, 0 = off, 1 = on
		public int icr_on; // IR Mode, 0 = off, 1 = on
		public int image_freeze; // Image freeze, 0 = off, 1 = on
		public int cam_lr_rev; // Left/Right flip, 0 = off, 1 = on
		public int privacy_zone_on; // Privacy zone enabled, 0 = off, 1 = on
		public int image_mute; // Image mute, 0 = off, 1 = on
		public int title_display; // Title display, 0 = off, 1 = on
		public int display_on; // Display on/off
		public int pict_effect_mode; // Picture effect mode code
		public int pict_freeze_on; // Picture freeze on/off
		public int v_phase_code; // V-Phase, 0 = 0 degree, 1 = 180 degree
		public int ext_lock_on; // External lock, 0 = off, 1 = on
		public int digital_zoom_pos; // Digital zoom position
		public int af_act_time; // AF activation time
		public int af_interval; // AF interval
		public int af_spot_x; // Spot AF X
		public int af_spot_y; // Spot AF Y
		public int alarm_on; // Alarm mode, 0 = off, 1 = on
		public int picture_flip; // Picture flip mode, 0 = off, 1 = on
		public int mem_recall; // Camera Memory Recall, 1 = Executing, 0 = Stopped
		public int spot_ae_x; // Spot AE X
		public int spot_ae_y; // Spot AE Y
		public int gamma_mode; // Gamma Mode 0, 1, or 2

		// Camera model and capabilities
		public int cam_type_id; // Camera ID code
		public string cam_model_name; // Camera model name
		public int ext_lock; // External lock, 0 = none, 1 = provided
		public int cam_memory; // Camera memory, 0 = none, 1 = provided
		public int cam_clock_1; // Camera clock, 0 = none, 1 = provided
		public int cam_icr; // Camera ICR capable, 0 = none, 1 = provided
		public int cam_stabilizer; // Camera stabilizer capable, 0 = none, 1 = provided
		public int cam_system_type; // Camera system type, 0 = NTSC, 1 = PAL
		public int cam_system_mode; // Camera system mode for HD
		public int cam_adv_privacy; // Advanced privacy, 0 = none, 1 = provided
		public int cam_alarm; // Alarm capabile, 0 = none, 1 = provided
		public int cam_picture_flip; // Picture flip capable, 0 = none, 1 = provided
		public int cam_pan_tilt; // Pan/Tilt capable, 0 = none, 1 = provided

		// Serial handling - called when data comes in/.
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		void SerialDatIn(int NamelessParameter1, ref string NamelessParameter2, int NamelessParameter3);
		//public Queue<sbyte,deque<sbyte, allocator<sbyte>>> inQueue = new Queue<sbyte,deque<sbyte, allocator<sbyte>>>();
		//public Queue<sbyte,deque<sbyte, allocator<sbyte>>> outQueue = new Queue<sbyte,deque<sbyte, allocator<sbyte>>>();

		// command_header() builds initial packet header
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int command_header(int DevNum);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int command_header_tilt(int DevNum);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int inquiry_header(int DevNum);

		// Inquiry handlers
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		void process_inquiry();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int process_inq_lcs(ref byte pkt);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int process_inq_ccs(ref byte pkt);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int process_inq_gen(ref byte pkt);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int process_inq_enl(ref byte pkt);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int process_inq_ver(ref byte pkt);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		int process_inq_register(ref byte pkt);

		// set addresses
		private int set_address;

		// Private variables
		private int errCode; // Error code - false indicates serial error.
		private string port; // COM port name
		private VISCA_inPacket inPacket; // Inbound packet buffer
		private int num_devices; // How many devices on com port -- not to be used
		private System.IntPtr VIDH; // Serial port handle - probably will be unused
		//private LPDCB commstate = new LPDCB(); // For serial status info -- probably not used
		private VISCA_Packet packet; // Packet to write out
		private int pkts_outstanding; // How many packets are out there.
		private int pkts_waiting; // How many packets are awaiting their turn to go out.
		private int pkt_queueing; // Are we composing a packet?
		private int pkt_timer; // Update each time we hit the timer to detect timeout conditions.
									// Reset to zero when we either timeout or send a packet.
		private int last_inq_cat; // Last inquiry category - 0 = normal, 1 = long/block, 2 = tilt, -1 = none
		private int last_inq_typ; // Last inquiry type.  Set to last inquiry type code or -1 for none
		private int last_inq_parm; // Last inquiry parameter for special tracking.

		// Inquiry thread vars
		private uint idInQThread;
		private System.IntPtr hInqThread;
	}
}
