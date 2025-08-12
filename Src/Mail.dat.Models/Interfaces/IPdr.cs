//
// This file is part of Open Mail.dat.
// Copyright (c) 2025 Open Mail.dat. All rights reserved.
//
// ************************************************************************************************************************
// License Agreement:
//
// Open Mail.dat is free software: you can redistribute it and/or modify it under the terms of the
// GNU LESSER GENERAL PUBLIC LICENSE as published by the Free Software Foundation, either version 3
// of the License, or (at your option) any later version.
// Open Mail.dat is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU LESSER GENERAL PUBLIC LICENSE for more details.
// You should have received three files as part of the license agreemen for Open Mail.dat.
//
// LICENSE.md (GNU Lesser General Public License)
// LICENSE-GPL3.md (GNU General Public License)
// LICENSE-ADDENDUM.md (Attribution and Public Use Addendum to the GNU Lesser General Public License v3.0 (LGPL-3.0))
//
// If not, see <https://www.gnu.org/licenses/>.
// ************************************************************************************************************************
//
// This code was auto-generated on July 9th, 2025 by the Open Mail.dat Code Generator.
// Code Generator Author: Daniel M porrey
//
namespace Mail.dat
{
	/// <summary>
	/// Quantity, rate, weight, and destination per piece (manifest). Provides piece level detail required
	/// of full service mailings; has additional capabilities beyond the Piece Barcode file; when used
	/// instead of the Piece Barcode file, acts as an extension of the PQT file.
	/// </summary>
	public interface IPdr : IMaildatEntity 
	{
		/// <summary>
		/// Job ID (PDR-1001)
		/// (Zero fill prior to numeric, if numeric only). See Header File’s Job Id.
		/// </summary>
		string JobId { get; set; }

		/// <summary>
		/// CQT Database ID (PDR-1034)
		/// </summary>
		int CqtDatabaseId { get; set; }

		/// <summary>
		/// Package ID (PDR-1012)
		/// The unique code for this package within this container. note: may use XXXXXX, if no packages
		/// created.).
		/// </summary>
		string PackageId { get; set; }

		/// <summary>
		/// Piece ID (PDR-1018)
		/// Unique ID of individual piece within mailing.
		/// </summary>
		string PieceId { get; set; }

		/// <summary>
		/// Piece Barcode (PDR-1108)
		/// Numeric values of the applicable  5-Digit, 9-Digit, or  11-Digit Barcode for the specific piece If
		/// specifying a 5-digit or 9-digit barcode, then leave the rest of the field blank.
		/// </summary>
		string PieceBarcode { get; set; }

		/// <summary>
		/// Line-Of-Travel Sequence Number (PDR-1114)
		/// Specific piece's L.O.T.-relative sequence number within ZIP+4.
		/// </summary>
		int? LineOfTravelSequenceNumber { get; set; }

		/// <summary>
		/// Line-Of-Travel Seq. Direction Code (PDR-1115)
		/// Piece's LOT-relative code, if carrier walk its block-face ascending order.
		/// </summary>
		string LineOfTravelSeqDirectionCode { get; set; }

		/// <summary>
		/// Walk Sequence Number (PDR-1116)
		/// Relative Walk Sequence number describing ranking within the carrier's actual delivery sequence.
		/// </summary>
		int? WalkSequenceNumber { get; set; }

		/// <summary>
		/// Wasted or Shortage Piece Indicator (PDR-1117)
		/// </summary>
		string WastedOrShortagePieceIndicator { get; set; }

		/// <summary>
		/// IM™ Barcode (PDR-1122)
		/// To be used for IM™ barcode only. This field not to be used to specify routing ZIP Barcode alone; use
		/// the Piece Barcode field identified above for routing ZIP barcode alone.
		/// </summary>
		string ImBarcode { get; set; }

		/// <summary>
		/// Machine ID (PDR-1124)
		/// Machine ID of the machine at mailers location printing barcodes on the mail pieces. This field
		/// allows participants to identify the machine which applied the barcode on the mailpiece.  When
		/// completed, this field will allow attribution of barcode quality to a single machine during the
		/// Seamless Acceptance postage assessment process.
		/// </summary>
		string MachineId { get; set; }

		/// <summary>
		/// Mailer ID of Barcode Applicator (PDR-1126)
		/// USPS assigned Mailer ID (MID) This field indicates through USPS MID who applied the IM™ barcode to
		/// each mail piece (e.g. Mail Owner, Mailing Agent, etc) . Completion of this field provides additional
		/// information used to attribute barcode  quality. Only digits 0 - 9 acceptable.
		/// </summary>
		string MailerIdOfBarcodeApplicator { get; set; }

		/// <summary>
		/// Move Update Method (PDR-1127)
		/// This field is used to identify Move Update method at the postage statement level.
		/// </summary>
		string MoveUpdateMethod { get; set; }

		/// <summary>
		/// ACS Key Line Data (PDR-1129)
		/// Remove formatting characters as needed to make the data fit in this sixteen byte field.
		/// </summary>
		string ACSKeyLineData { get; set; }

		/// <summary>
		/// Carrier Route (PDR-1130)
		/// USPS Carrier Route code.
		/// </summary>
		string CarrierRoute { get; set; }

		/// <summary>
		/// IMpb® Barcode Construct Code (PDR-1131)
		/// Populate when IMpb® is used.  This code identifies which combination of ZIP, MID, and serial number
		/// is used in the IMpb®.
		/// </summary>
		string ImpbBarcodeConstructCode { get; set; }

		/// <summary>
		/// MID in IMb® is Move Update Supplier (PDR-1132)
		/// </summary>
		string MidInImbIsMoveUpdateSupplier { get; set; }

		/// <summary>
		/// PDR Record Status (PDR-2000)
		/// O, D, I, U.
		/// </summary>
		string PdrRecordStatus { get; set; }

		/// <summary>
		/// Closing Character (PDR-9999)
		/// Must be the # sign.
		/// </summary>
		string ClosingCharacter { get; }
	}
}