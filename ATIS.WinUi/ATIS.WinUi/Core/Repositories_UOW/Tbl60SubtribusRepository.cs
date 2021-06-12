using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl60SubtribusRepository : Repository<Tbl60Subtribus>, ITbl60SubtribusRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl60SubtribusRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
