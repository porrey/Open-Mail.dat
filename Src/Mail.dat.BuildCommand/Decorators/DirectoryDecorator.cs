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
namespace Mail.dat.BuildCommand
{
	public static class DirectoryDecorator
	{
		//
		// This extension method deletes all files in the specified directory that match the given pattern.
		// It ensures the directory exists before attempting to delete files.
		//
		public static void DeleteAllFiles(this DirectoryInfo directory, string pattern)
		{
			//
			// Ensure the directory exists. If it does not, it will be created.
			//
			directory.Create();

			//
			// Retrieve all files in the directory that match the specified pattern.
			//
			FileInfo[] files = directory.GetFiles(pattern, SearchOption.TopDirectoryOnly);

			//
			// Iterate through each file and delete it.
			//
			foreach (FileInfo file in files)
			{
				file.Delete();
			}
		}
	}
}
