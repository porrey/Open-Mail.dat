//
// This file is part of Open Mail.dat.
// Copyright (c) 2025-2026 Daniel Porrey, Open Mail.dat. All rights reserved.
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
	/// Provides a default implementation for creating instances of <see cref="MaildatContext"/>.
	/// </summary>
	/// <remarks>This factory is designed to simplify the creation of <see cref="MaildatContext"/> instances by
	/// encapsulating the required dependencies, such as a logger and database context options.</remarks>
	public class DefaultDbContextFactory : IMaildatDbContextFactory<MaildatContext>
	{
		/// <summary>
		/// Creates a new instance of the <see cref="MaildatContext"/> class using the specified logger and database context
		/// options.
		/// </summary>
		/// <param name="logger">The logger used to log messages for the <see cref="MaildatContext"/> instance. Cannot be <see langword="null"/>.</param>
		/// <param name="options">The options used to configure the <see cref="MaildatContext"/> instance. Cannot be <see langword="null"/>.</param>
		/// <returns>A new instance of the <see cref="MaildatContext"/> class.</returns>
		public MaildatContext CreateDbContext(ILogger<MaildatContext> logger, string connectionString)
		{
			DbContextOptions<MaildatContext> options = new DbContextOptionsBuilder<MaildatContext>()
					.UseSqlite(connectionString)
					.Options;

			return new MaildatContext(logger, options);
		}

		public MaildatContext CreateDbContext()
		{
			throw new NotImplementedException();
		}
	}
}
