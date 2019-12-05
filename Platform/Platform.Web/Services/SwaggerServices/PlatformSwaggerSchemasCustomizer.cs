using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Platform.Fodels.Attributes;
using Platform.Fodels.Interfaces;
using Platform.Fodels.Enums;
using Microsoft.OpenApi.Any;
using Platform.Services.Dto;
using Platform.Services.Helpers;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Platform.Web.Services.SwaggerServices
{
    /// <summary>
    /// Сервис для кастомизации схем Swagger.
    /// </summary>
    public class PlatformSwaggerSchemasCustomizer
    {
        private readonly Type[] _listModels;

        public PlatformSwaggerSchemasCustomizer()
        {
            _listModels = TypeHelper.GetTypes(typeof(IEntityDto));
        }

        public void AddAdditionalSchemas(ISchemaGenerator schemaGenerator, SchemaRepository schemaRepository)
        {
            foreach(var model in _listModels
                .Where(x => !schemaRepository.Schemas
                    .Select(schema => schema.Key.ToLower())
                    .Contains(x.Name.ToLower())))
            {
                schemaGenerator.GenerateSchema(model, schemaRepository);
            }
        }

        /// <summary>
        /// Метод для изменения стандартных схем Swagger на основе атрибутов.
        /// </summary>
        public void CustomizeDefaultSwaggerSchemas(Dictionary<string, OpenApiSchema> defaultSchemas)
        {
            foreach(var model in defaultSchemas
                .Where(x => _listModels.Select(x => x.Name).Contains(x.Key)))
            {
                var modelPropertiesPlatformAttributeDict = GetModelPropertiesPlatformAttributeDict(model.Key);

                var labelAttributeDict = GetModelLabelsDict(model.Key);

                if (labelAttributeDict.ContainsKey(model.Key.ToLower()))
                {
                    model.Value.Extensions.Add("modelName", labelAttributeDict[model.Key.ToLower()]);
                }

                foreach(var property in model.Value.Properties)
                {
                    if (modelPropertiesPlatformAttributeDict.ContainsKey(property.Key.ToLower()))
                    {
                        property.Value.Extensions.Add("displayIn", modelPropertiesPlatformAttributeDict[property.Key.ToLower()]);
                    }

                    if (labelAttributeDict.ContainsKey(property.Key.ToLower()))
                    {
                        property.Value.Extensions.Add("label", labelAttributeDict[property.Key.ToLower()]);
                    }
                }
            }
        }

        /// <summary>
        /// Метод для формирования словаря, где
        /// ключ - наименование свойства модели,
        /// значение - объект со bool признаками
        /// отображения в Grid, Form.
        /// </summary>
        private Dictionary<string, OpenApiObject> GetModelPropertiesPlatformAttributeDict(string modelName)
        {            
            var modelType = _listModels
                .Single(x => x.Name == modelName);

            var modelProperties = modelType.GetProperties();

            var propAttributesDictionary = new Dictionary<string, OpenApiObject>();

            if (modelType.GetCustomAttribute(typeof(PlatformAttribute)) is PlatformAttribute modelPlatformAttribute)
            {
                if (modelPlatformAttribute.Value == (AttributesEnum.Grid | AttributesEnum.Form))
                {
                    propAttributesDictionary = ConvertToDictionaryWithBoolFields(modelProperties, true, true);
                }
                else
                {
                    propAttributesDictionary = modelPlatformAttribute.Value switch
                    {
                        AttributesEnum.Grid => ConvertToDictionaryWithBoolFields(modelProperties, true, false),
                        AttributesEnum.Form => ConvertToDictionaryWithBoolFields(modelProperties, false, true),
                        _ => propAttributesDictionary 
                    };
                }

                return propAttributesDictionary;
            }

            foreach (var property in modelProperties)
            {
                if (!(property.GetCustomAttribute(typeof(PlatformAttribute)) is PlatformAttribute propPlatformAttribute))
                {
                    propAttributesDictionary[property.Name.ToLower()] = CreateOpenApiObject(false, false);
                    continue;
                }
                    
                if (propPlatformAttribute.Value == (AttributesEnum.Grid | AttributesEnum.Form))
                {
                    propAttributesDictionary[property.Name.ToLower()] = CreateOpenApiObject(true, true);
                }
                else
                {
                    propAttributesDictionary[property.Name.ToLower()] = propPlatformAttribute.Value switch
                    {
                        AttributesEnum.Grid => CreateOpenApiObject(true, false),
                        AttributesEnum.Form => CreateOpenApiObject(false, true),
                        _ => propAttributesDictionary[property.Name.ToLower()]
                    };
                }
            }

            return propAttributesDictionary;
        }

        private OpenApiObject CreateOpenApiObject(bool gridValue, bool formValue)
        {
            return new OpenApiObject
            {
                ["grid"] = new OpenApiBoolean(gridValue),
                ["form"] = new OpenApiBoolean(formValue)
            };
        }

        private Dictionary<string, OpenApiObject> ConvertToDictionaryWithBoolFields(PropertyInfo[] array, bool gridValue, bool formValue)
        {
            return array.ToDictionary(
                x => x.Name.ToLower(),
                x => new OpenApiObject
                {
                    ["grid"] = new OpenApiBoolean(gridValue),
                    ["form"] = new OpenApiBoolean(formValue)
                });
        }

        private Dictionary<string, OpenApiString> GetModelLabelsDict(string modelName)
        {
            var modelType = _listModels
                .Single(x => x.Name == modelName);

            var modelProperties = modelType.GetProperties();

            var labelsDictionary = new Dictionary<string, OpenApiString>();

            if (modelType.GetCustomAttribute(typeof(LabelAttribute)) is LabelAttribute labelAttribute)
            {
                labelsDictionary[modelType.Name.ToLower()] = new OpenApiString(labelAttribute.Value);
            }

            foreach (var property in modelProperties)
            {
                if (property.GetCustomAttribute(typeof(LabelAttribute)) is LabelAttribute propLabelAttribute)
                {
                    labelsDictionary[property.Name.ToLower()] = new OpenApiString(propLabelAttribute.Value);
                }
            }

            return labelsDictionary;
        }
    }
}
