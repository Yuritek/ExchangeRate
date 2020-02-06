namespace RequestToSite
{
    public interface IChartFactory
    {
        Chart GetChart(ValCurs valCurs, string minVal, string title);
    }
}
