using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl30LegioRepository : Repository<Tbl30Legio>, ITbl30LegioRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl30LegioRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
