//v.1
/*
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class RomanNumber
    {
        private const char ZERO_DIGIT = 'N';
        private const String MINUS_SIGN = "-";
        private const String INVALID_DIGIT_MESSAGE = "Parse error. Invalid digit(s):";
        private const String EMPTY_INPUT_MESSAGE = "Empty or NULL input";
        private const String INVALID_STRUCTURE_MESSAGE = "Invalid roman number structure";
        private const String INVALID_DIGIT_FORMAT = "{0} '{1}'";  // pattern 
        private const string INVALID_DIGITS_FORMAT = "Parse error '{0}'. Invalid digit(s): '{1}'";  // placeholders
        private const Func<char,String> 
        public int Value { get; set; }

        public RomanNumber(int value = 0)
        {
            Value = value;
        }
        public override string ToString()
        {
            if (Value == 0)
            {
                return ZERO_DIGIT.ToString();
            }

            Dictionary<int, String> ranges = new()
            {
                { 1000, "M"  },
                { 900,  "CM" },
                { 500,  "D"  },
                { 400,  "CD" },
                { 100,  "C"  },
                { 90,   "XC" },
                { 50,   "L"  },
                { 40,   "XL" },
                { 10,   "X"  },
                { 9,    "IX" },
                { 5,    "V"  },
                { 4,    "IV" },
                { 1,    "I"  },
            };
            StringBuilder result = new();
            int value = Math.Abs(Value);
            foreach (var range in ranges)
            {
                while (value >= range.Key)
                {
                    result.Append(range.Value);
                    value -= range.Key;
                }
            }
            return $"{(Value < 0 ? MINUS_SIGN : String.Empty)}{result}";
        }

        private static int DigitValue(char digit)
        {
            return digit switch      // Визначення 
            {                        // величини
                'I' => 1,            // цифри
                'V' => 5,            // 
                'X' => 10,           // 
                'L' => 50,           // 
                'C' => 100,          // 
                'D' => 500,          // 
                'M' => 1000,         // 
                ZERO_DIGIT => 0,
                _ => throw new ArgumentException(  // $"{INVALID_DIGIT_MESSAGE} '{digit}' ")
                    String.Format(
                        INVALID_DIGIT_FORMAT,
                        INVALID_DIGIT_MESSAGE,
                        digit
                    )
                )
            }; 
        }

        private static void CheckValidityOrThrow(String input)
        {
            #region перевірка числа на недозволені символи
            if (String.IsNullOrEmpty(input))
            {
                throw new ArgumentException(EMPTY_INPUT_MESSAGE);
            }
            int firstDigitIndex = input.StartsWith(MINUS_SIGN) ? 1 : 0;
            List<char> invalidChars = new();
            for (int i = input.Length - 1; i >= firstDigitIndex; i--)
            {
                try 
                { 
                    DigitValue(input[i]); 
                }
                catch 
                { 
                    invalidChars.Add(input[i]); 
                }
            }
            if (invalidChars.Count > 0)
            {
                throw new ArgumentException(
                    // $"'{input}' {INVALID_DIGIT_MESSAGE} '{String.Join(", ", invalidChars.Select(c => $"'{c}'"))}' "
                    String.Format(
                        INVALID_DIGITS_FORMAT,
                        input,
                        String.Join(", ", invalidChars.Select(c => $"'{c}'"))
                    )
                );
                /* Продовжити рефакторинг hardcoded string
                 * Винести до констант роздільник неправильних цифр (у 
                 * повідомленні про помилку парсингу), а також
                 * формат перетворення цифр c => $"'{c}'"
                 
            }
            #endregion
        }

        private static void CheckCompositionOrThrow(String input)
{
    #region перевірка "легальності" - правильності композиції числа
    int maxDigit = 0;
    bool flag = false;
    int firstDigitIndex = input.StartsWith(MINUS_SIGN) ? 1 : 0;
    for (int i = input.Length - 1; i >= firstDigitIndex; i--)
    {
        int current = DigitValue(input[i]);
        if (current > maxDigit)
        {
            maxDigit = current;
        }
        if (current < maxDigit)
        {
            if (flag)
            {
                throw new ArgumentException(INVALID_STRUCTURE_MESSAGE);
            }
            flag = true;
        }
        else
        {
            flag = false;
        }
    }
    #endregion
}

public static RomanNumber Parse(String input)
{
    // попередня обробка - прибирання артефактів введення
    input = input?.Trim()!;

    CheckValidityOrThrow(input);

    CheckCompositionOrThrow(input);

    int firstDigitIndex = input.StartsWith(MINUS_SIGN) ? 1 : 0;
    int result = 0;
    int prev = 0;
    for (int i = input.Length - 1; i >= firstDigitIndex; i--)
    {
        int current = DigitValue(input[i]);
        result += (current < prev) ? -current : current;
        prev = current;
    }

    return new()
    {
        Value = firstDigitIndex == 0 ? result : -result
        // result * (1 - (firstDigitIndex << 1))
        // (firstDigitIndex << 1) == (firstDigitIndex * 2) тільки швидше
    };
}

            /* Правило "читання" римського числа:
             * остання цифра (права) завжди додається 
             * кожна наступна цифра (з права на ліво)
             *  - додається, якщо вона не менша за попередню (праву)
             *     XI = +10 +1, VI = +5 +1
             *  - віднімається, якщо менша
             *     IX = +10 -1,  IV = +5 -1,
             */
/*
public static RomanNumber Parse(string input)
{
    return new()
    {
        Value = input.Length
        // Value = input == "I" ? 1 : 2 
        // Value = input switch
        // {
        //     "I" => 1,
        //     _ => 2
        // }
    };
}
*/
/*
public static RomanNumber Parse(string input) 
{ 
    int value = 0; 
    switch (input) 
    { 
        case "I": value = 1; break; 
        case "II": value = 2; break; 
    } 
    return new() { Value = value }; 
}
*/
/*
private static Dictionary<string, int> romanNumerals = new Dictionary<string, int>() { { "I", 1 }, { "II", 2 } };
public static RomanNumber Parse(string input)
{
    if (romanNumerals.TryGetValue(input, out int value))
    {
        return new RomanNumber { Value = value };
    }
    else
    {
        return new RomanNumber { Value = 0 };
    }
}

}
}
/* Д.З. Провести рефакторинг методу Parse:
* відокремити алгоритм визначення величини цифри в окремий метод,
* внести модифікації у код, забезпечити проходження усіх тестів
* Також додати тестових кейсів для неправильних записів
* чисел як-то IVX, VIX, IIIX, VVIX забезпечивши появу в них
* всіх можливих цифр
*/








//V.2
using System.Text;

namespace App
{
    
    public record RomanNumber
    {
        private const char ZERO_DIGIT = 'N';
        private const char MINUS_SIGN = '-';
        private const char DIGIT_QOUTE = '\'';
        private const string DIGITS_SEPARATOR = ", ";
        private const string INVALID_DIGIT_MESSAGE = "Invalid Roman didgit(s):";
        private const string EMPTY_INPUT_MESSAGE = "NULL or empty input";
        private const string ADD_NULL_MESSAGE = "Cannot Add null object";
        private const string NULL_MESSAGE_PATTERN = "{0}: '{1}'";
        private const string SUM_NULL_MESSAGE = "Invalid Sum() invocation with NULL argument";
        private const string INVALID_DATA_SUM_MESSAGE = "Invalid Sum() invocation with NULL argument: ";
        public int Value { get; set; }
        public RomanNumber(int value = 0)
        {
            Value = value;
        }

        public override string ToString()
        {
            // відобразити значення Value у формі римського числа (в оптимальній формі)
            // головна ідея - послідовно зменшення початкового числа і формування результату
            Dictionary<int, string> parts = new()
            {
                { 1000 , "M"  },
                { 900  , "CM" },
                { 500  , "D"  },
                { 400  , "CD" },
                { 100  , "C"  },
                { 90   , "XC" },
                { 50   , "L"  },
                { 40   , "XL" },
                { 10   , "X"  },
                { 9    , "IX" },
                { 5    , "V"  },
                { 4    , "IV" },
                { 1    , "I"  }
            }; // 1982 - M CM L XXX II

            if (Value == 0) return ZERO_DIGIT.ToString();

            bool isNegative = Value < 0;
            var number = isNegative ? -Value : Value;

            StringBuilder sb = new();

            if (isNegative) sb.Append(MINUS_SIGN);

            foreach (var part in parts)
            {
                while (number >= part.Key)
                {
                    sb.Append(part.Value);
                    number -= part.Key;
                }
            }

            return sb.ToString();
        }

        public static RomanNumber Parse(String input)
        {
            input = input?.Trim()!;

            if (input == ZERO_DIGIT.ToString()) return new();

            CheckValidityOrThrow(input);
            CheckLegalityOrThrow(input);

            int lastDigitIndex = input[0] == MINUS_SIGN ? 1 : 0;

            int prev = 0;
            int current;
            int result = 0;

            for (int i = input.Length - 1; i >= lastDigitIndex; i--)
            {
                current = DigitValue(input[i]);
                result += prev > current ? -current : current;
                prev = current;
            }

            return new() { Value = result * (1 - 2 * lastDigitIndex) };

            /* Правила "читання" римських чисел:
             * Якщо цифра передує
             * більшій цифрі, то вона віднімається (IV, IX) - "I"
             * меньшій або рівні - додається (VI, II, XI)
             * Решту правил ігноруємо - робимо максимально "дружній" інтерфейс
             * 
             * Алгоритм - "заходимо" з правої цифри, її завжди додаємо, запам'ятовуємо та порівнюємо з наступною
             */
        }

        private static int DigitValue(char digit)
        {
            return digit switch
            {
                //ZERO_DIGIT => 0,
                'I' => 1,
                'V' => 5,
                'X' => 10,
                'L' => 50,
                'C' => 100,
                'D' => 500,
                'M' => 1000,
                _ => throw new ArgumentException($"{INVALID_DIGIT_MESSAGE} {DIGIT_QOUTE}{digit}{DIGIT_QOUTE}")
            };
        }

        private static void CheckValidityOrThrow(string input)
        {
            if (string.IsNullOrEmpty(input)) throw new ArgumentException(EMPTY_INPUT_MESSAGE);

            if (input.StartsWith(MINUS_SIGN)) input = input[1..];

            List<char> invalidChars = new();

            foreach(char c in input)
            {
                //if (c == '-') continue;
                try { DigitValue(c); }
                catch { invalidChars.Add(c); }
            }

            if(invalidChars.Count > 0)
            {
                string chars = string.Join(DIGITS_SEPARATOR, invalidChars.Select(c => $"{DIGIT_QOUTE}{c}{DIGIT_QOUTE}"));
                throw new ArgumentException($"{INVALID_DIGIT_MESSAGE} {chars}");
            }
        }

        private static void CheckLegalityOrThrow(string input)
        {
            if (input.Length == 0) return;

            int maxDigit = 0;
            int lessDigitsCount = 0;
            int lastDigitIndex = input.StartsWith(MINUS_SIGN) ? 1 : 0;
            for (int i = input.Length - 1; i >= lastDigitIndex; i--)
            {
                int digitValue = DigitValue(input[i]);
                if (digitValue < maxDigit)
                {
                    lessDigitsCount++;
                    if (lessDigitsCount > 1) throw new ArgumentException(input);
                }
                else
                {
                    maxDigit = digitValue;
                    lessDigitsCount = 0;
                }

            }
        }

        public RomanNumber Add(RomanNumber other)
        {
            if (other is null) throw new ArgumentNullException(string.Format(NULL_MESSAGE_PATTERN, ADD_NULL_MESSAGE, nameof(other)));
            return this with { Value = Value + other.Value };
        }

        public static RomanNumber Sum(params RomanNumber[] numbers)
        {
            if(numbers is null) throw new ArgumentNullException(string.Format(NULL_MESSAGE_PATTERN, SUM_NULL_MESSAGE, nameof(numbers)));
            //if (numbers.Length == 1) throw new ArgumentException(INVALID_DATA_SUM_MESSAGE + numbers.ToString());
            int sum = 0;
            foreach (RomanNumber number in numbers) sum += number.Value;
            return new RomanNumber(sum);
        }
    }
}
