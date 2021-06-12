using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl63InfratribusRepository : Repository<Tbl63Infratribus>, ITbl63InfratribusRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl63InfratribusRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
