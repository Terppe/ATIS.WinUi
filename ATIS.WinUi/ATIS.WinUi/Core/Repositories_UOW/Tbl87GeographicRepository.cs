using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl87GeographicRepository : Repository<Tbl87Geographic>, ITbl87GeographicRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl87GeographicRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
