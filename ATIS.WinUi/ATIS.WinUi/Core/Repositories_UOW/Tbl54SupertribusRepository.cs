using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl54SupertribusRepository : Repository<Tbl54Supertribus>, ITbl54SupertribusRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl54SupertribusRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
