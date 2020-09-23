using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Add below library from Reference memory cache
using System.Runtime.Caching;
using MyShop.Core;
using MyShop.Core.Models;
using System.Diagnostics.Contracts;
// InMemory Cache to Extract db
namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        //Create Cache Memory by creating object
        ObjectCache cache = MemoryCache.Default;
        //Internal List of products.
        List<Product> products;

        //Constructor to initialize product repository
        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if(products == null)
            {
                products = new List<Product>();
            }
        }
        // method lets add products to repository but not save them.
        public void Commit()
        {
            cache["products"] = products;
        }
        // method to Insert Product
        public void  Insert(Product p)
        {
            products.Add(p);
        }
        // Method to update product.
        public void Update(Product product)
        {
            
            Product productToUpdate = products.Find(p => p.Id == product.Id);

            if (productToUpdate != null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }

        //To Find the product using ID.
        public Product Find(string Id)
        {
            Product product = products.Find(p => p.Id == Id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }
        //Return list of products that can be queried
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        //Method to delete product
        public void Delete(string Id)
        {
            Product productToDelete = products.Find(p => p.Id == Id);

            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }
    }
}
