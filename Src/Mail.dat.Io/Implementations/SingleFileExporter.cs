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
using Mail.dat.Io.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Mail.dat.Io
{
	/// <summary>
	/// Provides functionality to export a collection of entities to a single file in a specified format.
	/// </summary>
	/// <remarks>This class is designed to handle the export of entities implementing the <see
	/// cref="IMaildatEntity"/> interface. The export process includes writing the entities to a file in the specified
	/// format, updating progress during the operation, and ensuring the target directory and file are properly
	/// managed.</remarks>
	internal class SingleFileExporter
	{
		/// <summary>
		/// Gets or sets the delegate to handle asynchronous progress updates.
		/// </summary>
		public ProgressAsyncDelegate ProgressUpdateAsync { get; set; }

		/// <summary>
		/// Exports a collection of entities to a file in a specified format.
		/// </summary>
		/// <remarks>This method creates a file in the target directory specified by <paramref name="options"/>. If
		/// the target directory does not exist, it will be created. If a file with the same name already exists, it will be
		/// overwritten. The method writes the entities to the file in the order specified by their <see
		/// cref="IMaildatEntity.FileLineNumber"/> property and sends progress updates during the export process.</remarks>
		/// <typeparam name="T">The type of the entities to export. Must implement <see cref="IMaildatEntity"/> and have a parameterless
		/// constructor.</typeparam>
		/// <param name="options">The export options that specify the target file, encoding, and line terminator.</param>
		/// <param name="version">The version of the export format to use.</param>
		/// <param name="entityType">The type of the entities being exported. This is used to determine the appropriate export behavior.</param>
		/// <param name="items">A queryable collection of entities to export. The entities must implement <see cref="IMaildatEntity"/>.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous export operation. The task result is <see langword="true"/> if the export
		/// completes successfully; otherwise, <see langword="false"/>.</returns>
		public async Task<bool> ExportAsync<T>(IExportOptions options, string version, Type entityType, IQueryable<IMaildatEntity> items, CancellationToken cancellationToken) where T : class, IMaildatEntity, new()
		{
			bool returnValue = true;

			int lineCount = items.Count();

			if (lineCount > 0)
			{
				//
				// Get the MaildatFileAttribute attribute.
				//
				MaildatFileAttribute classAttribute = typeof(T).GetAttribute<MaildatFileAttribute>();

				//
				// Get the path for the export file.
				//
				string filePath = $"{Path.GetDirectoryName(options.TargetFile.FilePath)}/{Path.GetFileNameWithoutExtension(options.TargetFile.FilePath)}.{classAttribute.Extension}";
				DirectoryInfo targetDirectoy = new(Path.GetDirectoryName(options.TargetFile.FilePath));

				//
				// Create the target directory if it does not exist.
				//
				if (!targetDirectoy.Exists)
				{
					targetDirectoy.Create();
				}

				await this.FireProgressUpdateAsync(new ProgressMessage() { ItemName = classAttribute.File, ItemAction = ProgressMessageType.Start, WillShowProgress = true, ItemSource = filePath, ItemIndex = 1, ItemCount = lineCount });

				//
				// Delete the target file if it exists.
				//
				if (File.Exists(filePath))
				{
					File.Delete(filePath);
				}

				//
				// Get the record length for the file.
				//
				int recordLength = entityType.GetMaildatFieldAttribute(version).LineLength + options.LineTerminator.Length;

				//
				// Create a counter to keep track of the number of lines processed.
				//
				int processedCount = 0;

				//
				// Set up parallel options for the export operation.
				//
				ParallelOptions parallelOptions = new()
				{
					CancellationToken = options.CancellationToken,
#if DEBUG
					MaxDegreeOfParallelism = Environment.ProcessorCount
#else
					MaxDegreeOfParallelism = Environment.ProcessorCount
#endif
				};

				//
				// Create a memory-mapped file for the export.
				//
				using (MemoryMappedRecordFile mmf = new(filePath, recordLength, lineCount))
				{
					//
					// Pre-encode the line terminator.
					//
					byte[] terminator = options.Encoding.GetBytes(options.LineTerminator);

					Parallel.ForEach(items, parallelOptions, async (item, token) =>
					{
						if (!token.ShouldExitCurrentIteration)
						{
							//
							// Allocate space on the stack for the record data.
							//
							Span<byte> buffer = stackalloc byte[recordLength];

							//
							// Export the data to the buffer.
							//
							item.ExportData(version, buffer, recordLength, options.Encoding);

							//
							// Append the line terminator.
							//
							terminator.CopyTo(buffer.Slice(buffer.Length - options.LineTerminator.Length, options.LineTerminator.Length));

							//
							// Write the record to the memory-mapped file.
							//
							mmf.WriteRecord(item.FileLineNumber - 1, buffer);

							//
							// Send a progress update.
							//
							Interlocked.Increment(ref processedCount);
							await this.FireProgressUpdateAsync(new ProgressMessage() { ItemName = classAttribute.File, ItemAction = ProgressMessageType.Progress, WillShowProgress = true, ItemSource = filePath, ItemIndex = processedCount, ItemCount = lineCount });
						}
					});

					//
					// Fire the completed message.
					//
					await this.FireProgressUpdateAsync(new ProgressMessage() { ItemName = classAttribute.File, ItemAction = ProgressMessageType.Completed, WillShowProgress = true, ItemSource = filePath, ItemIndex = lineCount, ItemCount = lineCount });
				}
			}

			return returnValue;
		}

		/// <summary>
		/// Triggers the <see cref="ProgressUpdateAsync"/> event with the specified progress message.
		/// </summary>
		/// <remarks>This method invokes the <see cref="ProgressUpdateAsync"/> event if it has subscribers.  The
		/// provided <paramref name="message"/> should contain the relevant progress information.</remarks>
		/// <param name="message">The progress message to pass to the event handlers. Cannot be <see langword="null"/>.</param>
		/// <returns>A completed <see cref="Task"/> representing the asynchronous operation.</returns>
		protected Task FireProgressUpdateAsync(IProgressMessage message)
		{
			this.ProgressUpdateAsync?.Invoke(message);
			return Task.CompletedTask;
		}
	}
}