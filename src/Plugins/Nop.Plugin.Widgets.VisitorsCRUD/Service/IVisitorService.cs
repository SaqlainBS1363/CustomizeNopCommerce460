using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Plugin.Widgets.VisitorsCrud.Domain;

namespace Nop.Plugin.Widgets.VisitorsCrud.Service
{
    public interface IVisitorService
    {
        /// <summary>
        /// Gets all visitors
        /// </summary>
        /// <param name="visitorName">Visitor name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="visitorGender">Visitor gender</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the categories
        /// </returns>
        Task<IPagedList<Visitor>> GetAllVisitorsAsync(string visitorName, string visitorGender, bool visitorActiveStatus, int pageIndex = 0, int pageSize = int.MaxValue);
        //Get All Visitors
        Task<IPagedList<Visitor>> GetAllVisitorsAsync();

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
