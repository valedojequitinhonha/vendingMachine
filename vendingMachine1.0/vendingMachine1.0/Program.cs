using System;
using System.Collections.Generic;

namespace vendingMachine1._0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string[] produtos = { "Água", "Coca-Cola", "Chocolate", "Batatas", "Café", "Sandes" };
            double[] precos = { 0.50, 1.00, 1.50, 1.20, 0.80, 2.00 };

            ExibirMenuPrincipal(produtos, precos);
        }

        static void ExibirMenuPrincipal(string[] produtos, double[] precos)
        {
            Console.WriteLine("🍔🍬🍰🍟 VENDING MACHINE ☕️🥐🍺🍔");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nUse ⬆️  e ⬇️  para navegar e pressione \u001b[32mEnter/Return\u001b[0m para selecionar:");
            Console.ResetColor();

            int option = 1;
            const string decorador = "🤑 \u001b[32m";
            bool isSelected = false;

            while (!isSelected)
            {
                Console.Clear();
                Console.WriteLine("🍔🍬🍰🍟 VENDING MACHINE ☕️🥐🍺🍔");
                ExibirOpcoes(produtos, precos, option, decorador);
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        option = option == 1 ? produtos.Length + 2 : option - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        option = option == produtos.Length + 2 ? 1 : option + 1;
                        break;

                    case ConsoleKey.Enter:
                        isSelected = true;
                        break;
                }
            }

            if (option == produtos.Length + 1) // Sair
            {
                Console.WriteLine($"{decorador}Você selecionou: Sair\u001b[0m");
            }
            else if (option == produtos.Length + 2) // Admin Restock
            {
                Console.Write("\nDigite o PIN de administrador: ");
                string pin = Console.ReadLine();
                if (pin == "1234")
                {
                    ExibirMenuRestock(ref produtos, ref precos);
                }
                else 
                {
                    Console.WriteLine("PIN incorreto!");
                    Console.ReadKey();
                    ExibirMenuPrincipal(produtos, precos);
                }
            }
            else // Produto selecionado
            {
                string NomeProd = produtos[option - 1];
                double preco = precos[option - 1];

                Console.WriteLine($"\n{decorador}Você selecionou: {NomeProd}\u001b[0m");

                int quantidade;
                while (true)
                {
                    Console.Write("Quantidade: ");
                    if (int.TryParse(Console.ReadLine(), out quantidade) && quantidade > 0)
                        break;
                    Console.WriteLine("Por favor, insira um número válido.");
                }

                double CustoTotal = preco * quantidade;
                Console.WriteLine($"Total: {CustoTotal:C}");

                // Calcular montante inserido
                decimal ValorInserido;
                while (true)
                {
                    Console.Write("Qual valor vai inserir?  ");
                    if (decimal.TryParse(Console.ReadLine(), out ValorInserido) && ValorInserido >= (decimal)CustoTotal)
                        break;
                    Console.WriteLine("O valor inserido deve ser maior ou igual ao total.");
                }

                // Calculate the change (troco)
                decimal Troco = Math.Round(ValorInserido - (decimal)CustoTotal, 2);
                Console.WriteLine($"Troco: {Troco:C}");

                // Get detailed breakdown of the change
                var TrocoTotal = CalcularTroco(Troco);
                Console.WriteLine("Troco detalhado:");
                foreach (var item in TrocoTotal)
                {
                    Console.WriteLine($"{item.Key:C} x {item.Value}");
                }
            }
        }

        static void ExibirOpcoes(string[] produtos, double[] precos, int option, string decorador)
        {
            for (int i = 0; i < produtos.Length; i++)
            {
                Console.WriteLine($"{(option == i + 1 ? decorador : "   ")}{produtos[i]} - {precos[i]:C}\u001b[0m");
            }
            Console.WriteLine($"{(option == produtos.Length + 1 ? decorador : "   ")}Sair\u001b[0m");
            Console.WriteLine($"{(option == produtos.Length + 2 ? decorador : "   ")}Restock (Admin)\u001b[0m");
        }

        static Dictionary<decimal, int> CalcularTroco(decimal Troco)
        {
            var denominacao = new List<decimal> { 0.50m, 0.20m, 0.10m, 0.05m, 0.02m, 0.01m };
            var TrocoTotal = new Dictionary<decimal, int>();

            foreach (var denom in denominacao)
            {
                int contar = (int)(Troco / denom);
                if (contar > 0)
                {
                    TrocoTotal[denom] = contar;
                    Troco = Math.Round(Troco - contar * denom, 2);
                }
            }

            return TrocoTotal;
        }

        static void ExibirMenuRestock(ref string[] produtos, ref double[] precos)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("🔧 Menu de Restock 🔧");
                Console.WriteLine("1. Restock de produtos");
                Console.WriteLine("2. Actualizar preço de produtos");
                Console.WriteLine("3. Adicionar novo produto");
                Console.WriteLine("4. Voltar ao Menu de Produtos");
                Console.Write("Escolha uma opçao: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        RestockProdutos(produtos);
                        break;
                    case "2":
                        ActualizarPrecos(ref precos);
                        break;
                    case "3":
                        AdicionarProd(ref produtos, ref precos);
                        break;
                    case "4":
                        exit = true;
                        ExibirMenuPrincipal(produtos, precos);
                        break;
                    default:
                        Console.WriteLine("Opçao Invalida!");
                        break;
                }
            }
        }

        static void RestockProdutos(string[] produtos)
        {
            Console.WriteLine("A fazer restock de produtos...");
            for (int i = 0; i < produtos.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {produtos[i]}");
            }
            Console.WriteLine("Restock feito com sucesso!");
        }

        static void ActualizarPrecos(ref double[] precos)
        {
            Console.WriteLine("A actualizar preços dos produtos...");
            for (int i = 0; i < precos.Length; i++)
            {
                Console.Write($"Novo preço para o produto {i + 1}: ");
                if (double.TryParse(Console.ReadLine(), out double novoPreco) && novoPreco > 0)
                {
                    precos[i] = novoPreco;
                }
                else
                {
                    Console.WriteLine("Preço inválido. Tente novamente.");
                    i--;
                }
            }
            Console.WriteLine("Preços atualizados com sucesso.");
        }

        static void AdicionarProd(ref string[] produtos, ref double[] precos)
        {
            Console.Write("Nome do novo produto: ");
            string novoProduto = Console.ReadLine();

            Console.Write("Preço do novo produto: ");
            if (double.TryParse(Console.ReadLine(), out double novoPreco) && novoPreco > 0)
            {
                Array.Resize(ref produtos, produtos.Length + 1);
                Array.Resize(ref precos, precos.Length + 1);
                produtos[^1] = novoProduto;
                precos[^1] = novoPreco;
                Console.WriteLine("Produto adicionado com sucesso.");
            }
            else
            {
                Console.WriteLine("Preço inválido. Produto não adicionado.");
            }
        }
    }
}