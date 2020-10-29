using ClassLibrary1.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace ClassLibrary1
{
    public class Repository
    {
        StuffyCareContext context = new StuffyCareContext();
        public List<Admins> GetAdminNames()
        {
            var b = (from a in context.Admins
                     where a.Adminid=="A0000000001"
                     select a
                   ).ToList();
            return b;
        }
    }
}
