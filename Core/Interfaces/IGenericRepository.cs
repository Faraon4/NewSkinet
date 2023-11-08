using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity // We use BaseEntity to make sure that classes that derives from BaseEntity , can be used in generic repo
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();

        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

        // Method that will count all elemets that there are after all the filters are applied
        Task<int> CountAsync(ISpecification<T> spec);

        // Method to support updating
        // not async method, because not interact with db
        // used for tracking
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}