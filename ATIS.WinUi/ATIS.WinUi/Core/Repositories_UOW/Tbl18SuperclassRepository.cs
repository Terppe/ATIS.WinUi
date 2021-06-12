using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl18SuperclassRepository : Repository<Tbl18Superclass>, ITbl18SuperclassRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl18SuperclassRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;
        }
    }
}
