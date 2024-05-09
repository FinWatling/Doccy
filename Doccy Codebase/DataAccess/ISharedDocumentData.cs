using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
    public interface ISharedDocumentData
    {
        Task<List<SharedDocumentModel>> GetAllUserSharedDocuments(string username);
        Task InsertSharedDocument(SharedDocumentModel sharedDocument);
    }
}