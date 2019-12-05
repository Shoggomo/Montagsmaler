using System;
using System.Collections.Generic;

namespace Montagsmaler
{
    /// <summary>
    /// A static class where all possible words to guess are saved.
    /// </summary>
    public static class WordList
    {
        private static readonly Random random = new Random();
        private static readonly List<string> wordList  = new List<string>()
        {
            "Baum",
            "Apfel",
            "Haus",
            "Auto",
            "Elektron",
            "Knüppel",
            "Solarium",
            "Taschenrechner",
            "Kaugummi",
            "Tabak",
            "Relativität",
            "Ohrwurm",
            "Prozessor",
            "Speck",
            "Rührei",
            "Zahnarzt",
            "Fußball",
            "Fluss",
            "Virus"
        };

        /// <summary>
        /// Returns a random word from the wordlist.
        /// </summary>
        /// <returns>A random word</returns>
        public static string GetRandomWord()
        {
            var index = random.Next(wordList.Count);
            return wordList[index];
        }
    }
}
