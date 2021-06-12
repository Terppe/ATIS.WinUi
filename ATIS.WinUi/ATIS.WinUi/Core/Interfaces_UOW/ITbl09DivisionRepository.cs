﻿using System.Collections.Generic;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Interfaces_UOW
{
    public interface ITbl09DivisionRepository : IRepository<Tbl09Division>
    {
        IEnumerable<Tbl09Division> ListTbl09DivisionsOnlyPlantaeOrderBy(string search);

    }
}