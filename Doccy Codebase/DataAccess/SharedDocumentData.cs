using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace DataAccessLibrary
{
    public class SharedDocumentData(IDataAccess db) : ISharedDocumentData
    {
        private readonly IDataAccess _db = db;

        public Task<List<SharedDocumentModel>> GetAllUserSharedDocuments(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException(nameof(username));
            }

            string sql =
       @"
SELECT TOP(500) *
  FROM [dbo].[SharedDocument]
  WHERE username = '" + username + "'";



            return _db.LoadData<SharedDocumentModel, dynamic>(sql, new { });
        }

        public Task InsertSharedDocument(SharedDocumentModel sharedDocument)
        {

            var parameters = new
            {
                DocumentTitle = sharedDocument.DocumentTitle,
                DocumentType = sharedDocument.DocumentType,
                SharedWith = sharedDocument.SharedWith,
                Username = sharedDocument.Username,
                ShareDT = DateTime.Now,
            };

            string sql =
@"
INSERT INTO [dbo].[SharedDocument]
(documentTitle, documentType, sharedWith, username, shareDT)
values (@DocumentTitle, @DocumentType, @SharedWith, @Username, @ShareDT)";

            return _db.SaveData(sql, parameters);
        }

    }
}

