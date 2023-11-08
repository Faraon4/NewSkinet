using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        private Hashtable _repositories; // any repository that we are going to use inside the unit of work , will be stored inside this hashtabel
        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }

        public async Task<int> Complete()
        {
           return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }


        // This method is checking if the TEntity is in hastable or not
        // Supose as example the Product instead of the TEntity

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
           if(_repositories == null) _repositories = new Hashtable();

           var type = typeof(TEntity).Name;

           if(!_repositories.ContainsKey(type))
           {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

            _repositories.Add(type, repositoryInstance);
           }
           return (IGenericRepository<TEntity>) _repositories[type];
        }
    }
}