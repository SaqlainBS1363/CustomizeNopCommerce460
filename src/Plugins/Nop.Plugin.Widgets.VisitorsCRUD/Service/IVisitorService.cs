using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Plugin.Widgets.VisitorsCrud.Domain;

namespace Nop.Plugin.Widgets.VisitorsCrud.Service
{
    public interface IVisitorService
    {
        //Get All Visitors
        Task<IEnumerable<Visitor>> GetAllVisitorsAsync();

        //Get Single Visitor
        Task<Visitor> GetSingleVisitorAsync(int Id);

        //Add Visitor
        Task AddVisitorAsync(Visitor visitor);

        //Update or Edit Visitor
        Task UpdateVisitorAsync(Visitor visitor);

        //Delete Visitor
        Task DeleteVisitorAsync(Visitor visitor);
    }
}
