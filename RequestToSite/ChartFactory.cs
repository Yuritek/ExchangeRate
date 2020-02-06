using System.Linq;
using JsonConvert = Newtonsoft.Json.JsonConvert;
namespace RequestToSite
{
    public class ChartFactory : IChartFactory
    {
        public Chart GetChart(ValCurs valCurs, string minVal, string title)
        {
            if (valCurs?.Record != null)
                return new Chart
                {
                    YValues = JsonConvert.SerializeObject(
                        valCurs.Record.Select(valCursRecord => valCursRecord.Value).ToList()
                        ),
                    XLabels = JsonConvert.SerializeObject(
                        valCurs.Record.Select(valCursRecord => valCursRecord.Date).ToList()
                        ),
                    MinYValues = minVal,
                    Title = title
                };
            return new Chart
            {
                YValues = "[\"0,0\"]",
                XLabels = "[]",
                MinYValues = minVal,
                Title = title
            };
        }
    }
}
