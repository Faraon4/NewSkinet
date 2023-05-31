using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria {get; } // Func<T, bool> -> T is the type that is takes and the bool it is what it is returning  //Criteria => Where (...) as example 
        List<Expression<Func<T, object>>> Includes {get;} // The Include 


        // expressions for the ordering
        Expression<Func<T, object>> OrderBy {get;}
        Expression<Func<T, object>> OrderByDescending {get;}
    }
}