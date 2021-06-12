using System.Collections.Generic;
using System.Linq;
using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl90RefSourceRepository : Repository<Tbl90RefSource>, ITbl90RefSourceRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl90RefSourceRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }

        public IEnumerable<Tbl90RefSource> ListTbl90RefSourcesOrderBy()
        {
            return _atisDbContext.Tbl90RefSources.OrderBy(x => x.RefSourceName).ToList();

        }
    }
}