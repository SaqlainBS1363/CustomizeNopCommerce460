using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Data;
using Nop.Plugin.Widgets.VisitorsCrud.Domain;

namespace Nop.Plugin.Widgets.VisitorsCrud.Service
{
    public class VisitorService : IVisitorService
    {
        private readonly IRepository<Visitor> _visitorRepository;

        public VisitorService(IRepository<Visitor> visitorRepository)
        {
            _visitorRepository = visitorRepository;
        }

        public async Task<IEnumerable<Visitor>> GetAllVisitorsAsync()
        {
            return await _visitorRepository.GetAllAsync(query =>
            {
                return from v in query where v.Id > 0 select v;
            });
        }

        public async Task<Visitor> GetSingleVisitorAsync(int Id)
        {
            return await _visitorRepository.GetByIdAsync(Id);
        }

        public async Task AddVisitorAsync(Visitor visitor)
        {
            await _visitorRepository.InsertAsync(visitor);
        }

        public async Task DeleteVisitorAsync(Visitor visitor)
        {
            await _visitorRepository.DeleteAsync(visitor);
        }

        public async Task UpdateVisitorAsync(Visitor visitor)
        {
            await _visitorRepository.UpdateAsync(visitor);
        }
    }
}
