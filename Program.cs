using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Codewars
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string phrase = "How can mirrors be real if our eyes aren't real";
            //string newPhrase = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(phrase);
            //Kata.IsPangram("The quick brown fox jumps over the lazy dog.");
            //Console.WriteLine(Kata.DuplicateEncode("recede"));
            //Console.WriteLine(Kata.IsValidWalk(new string[] { "w", "e", "w", "e", "w", "e", "w", "e", "w", "e", "w", "e" }));
            //Console.WriteLine(Kata.Solution(200));
            //Console.WriteLine(Kata.Likes(new string[] { "Alex", "Jacob", "Mark", "Max"}));
            //Console.WriteLine(Kata.Disemvowel("This website is for losers LOL!"));
            //Console.WriteLine(Kata.DuplicateCount("aabbcde"));
            //Console.WriteLine(Kata.find_it(new[] { 20, 1, -1, 2, -2, 3, 3, 5, 5, 1, 2, 4, 20, 4, -1, -2, 5 }));
            //Console.WriteLine(Kata.MakeComplement("GTATCGATCGATCGATCGATTATATTTTCGACGAGATTTAAATATATATATATACGAGAGAATACAGATAGACAGATTA"));
            //Console.WriteLine(Arge.NbYear(1500000, 0.25, 1000, 2000000));
            //Console.WriteLine(Kata.Number(new List<int[]>() { new[] { 3, 0 }, new[] { 9, 1 }, new[] { 4, 10 }, new[] { 12, 2 }, new[] { 6, 1 }, new[] { 7, 10 } }));
            //Console.WriteLine(Kata.Longest("aretheyhere", "yestheyarehere"));
            //Console.WriteLine(Kata.Solution("I"));
            //Console.WriteLine(Kata.BreakCamelCase("camelCasingTest"));
            //foreach (var element in Kata.UniqueInOrder("AAAABBCCDAABBB"))
            //{
            //    Console.Write(element);
            //}
            //foreach (var element in Kata.DeleteNth(new int[] { 1, 1, 3, 3, 7, 2, 2, 2, 2 }, 3))
            //{
            //    Console.Write(element);
            //}
            //Console.WriteLine(Kata.Order("4of Fo1r pe6ople g3ood th5e the2"));
            //Console.WriteLine(Kata.Rot13("EBG13 rknzcyr."));

            //var posPeaks = Kata.GetPeaks(new int[] { 3, 2, 3, 6, 4, 1, 2, 3, 2, 1, 2, 2, 2, 1 });
            //if (posPeaks["peaks"].Count() == 0)
            //{
            //    Console.WriteLine("No peaks");
            //}
            //else
            //{
            //    Console.Write("pos: ");
            //    foreach (var item in posPeaks["pos"])
            //    {
            //        Console.Write(item + " ");
            //    }
            //    Console.WriteLine();
            //    Console.Write("peak: ");
            //    foreach (var item in posPeaks["peaks"])
            //    {
            //        Console.Write(item + " ");
            //    }
            //}

            //Console.WriteLine(Kata.Rot13Second("Test"));



            //Console.WriteLine(Kata.DecodeBits1("10001"));
            //Console.WriteLine(Kata.Decode(".."));
            // EE
            Console.WriteLine(Kata.Decode(Kata.DecodeBits1("1100000011")));
            // II
            Console.WriteLine(Kata.Decode(Kata.DecodeBits1("111000111000000000111000111")));
            // M
            Console.WriteLine(Kata.Decode(Kata.DecodeBits1("11111100111111")));

        }
    }

    public static class Kata
    {
        public static string DecodeBits1(string bits)
        {
            bits = bits.Trim('0');
            int tik = 0;

            var tiks = bits.Split('0', StringSplitOptions.RemoveEmptyEntries);
            if (tiks.Length > 1)
            {
                int minTik1 = bits.Split('0', StringSplitOptions.RemoveEmptyEntries).Select(group => group.Length).Min();
                int maxTik1 = bits.Split('0', StringSplitOptions.RemoveEmptyEntries).Select(group => group.Length).Max();

                int minTik0 = bits.Split('1', StringSplitOptions.RemoveEmptyEntries).Select(group => group.Length).Min();
                int maxTik0 = bits.Split('1', StringSplitOptions.RemoveEmptyEntries).Select(group => group.Length).Max();

                if (minTik1 == maxTik1 && minTik0 == maxTik0 && minTik1 <= maxTik0 || minTik1 == maxTik1 && minTik1 == minTik0 && minTik1 <= maxTik0)
                {
                    tik = minTik1;
                }
                else if (minTik1 == maxTik1 && minTik0 == maxTik0 && minTik1 > maxTik0)
                {
                    tik = maxTik0;
                }
                else
                {
                    tik = bits.Split('1', StringSplitOptions.RemoveEmptyEntries).Select(group => group.Length).Min();
                }
            }
            else
            {
                //Тик для одной точки
                tik = tiks[0].Length;
            }

            string bitsToDecode = string.Empty;
            for (int i = 0; i < bits.Length; i = i + tik)
            {
                bitsToDecode += bits[i];
            }

            Dictionary<string, string> bitDictionary = new Dictionary<string, string>();
            bitDictionary.Add("1", ".");
            bitDictionary.Add("111", "-");

            var bitWords = bitsToDecode.Split("0000000", StringSplitOptions.RemoveEmptyEntries);
            var bitLetters = bitWords.Select(word => word.Split("000", StringSplitOptions.RemoveEmptyEntries)).ToList();
            string morseCode = string.Empty;

            foreach (var letter in bitLetters)
            {
                foreach (var bit in letter)
                {
                    morseCode += string.Join("", bit.Split('0').Select(letters => bitDictionary[letters])) + " ";
                }
                morseCode += "  ";
            }
            return morseCode.TrimEnd();


            // Best practice
            //var cleanedBits = bits.Trim('0');
            //var rate = GetRate();
            //return cleanedBits
            //  .Replace(GetDelimiter(7, "0"), "   ")
            //  .Replace(GetDelimiter(3, "0"), " ")
            //  .Replace(GetDelimiter(3, "1"), "-")
            //  .Replace(GetDelimiter(1, "1"), ".")
            //  .Replace(GetDelimiter(1, "0"), "");

            //string GetDelimiter(int len, string c) => Enumerable.Range(0, len * rate).Aggregate("", (acc, _) => acc + c);
            //int GetRate() => GetLengths("0").Union(GetLengths("1")).Min();
            //IEnumerable<int> GetLengths(string del) => cleanedBits.Split(del, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Length);

            //public static string DecodeMorse(string morseCode)
            //{
            //    return morseCode
            //      .Split("   ")
            //      .Aggregate("", (res, word) => $"{res}{ConvertWord(word)} ")
            //      .Trim();

            //    string ConvertWord(string word) => word.Split(' ').Aggregate("", (wordRes, c) => wordRes + MorseCode.Get(c));
            //}
        }

        public static string Decode(string morseCode)
        {
            Dictionary<string, string> morseDictionary = new Dictionary<string, string>();
            morseDictionary.Add(".-", "A");
            morseDictionary.Add("-...", "B");
            morseDictionary.Add("-.-.", "C");
            morseDictionary.Add("-..", "D");
            morseDictionary.Add(".", "E");
            morseDictionary.Add("..-.", "F");
            morseDictionary.Add("--.", "G");
            morseDictionary.Add("....", "H");
            morseDictionary.Add("..", "I");
            morseDictionary.Add(".---", "J");
            morseDictionary.Add("-.-", "K");
            morseDictionary.Add(".-..", "L");
            morseDictionary.Add("--", "M");
            morseDictionary.Add("-.", "N");
            morseDictionary.Add("---", "O");
            morseDictionary.Add(".--.", "P");
            morseDictionary.Add("--.-", "Q");
            morseDictionary.Add(".-.", "R");
            morseDictionary.Add("...", "S");
            morseDictionary.Add("-", "T");
            morseDictionary.Add("..-", "U");
            morseDictionary.Add("...-", "V");
            morseDictionary.Add(".--", "W");
            morseDictionary.Add("-..-", "X");
            morseDictionary.Add("-.--", "Y");
            morseDictionary.Add("--..", "Z");
            morseDictionary.Add(".----", "1");
            morseDictionary.Add("..---", "2");
            morseDictionary.Add("...--", "3");
            morseDictionary.Add("....-", "4");
            morseDictionary.Add(".....", "5");
            morseDictionary.Add("-....", "6");
            morseDictionary.Add("--...", "7");
            morseDictionary.Add("---..", "8");
            morseDictionary.Add("----.", "9");
            morseDictionary.Add("-----", "0");
            morseDictionary.Add("...---...", "SOS");

            

            string[] morseCodeWords = morseCode.Split("   ");

            string[] decodedWords = new string[morseCodeWords.Length];

            for (int i = 0; i <  morseCodeWords.Length; i++)
            {
                foreach (var morseLetter in morseCodeWords[i].Split(" "))
                {
                    decodedWords[i] = string.Join("", decodedWords[i], morseDictionary[morseLetter]);
                }
            }

            return string.Join(" ", decodedWords).TrimStart();

            // Best practice
            //var words = morseCode.Trim().Split(new[] { "   " }, StringSplitOptions.None);
            //var translatedWords = words.Select(word => word.Split(' ')).Select(letters => string.Join("", letters.Select(letter => morseDictionary[letter]))).ToList();
            //return string.Join(" ", translatedWords);

            //words.Select(word => word.Split(' ')):
            //words представляет собой коллекцию строк(предположительно, слов в английском языке).
            //.Select(word => word.Split(' ')) разбивает каждую строку(слово) на массив символов, используя пробел как разделитель. Теперь у нас есть коллекция массивов символов, где каждый массив представляет отдельное слово.

            //.Select(letters => string.Join("", letters.Select(letter => morseDictionary[letter]))):
            //letters представляет собой массив символов, представляющий отдельное слово.
            //.Select(letter => morseDictionary[letter]) перебирает каждый символ в слове и заменяет его на соответствующий символ в алфавите Морзе, используя словарь morseDictionary.
            //string.Join("", ...) объединяет символы Морзе обратно в одну строку без пробелов.Теперь у нас есть строка, представляющая слово на алфавите Морзе.

            //.ToList(): Преобразует результат в список строк.
        }

        public static string Rot13Second(string message)
        {
            char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToArray();
            char[] alphabetROT13 = "NOPQRSTUVWXYZABCDEFGHIJKLMnopqrstuvwxyzabcdefghijklm".ToArray();

            return string.Join("", message.Select(ch => char.IsLetter(ch) ? alphabetROT13[Array.IndexOf(alphabet, ch)] : ch));
        }

        public static Dictionary<string, List<int>> GetPeaks(int[] arr)
        {
            Dictionary<string, List<int>> posPeaks = new Dictionary<string, List<int>>();
            posPeaks.Add("pos", new List<int>());
            posPeaks.Add("peaks", new List<int>());

            int pos = 0;
            int peak = 0;

            if (arr.Length > 2)
            {
                for (int i = 1; i < arr.Length - 1; i++)
                {
                    if (arr[i] == arr[i + 1])
                    {
                        pos = i;
                        peak = arr[i];

                        do
                        {
                            i++;
                            if (i >= arr.Length - 1)
                            {
                                if (peak > arr[pos - 1] && peak > arr[i])
                                {
                                    posPeaks["pos"].Add(pos);
                                    posPeaks["peaks"].Add(peak);
                                }

                                break;
                            }
                        }
                        while (arr[i] == arr[i + 1]);

                        if (i < arr.Length - 1 && peak > arr[pos - 1] && peak > arr[i + 1])
                        {
                            posPeaks["pos"].Add(pos);
                            posPeaks["peaks"].Add(peak);
                        }
                    }

                    if (arr[i] > arr[i - 1] && arr[i] > arr[i + 1])
                    {
                        pos = i;
                        peak = arr[i];
                        posPeaks["pos"].Add(pos);
                        posPeaks["peaks"].Add(peak);
                        continue;
                    }
                }
                return posPeaks;
            }
            else
            {
                return posPeaks;
            }

            // Best practice

            //int pos = 0, peaks = 0;

            //Dictionary<string, List<int>> dictionary = new Dictionary<string, List<int>>();
            //dictionary.Add("pos", new List<int>());
            //dictionary.Add("peaks", new List<int>());
            //for (int i = 1; i < arr.Length - 1; i++)
            //{
            //    if (arr[i] > arr[i - 1])
            //    {
            //        pos = i;
            //        peaks = arr[i];
            //    }
            //    if (arr[i] > arr[i + 1] && pos != 0)
            //    {
            //        dictionary["pos"].Add(pos);
            //        dictionary["peaks"].Add(peaks);
            //        pos = 0;
            //        peaks = 0;
            //    }
            //}

            //return dictionary;
        }

        public static string Rot13(string input)
        {
            char[] alphabet = new char[52]
            { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

            char[] alphabetROT13 = new char[52]
            { 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm' };

            return new string(input.Select(ch => char.IsLetter(ch) ? alphabetROT13[Array.IndexOf(alphabet, ch)] : ch).ToArray());

            // Best practice
            //var s1 = "NOPQRSTUVWXYZABCDEFGHIJKLMnopqrstuvwxyzabcdefghijklm";
            //var s2 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            //return string.Join("", input.Select(x => char.IsLetter(x) ? s1[s2.IndexOf(x)] : x));
        }

        public static string Order(string words)
        {
            string[] newString = new string[words.Split(" ", StringSplitOptions.RemoveEmptyEntries).Length];
            int index = 0;

            foreach (string word in words.Split(" ", StringSplitOptions.RemoveEmptyEntries))
            {
                string ch = word.FirstOrDefault(char.IsDigit, default).ToString();
                index = int.Parse(ch) - 1;
                newString[index] = word;
            }

            return string.Join(" ", newString);

            // Best practice
            //if (string.IsNullOrEmpty(words)) return words;
            //return string.Join(" ", words.Split(' ').OrderBy(s => s.ToList().Find(c => char.IsDigit(c))));
        }

        public static int[] DeleteNth(int[] arr, int x)
        {
            List<int> list = new List<int>();

            foreach (int item in arr)
            {
                if (list.Count(element => element == item) < x)
                {
                    list.Add(item);
                }
            }

            // Способ через словарь
            //Dictionary<int, int> values = new Dictionary<int, int>();

            //foreach (int item in arr)
            //{
            //    if (!values.ContainsKey(item))
            //    {
            //        values[item] = 1;
            //    }
            //    else
            //    {
            //        values[item]++;
            //    }

            //    if (values[item] <= x)
            //    {
            //        list.Add(item);
            //    }
            //}

            return list.ToArray();
        }

        public static IEnumerable<T> UniqueInOrder<T>(IEnumerable<T> iterable)
        {
            List<T> newSeq = new List<T>();
            T item = default;
            
            if (iterable.Count() > 0)
            {
                item = iterable.First();
                newSeq.Add(item);
            }

            foreach(var element in iterable)
            {
                if (!element.Equals(item))
                {
                    newSeq.Add(element);
                    item = element;
                }
            }

            return newSeq;
        }

        public static string BreakCamelCase(string str)
        {
            string newString = Regex.Split(str, @"(?<!^)(?=[A-Z])")[0];

            if (Regex.Split(str, @"(?<!^)(?=[A-Z])").Length > 1)
            {
                for (int i = 1; i < Regex.Split(str, @"(?<!^)(?=[A-Z])").Length; i++)
                {
                    newString += " " + Regex.Split(str, @"(?<!^)(?=[A-Z])")[i];
                }
            }

            //Регулярное выражение @"(?<!^)(?=[A-Z])" используется для разбиения строки на слова с учетом camelCase.Давайте разберем его подробно:
            //@ перед строкой указывает, что это строка в формате "символьной строки"(verbatim string literal), что означает, что символы в строке должны интерпретироваться буквально,
            //без необходимости удваивать обратные слэши(как в обычных строках).
            //(?< !^) - Это называется негативным lookbehind(отрицательное утверждение назад).Оно гарантирует, что совпадение не начинается с начала строки.В данном случае ^ обозначает начало строки,
            //и (?< !^) говорит "не совпадать, если это начало строки".
            //(?=[A - Z]) - Это называется позитивным lookahead(положительное утверждение вперед).Оно гарантирует, что совпадение будет происходить только перед заглавной буквой(A-Z).
            //То есть, если перед совпадением стоит заглавная буква, это совпадение будет учтено.
            //Комбинируя все это вместе, регулярное выражение @"(?<!^)(?=[A-Z])" разбивает строку на слова, если они начинаются после заглавной буквы.Это делает разбиение в camelCase,
            //так что каждое слово начинается с заглавной буквы, и они разделяются на отдельные части.


            return new Regex("([A-Z])").Replace(str, " 1");
            //Этот код выполняет разбиение строки str на слова с использованием регулярного выражения. Регулярное выражение([A-Z]) и метод Replace применяются для вставки пробела перед каждой заглавной буквой в строке.
            //Давайте разберем его подробно:
            //new Regex("([A-Z])") - Здесь мы создаем новый объект Regex с указанным регулярным выражением([A - Z]). Это регулярное выражение ищет любую заглавную букву(A - Z) в строке и группирует ее в круглые скобки,
            //чтобы мы могли обратиться к этой букве позже.
            //.Replace(str, " 1") - Мы вызываем метод Replace для объекта Regex, который был создан на предыдущем шаге.Метод Replace выполняет поиск и замену в строке str.В данном случае,
            //мы заменяем каждую найденную заглавную букву на пробел и ту же букву(захваченную в группу 1).
        }

        public static int Solution(string roman)
        {
            int arabianNumber = 0;
            List<int> numbers = new List<int>();

            foreach (char c in roman)
            {
                switch (c)
                {
                    case 'I':
                        numbers.Add(1);
                        break;
                    case 'V':
                        numbers.Add(5);
                        break;
                    case 'X':
                        numbers.Add(10);
                        break;
                    case 'L':
                        numbers.Add(50);
                        break;
                    case 'C':
                        numbers.Add(100);
                        break;
                    case 'D':
                        numbers.Add(500);
                        break;
                    case 'M':
                        numbers.Add(1000);
                        break;
                }
            }

            if (numbers.Count == 1) 
            {
                return numbers[0];
            }

            for (int i = 0; i < numbers.Count - 1; i++)
            {
                if (numbers[i] < numbers[i + 1])
                {
                    arabianNumber += numbers[i + 1] - numbers[i];
                    i++;
                }
                else
                {
                    arabianNumber += numbers[i];
                }
            }

            if (numbers[numbers.Count - 1] <= numbers[numbers.Count - 2])
            {
                arabianNumber += numbers[numbers.Count - 1];
            }
                return arabianNumber;
        }

        public static string Longest(string s1, string s2)
        {
            return new string(string.Concat(s1, s2).Distinct().OrderBy(ch => ch).ToArray());
        }

        public static int Number(List<int[]> peopleListInOut)
        {
            int peopleInBus = 0;

            for (int i = 0; i < peopleListInOut.Count; i++)
            {
                peopleInBus += peopleListInOut[i][0] - peopleListInOut[i][1];
            }

            return peopleInBus;
        }

        public static string ToJadenCase(this string phrase)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(phrase);
        }

        public static bool IsPangram(string str)
        {
            var chars = str.ToLower().Where(char.IsLetter).Distinct();

            if (chars.Count() == 26)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string DuplicateEncode(string word)
        {
            string newWord = string.Empty;

            for (int i = 0; i < word.Length; i++)
            {
                if (word.ToLower().IndexOf(word[i].ToString().ToLower()) == word.ToLower().LastIndexOf(word[i].ToString().ToLower()))
                {
                    newWord += '(';
                }
                else
                {
                    newWord += ')';
                }
            }
           
            return newWord;
        }

        public static bool IsValidWalk(string[] walk)
        {
            int sumSN = 0;
            int sumWE = 0;

            if (walk.Length != 10)
            {
                return false;
            }

            for (int i = 0; i < walk.Length; i++)
            {
                switch(walk[i])
                {
                    case "n":
                        sumSN++;
                        break;
                    case "e":
                        sumWE++;
                        break;
                    case "s":
                        sumSN--;
                        break;
                    case "w":
                        sumWE--;
                        break;
                }
            }

            if (sumSN == 0 && sumWE == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int Solution(int value)
        {
            if (value < 0)
            {
                return 0;
            }
            else
            {
                int sum = 0;

                for (int i = 0; i < value; i++)
                {
                    if (i % 3 == 0)
                    {
                        sum += i;
                    }
                    else if (i % 5 == 0)
                    {
                        sum += i;
                    }
                }
                return sum;
            }
        }

        public static string Likes(string[] name)
        {
            switch(name.Length)
            {
                case 0:
                    return "no one likes this";
                case 1:
                    return $"{name[0]} likes this";
                case 2:
                    return $"{name[0]} and {name[1]} likes this";
                case 3:
                    return $"{name[0]}, {name[1]} and {name[2]} likes this";
                default:
                    return $"{name[0]}, {name[1]} and {name.Length - 2} others likes this";
            }
        }

        public static string Disemvowel(string str)
        {
            return Regex.Replace(str, "[aeiou]", "", RegexOptions.IgnoreCase);
        }

        public static int DuplicateCount(string str)
        {
            return str.ToLower().GroupBy(x => x).Where(x => x.Count() > 1).Count();
        }

        public static int find_it(int[] seq)
        {
            return seq.GroupBy(x => x).Single(g => g.Count() % 2 == 1).Key;

            // Можно и так
            // return seq.GroupBy(num => num).Where(group => group.Count() % 2 != 0).First().Key;
            
            //Просто пример группировки и получения ключей в строку
            //iterable.GroupBy(x => x).Select(selector => selector.Key).ToString();
        }

        public static string MakeComplement(string dna)
        {
            string newDna = string.Empty;
            
            foreach (char ch in dna)
            {
                switch(ch)
                {
                    case 'A':
                        newDna += 'T';
                        break;
                    case 'T':
                        newDna += 'A';
                        break;
                    case 'C':
                        newDna += 'G';
                        break;
                    case 'G':
                        newDna += 'C';
                        break;
                }
            }
            return newDna;
        }
    }

    class Arge
    {
        public static int NbYear(int p0, double percent, int aug, int p)
        {
            //1000 + 1000 * 0.02 + 50 => 1070 inhabitants
            int years = 0;
            percent = percent / 100;

            while ( p0 < p )
            {
                p0 = p0 + (int)(p0 * percent) + aug;
                years++;
            }

            return years;
        }
    }
}