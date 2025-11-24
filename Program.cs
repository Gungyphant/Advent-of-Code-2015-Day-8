namespace AOC2015Day8
{
    class Program
    {
        static string ToLiteral(string valueTextForCompiler)
        {
            return "\"" + Microsoft.CodeAnalysis.CSharp.SymbolDisplay.FormatLiteral(valueTextForCompiler, false).Replace("\"", "\\\"") + "\"";
        }
        static string PartOne(string data)
        {
            int charsInCode = data.Length - data.Count(Char.IsWhiteSpace);
            int charsInMem = 0;
            foreach (string line in data.Split(Environment.NewLine))
            {
                string realLine = line[1..(line.Length - 1)];
                bool isEscape = false;
                bool isAscii = false;
                foreach (char c in realLine)
                {
                    if (c == '\\' && !isEscape)
                    {
                        isEscape = true;
                    }
                    else
                    {
                        if (isEscape)
                        {
                            isEscape = false;
                            switch (c)
                            {
                                case '\\':
                                    charsInMem++;
                                    break;
                                case '"':
                                    charsInMem++;
                                    break;
                                case 'x':
                                    isAscii = true;
                                    break;
                                default:
                                    throw new Exception(Convert.ToString(c));
                            }
                        }
                        else if (isAscii)
                        {
                            isAscii = false;
                        }
                        else
                        {
                            charsInMem++;
                        }
                    }
                }
            }
            int result = charsInCode - charsInMem;
            return Convert.ToString(result);
        }
        static string PartTwo(string data)
        {
            int charsInCode = data.Length - data.Count(Char.IsWhiteSpace);
            int charsInEncoded = 0;
            foreach (string line in data.Split(Environment.NewLine))
            {
                string encoded = ToLiteral(line);
                charsInEncoded += encoded.Length;
            }
            int result = charsInEncoded - charsInCode;
            return Convert.ToString(result);
        }
        static void Main()
        {
            string file = File.ReadAllText(@"../../../input.txt");
            Console.WriteLine(PartOne(file));
            Console.WriteLine(PartTwo(file));
        }
    }
}