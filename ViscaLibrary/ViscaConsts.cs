/*

 visca_consts.h

 Constants for the VISCA camera control library

 * VISCA(tm) Camera Control
 * Copyright (C) 2005 - 2008 MGC Works Inc.
 *
 * Written by John Mazza <maz@mgcworks.com>
 * Based on LibVISCA library for Unix and LibViscaWin 
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

 */




// VISCA_DEV_CAPS	Type is a struct to store specifics for 
//					devices we know about.

public class VISCA_DEV_CAPS
{
	public int dev_cap_id;
	public int dev_type_id;
	public int full_id;
	public int mfg_id;
	public int model_id;
	public string model_name = new string(new char[DefineConstants.VISCA_MAX_NAME_LEN]);
	public bool zoom_lens;
	public int min_zoom;
	public int max_zoom;
	public bool pan_tilt;
}

// Allow up to 4 cameras

// Send Headers


//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define VISCA_IF_CLEAR '\x88\x01\x00\xFF'

// Receive Headers

// Error Codes

// General Constants for Parameter Values

// "Normal" command headers
// Normal means that the above constants apply as far as they are supported 
// by the hardware (i.e., VISCA_ON can be used to turn these on)

// Pan/Tilt command headers

// TEST





// Pan/Tilt general constants

// Modes for multi-standard HD cameras
// In FCB-H10, they are stored in register 0x70

// Strange command headers
// "Strange" means that either the VISCA_ON etc. constants do not 
// apply to these, or those parameters are not supported

// Register command headers
// Used for specific read/write of camera registers
										// from FCB-H10 and possibly other multi-system
										// cams
// Exposure Compensation

//Auto-Exposure constants

// Manual-Exposure constants

// White Balance Constants

// Zoom Constants
										// 0x0000 to 0x4000, data length is 4 byes 
										// stored in low-nibble of each byte
										// (i.e, 04 00 00 00 = 4000, 00 00 00 00= 0000)

// Sharpness Aperture Constants

//Title constants

// Colors: used for setting the color for the title

// These features require their own functions
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define VISCA_ADDRESS_SET '\x30\x01'

// INQUIRY CONSTANTS
// "Bulk" inquiries



// "Specific" Inquiries

									// from FCB-H10 and possibly other multi-system cams

// Constants for various camera models


// Names for various models

// TERMINATOR (this comes at the end of each packet)
