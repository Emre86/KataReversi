using System;

namespace Reversi
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start playing!");
            Reversi reversi = new Reversi();
            do
            {
                reversi.DisplayGridReversi();
                Console.WriteLine("Entrez une lettre entre A et H :");
                string inputCharacter = Console.In.ReadLine();
                Console.WriteLine("Entrez un chiffre entre 1 et 8 :");
                string inputDigit = Console.In.ReadLine();
                if (string.IsNullOrEmpty(inputCharacter) || inputCharacter.Length > 1 || string.IsNullOrEmpty(inputDigit) || inputDigit.Length > 1)
                {
                    continue;
                }

                string character = inputCharacter.ToUpper();
                int digit = int.Parse(inputDigit);
                if (reversi.CheckValidStroke(character, digit))
                {
                    reversi.PlayStroke(character, digit);
                }
                else
                {
                    Console.WriteLine($"Erreur mouvement non authorisé {character} {digit}");
                }


            } while (true);

        }
    }
}