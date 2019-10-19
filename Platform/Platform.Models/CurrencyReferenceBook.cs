using Platform.Models.Attributes;

namespace Platform.Models
{
    public class CurrencyReferenceBook
    {
        [GridAttribute]
        [FormAttribute]
        public string Name { get; set; }

        [GridAttribute]
        public string Code { get; set; }
    }
}
