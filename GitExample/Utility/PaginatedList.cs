using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPIClient;

namespace CarSales.Utility
{
    //This class handles Pagination with only 5 products per page
    public class PaginatedList<Product> : List<Product>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public static int count;
       

        public PaginatedList(List<Product> items,int count, int pageIndex, int pageSize)
        {
           
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        /* The CreateAsync method in this code takes page size and page number and 
            applies the appropriate Skip and Take statements to the IQueryable. 
            When ToListAsync is called on the IQueryable, it will return a List containing 
            only the requested page. The properties HasPreviousPage and HasNextPage can be 
            used to enable or disable Previous and Next paging buttons. */
        public static async Task<PaginatedList<Product>> CreateAsync(List<Product> products, int pageIndex, int pageSize)
        {
            count = products.Count();
           var items = products.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return new PaginatedList<Product>(items.ToList(), count,pageIndex, pageSize);
        }
    }
}
