using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkDemo
{
   public class ProductDal
    {
        public List<Product> GetAll()
        {
            using (ETradeContext context = new ETradeContext())
            {
                return context.Products.ToList(); // veri tabanındaki tablolara erişim sağladık.
            }
        }

        public void Add(Product product)
        {
            using (ETradeContext context = new ETradeContext())
            {
                //context.Products.Add(product); // gönderilen ürünü ekle dedik.

                var entity = context.Entry(product); // context üzerinden abone ol dedik.Veri tabanındaki product ile eşitliyor.
                entity.State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(Product product)
        {
            using (ETradeContext context = new ETradeContext())
            {
                var entity = context.Entry(product); 
                entity.State = System.Data.Entity.EntityState.Modified;                                       
                context.SaveChanges();
            }
        }


        public void Delete(Product product)
        {
            using (ETradeContext context = new ETradeContext())
            {
                var entity = context.Entry(product);
                entity.State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

    }
}
