using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl57TribusRepository : Repository<Tbl57Tribus>, ITbl57TribusRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl57TribusRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
