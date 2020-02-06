using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReadWriteFile;

namespace ExchangeRatesTests
{
    [TestClass]
    public class ReadWriteTextFileTests
    {
        private IDictionary<string,int> _dictCount;
        [TestInitialize]
        public void Initialization()
        {
            _dictCount = new Dictionary<string, int> {{"наша", 1}};
        }

        public async Task ReadWriteTextFiles()
        {
            string fromFile = @"C:\Тест1\1.txt";
            string toFile = @"C:\Тест1\2.txt";
            var file = new ReadTextFile();
            IDictionary<string, int> dictionary = await file.ParseTextAsync(fromFile, ReadTextFile.ParamSortDic.ValueDesc);
            var textFormatting=file.TextFormatting(dictionary);
            var writeTextFile= new WriteTextFile();
            await writeTextFile.WriteAsync(toFile, textFormatting);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ParseTextAsync_must_return_error_if_file_path_empty()
        {
           var readTextFile = new ReadTextFile();
           await readTextFile.ParseTextAsync("", ReadTextFile.ParamSortDic.ValueDesc);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Укажите текстовой файл")]
        public async Task ParseTextAsync_must_return_error_if_format_file_not_txt()
        {
            var readTextFile = new ReadTextFile();
            await readTextFile.ParseTextAsync("test.doc", ReadTextFile.ParamSortDic.ValueDesc);
        }

        [TestMethod]
        public async Task ParseTextAsync_must_return_dictionary_number_words_in_the_text()
        {
            var file = new ReadTextFile();
            IDictionary<string, int> dictionary = await file.ParseTextAsync(@"C:\Тест1\1.txt", ReadTextFile.ParamSortDic.ValueDesc);
            Assert.AreEqual(_dictCount.ElementAt(0).Key, dictionary.ElementAt(0).Key);
            Assert.AreEqual(_dictCount.ElementAt(0).Value, dictionary.ElementAt(0).Value);
        }

        [TestMethod]
        public void TextFormatting_must_return_formatted_text()
        {
            var file = new ReadTextFile();
            var result = file.TextFormatting(_dictCount);
            Assert.AreEqual("наша:1",result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Не указаны все параметры")]
        public async Task WriteAsync_must_return_error_if_empty_fileName_and_content()
        {
            var file = new WriteTextFile();
            await file.WriteAsync(string.Empty, string.Empty);
        }
    }
}
