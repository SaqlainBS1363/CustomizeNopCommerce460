using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using Nop.Core;
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

        public async Task<IPagedList<Visitor>> GetAllVisitorsAsync()
        {
            int pageIndex = 0, pageSize = int.MaxValue;
            var visitors =  await _visitorRepository.GetAllAsync(query =>
            {
                return from v in query where v.Id > 0 select v;
            });

            //paging
            return new PagedList<Visitor>(visitors, pageIndex, pageSize);
        }

        public async Task<IPagedList<Visitor>> GetAllVisitorsAsync(string visitorName, string visitorGender,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var visitors = await _visitorRepository.GetAllAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(visitorGender))
                    query = query.Where(c => c.Gender == visitorGender);

                if (!string.IsNullOrWhiteSpace(visitorName))
                    query = query.Where(c => c.Name.Contains(visitorName));

                return query.OrderBy(c => c.Id);
            });

            //paging
            return new PagedList<Visitor>(visitors, pageIndex, pageSize);
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
