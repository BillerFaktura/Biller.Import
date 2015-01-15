using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Import_Biller.Import.CSV
{
    class Import : IImport
    {
        public Import(string dataDirectory, Biller.Core.Interfaces.IDatabase database)
        {
            Database = database;
            DataDirectory = dataDirectory;
        }

        public async Task<bool> ImportEverything()
        {
            throw new NotImplementedException();
        }

        public string DataDirectory { get; set; }

        public Biller.Core.Interfaces.IDatabase Database { get; set; }

        public async Task<bool> ImportCompanies()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ImportCustomers()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ImportArticle()
        {
            return await Task<bool>.Run(() => importArticles());
        }

        private async Task<bool> importArticles()
        {
            var savingList = new List<Biller.Core.Articles.Article>();
            using (TextReader reader = File.OpenText(DataDirectory))
            {
                var csv = new CsvReader(reader);
                csv.Configuration.Delimiter = ";";
                while (csv.Read())
                {
                    String id;
                    if (!csv.TryGetField("Artikelnummer", out id))
                        continue;
                    
                    String name;
                    if (!csv.TryGetField("Bezeichnung", out name))
                        continue;

                    double price;
                    if (!csv.TryGetField("Preis 1", out price))
                        price = 0; ;

                    String text;
                    if (!csv.TryGetField("Text", out text))
                        text = "";

                    var outputArticle = new Biller.Core.Articles.Article();
                    outputArticle.ArticleID = id;
                    outputArticle.ArticleDescription = name;
                    outputArticle.ArticleText = text;
                    outputArticle.Price1 = new Biller.Core.Models.PriceModel(outputArticle) { Price1 = new Biller.Core.Utils.EMoney(price, true) };
                    await Database.SaveOrUpdateArticle(outputArticle);
                }
            }
            
            return true;
        }

        public async Task<bool> ImportDocuments()
        {
            throw new NotImplementedException();
        }
    }
}
