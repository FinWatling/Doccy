using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class DocumentModel
    {
        public int DocumentID { get; set; }
        public string? DocumentTitle { get; set; }
        public short DocumentType { get; set; }
        public int? UserID { get; set; }
        public DateTime UploadedDT { get; set; }
        public DateTime LastEditedDT { get; set; }
        public byte[]? Data { get; set; }
        public string? Username { get; set; }

    }
}
