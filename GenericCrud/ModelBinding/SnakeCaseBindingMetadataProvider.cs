using GenericCrud.Extensions;
using GenericCrud.Filter.Base;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace GenericCrud.ModelBinding
{
    public class SnakeCaseBindingMetadataProvider : IBindingMetadataProvider
    {
        public void CreateBindingMetadata(BindingMetadataProviderContext context)
        {
            bool isDtoFilterProperty = typeof(ISnakeCaseConvertible).IsAssignableFrom(context.Key.ContainerType);

            if (isDtoFilterProperty)
            {
                if (context.BindingMetadata.BinderModelName == null)
                {
                    context.BindingMetadata.BinderModelName = context.Key.Name.ToSnakeCase();
                }

            }
        }
    }
}
