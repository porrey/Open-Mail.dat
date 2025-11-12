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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Mail.dat.Models
{
	/// <summary>
	/// Defines a factory for creating instances of <see cref="DbContext"/>.
	/// </summary>
	/// <remarks>This interface is typically used to create new instances of a <see cref="DbContext"/> with specific
	/// configuration or dependencies, such as logging or database options. It is particularly useful in scenarios where
	/// dependency injection is required or when managing the lifecycle of a <see cref="DbContext"/>.</remarks>
	/// <typeparam name="TDatabaseContext">The type of <see cref="DbContext"/> to create. This type must derive from <see cref="DbContext"/>.</typeparam>
	public interface IMaildatDbContextFactory<TDatabaseContext> : IDbContextFactory<TDatabaseContext>
		where TDatabaseContext : MaildatContext
	{
		/// <summary>
		/// Creates and configures an instance of the database context.
		/// </summary>
		/// <param name="logger">The logger used to capture diagnostic information and log messages related to the database context.</param>
		/// <param name="connectionString">The connection string used to establish a connection to the database. Cannot be null or empty.</param>
		/// <returns>An instance of <typeparamref name="TDatabaseContext"/> configured with the specified connection string and logger.</returns>
		TDatabaseContext CreateDbContext(ILogger<TDatabaseContext> logger, string connectionString);
	}
}
