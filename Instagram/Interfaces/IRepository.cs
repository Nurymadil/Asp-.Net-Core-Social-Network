using System;
using System.Collections.Generic;

namespace Instagram.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        IList<T> GetAll();
        void Create(T entity);
        T Edit(T entity);
        void Delete(T entity);
    }
}