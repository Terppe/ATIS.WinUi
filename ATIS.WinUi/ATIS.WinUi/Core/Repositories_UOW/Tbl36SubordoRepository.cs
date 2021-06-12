using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl36SubordoRepository : Repository<Tbl36Subordo>, ITbl36SubordoRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl36SubordoRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
