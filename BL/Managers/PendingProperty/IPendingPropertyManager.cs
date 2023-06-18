using BL.Dtos.PendingProperty;
using DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public interface IPendingPropertyManager
 {
   
    List<PendingReadDto> GetAll();
    PendingReadDetailsDto? GetById(int id);
    void Delete(int id);
   Appartment? Accept(int id,string brokerId, string managerId);
    List<BrokerDataDto> GetAllBroker();

}

