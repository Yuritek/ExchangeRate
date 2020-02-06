namespace RequestToSite
{
    public interface IRequestToCbr
    {
        ValCurs GetCurs(string dateBegin, string dateEnd, string codeCurs);
    }
}
