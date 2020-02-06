using System;
using System.IO;
using System.Threading.Tasks;

namespace ReadWriteFile
{
    public class WriteTextFile: IWriteTextFile
    {
        public async Task WriteAsync(string filePath, string contents)
        {
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(contents))
            {
                throw new ArgumentException("Не указаны все параметры");
            }
            using (StreamWriter writer = new StreamWriter(new FileStream(
                filePath, FileMode.Create, FileAccess.Write,
                FileShare.Read, 4096, true)))
            {
                await writer.WriteAsync(contents);
            }
        }
    }
}
