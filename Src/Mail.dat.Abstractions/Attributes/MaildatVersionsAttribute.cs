﻿//
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
// Author: Daniel M porrey
//
namespace Mail.dat
{
	/// <summary>
	/// Attribute to specify the Mail.dat version supporting the given object.
	/// </summary>
	[AttributeUsage(AttributeTargets.All)]
	public class MaildatVersionsAttribute : Attribute
	{
		/// <summary>
		/// Specifies the Maildat versions supported by the associated component or functionality.
		/// </summary>
		/// <param name="supportedVersions">An array of strings representing the supported Maildat versions. Each version should be specified as a non-null,
		/// non-empty string.</param>
		public MaildatVersionsAttribute(params string[] supportedVersions)
		{
			this.SupportedVersions = supportedVersions;
		}

		/// <summary>
		/// Gets or sets the collection of supported version identifiers.
		/// </summary>
		/// <remarks>This property allows you to specify or retrieve the versions that are supported by the current
		/// context.  Ensure that the collection contains valid version identifiers relevant to your application's
		/// requirements.</remarks>
		public IEnumerable<string> SupportedVersions { get; set; }
	}
}
