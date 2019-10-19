using Microsoft.OpenApi.Models;
using Platform.Models;
using Platform.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Platform.Web.Services.SwaggerServices
{
    /// <summary>
    /// Сервис для кастомизации схем Swagger
    /// </summary>
    public class PlatformSwaggerSchemasCustomizer
    {
        private readonly List<Type> _listModels;

        public PlatformSwaggerSchemasCustomizer()
        {
            _listModels = new List<Type>();

            // Сюда добавлять все модели
            _listModels.Add(typeof(CurrencyReferenceBook));
            _listModels.Add(typeof(WeatherForecast));
        }

        /// <summary>
        /// Метод для формирования словаря, где
        /// ключ - наименование свойства модели,
        /// значение - Grid, Form или Both
        /// в зависимости от наличия атрибутов.
        /// Если нет ни одного атрибута в словарь не добавляется.
        /// </summary>
        private Dictionary<string, string> GetModelPropertiesAttributesDict(string modelName)
        {
            var modelType = _listModels
                .FirstOrDefault(x => x.Name == modelName) ?? throw new Exception("Не найдена соответствующая модель");

            var modelProperties = modelType.GetProperties();

            var propAttributesDictionary = new Dictionary<string, string>();

            foreach (var property in modelProperties)
            {
                var hasGridAttribute = property.GetCustomAttribute(typeof(GridAttribute)) != null 
                    ? true
                    : false;

                var hasFormAttribute = property.GetCustomAttribute(typeof(FormAttribute)) != null
                    ? true
                    : false;

                if (hasGridAttribute && hasFormAttribute)
                {
                    propAttributesDictionary[property.Name.ToLower()] = "Both";
                }
                else if (hasGridAttribute)
                {
                    propAttributesDictionary[property.Name.ToLower()] = "Grid";
                } 
                else if (hasFormAttribute)
                {
                    propAttributesDictionary[property.Name.ToLower()] = "Form";
                }
            }

            return propAttributesDictionary;
        }

        /// <summary>
        /// Метод для изменения стандартных схем Swagger на основе атрибутов
        /// </summary>
        public void CustomizeDefaultSwaggerSchemas(Dictionary<string, OpenApiSchema> defaultSchemas)
        {
            foreach(var model in defaultSchemas)
            {
                var modelPropertiesDict = GetModelPropertiesAttributesDict(model.Key);

                foreach(var property in model.Value.Properties)
                {
                    if (modelPropertiesDict.ContainsKey(property.Key.ToLower()))
                    {
                        property.Value.Description = modelPropertiesDict[property.Key.ToLower()];
                    }
                }
            }
        }
    }
}