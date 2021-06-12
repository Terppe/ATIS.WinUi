using System.Collections.Generic;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Interfaces_UOW
{
    public interface ITbl90RefSourceRepository : IRepository<Tbl90RefSource>
    {
        IEnumerable<Tbl90RefSource> ListTbl90RefSourcesOrderBy();
    }
}
