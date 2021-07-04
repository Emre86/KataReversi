using System;
using System.Net.Security;

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
                Console.WriteLine("Entrez une lettre entre A et H");
                string inputCharacter = Console.In.ReadLine();
                Console.WriteLine("Entrez un chiffre entre 1 et 8");
                string inputChiffre = Console.In.ReadLine();
                if (string.IsNullOrEmpty(inputCharacter) || inputCharacter.Length > 1 || string.IsNullOrEmpty(inputChiffre) || inputChiffre.Length > 1)
                {
                    continue;
                }

                string character = inputCharacter.ToUpper();
                int chiffre = int.Parse(inputChiffre);
                if (reversi.CheckValidStroke(character, chiffre))
                {
                    reversi.PlayStroke(character, chiffre);
                }
                else
                {
                    
                    Console.WriteLine($"Erreur mouvement non authorisé {character} {chiffre}");
                }


            } while (true);

        }
    }
}