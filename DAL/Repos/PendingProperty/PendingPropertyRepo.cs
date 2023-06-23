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
        public Appartment? Accept(int id , string brokerId, string managerId)
        {
            
         var appartment=  _context.Appartments.FirstOrDefault(x => x.Id == id);
            if(appartment != null)
            {
                appartment.Pending = false;
                appartment.BrokerId = brokerId.ToString();
                appartment.AdminId= managerId.ToString();
            }
            return appartment;

        }

        public void Delete(int id)
        {
            var appartmentt= _context.Appartments.Find(id);
            if(appartmentt != null) { _context.Appartments.Remove(appartmentt); }

            
        }

        public Appartment? GetById(int id)
        {
            return _context.Appartments.Include(a=>a.User).Include(a=>a.Photos).FirstOrDefault(a=>a.Id==id);
        }

        public IEnumerable<Appartment> GetAll()
        {
            var apartments = _context.Appartments.Include(a => a.User).Where(d => d.Pending == true);
            return apartments;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        

        public IEnumerable<Broker> GetAllBroker()
        {
            var brokers = _context.Broker.ToList();
            return brokers;
        }
    }
}
