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
        IEnumerable<Broker> GetAllBroker();
        Appartment? GetById(int id);
        
        void Delete(int id);
        Appartment? Accept (int id , string brokerId,string managerId);
        int SaveChanges();
    }
}
