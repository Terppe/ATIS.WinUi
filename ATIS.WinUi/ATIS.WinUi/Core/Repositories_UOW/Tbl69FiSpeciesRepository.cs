using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl69FiSpeciesRepository : Repository<Tbl69FiSpecies>, ITbl69FiSpeciesRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl69FiSpeciesRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
