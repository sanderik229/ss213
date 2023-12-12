
internal class Strelki
{

    public static int Pokaz(int minStrelochka, int maxStrelochka)
    {
        int pos = 0;
        ConsoleKeyInfo key;

        do
        {
            Console.SetCursorPosition(0, pos);
            Console.WriteLine("->");

            key = Console.ReadKey();

            Console.SetCursorPosition(0, pos);
            Console.WriteLine("   ");

            if (key.Key == ConsoleKey.UpArrow && pos != minStrelochka)

                pos--;

            else if (key.Key == ConsoleKey.DownArrow && pos != maxStrelochka)

                pos++;
            else if (key.Key == ConsoleKey.Escape)
                return -1;

        } while (key.Key != ConsoleKey.Enter);
        return pos;
    }
}

