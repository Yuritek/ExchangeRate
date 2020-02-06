using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace RequestToSite
{
    public class RequestToCbr : IRequestToCbr
    {
        public ValCurs GetCurs(string dateBegin, string dateEnd, string codeCurs)
        {
            var url =
                $"http://www.cbr.ru/scripts/XML_dynamic.asp?date_req1={dateBegin.Replace('.', '/')}&date_req2={dateEnd.Replace('.', '/')}&VAL_NM_RQ={codeCurs}";
            XmlTextReader reader = new XmlTextReader(url);
            var serializer = new XmlSerializer(typeof(ValCurs));
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding("windows-1251")
            };
            return serializer.Deserialize(reader) as ValCurs;
        }
    }
}
