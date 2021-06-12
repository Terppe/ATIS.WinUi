using System.Collections.Generic;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Interfaces_UOW
{
    public interface ITbl90RefAuthorRepository : IRepository<Tbl90RefAuthor>
    {
        IEnumerable<Tbl90RefAuthor> ListTbl90RefAuthorsOrderBy();
    }
}
