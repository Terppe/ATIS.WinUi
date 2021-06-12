using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl48SubfamilyRepository : Repository<Tbl48Subfamily>, ITbl48SubfamilyRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl48SubfamilyRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
