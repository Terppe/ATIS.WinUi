using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl45FamilyRepository : Repository<Tbl45Family>, ITbl45FamilyRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl45FamilyRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
