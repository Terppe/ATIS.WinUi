using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl51InfrafamilyRepository : Repository<Tbl51Infrafamily>, ITbl51InfrafamilyRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl51InfrafamilyRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
