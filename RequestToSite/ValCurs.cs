using System.Xml.Serialization;

namespace RequestToSite
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class ValCurs
    {
        [XmlElement("Record")]
        public ValCursRecord[] Record { get; set; }

        [XmlAttribute]
        public string DateRange2 { get; set; }
        [XmlAttribute]
        public string DateRange1 { get; set; }

        [XmlAttribute]
        public string ID { get; set; }
    }
}
