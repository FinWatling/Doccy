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
    public class DocumentData(IDataAccess db) : IDocumentData
    {
        private readonly IDataAccess _db = db;

        public Task<List<DocumentModel>> GetAllUserDocuments(BigInteger? userID)
        {
            if (userID == null)
            {
                throw new ArgumentNullException(nameof(userID));
            }

            string sql =
       @"
SELECT TOP(500)[documentID]
      ,[documentTitle]
      ,[documentType]
      ,[data]
	  ,Document.[userID]
	  ,dbo.[User].email
      ,Document.[uploadedDT]
      ,Document.[lastEditedDT]
  FROM [dbo].[Document]
  INNER JOIN dbo.[User]
  ON Document.userID = [User].userID
  WHERE dbo.[User].userID = " + userID;



            return _db.LoadData<DocumentModel, dynamic>(sql, new { });
        }

        public Task InsertDocument(DocumentModel document, string username)
        {

            var parameters = new
            {
                DocumentTitle = document.DocumentTitle,
                DocumentType = document.DocumentType,
                UserID = (int)document.UserID,
                UploadedDT = DateTime.Now,
                LastEditedDT = document.LastEditedDT,
                Data = document.Data
            };

            string sql =
@"
INSERT INTO [dbo].[Document]
(documentTitle, documentType, userID, uploadedDT, lastEditedDT, data)
values (@DocumentTitle, @DocumentType, @UserID, @UploadedDT, @LastEditedDT, @Data)";

            return _db.SaveData(sql, parameters);
        }

        public Task DeleteDocument(int documentID)
        {
            var parameters = new
            {
                DocumentID = documentID
            };

            string sql =
@"
DELETE FROM [dbo].[Document]
WHERE documentID = @DocumentID";

            return _db.SaveData(sql, parameters);


        }

        public Task ShareDocuments(IEnumerable<DocumentModel> documents, string email, DateTime expiry)
        {
            return null;
        }
    }
}
