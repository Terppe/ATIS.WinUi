using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl93CommentRepository : Repository<Tbl93Comment>, ITbl93CommentRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl93CommentRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
