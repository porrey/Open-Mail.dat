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
	public class CommentBuilder : ICodeBuilder<CommentBuilder>
	{
		//
		// Stores the lines of comments to be written.
		//
		public List<string> Lines { get; internal set; } = new();

		//
		// Specifies the maximum length for each comment line.
		//
		public int MaximumLineLength { get; internal set; } = 100;

		//
		// Creates a new CommentBuilder instance with the provided lines.
		//
		public static CommentBuilder Create(params List<string> lines)
		{
			return new CommentBuilder() { Lines = lines };
		}

		//
		// Sets the maximum line length for the comment builder.
		//
		public CommentBuilder SetMaximumLineLength(int maximumLineLength)
		{
			this.MaximumLineLength = maximumLineLength;
			return this;
		}

		//
		// Adds a single comment line to the builder.
		//
		public CommentBuilder AddComment(string comment)
		{
			this.Lines.Add(comment);
			return this;
		}

		//
		// Adds multiple comment lines to the builder.
		//
		public CommentBuilder AddComments(params List<string> lines)
		{
			this.Lines.AddRange(lines);
			return this;
		}

		//
		// Writes all comment lines to the specified file, with optional indentation.
		//
		public CommentBuilder Build(string filePath, int indentLevel = 0)
		{
			foreach (string line in this.Lines)
			{
				//
				// Write each comment line to the file, prepending indentation and comment slashes.
				//
				File.AppendAllLines(filePath, [$"{Tabs.Create(indentLevel)}// {line.Trim()}".Trim()]);
			}

			return this;
		}
	}
}
