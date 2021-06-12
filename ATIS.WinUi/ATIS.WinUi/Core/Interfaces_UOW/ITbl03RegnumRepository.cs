﻿using System.Collections.Generic;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Interfaces_UOW
{
    public interface ITbl03RegnumRepository : IRepository<Tbl03Regnum>
    {
        IEnumerable<Tbl03Regnum> GetBestRegnums(int countRegnum);

        IEnumerable<Tbl03Regnum> ListTbl03RegnumsByFilterTextAboutAllFields(string filterText);
        IEnumerable<Tbl03Regnum> ListRegnumsBySearchName(object searchName);
        IEnumerable<Tbl03Regnum> ListTbl03RegnumsOrderBy();

    }
}
