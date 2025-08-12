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
namespace Mail.dat.Json.Specification
{
	/// <summary>
	/// Calculates the total line length of all <see cref="RecordDefinition"/> objects in the collection.
	/// </summary>
	public static class RecordDefinitionDecorator
	{
		/// <summary>
		/// Calculates the total length of all lines represented by the collection of <see cref="RecordDefinition"/> objects.
		/// </summary>
		/// <param name="items">A collection of <see cref="RecordDefinition"/> objects. Each object must have a valid <c>Length</c> property.</param>
		/// <returns>The sum of the <c>Length</c> property values for all <see cref="RecordDefinition"/> objects in the collection.
		/// Returns 0 if the collection is empty.</returns>
		public static int TotalLineLength(this IEnumerable<RecordDefinition> items)
		{
			return items.Sum(t => t.Length);
		}
	}
}
