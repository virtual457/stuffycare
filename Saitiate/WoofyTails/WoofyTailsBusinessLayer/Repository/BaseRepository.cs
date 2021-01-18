using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoofyTailsDALLayer.EFModels;

namespace WoofyTailsBusinessLayer.Repository
{
    public class BaseRepository
    {
        protected readonly WoofyTailsDBContext context;
        public BaseRepository()
        {
            context = new WoofyTailsDBContext();
        }
        public virtual int save()
        {
            return context.SaveChanges();
        }
    }
}
