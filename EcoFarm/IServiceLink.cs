using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm;

internal interface IServiceLink
{
    string localHostClient { get; }
    string mainClient { get; }

    internal Task<List<Supplier>> GetSuppliers();
    internal Task<List<Product>> GetProducts(int supplierId);
    internal Task<List<Review>> GetReviews(int supplierId);
    internal Task<SupplierAbout> GetSupplierDesciption(int supplierId);

    internal Task<string> RegisterUser(RegisterDTO registerDTO);
}
