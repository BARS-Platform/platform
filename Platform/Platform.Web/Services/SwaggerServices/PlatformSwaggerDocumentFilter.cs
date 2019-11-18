using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Platform.Web.Services.SwaggerServices
{
    public class PlatformSwaggerDocumentFilter : IDocumentFilter
    {
        private readonly PlatformSwaggerSchemasCustomizer _platformSwaggerSchemaCustomizer;

        public PlatformSwaggerDocumentFilter(PlatformSwaggerSchemasCustomizer platformSwaggerSchemaCustomizer)
        {
            _platformSwaggerSchemaCustomizer = platformSwaggerSchemaCustomizer;
        }
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var pairs = context.SchemaRepository.Schemas
                .Where(x=>!x.Value.Enum.Any())
                .ToDictionary(x => x.Key, x => x.Value);
            _platformSwaggerSchemaCustomizer.CustomizeDefaultSwaggerSchemas(pairs);
        }
    }
}