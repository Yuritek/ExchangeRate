using System.Xml.Serialization;

namespace RequestToSite
{
    [XmlType(AnonymousType = true)]
    public class ValCursRecord
    {
        public string Nominal { get; set; }
        public string Value { get; set; }
        [XmlAttribute]
        public string Date { get; set; }
    }
}
