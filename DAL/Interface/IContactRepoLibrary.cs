using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IContactRepoLibrary
    {
        IEnumerable<Contact> GetAll();
        Contact GetById(int Id);
        bool Insert(Contact c);
        bool Update(Contact c);
        void Delete(int Id);
    }
}
