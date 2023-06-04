using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity // We make is to work only with out entity clases
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;
            //Evaluate what is inside the spec parameter that we input
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            // We add the evaluation of the ordering by adding the two check if we fo have OrderBy parametere
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            if (spec.IsPaginingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take); // ordering is important , and paging operator need to come after any filter operation
            }

            //Aggregate is putting toghether the Include that we have in the ProductRepository
            // query => is our query with the criteria (Where keyword)
            // current is the entoty that we are passing in here
            // include -> expressions of our include statements
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));  

            return query;
        }
    }
}