using System.Threading.Tasks;

namespace ReadWriteFile
{
    public interface IWriteTextFile
    {
        Task WriteAsync(string filePath, string contents);
    }
}
