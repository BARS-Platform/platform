using System;

namespace Platform.Models.Enums
{
    [Flags]
    public enum AttributesEnum
    {
        /// <summary>
        /// Признак отображения в гриде
        /// </summary>
        Grid = 1,

        /// <summary>
        /// Признак отображения в форме
        /// </summary>
        Form = 2
    }
}
