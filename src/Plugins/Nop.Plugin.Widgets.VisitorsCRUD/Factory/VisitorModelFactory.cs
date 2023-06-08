using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.Excel;
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
                    Id = visitor.Id,
                    Name = visitor.Name,
                    Age = visitor.Age,
                    Gender = visitor.Gender,
                    Phone = visitor.Phone
                });
            }

            return visitorList;
        }

        public async Task<IEnumerable<PublicInfoModel>> PreparePublicVisitorModelListAsync()
        {
            var visitors = _visitorService.GetAllVisitorsAsync().Result;
            var visitorList = new List<PublicInfoModel>();

            foreach (var visitor in visitors)
            {
                visitorList.Add(new PublicInfoModel
                {
                    Id = visitor.Id,
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

        public async Task<ConfigurationModel> GetVisitorModelAsync(int Id)
        {
            var getVisitor = _visitorService.GetSingleVisitorAsync(Id).Result;
            var sendVisitor = new ConfigurationModel
            { 
                Id = Id,
                Name = getVisitor.Name,
                Age = getVisitor.Age,
                Gender = getVisitor.Gender,               
                Phone = getVisitor.Phone
            };

            sendVisitor.GenderSelection = new List<SelectListItem>
            {
                new SelectListItem { Text = "Select a gender", Value = "" },
                new SelectListItem { Text = "Male", Value = "Male" },
                new SelectListItem { Text = "Female", Value = "Female" },
                new SelectListItem { Text = "Other", Value = "Other" }
            };

            return sendVisitor;
        }

        public async Task<ConfigurationModel> EditVisitorModelAsync(ConfigurationModel configurationModel)
        {
            var newVisitor = _visitorService.GetSingleVisitorAsync(configurationModel.Id).Result;

            newVisitor.Name = configurationModel.Name;
            newVisitor.Phone = configurationModel.Phone;
            newVisitor.Age = configurationModel.Age;
            newVisitor.Gender = configurationModel.Gender;

            await _visitorService.UpdateVisitorAsync(newVisitor);

            return null;
        }

        public async Task<ConfigurationModel> DeleteVisitorModelAsync(int Id)
        {
            var newVisitor = _visitorService.GetSingleVisitorAsync(Id).Result;

            await _visitorService.DeleteVisitorAsync(newVisitor);

            return null;
        }
    }
}
