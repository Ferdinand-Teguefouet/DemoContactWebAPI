using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IContactBusiness<TEntity> 
        where TEntity : new()
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int Id);
        void Delete(int Id);
        void Insert(TEntity c);
        void Update(TEntity c);
    }
}
