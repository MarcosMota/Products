using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Domain.Entities
{
    public class Pagination
    {
        const int PAGE_SIZE = 10;
        const int PAGE_NUMBER = 1;
        public Pagination()
        {
            PageNumber = PAGE_NUMBER;
            PageSize = PAGE_SIZE;
        }
        public Pagination(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
