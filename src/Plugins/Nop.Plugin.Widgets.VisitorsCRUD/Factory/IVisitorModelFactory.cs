using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.Widgets.VisitorsCrud.Domain;
using Nop.Plugin.Widgets.VisitorsCrud.Models;

namespace Nop.Plugin.Widgets.VisitorsCrud.Factory
{
    public interface IVisitorModelFactory
    {
        Task<ConfigurationModel> PrepareVisitorModelAsync(ConfigurationModel model, Visitor visitor, bool excludeProperties = false);

        Task<ConfigurationModel> PrepareVisitorModelAsync(ConfigurationModel configurationModel);

        Task<ConfigurationSearchModel> PrepareVisitorSearchModelAsync(ConfigurationSearchModel searchModel);

        Task<ConfigurationListModel> PrepareVisitorModelListAsync(ConfigurationSearchModel searchModel);

        Task<IEnumerable<PublicInfoModel>> PreparePublicVisitorModelListAsync();

        Task<Visitor> AddVisitorModelAsync(ConfigurationModel configurationModel);

        Task<Visitor> GetVisitorAsync(int Id);

        Task<ConfigurationModel> GetVisitorModelAsync(int Id);

        Task<Visitor> EditVisitorModelAsync(ConfigurationModel configurationModel);

        Task<ConfigurationModel> DeleteVisitorModelAsync(int Id);
    }
}
