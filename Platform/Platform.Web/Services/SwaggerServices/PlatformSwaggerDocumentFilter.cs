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
            _platformSwaggerSchemaCustomizer.CustomizeDefaultSwaggerSchemas(context.SchemaRepository.Schemas);
        }
    }
}