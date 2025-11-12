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
// You should have received three files as part of the license agreement for Open Mail.dat.
//
// LICENSE.md (GNU Lesser General Public License)
// LICENSE-GPL3.md (GNU General Public License)
// LICENSE-ADDENDUM.md (Attribution and Public Use Addendum to the GNU Lesser General Public License v3.0 (LGPL-3.0))
//
// If not, see <https://www.gnu.org/licenses/>.
// ************************************************************************************************************************
//
// Author: Daniel M Porrey
//
using System.IO.MemoryMappedFiles;
using System.Text;

namespace Mail.dat.Io
{
	public static class MemoryMappedDecorator
	{
		public static long StartingOffset(this MemoryMappedFile mmf)
		{
			long returnValue = 0;

			using (MemoryMappedViewStream view = mmf.CreateViewStream(0, 0, MemoryMappedFileAccess.Read))
			{
				Encoding encoding = Encoding.UTF8;

				byte[] header = new byte[4];
				int read = view.Read(header, 0, header.Length);
				view.Position = 0;

				if (read >= 4 && header[0] == 0xFF && header[1] == 0xFE && header[2] == 0x00 && header[3] == 0x00)
				{
					encoding = new UTF32Encoding(bigEndian: false, byteOrderMark: true);
					returnValue = 4;
				}
				else if (read >= 4 && header[0] == 0x00 && header[1] == 0x00 && header[2] == 0xFE && header[3] == 0xFF)
				{
					encoding = new UTF32Encoding(bigEndian: true, byteOrderMark: true);
					returnValue = 4;
				}
				else if (read >= 3 && header[0] == 0xEF && header[1] == 0xBB && header[2] == 0xBF)
				{
					encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);
					returnValue = 3;
				}
				else if (read >= 2 && header[0] == 0xFF && header[1] == 0xFE)
				{
					encoding = Encoding.Unicode; // UTF-16 LE
					returnValue = 2;
				}
				else if (read >= 2 && header[0] == 0xFE && header[1] == 0xFF)
				{
					encoding = Encoding.BigEndianUnicode; // UTF-16 BE
					returnValue = 2;
				}
				else
				{
					encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
					returnValue = 0;
				}
			}

			return returnValue;
		}
	}
}
