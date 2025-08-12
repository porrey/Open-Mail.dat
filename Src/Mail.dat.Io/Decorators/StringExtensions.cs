//
// This file is part of Open Mail.dat.
// Copyright (c) 2025 Daniel Porrey, Open Mail.dat. All rights reserved.
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
using System.Text;

namespace Mail.dat.Io
{
	/// <summary>
	/// 
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// Converts non-printable characters to printable form.
		/// Common control characters are shown as escaped literals (\n, \r, \t).
		/// Others are shown as \xNN.
		/// </summary>
		public static string ToPrintable(this string input)
		{
			if (input == null)
			{
				return string.Empty;
			}

			StringBuilder sb = new(input.Length);

			foreach (char c in input)
			{
				switch (c)
				{
					case '\n':
						sb.Append("\\n");
						break;
					case '\r':
						sb.Append("\\r");
						break;
					case '\t':
						sb.Append("\\t");
						break;
					default:
						if (char.IsControl(c) || !IsPrintableAscii(c))
						{
							sb.Append("\\x");
							sb.Append(((int)c).ToString("X2"));
						}
						else
						{
							sb.Append(c);
						}
						break;
				}
			}

			return sb.ToString();
		}

		/// <summary>
		/// Checks if the character is printable in standard ASCII.
		/// </summary>
		private static bool IsPrintableAscii(char c)
		{
			return c >= 0x20 && c <= 0x7E;
		}
	}
}