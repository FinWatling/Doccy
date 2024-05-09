using DataAccessLibrary.Models;
using System.Numerics;

namespace DataAccessLibrary
{
    public interface IDocumentData
    {
        Task DeleteDocument(int documentID);
        Task<List<DocumentModel>> GetAllUserDocuments(BigInteger? userID);
        Task InsertDocument(DocumentModel document, string username);
    }
}