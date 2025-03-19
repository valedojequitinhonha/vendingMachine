namespace vendingMachine1._0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string[] produtos = { "Água", "Coca-Cola", "Chocolate", "Batatas", "Café", "Sandes" };
            double[] precos = { 0.50, 1.00, 1.50, 1.20, 0.80, 2.00 };

            Console.WriteLine("🍔🍬🍰🍟 VENDING MACHINE ☕️🥐🍺🍔");
            ExibirVendingMachine();
            Console.ReadKey();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nUse ⬆️  e ⬇️  para navegar e pressione \u001b[32mEnter/Return\u001b[0m para selecionar:");
            Console.ResetColor();

            (int left, int top) = Console.GetCursorPosition();
            int option = 1;
            const string decorator = "🤑 \u001b[32m";
            bool isSelected = false;

            while (!isSelected)
            {
                Console.SetCursorPosition(left, top);
                ExibirOpcoes(produtos, precos, option, decorator);
                ConsoleKeyInfo key = Console.ReadKey(false);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        option = option == 1 ? produtos.Length + 1 : option - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        option = option == produtos.Length + 1 ? 1 : option + 1;
                        break;

                    case ConsoleKey.Enter:
                        isSelected = true;
                        break;
                }
            }

            Console.WriteLine($"\n{decorator}Você selecionou: {(option == produtos.Length + 1 ? "Sair" : produtos[option - 1])}\u001b[0m");
        }

        static void ExibirOpcoes(string[] produtos, double[] precos, int option, string decorator)
        {
            for (int i = 0; i < produtos.Length; i++)
            {
                Console.WriteLine($"{(option == i + 1 ? decorator : "   ")}{produtos[i]} - {precos[i]:C}\u001b[0m");
            }
            Console.WriteLine($"{(option == produtos.Length + 1 ? decorator : "   ")}Sair\u001b[0m");
        }

        //Exibir Menu!
        static void ExibirVendingMachine()
        {
            Console.WriteLine(@"
 ____________________________________________
|############################################|
|#|                           |##############|
|#|  =====  ..--''`  |~~``|   |##|````````|##|
|#|  |   |  \     |  :    |   |##| Exact  |##|
|#|  |___|   /___ |  | ___|   |##| Change |##|
|#|  /=__\  ./.__\   |/,__\   |##| Only   |##|
|#|  \__//   \__//    \__//   |##|________|##|
|#|===========================|##############|
|#|```````````````````````````|##############|
|#| =.._      +++     //////  |##############|
|#| \/  \     | |     \    \  |#|`````````|##|
|#|  \___\    |_|     /___ /  |#| _______ |##|
|#|  / __\\  /|_|\   // __\   |#| |1|2|3| |##|
|#|  \__//-  \|_//   -\__//   |#| |4|5|6| |##|
|#|===========================|#| |7|8|9| |##|
|#|```````````````````````````|#| ``````` |##|
|#| ..--    ______   .--._.   |#|[=======]|##|
|#| \   \   |    |   |    |   |#|  _   _  |##|
|#|  \___\  : ___:   | ___|   |#| ||| ( ) |##|
|#|  / __\  |/ __\   // __\   |#| |||  `  |##|
|#|  \__//   \__//  /_\__//   |#|  ~      |##|
|#|===========================|#|_________|##|
|#|```````````````````````````|##############|
|############################################|
|#|||||||||||||||||||||||||||||####```````###|
|#||||||||||||PUSH|||||||||||||####\|||||/###|
|############################################|
\\\\\\\\\\\\\\\\\\\\\\///////////////////////
 |________________________________|CX101|___|
            ");
        }


    }

}



