using System;

namespace Platform.Services.Dto.Attributes
{
    public class RefAttribute : Attribute
    {
        /// <summary>
        /// Наименование контроллера.
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// Наименование метода контроллера.
        /// </summary>
        public string ControllerMethod { get; set; }

        /// <summary>
        /// Наименование отображаемого свойства(только для форм).
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Конструктор для гридов.
        /// </summary>
        public RefAttribute(string controller, string controllerMethod)
        {
            Controller = controller;
            ControllerMethod = controllerMethod;
        }

        /// <summary>
        /// Конструктор для форм.
        /// </summary>
        public RefAttribute(string controller, string controllerMethod, string propertyName)
        {
            Controller = controller;
            ControllerMethod = controllerMethod;
            PropertyName = propertyName;
        }
    }
}
