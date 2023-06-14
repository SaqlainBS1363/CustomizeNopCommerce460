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
using Nop.Services.Localization;
using Nop.Services.Seo;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Framework.Factories;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.Widgets.VisitorsCrud.Factory
{
    public class VisitorModelFactory : IVisitorModelFactory
    {
        private readonly IVisitorService _visitorService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedModelFactory _localizedModelFactory;

        public VisitorModelFactory(IVisitorService visitorService, 
            IUrlRecordService urlRecordService, 
            ILocalizationService localizationService, 
            ILocalizedModelFactory localizedModelFactory)
        {
            _visitorService = visitorService;
            _urlRecordService = urlRecordService;
            _localizationService = localizationService;
            _localizedModelFactory = localizedModelFactory;
        }

        /// <summary>
        /// Prepare visitor model (configuration model)
        /// </summary>
        /// <param name="model">configuration model</param>
        /// <param name="visitor">visitor</param>
        /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the configuration model
        /// </returns>
        public async Task<ConfigurationModel> PrepareVisitorModelAsync(ConfigurationModel model, Visitor visitor, bool excludeProperties = false)
        {
            Func<VisitorLocalizedModel, int, Task> localizedModelConfiguration = null;

            if (visitor != null)
            {
                //fill in model values from the entity
                if (model == null)
                {
                    model = new ConfigurationModel();

                    model.Name = visitor.Name;
                    model.Phone = visitor.Phone;
                    model.Age = visitor.Age;
                    model.Gender = visitor.Gender;
                    model.IsActive = visitor.IsActive;

                    /*model = visitor.ToModel<ConfigurationModel>();*/

                    model.SeName = await _urlRecordService.GetSeNameAsync(visitor, 0, true, false);
                }

                //define localized model configuration action
                localizedModelConfiguration = async (locale, languageId) =>
                {
                    locale.Name = await _localizationService.GetLocalizedAsync(visitor, entity => entity.Name, languageId, false, false);
                };
            }

            //prepare localized models
            if (!excludeProperties)
                model.Locales = await _localizedModelFactory.PrepareLocalizedModelsAsync(localizedModelConfiguration);

            model = await PrepareVisitorModelAsync(model);

            return model;
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

        public async Task<ConfigurationSearchModel> PrepareVisitorSearchModelAsync(ConfigurationSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //prepare "Gender" filter
            searchModel.AvailableVisitorGenderOptions = new List<SelectListItem>
            {
                new SelectListItem { Text = "All", Value = "" },
                new SelectListItem { Text = "Male", Value = "Male" },
                new SelectListItem { Text = "Female", Value = "Female" },
                new SelectListItem { Text = "Other", Value = "Other" }
            };

            //prepare page parameters
            searchModel.SetGridPageSize();

            return searchModel;
        }

        public async Task<ConfigurationListModel> PrepareVisitorModelListAsync(ConfigurationSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get visitors
            var visitors = await _visitorService.GetAllVisitorsAsync(
                    visitorName: searchModel.SearchVisitorName,
                    visitorGender: searchModel.SearchVisitorGender,
                    visitorActiveStatus: searchModel.SearchVisitorActiveStatus,
                    pageIndex: searchModel.Page - 1,
                    pageSize: searchModel.PageSize
                );

            //prepare grid model
            var model = await new ConfigurationListModel().PrepareToGridAsync(searchModel, visitors, () =>
            {
                return visitors.SelectAwait(async visitor =>
                {
                    //fill in model values from the entity
                    /*var configurationModel = visitor.ToModel<ConfigurationModel>();*/

                    //fill in model values from the entity
                    var configurationModel = new ConfigurationModel
                    {
                        Id = visitor.Id,
                        Name = visitor.Name,
                        Age = visitor.Age,
                        SeName = "" + visitor.Name + visitor.Age,
                        Gender = visitor.Gender,
                        Phone = visitor.Phone,
                        IsActive = visitor.IsActive
                    };
                    return configurationModel;
                });
            });

            return model;            
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

        public async Task<Visitor> AddVisitorModelAsync(ConfigurationModel configurationModel)
        {
            var newVisitor = new Visitor();

            newVisitor.Name = configurationModel.Name;
            newVisitor.Phone = configurationModel.Phone;
            newVisitor.Age = configurationModel.Age;
            newVisitor.Gender = configurationModel.Gender;
            newVisitor.IsActive = configurationModel.IsActive;

            await _visitorService.AddVisitorAsync(newVisitor);

            return newVisitor;
        }

        public async Task<Visitor> GetVisitorAsync(int Id)
        {
            var getVisitor = _visitorService.GetSingleVisitorAsync(Id).Result;
            
            return getVisitor;
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
                Phone = getVisitor.Phone,
                IsActive = getVisitor.IsActive
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

        public async Task<Visitor> EditVisitorModelAsync(ConfigurationModel configurationModel)
        {
            var newVisitor = _visitorService.GetSingleVisitorAsync(configurationModel.Id).Result;

            newVisitor.Name = configurationModel.Name;
            newVisitor.Phone = configurationModel.Phone;
            newVisitor.Age = configurationModel.Age;
            newVisitor.Gender = configurationModel.Gender;
            newVisitor.IsActive = configurationModel.IsActive;

            await _visitorService.UpdateVisitorAsync(newVisitor);

            return newVisitor;
        }

        public async Task<ConfigurationModel> DeleteVisitorModelAsync(int Id)
        {
            var newVisitor = _visitorService.GetSingleVisitorAsync(Id).Result;

            await _visitorService.DeleteVisitorAsync(newVisitor);

            return null;
        }
    }
}
