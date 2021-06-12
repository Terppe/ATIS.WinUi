using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl66GenusRepository : Repository<Tbl66Genus>, ITbl66GenusRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl66GenusRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
