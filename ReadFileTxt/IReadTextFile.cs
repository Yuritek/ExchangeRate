using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadWriteFile
{
    public interface IReadTextFile
    {
        Task<IDictionary<string, int>> ParseTextAsync(string filePath, ReadTextFile.ParamSortDic dic);
        string TextFormatting(IDictionary<string, int> dictionary);
    }
}
