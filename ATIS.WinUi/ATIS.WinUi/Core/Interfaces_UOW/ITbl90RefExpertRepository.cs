using System.Collections.Generic;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Interfaces_UOW
{
    public interface ITbl90RefExpertRepository : IRepository<Tbl90RefExpert>
    {
        IEnumerable<Tbl90RefExpert> ListTbl90RefExpertsOrderBy();
    }
}
