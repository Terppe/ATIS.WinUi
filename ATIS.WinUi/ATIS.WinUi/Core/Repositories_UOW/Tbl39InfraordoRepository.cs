using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl39InfraordoRepository : Repository<Tbl39Infraordo>, ITbl39InfraordoRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl39InfraordoRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
