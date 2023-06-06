using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Widgets.VisitorsCrud.Domain;
using Nop.Plugin.Widgets.VisitorsCrud.Models;
using Nop.Plugin.Widgets.VisitorsCrud.Service;

namespace Nop.Plugin.Widgets.VisitorsCrud.Factory
{
    public class VisitorModelFactory : IVisitorModelFactory
    {
        private readonly IVisitorService _visitorService;

        public VisitorModelFactory(IVisitorService visitorService)
        {
            _visitorService = visitorService;
        }

        public async Task<ConfigurationModel> PrepareVisitorModelAsync(ConfigurationModel configurationModel)
        {
            configurationModel.GenderSelection = new List<SelectListItem>
            {
                new SelectListItem { Text = "Select a gender", Value = "" },
                new SelectListItem { Text = "Male", Value = "Male" },
                new SelectListItem { Text = "Female", Value = "Female" },
                new SelectListItem { Text = "Other", Value = "Other" }
            };

            return configurationModel;
        }

        public async Task<IEnumerable<ConfigurationModel>> PrepareVisitorModelListAsync()
        {
            var visitors = _visitorService.GetAllVisitorsAsync().Result;
            var visitorList = new List<ConfigurationModel>();

            foreach (var visitor in visitors)
            { 
                visitorList.Add(new ConfigurationModel
                {
                    VisitorId = visitor.Id,
                    Name = visitor.Name,
                    Age = visitor.Age,
                    Gender = visitor.Gender,
                    Phone = visitor.Phone
                });
            }

            return visitorList;
        }

        public async Task<ConfigurationModel> AddVisitorModelAsync(ConfigurationModel configurationModel)
        {
            var newVisitor = new Visitor();

            newVisitor.Name = configurationModel.Name;
            newVisitor.Phone = configurationModel.Phone;
            newVisitor.Age = configurationModel.Age;
            newVisitor.Gender = configurationModel.Gender;

            await _visitorService.AddVisitorAsync(newVisitor);

            return null;
        }
    }
}
