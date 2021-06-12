using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl15SubdivisionRepository : Repository<Tbl15Subdivision>, ITbl15SubdivisionRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl15SubdivisionRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;
        }
    }
}
