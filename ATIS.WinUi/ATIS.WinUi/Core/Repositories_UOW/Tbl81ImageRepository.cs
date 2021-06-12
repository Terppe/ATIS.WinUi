using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl81ImageRepository : Repository<Tbl81Image>, ITbl81ImageRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl81ImageRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
