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
using Mail.dat.Abstractions;

namespace Mail.dat
{
	/// <summary>
	/// These are the allowed values for the property FormType (CHR-1101).
	/// </summary>
	[MaildatVersions("24-1", "25-1")]
	[MaildatFieldLink(File = "chr", FieldCode = "CHR-1101")]
	public class FormTypes : MaildatValues 
	{
		/// <summary>
		/// Returns the Mail.dat file this set of values is lined to.
		/// </summary>
		protected override string OnGetFieldCode()
		{
			return "chr";
		}

		/// <summary>
		/// Returns the field code that this set of values is linked to.
		/// </summary>
		protected override string OnGetFile()
		{
			return "CHR-1101";
		}

		/// <summary>
		/// Initializes the values.
		/// </summary>
		protected override void OnInitializeValues()
		{
			this.Add(new MaildatValue() { Version = "24-1", Key = "A", FileExtension = "chr", Description = "PS Form 3606-D (Bulk Certificate of Mailing)", FieldCode = "CHR-1101", FieldName = "FormType" });
			this.Add(new MaildatValue() { Version = "25-1", Key = "A", FileExtension = "chr", Description = "PS Form 3606-D (Bulk Certificate of Mailing)", FieldCode = "CHR-1101", FieldName = "FormType" });
			this.Add(new MaildatValue() { Version = "24-1", Key = "B", FileExtension = "chr", Description = "PS Form 3665 (Certificate of Mailing)", FieldCode = "CHR-1101", FieldName = "FormType" });
			this.Add(new MaildatValue() { Version = "25-1", Key = "B", FileExtension = "chr", Description = "PS Form 3665 (Certificate of Mailing)", FieldCode = "CHR-1101", FieldName = "FormType" });
			this.Add(new MaildatValue() { Version = "24-1", Key = "C", FileExtension = "chr", Description = "PS Form 3877 (Firm Book for Accountable Mail)", FieldCode = "CHR-1101", FieldName = "FormType" });
			this.Add(new MaildatValue() { Version = "25-1", Key = "C", FileExtension = "chr", Description = "PS Form 3877 (Firm Book for Accountable Mail)", FieldCode = "CHR-1101", FieldName = "FormType" });
			this.Add(new MaildatValue() { Version = "24-1", Key = "D", FileExtension = "chr", Description = "Reserved", FieldCode = "CHR-1101", FieldName = "FormType" });
			this.Add(new MaildatValue() { Version = "25-1", Key = "D", FileExtension = "chr", Description = "Reserved", FieldCode = "CHR-1101", FieldName = "FormType" });
			this.Add(new MaildatValue() { Version = "24-1", Key = "E", FileExtension = "chr", Description = "Reserved", FieldCode = "CHR-1101", FieldName = "FormType" });
			this.Add(new MaildatValue() { Version = "25-1", Key = "E", FileExtension = "chr", Description = "Reserved", FieldCode = "CHR-1101", FieldName = "FormType" });
			this.Add(new MaildatValue() { Version = "24-1", Key = "F", FileExtension = "chr", Description = "Reserved", FieldCode = "CHR-1101", FieldName = "FormType" });
			this.Add(new MaildatValue() { Version = "25-1", Key = "F", FileExtension = "chr", Description = "Reserved", FieldCode = "CHR-1101", FieldName = "FormType" });
		}
	}
}