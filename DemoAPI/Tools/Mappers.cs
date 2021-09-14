using BLL.Models;
using DemoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Tools
{
    public static class Mappers
    {
        public static ContactForm ToApi(this BusinessContact bc)
        {
            return new ContactForm
            {
                Id = bc.Id,
                Email = bc.Email,
                FirstName = bc.FirstName,
                LastName = bc.LastName
            };
        }

        public static BusinessContact ToBLL(this ContactForm c)
        {
            return new BusinessContact
            {
                Id = c.Id,
                Email = c.Email,
                LastName = c.LastName,
                FirstName = c.FirstName
            };
        }
    }
}
