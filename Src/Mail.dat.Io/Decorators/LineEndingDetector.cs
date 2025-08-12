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
using System.Text;

namespace Mail.dat.Io
{
	/// <summary>
	/// Detects the line ending style used in the specified file.
	/// </summary>
	/// <remarks>This method reads up to the first two lines of the file to determine the line ending style. If no
	/// line ending is found within the first two lines, the method returns <see langword="null"/>.</remarks>
	public static class LineEndingDetector
	{
		/// <summary>
		/// Detects the line ending style used in the specified file.
		/// </summary>
		/// <remarks>This method reads up to the first two lines of the file to determine the line ending style. If no
		/// line endings are found within the first two lines, the method returns <see langword="null"/>.</remarks>
		/// <param name="filePath">The path to the file whose line ending style is to be detected. Cannot be null or empty.</param>
		/// <returns>A string representing the detected line ending style. Possible values are: <list type="bullet">
		/// <item><description>"\r\n" for Windows-style line endings.</description></item> <item><description>"\n" for
		/// Unix-style line endings.</description></item> <item><description>"\r" for old Mac-style line
		/// endings.</description></item> </list> Returns <see langword="null"/> if no line ending style could be detected
		/// within the first two lines of the file.</returns>
		public static string DetectLineEnding(this string filePath)
		{
			string returnValue = null;

			using (StreamReader reader = new(filePath))
			{
				for (int i = 0; i < 2 && !reader.EndOfStream; i++)
				{
					string line = ReadRawLine(reader, out string lineEnding);

					if (!string.IsNullOrEmpty(lineEnding))
					{
						returnValue= lineEnding;
						break;
					}
				}
			}

			return returnValue;
		}

		/// <summary>
		/// Reads a single line of text from the specified <see cref="StreamReader"/> without interpreting the line ending.
		/// </summary>
		/// <remarks>This method reads characters from the stream until a line ending sequence is encountered or the
		/// end of the stream is reached. The line ending sequence is not included in the returned string but is provided via
		/// the <paramref name="lineEnding"/> output parameter.</remarks>
		/// <param name="reader">The <see cref="StreamReader"/> to read from. Must not be null.</param>
		/// <param name="lineEnding">When the method returns, contains the line ending sequence encountered during the read operation, which can be
		/// <c>"\r\n"</c>, <c>"\r"</c>, or <c>"\n"</c>. If the end of the stream is reached without encountering a line
		/// ending, this will be an empty string.</param>
		/// <returns>A string containing the characters read from the current line, excluding the line ending sequence. Returns an
		/// empty string if the end of the stream is reached without reading any characters.</returns>
		private static string ReadRawLine(StreamReader reader, out string lineEnding)
		{
			StringBuilder result = new StringBuilder();
			lineEnding = "";

			while (reader.Peek() >= 0)
			{
				char c = (char)reader.Read();
				if (c == '\r')
				{
					if (reader.Peek() == '\n')
					{
						reader.Read(); // consume \n
						lineEnding = "\r\n";
					}
					else
					{
						lineEnding = "\r";
					}
					break;
				}
				else if (c == '\n')
				{
					lineEnding = "\n";
					break;
				}
				else
				{
					result.Append(c);
				}
			}

			return result.ToString();
		}
	}
}