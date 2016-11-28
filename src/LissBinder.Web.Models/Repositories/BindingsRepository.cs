using System.Threading.Tasks;

using Escyug.LissBinder.Data.QueryProcessors;

using Escyug.LissBinder.Web.Models.Mappings;

namespace Escyug.LissBinder.Web.Models.Repositories
{
    public class BindingsRepository : IBindingsRepository
    {
        private readonly IBindingsQueryProcessor _bindingsQueryProcessor;

        public BindingsRepository(IBindingsQueryProcessor bindingQueryProcessor)
        {
            _bindingsQueryProcessor = bindingQueryProcessor;
        }

        public async Task AddBindingAsync(Models.Binding binding)
        {
            var entity = BindingMappings.ModelToEntity(binding);

            await _bindingsQueryProcessor.InsertAsync(entity);
        }
    }
}
