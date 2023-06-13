using DAL.Data.Context;
using DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos.PendingProperty
{
    public class PendingPropertyRepo : IPendingPropertyRepo
    {
        private readonly MyProperyContext _context;

        public PendingPropertyRepo(MyProperyContext context)
        {
            _context=context;
        }
        public void Accept(int id )
        {
         var appartment=  _context.Appartments.FirstOrDefault(x => x.Id == id);
            if(appartment != null)
            {
                appartment.Pending = false;
            }

        }

        public void Delete(int id)
        {
            var appartmentt= _context.Appartments.Find(id);
            if(appartmentt != null) { _context.Appartments.Remove(appartmentt); }

            
        }

        public Appartment? GetById(int id)
        {
            return _context.Appartments.Include(a=>a.User).FirstOrDefault(a=>a.Id==id);
        }

        public IEnumerable<Appartment> GetAll()
        {
            return _context.Appartments.Include(a=>a.User).Where(d=> d.Pending == true).AsNoTracking();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
