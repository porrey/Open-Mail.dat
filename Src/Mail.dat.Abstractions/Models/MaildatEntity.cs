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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Diamond.Core.Repository;

namespace Mail.dat
{
	/// <summary>
	/// Represents the base class for entities in the Mail.dat system, providing common functionality  for importing and
	/// exporting data associated with Mail.dat records.
	/// </summary>
	/// <remarks>This abstract class serves as a foundation for Mail.dat entities, encapsulating shared properties
	/// and methods for handling data import and export operations. Derived classes can override the  <see
	/// cref="OnImportDataAsync"/> and <see cref="OnExportData"/> methods to implement  custom behavior for specific
	/// entity types.</remarks>
	public abstract class MaildatEntity : Entity, IMaildatEntity
	{
		/// <summary>
		/// The unique identifier for the record.
		/// </summary>
		[Key]
		[Column("Id", Order = 0)]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the line number in the file associated with this record.
		/// </summary>
		[Column("FileLineNumber", Order = 1)]
		public long FileLineNumber { get; set; }

		[Column("Ref1", Order = 9992)]
		[MaxLength(60)]
		public string Ref1 { get; set; }

		[Column("Ref2", Order = 9993)]
		[MaxLength(40)]
		public string Ref2 { get; set; }

		[Column("Ref3", Order = 9994)]
		[MaxLength(20)]
		public string Ref3 { get; set; }

		[Column("Ref4", Order = 9995)]
		[MaxLength(20)]
		public string Ref4 { get; set; }

		[Column("Ref5", Order = 9996)]
		[MaxLength(10)]
		public string Ref5 { get; set; }

		[Column("Ref6", Order = 9997)]
		[MaxLength(10)]
		public string Ref6 { get; set; }

		[Column("Ref7", Order = 9998)]
		[MaxLength(10)]
		public string Ref7 { get; set; }

		[Column("Ref8", Order = 9999)]
		public int Ref8 { get; set; }

		[Column("Ref9", Order = 9999)]
		public decimal Ref9 { get; set; }

		[Column("Ref10", Order = 9999)]
		public DateTime Ref10 { get; set; }

		/// <summary>
		/// Imports data asynchronously by processing a single line of input.
		/// </summary>
		/// <remarks>This method delegates the actual import logic to the <see cref="OnImportDataAsync"/> method,
		/// which can be overridden in derived classes to customize the import behavior. Callers should handle any errors
		/// returned in the result to ensure data integrity.</remarks>
		/// <param name="version">The version of the data format being imported. This value determines how the input line is interpreted.</param>
		/// <param name="lineNumber">The line number of the input data, used for tracking or error reporting purposes. Must be a non-negative integer.</param>
		/// <param name="line">A read-only span of bytes representing the data to be imported. The content and encoding of the data must conform
		/// to the specified <paramref name="version"/>.</param>
		/// <returns>A task that represents the asynchronous operation. The task result is an array of <see cref="ILoadError"/>
		/// objects,  where each element describes an error encountered during the import process. If no errors occur, the
		/// array will be empty.</returns>
		public virtual Task<ILoadError[]> ImportDataAsync(string version, int lineNumber, ReadOnlySpan<byte> line)
		{
			return this.OnImportDataAsync(version, lineNumber, line);
		}

		/// <summary>
		/// Exports data to the specified buffer using the provided version, width, and encoding.
		/// </summary>
		/// <remarks>This method delegates the export operation to an overridden implementation in a derived class.
		/// Ensure that the buffer is appropriately sized to avoid data truncation or overflow.</remarks>
		/// <param name="version">The version identifier used to determine the format of the exported data. Cannot be null or empty.</param>
		/// <param name="buffer">The buffer to which the data will be written. Must be large enough to hold the exported data.</param>
		/// <param name="width">The width parameter that influences the structure or layout of the exported data. Must be a positive integer.</param>
		/// <param name="encoding">The character encoding used to process the data during export. Cannot be null.</param>
		public virtual void ExportData(string version, Span<byte> buffer, int width, Encoding encoding)
		{
			this.OnExportData(version, buffer, width, encoding);
		}

		/// <summary>
		/// Handles the import of a single line of data during a data loading operation.
		/// </summary>
		/// <remarks>This method is intended to be overridden in a derived class to provide custom logic for
		/// processing individual lines of data. The default implementation returns an empty array,  indicating no
		/// errors.</remarks>
		/// <param name="version">The version of the data format being imported.</param>
		/// <param name="lineNumber">The line number of the data being processed, starting from 1.</param>
		/// <param name="line">A read-only span of bytes representing the data content of the line.</param>
		/// <returns>A task that represents the asynchronous operation. The task result is an array of  <see cref="ILoadError"/>
		/// objects indicating any errors encountered while processing the line.  If no errors occur, the array will be empty.</returns>
		protected virtual Task<ILoadError[]> OnImportDataAsync(string version, int lineNumber, ReadOnlySpan<byte> line)
		{
			return Task.FromResult<ILoadError[]>([]);
		}

		/// <summary>
		/// Exports data to the specified buffer using the provided parameters.
		/// </summary>
		/// <remarks>This method is intended to be overridden in a derived class to implement custom export logic.
		/// The base implementation does not perform any operations.</remarks>
		/// <param name="version">The version of the data format to use during export. This value determines the structure of the exported data.</param>
		/// <param name="buffer">The buffer to which the exported data will be written. The buffer must be large enough to hold the data.</param>
		/// <param name="width">The width parameter that influences the layout or structure of the exported data. Must be a positive value.</param>
		/// <param name="encoding">The character encoding to use for any text data included in the export. This determines how text is represented in
		/// the output.</param>
		protected virtual void OnExportData(string version, Span<byte> buffer, int width, Encoding encoding)
		{
		}
	}
}
