using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl90ReferenceRepository : Repository<Tbl90Reference>, ITbl90ReferenceRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl90ReferenceRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;
        }
    }
}
