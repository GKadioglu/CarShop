using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.DataAccess
{
    public interface IRepository<T> 
    {
        Task<T> GetById(int id);
        Task <List<T>> GetAll();
        void Create(T entity);
        Task CreateAsync(T entity);
        void Update(T entity);    
        void Delete(T entity);
    }
}