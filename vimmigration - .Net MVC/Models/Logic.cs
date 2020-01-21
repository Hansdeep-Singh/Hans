using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VeraCityImmigration.Models;

namespace VeraCityImmigration.Models
{
    public class Logic
    {
       private ClientDetailRepository cdr = new ClientDetailRepository();
        public bool IsPhoto(string id)
        {
            bool flag = true;
            if(cdr.OneRow(id).photo == null)
            { flag = false; }
            return flag;
        }
    }
}