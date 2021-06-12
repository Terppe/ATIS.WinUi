using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl33OrdoRepository : Repository<Tbl33Ordo>, ITbl33OrdoRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl33OrdoRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
