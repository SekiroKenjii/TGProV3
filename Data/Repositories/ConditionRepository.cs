using Core.Repositories;
using Domain.Entities;

namespace Data.Repositories
{
    public class ConditionRepository : RepositoryBase<Condition>, IConditionRepository
    {
        public ConditionRepository(DataContext context) : base(context)
        {
        }
    }
}
