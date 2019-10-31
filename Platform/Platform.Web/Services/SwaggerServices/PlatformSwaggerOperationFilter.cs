using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Platform.Web.Services.SwaggerServices
{
    public class PlatformSwaggerOperationFilter : IOperationFilter
    {
        private readonly PlatformSwaggerSchemasCustomizer _platformSwaggerSchemaCustomizer;

        public PlatformSwaggerOperationFilter()
        {
            _platformSwaggerSchemaCustomizer = new PlatformSwaggerSchemasCustomizer();
        }
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            _platformSwaggerSchemaCustomizer.CustomizeDefaultSwaggerSchemas(context.SchemaRepository.Schemas);
        }
    }
}