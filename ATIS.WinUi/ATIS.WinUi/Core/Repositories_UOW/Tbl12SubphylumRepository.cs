using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl12SubphylumRepository : Repository<Tbl12Subphylum>, ITbl12SubphylumRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl12SubphylumRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
