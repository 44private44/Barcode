using Barcoad_Entities.BarcoadsModels;
using Barcoad_Entities.DataModels;
using Barcoad_Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barcoad_Repositories.Repositories
{
    public class GenerateBarcoadRepository : IGenerateBarcoadRepository
    {
        private readonly ImagesdbContext _context;
        private readonly string connectionString = "Server=PCA172\\SQL2017;Database=imagesdb;Trusted_Connection=True;MultipleActiveResultSets=true;User ID=sa;Password=Tatva@123;Integrated Security=False; TrustServerCertificate=True";

        public GenerateBarcoadRepository(ImagesdbContext context)
        {
            _context = context;
        }

        // Get All Barcoad Data
        public List<string> GetAllBarcodes(BarcoadsViewModel data)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var parameters = new
                {
                    numBarcodes = data.numBarcodes,
                    barcodeLength = data.barcodeLength,
                };

                var results = connection.Query<string>("GenerateRandomCharacters", parameters, commandType: CommandType.StoredProcedure).ToList();

                connection.Close();
                return results;
            }
        }


    }
}
