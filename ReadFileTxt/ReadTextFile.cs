using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReadWriteFile
{
    public class ReadTextFile:IReadTextFile
    {
        public enum ParamSortDic
        {
            KeyAsc,
            KeyDesc,
            ValueAsc,
            ValueDesc
        }
        private static readonly char[] Separators = { ' ' };
        public async Task<IDictionary<string, int>> ParseTextAsync(string filePath, ParamSortDic dic)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (Path.GetExtension(filePath)!=".txt")
            {
                throw new ArgumentException("Укажите текстовой файл");
            }
            Dictionary<ParamSortDic, Func<IDictionary<string, int>, IOrderedEnumerable<KeyValuePair<string, int>>>> sorting =
                new Dictionary<ParamSortDic, Func<IDictionary<string, int>, IOrderedEnumerable<KeyValuePair<string, int>>>>
                {
                    {ParamSortDic.KeyAsc,word=> word.OrderBy(ws=>ws.Key)},
                    {ParamSortDic.KeyDesc,word=>word.OrderByDescending(ws=>ws.Key)},
                    {ParamSortDic.ValueAsc,word=>word.OrderBy(ws=>ws.Value)},
                    {ParamSortDic.ValueDesc,word=>word.OrderByDescending(ws=>ws.Value)},
                };
            var wordCount = new Dictionary<string, int>();
            object lockObject = new object();
            using (var fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    string line;
                    while ((line = await streamReader.ReadLineAsync()) != null)
                    {
                        var lineModifyLower = Regex
                            .Replace(line, "[^а-яА-я \\dictionary]", "")
                            .ToLower();
                        var words = lineModifyLower
                            .Split(Separators, StringSplitOptions.RemoveEmptyEntries)
                            .Where(ws => ws.Length >= 4);

                        Parallel.ForEach(words, word =>
                        {
                            lock (lockObject)
                            {
                                if (wordCount.ContainsKey(word))
                                {
                                    wordCount[word] = wordCount[word] + 1;
                                }
                                else
                                {
                                    wordCount.Add(word, 1);
                                }
                            }
                        });
                    }
                }
            }
            return sorting[dic](wordCount).ToDictionary(k => k.Key, k => k.Value);
        }
        public string TextFormatting(IDictionary<string, int> dictionary)
        {
            return string.Join("\n", dictionary.Select(x => x.Key + ":" + x.Value).ToArray());
        }
    }
}
