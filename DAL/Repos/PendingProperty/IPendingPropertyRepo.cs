using DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos.PendingProperty
{
    public interface IPendingPropertyRepo
    {
        IEnumerable<Appartment> GetAll();
        Appartment? GetById(int id);
        void Delete(int id);
        void Accept (int id);
        int SaveChanges();
    }
}
