using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class SharedDocumentModel
    {
        public int SharedDocumentID { get; set; }
        public string? DocumentTitle { get; set; }
        public short DocumentType { get; set; }
        public string? SharedWith { get; set; }
        public string? Username { get; set; }
        public DateTime ShareDT { get; set; }

    }
}
