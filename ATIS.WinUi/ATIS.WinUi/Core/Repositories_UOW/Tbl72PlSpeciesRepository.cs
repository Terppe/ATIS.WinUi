using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl72PlSpeciesRepository : Repository<Tbl72PlSpecies>, ITbl72PlSpeciesRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl72PlSpeciesRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
