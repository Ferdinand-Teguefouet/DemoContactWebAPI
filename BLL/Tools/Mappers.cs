using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Tools
{
    public static class Mappers
    {
        public static BusinessContact ToBll(this Contact contact)
        {
            return new BusinessContact
            {
                Id = contact.Id,
                LastName = contact.LastName,
                FirstName = contact.FirstName, 
                Email = contact.Email
            };
        }

        public static Contact ToDal(this BusinessContact contact)
        {
            return new Contact
            {
                Id = contact.Id,
                LastName = contact.LastName,
                FirstName = contact.FirstName,
                Email = contact.Email
            };
        }
    }
}
