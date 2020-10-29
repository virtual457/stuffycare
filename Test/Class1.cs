using System;
using System.Collections.Generic;
using Test.Models;
using System.Linq;
namespace Test
{
    public class Class1
    {
        testContext context = new testContext();
        public List<Admins> GetAdmins()
        {
            var a = new List<Admins>();
            try
            {
                a = (from b in context.Admins
                         orderby b.Adminid
                         select b).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return a;
        }
    }
}
