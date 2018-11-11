using Products.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Domain.Extensions
{
    public static class PaginationExtension
    {
        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> query, Pagination pagination)
        {
            return query
                .Skip((pagination.PageNumber - 1) * pagination.PageNumber)
                .Take(pagination.PageSize);
        }

        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> query)
        {
            Pagination pagination = new Pagination();

            return query
                .Skip((pagination.PageNumber - 1) * pagination.PageNumber)
                .Take(pagination.PageSize);
        }
    }
}
