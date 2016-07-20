﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Escyug.LissBinder.Models.Metadata;

namespace Escyug.LissBinder.Models.Services
{
    public interface IImportService
    {
        /// <summary>
        /// Get file metadata
        /// </summary>
        /// <param name="connectionString">Connection string to OleDb file.</param>
        /// <returns>Tables metadata.</returns>
        Task<IEnumerable<TableMetadata>> GetFileMetadataAsync(string connectionString);

        /// <summary>
        /// Import data from OleDb file to  MS SqlServer
        /// </summary>
        /// <param name="connectionString">OleDb file connection string.</param>
        /// <param name="tableName">OleDb file table name.</param>
        /// <param name="pharmacyId">Pharmacy id.</param>
        /// <returns>Operation status.</returns>
        Task<bool> ImportAsync(string connectionString, string tableName, int pharmacyId);
    }
}
