using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExchangeRates.Model;
using Microsoft.AspNetCore.Mvc;
using RequestToSite;

namespace ExchangeRates.Controllers
{
    [Route("api/CurrencyRate")]
    [ApiController]
    public class CurrencyRateController : ControllerBase
    {
        private readonly IRequestToCbr _requestToCbr;
        private readonly IChartFactory _chartFactory;
        public CurrencyRateController(IRequestToCbr requestToCbr, IChartFactory chartFactory)
        {
            _requestToCbr = requestToCbr;
            _chartFactory = chartFactory;
        }
        [HttpPost]
        public async Task<ActionResult<List<Chart>>> Post([FromBody]SamplingParameters query)
        {
            Dictionary<string, string> currencies = new Dictionary<string, string>
            {
                {"Доллар США","R01235" },
                {"Евро","R01239"}
            };

            Task[] tasks = new Task[2];
            ValCurs valCurs1 = null;
            ValCurs valCurs2 = null;

            tasks[0] = Task.Factory.StartNew(
                () => valCurs1 = _requestToCbr
                    .GetCurs(query.DateBegin, query.DateEnd, currencies["Доллар США"]));
            tasks[1] = Task.Factory.StartNew(
                () => valCurs2 = _requestToCbr
                    .GetCurs(query.DateBegin, query.DateEnd, currencies["Евро"]));
            List<Chart> charts = new List<Chart>();
            await Task.Factory.ContinueWhenAll(tasks, completedTasks =>
            {
                var minVal = valCurs1.Record == null || valCurs2.Record == null
                    ? "50"
                    : valCurs1.Record.Concat(valCurs2.Record).Select(valCursRecord => valCursRecord.Value).Min();
                charts.Add(_chartFactory.GetChart(valCurs1, minVal, currencies.ElementAt(0).Key));
                charts.Add(_chartFactory.GetChart(valCurs2, minVal, currencies.ElementAt(1).Key));
            });
            return new ObjectResult(charts);
        }
    }
}
