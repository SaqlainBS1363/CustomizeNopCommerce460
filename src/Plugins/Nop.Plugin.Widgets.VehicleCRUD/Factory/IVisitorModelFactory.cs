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
        Task<ConfigurationModel> PrepareVisitorModelAsync(ConfigurationModel configurationModel);

        Task<IEnumerable<ConfigurationModel>> PrepareVisitorModelListAsync();

        Task<IEnumerable<PublicInfoModel>> PreparePublicVisitorModelListAsync();

        Task<ConfigurationModel> AddVisitorModelAsync(ConfigurationModel configurationModel);

        Task<ConfigurationModel> GetVisitorModelAsync(int Id);

        Task<ConfigurationModel> EditVisitorModelAsync(ConfigurationModel configurationModel);

        Task<ConfigurationModel> DeleteVisitorModelAsync(int Id);
    }
}
