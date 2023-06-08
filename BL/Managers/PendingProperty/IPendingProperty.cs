using BL.Dtos.PendingProperty;
using DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public interface IPendingProperty
 {
   
    List<PendingReadDto> GetAll();
    PendingReadDetailsDto? GetById(int id);
    PendingDeleteORAcceptDto? Delete(int id);
    void Accept(int id);

}

