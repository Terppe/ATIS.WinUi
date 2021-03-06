using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class TblCountryRepository : Repository<TblCountry>, ITblCountryRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public TblCountryRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
