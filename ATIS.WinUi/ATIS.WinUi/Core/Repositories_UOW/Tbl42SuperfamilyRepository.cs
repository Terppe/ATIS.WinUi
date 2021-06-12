using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl42SuperfamilyRepository : Repository<Tbl42Superfamily>, ITbl42SuperfamilyRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl42SuperfamilyRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
