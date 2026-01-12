//
// This file is part of Open Mail.dat.
// Copyright (c) 2025-2026 Open Mail.dat. All rights reserved.
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
// You should have received three files as part of the license agreement for Open Mail.dat.
//
// LICENSE.md (GNU Lesser General Public License)
// LICENSE-GPL3.md (GNU General Public License)
// LICENSE-ADDENDUM.md (Attribution and Public Use Addendum to the GNU Lesser General Public License v3.0 (LGPL-3.0))
//
// If not, see <https://www.gnu.org/licenses/>.
// ************************************************************************************************************************
//
// This code was auto-generated on January 11th, 2026 by the Open Mail.dat Code Generator.
// Code Generator Author: Daniel M Porrey
//
using Mail.dat.Abstractions;

namespace Mail.dat
{
	/// <summary>
	/// These are the allowed values for the property ComponentProcessingCategory (CPT-1112).
	/// </summary>
	[MaildatVersions("23-1", "24-1", "25-1", "26-1")]
	[MaildatFieldLink(File = "cpt", FieldCode = "CPT-1112")]
	public class ComponentProcessingCategories : MaildatValues
	{
		/// <summary>
		/// Returns the Mail.dat file this set of values is lined to.
		/// </summary>
		protected override string OnGetFieldCode()
		{
			return "cpt";
		}

		/// <summary>
		/// Returns the field code that this set of values is linked to.
		/// </summary>
		protected override string OnGetFile()
		{
			return "CPT-1112";
		}

		/// <summary>
		/// Initializes the values.
		/// </summary>
		protected override void OnInitializeValues()
		{
			this.Add(new MaildatValue() { Version = "23-1", Key = "2", FileExtension = "cpt", Description = "Parcel > 108 <= 130", FieldCode = "CPT-1112", FieldName = "ComponentProcessingCategory" });
			this.Add(new MaildatValue() { Version = "24-1", Key = "2", FileExtension = "cpt", Description = "Parcel > 108 <= 130", FieldCode = "CPT-1112", FieldName = "ComponentProcessingCategory" });
			this.Add(new MaildatValue() { Version = "25-1", Key = "CD", FileExtension = "cpt", Description = "Card", FieldCode = "CPT-1112", FieldName = "ComponentProcessingCategory" });
			this.Add(new MaildatValue() { Version = "26-1", Key = "CD", FileExtension = "cpt", Description = "Card", FieldCode = "CPT-1112", FieldName = "ComponentProcessingCategory" });
			this.Add(new MaildatValue() { Version = "25-1", Key = "CM", FileExtension = "cpt", Description = "Custom Mail", FieldCode = "CPT-1112", FieldName = "ComponentProcessingCategory" });
			this.Add(new MaildatValue() { Version = "26-1", Key = "CM", FileExtension = "cpt", Description = "Custom Mail", FieldCode = "CPT-1112", FieldName = "ComponentProcessingCategory" });
			this.Add(new MaildatValue() { Version = "23-1", Key = "D", FileExtension = "cpt", Description = "Dim Weight-Rectangular", FieldCode = "CPT-1112", FieldName = "ComponentProcessingCategory" });
			this.Add(new MaildatValue() { Version = "24-1", Key = "D", FileExtension = "cpt", Description = "Dim Weight-Rectangular", FieldCode = "CPT-1112", FieldName = "ComponentProcessingCategory" });
			this.Add(new MaildatValue() { Version = "25-1", Key = "FL", FileExtension = "cpt", Description = "Flat", FieldCode = "CPT-1112", FieldName = "ComponentProcessingCategory" });
			this.Add(new MaildatValue() { Version = "26-1", Key = "FL", FileExtension = "cpt", Description = "Flat", FieldCode = "CPT-1112", FieldName = "ComponentProcessingCategory" });
			this.Add(new MaildatValue() { Version = "25-1", Key = "IR", FileExtension = "cpt", Description = "Nonstandard Parcels", FieldCode = "CPT-1112", FieldName = "ComponentProcessingCategory" });
			this.Add(new MaildatValue() { Version = "26-1", Key = "IR", FileExtension = "cpt", Description = "Nonstandard Parcels", FieldCode = "CPT-1112", FieldName = "ComponentProcessingCategory" });
			this.Add(new MaildatValue() { Version = "25-1", Key = "LT", FileExtension = "cpt", Description = "Letter", FieldCode = "CPT-1112", FieldName = "ComponentProcessingCategory" });
			this.Add(new MaildatValue() { Version = "26-1", Key = "LT", FileExtension = "cpt", Description = "Letter", FieldCode = "CPT-1112", FieldName = "ComponentProcessingCategory" });
			this.Add(new MaildatValue() { Version = "25-1", Key = "MP", FileExtension = "cpt", Description = "Machinable Parcel", FieldCode = "CPT-1112", FieldName = "ComponentProcessingCategory" });
			this.Add(new MaildatValue() { Version = "26-1", Key = "MP", FileExtension = "cpt", Description = "Machinable Parcel", FieldCode = "CPT-1112", FieldName = "ComponentProcessingCategory" });
			this.Add(new MaildatValue() { Version = "23-1", Key = "N", FileExtension = "cpt", Description = "Not Oversized", FieldCode = "CPT-1112", FieldName = "ComponentProcessingCategory" });
			this.Add(new MaildatValue() { Version = "24-1", Key = "N", FileExtension = "cpt", Description = "Not Oversized", FieldCode = "CPT-1112", FieldName = "ComponentProcessingCategory" });
			this.Add(new MaildatValue() { Version = "25-1", Key = "PF", FileExtension = "cpt", Description = "Parcel, First Class", FieldCode = "CPT-1112", FieldName = "ComponentProcessingCategory" });
			this.Add(new MaildatValue() { Version = "26-1", Key = "PF", FileExtension = "cpt", Description = "Parcel, First Class", FieldCode = "CPT-1112", FieldName = "ComponentProcessingCategory" });
			this.Add(new MaildatValue() { Version = "23-1", Key = "R", FileExtension = "cpt", Description = "Non-Mach Surcharge", FieldCode = "CPT-1112", FieldName = "ComponentProcessingCategory" });
			this.Add(new MaildatValue() { Version = "24-1", Key = "R", FileExtension = "cpt", Description = "Non-Mach Surcharge", FieldCode = "CPT-1112", FieldName = "ComponentProcessingCategory" });
			this.Add(new MaildatValue() { Version = "23-1", Key = "T", FileExtension = "cpt", Description = "Dim Weight-Non-Rectangular", FieldCode = "CPT-1112", FieldName = "ComponentProcessingCategory" });
			this.Add(new MaildatValue() { Version = "24-1", Key = "T", FileExtension = "cpt", Description = "Dim Weight-Non-Rectangular", FieldCode = "CPT-1112", FieldName = "ComponentProcessingCategory" });
		}
	}
}