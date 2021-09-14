using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetAll();
        Contact GetById(int Id);
        void Insert(Contact c);
        void Update(Contact c);
        void Delete(int Id);
    }
}
