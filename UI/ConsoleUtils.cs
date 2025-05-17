namespace Metamon.UI
{
    public static class ConsoleUtils
    {
        public static void DrawImageAt(string image, int x, int y)
        {
            var lines = image.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(lines[i].TrimEnd('\r'));
            }
        }

        public static void DrawImageAt(string image, int x, int y, float ratio, ConsoleColor colorLeft, ConsoleColor colorRight)
        {
            var lines = image.Split('\n');
            var previousColor = Console.ForegroundColor;

            foreach (var (line, i) in lines.Select((line, i) => (line.TrimEnd('\r'), i)))
            {
                Console.SetCursorPosition(x, y + i);

                int width = line.Length;
                int cutoff = (int)Math.Min(Math.Floor(width * ratio), width);

                string leftPart = line.Substring(0, cutoff);
                string rightPart = line.Substring(cutoff); // rest of the line

                Console.ForegroundColor = colorLeft;
                Console.Write(leftPart);

                Console.ForegroundColor = colorRight;
                Console.Write(rightPart);
            }

            Console.ForegroundColor = previousColor;
        }

        public static void DrawWordWrappedText(string text, int x, int y, int maxWidth, int maxHeight)
        {
            var words = text.Split([' '], StringSplitOptions.None);
            int cursorX = x;
            int cursorY = y;
            int linesDrawn = 0;

            foreach (var word in words)
            {
                // Wrap to next line if word doesn't fit on current line
                if (cursorX - x + word.Length + 1 > maxWidth)
                {
                    cursorX = x;
                    cursorY++;
                    linesDrawn++;

                    if (linesDrawn >= maxHeight)
                        break;
                }

                if (cursorY >= y + maxHeight)
                    break;

                Console.SetCursorPosition(cursorX, cursorY);
                Console.Write(word + " ");
                cursorX += word.Length + 1;
            }
        }

        public static string GenerateBorderedTextBox(string text, int width = 15)
        {
            // Truncate if too long
            if (text.Length > width - 2) text = text[..(width - 2)];

            int padding = width - 2 - text.Length;
            int leftPadding = padding / 2;
            int rightPadding = padding - leftPadding;

            string top = "╔" + new string('═', width - 2) + "╗";
            string middle = "║" + new string(' ', leftPadding) + text + new string(' ', rightPadding) + "║";
            string bottom = "╚" + new string('═', width - 2) + "╝";

            return $"{top}\n{middle}\n{bottom}";
        }
    }
}