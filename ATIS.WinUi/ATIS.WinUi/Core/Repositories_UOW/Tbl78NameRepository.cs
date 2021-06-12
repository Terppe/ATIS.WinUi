using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl78NameRepository : Repository<Tbl78Name>, ITbl78NameRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl78NameRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
