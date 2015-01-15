using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Import_Biller.Import
{
    interface IImport
    {
        Task<bool> ImportEverything();
        string DataDirectory { get; set; }
        Biller.Core.Interfaces.IDatabase Database { get; set; }
        Task<bool> ImportCompanies();
        Task<bool> ImportCustomers();
        Task<bool> ImportArticle();
        Task<bool> ImportDocuments();
    }
}
