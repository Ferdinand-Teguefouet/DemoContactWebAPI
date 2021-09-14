using BLL.Interface;
using BLL.Models;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Tools;

namespace BLL.Services
{
    public class BusinessContactService : IContactBusiness<BusinessContact>
    {
        private IContactRepoLibrary _contactRepo;

        public BusinessContactService(IContactRepoLibrary cr)
        {
            _contactRepo = cr;
        }

        public void Delete(int Id)
        {
            if(Id == 1)
            {
                throw new ArgumentException("On ne peut supprimer le contact ID 1");
            }
            _contactRepo.Delete(Id);
        }

        public IEnumerable<BusinessContact> GetAll()
        {
            return _contactRepo.GetAll().Select(c => c.ToBll());
        }

        public BusinessContact GetById(int Id)
        {
            return _contactRepo.GetById(Id).ToBll();
        }

        public void Insert(BusinessContact c)
        {
            _contactRepo.Insert(c.ToDal());
        }

        public void Update(BusinessContact c)
        {
            _contactRepo.Update(c.ToDal());
        }
    }
}
