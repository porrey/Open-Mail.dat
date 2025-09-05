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
using System.IO.MemoryMappedFiles;
using System.Text;
using EFCore.BulkExtensions;
using Mail.dat.Io.Models;

namespace Mail.dat.Io
{
	/// <summary>
	/// Provides functionality to import a single file into the system, processing its contents and updating progress
	/// asynchronously.
	/// </summary>
	/// <remarks>This class is designed to handle the import of files associated with a specific type of entity,
	/// represented by the generic parameter <typeparamref name="T"/>. It supports progress updates, error handling, and
	/// bulk insertion of data into the provided context.</remarks>
	internal class SingleFileImporter
	{
		/// <summary>
		/// Gets or sets the delegate to handle asynchronous progress updates.
		/// </summary>
		public ProgressAsyncDelegate ProgressUpdateAsync { get; set; }
		internal static readonly char[] first = new char[] { '#' };

		/// <summary>
		/// Imports data from a specified source file into the provided database context asynchronously.
		/// </summary>
		/// <remarks>This method processes the source file in parallel to improve performance and uses bulk insertion
		/// to optimize database operations. It also provides progress updates and handles errors encountered during the
		/// import process.</remarks>
		/// <typeparam name="T">The type of the entity to import. Must implement <see cref="IMaildatEntity"/> and have a parameterless
		/// constructor.</typeparam>
		/// <param name="options">The import options that control the behavior of the import process, such as file source and memory usage
		/// preferences.</param>
		/// <param name="version">The version of the data format being imported. This is used to interpret the structure of the source file.</param>
		/// <param name="context">The database context into which the imported data will be inserted.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests. If cancellation is requested, the import process will terminate
		/// early.</param>
		/// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the import
		/// completes successfully; otherwise, <see langword="false"/>.</returns>
		public async Task<bool> ImportAsync<T>(IImportOptions options, string version, MaildatContext context, CancellationToken cancellationToken) where T : class, IMaildatEntity, new()
		{
			bool returnValue = true;

			//
			// Get the MaildatFileAttribute attribute.
			//
			MaildatFileAttribute classAttribute = typeof(T).GetMaildatFieldAttribute(version);

			if (classAttribute != null && !options.ExcludeExtensions.Contains(classAttribute.Extension, StringComparer.OrdinalIgnoreCase))
			{
				//
				// Get the header file
				//
				string extension = classAttribute.Extension;

				string filePath = options.SourceFile.GetFile(extension);

				if (filePath != null)
				{
					//
					// Create a file info object.
					//
					FileInfo maildatFile = new(filePath);

					if (maildatFile.Exists && maildatFile.Length > 0)
					{
						//
						// Get the line ending character(s). These can be empty.
						//
						string lineEndingCharacters = maildatFile.FullName.DetectLineEnding();
						int lineLength = classAttribute.LineLength + lineEndingCharacters.Length;

						//
						// Get the line count. This is used to update the progress bar.
						//
						int lineCount = (int)maildatFile.Length / lineLength;

						//
						// Create list for the modesl, they will
						// all be bulk inserted at the end.
						//
						List<T> modelBuffer = new(lineCount);
						List<Error> errorBuffer = [];

						//
						// Bulk inert cofiguration.
						//
						BulkConfig bc = new()
						{
							PreserveInsertOrder = false
						};

						//
						// Send the start progress update.
						//
						await this.FireProgressUpdateAsync(new ProgressMessage() { ItemName = classAttribute.File, ItemAction = ProgressMessageType.Start, WillShowProgress = true, ItemSource = maildatFile.Name, ItemIndex = 1, ItemCount = lineCount, Context = classAttribute });

						//
						// Create a memory-mapped file to read the file in parallel.
						//
						using (MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile(maildatFile.FullName, FileMode.Open, null))
						{
							//
							// Create a counter to keep track of the number of lines processed.
							//
							int processedCount = 0;

							//
							// Set up parallel options for the import operation.
							//
							ParallelOptions parallelOptions = new()
							{
								CancellationToken = options.CancellationToken,
#if DEBUG
								MaxDegreeOfParallelism = 1
#else
								MaxDegreeOfParallelism = Environment.ProcessorCount
#endif
							};

							//
							// Use Parallel.For to read the file in parallel.
							//
							Parallel.For(0, lineCount, parallelOptions, async (lineNumber, o) =>
							{
								if (!o.ShouldExitCurrentIteration)
								{
									//
									// Calculate the offset for the line in the file.
									//
									long offset = lineNumber * lineLength;

									//
									// Create a view accessor for the line in the file.
									//
									using (MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor(offset, lineLength, MemoryMappedFileAccess.Read))
									{
										//
										// Create a buffer to read the file line into.
										//
										byte[] buffer = new byte[lineLength];

										//
										// Read the line from the file.
										//
										accessor.ReadArray(0, buffer, 0, lineLength);

										//
										// Check if the buffer ends with the expected line ending characters.
										// This is done to ensure that the line is read correctly and ends with the expected characters.
										//
										char[] c = [.. (first).Union(lineEndingCharacters.ToCharArray())];

										if (!buffer.Verify(c))
										{
											string actualEndCharacters = Encoding.UTF8.GetString(buffer, buffer.Length - c.Length, c.Length);
											throw new Exception($"File format error or file misread error encountered.The line {lineNumber + 1} in file {maildatFile.Name} does not end with the expected line ending characters. Expected: '{lineEndingCharacters.ToPrintable()}', Actual: '{actualEndCharacters.ToPrintable()}'");
										}

										//
										// Increment the line counter for each line read.
										//
										Interlocked.Increment(ref processedCount);

										if (cancellationToken.IsCancellationRequested)
										{
											//
											// The import was cancelled. Break out of the loop.
											//
											await this.FireProgressUpdateAsync(new ProgressMessage() { ItemName = "Import", ItemAction = ProgressMessageType.Completed, Message = "Import cancelled.", Context = classAttribute });
										}
										else
										{
											//
											// Create the new model.
											//
											T model = new();

											//
											// Load the data into the model.
											//
											ILoadError[] loadErrors = await model.ImportDataAsync(version, lineNumber + 1, buffer.AsSpan());

											if (options.FavorMemoryOverPerformance)
											{
												lock (modelBuffer)
												{
													if (modelBuffer.Count > options.MaxRecordsInMemory)
													{
														modelBuffer.Add(model);
														context.BulkInsert(modelBuffer, bulkConfig: bc);
														modelBuffer.Clear();
													}
													else
													{
														//
														// Add the model to the context.
														//
														modelBuffer.Add(model);
													}
												}
											}
											else
											{
												lock (modelBuffer)
												{
													//
													// Add the model to the context.
													//
													modelBuffer.Add(model);
												}
											}

											//
											// Check for errors.
											//
											if (loadErrors.Length != 0)
											{
												//
												// Load the errors into the context.
												//
												errorBuffer.AddRange((from tbl in loadErrors
																	  select new Error
																	  {
																		  Process = "Import",
																		  File = extension,
																		  FieldName = tbl.Attribute.FieldName,
																		  FieldCode = tbl.Attribute.FieldCode,
																		  DataType = tbl.Attribute.DataType,
																		  Type = tbl.Attribute.Type,
																		  StartPosition = tbl.Attribute.Start,
																		  Length = tbl.Attribute.Length,
																		  Value = tbl.Value,
																		  ErrorMessage = tbl.ErrorMessage,
																		  LineNumber = lineNumber
																	  }).Select(m => m));
											}

											//
											// Send a progress update.
											//
											await this.FireProgressUpdateAsync(new ProgressMessage() { ItemName = classAttribute.File, ItemAction = ProgressMessageType.Progress, WillShowProgress = true, ItemSource = maildatFile.Name, ItemIndex = processedCount, ItemCount = lineCount, Context = classAttribute });
										}
									}
								}
							});
						}

						//
						// Using the EFCore.BulkExtensions.Sqlite package to perform a bulk insert
						// to improve performance.
						//
						if (modelBuffer.Count > 0)
						{
							await context.BulkInsertAsync(modelBuffer, bulkConfig: bc, cancellationToken: cancellationToken);
						}

						//
						// Check for errors and insert them into the context.
						//
						if (errorBuffer.Count > 0)
						{
							//
							// Add the errors to the context.
							//
							await context.BulkInsertAsync(errorBuffer, bulkConfig: bc, cancellationToken: cancellationToken);
						}

						//
						// Send the completed update.
						//
						_ = this.FireProgressUpdateAsync(new ProgressMessage() { ItemName = classAttribute.File, ItemAction = ProgressMessageType.Completed, WillShowProgress = true, ItemSource = maildatFile.Name, ItemIndex = lineCount, ItemCount = lineCount, Context = classAttribute });
					}
				}
			}

			return returnValue;
		}

		/// <summary>
		/// Triggers the <see cref="ProgressUpdateAsync"/> event with the specified progress message.
		/// </summary>
		/// <remarks>This method invokes the <see cref="ProgressUpdateAsync"/> event if it has been subscribed to.
		/// Callers should ensure that <paramref name="message"/> is not <see langword="null"/> before invoking this
		/// method.</remarks>
		/// <param name="message">The progress message to be passed to the event handler. Cannot be <see langword="null"/>.</param>
		/// <returns>A completed <see cref="Task"/> representing the asynchronous operation.</returns>
		protected Task FireProgressUpdateAsync(IProgressMessage message)
		{
			this.ProgressUpdateAsync?.Invoke(message);
			return Task.CompletedTask;
		}
	}
}