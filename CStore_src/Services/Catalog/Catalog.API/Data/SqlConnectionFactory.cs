﻿using Npgsql;
using System.Data;

namespace Catalog.API.Data
{
    public sealed class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly string _connectionString;
        public SqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Database") ??
                        throw new ApplicationException("Connection string is missing");
        }
        public IDbConnection Create()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
