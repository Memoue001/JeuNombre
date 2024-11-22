using System;
using System.Collections.Generic;

namespace JeuNombre
{
    internal class Program
    {
        // Variables globales pour le jeu
        static List<int> choixFaits = new List<int>();
        static int bornesMin = 1, bornesMax = 10, nombreAleatoire;
        static Random random = new Random();

        static void Main(string[] args)
        {
            // Menu principal
            while (true)
            {
                Console.WriteLine("\n=== Menu Principal ===");
                Console.WriteLine("1. Étape 1 : Jeu de base");
                Console.WriteLine("2. Étape 2 : Gestion des erreurs et historique");
                Console.WriteLine("3. Étape 3 : Personnalisation avec des bornes et calcul de note");
                Console.WriteLine("4. Quitter");

                Console.Write("Choisissez une option : ");
                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        Etape1();
                        break;
                    case "2":
                        Etape2();
                        break;
                    case "3":
                        Etape3();
                        break;
                    case "4":
                        Console.WriteLine("Au revoir !");
                        return;
                    default:
                        Console.WriteLine("Option invalide, veuillez réessayer.");
                        break;
                }
            }
        }

        static void Etape1()
        {
            Console.WriteLine("\n--- Étape 1 : Jeu de base ---");
            try
            {
                Console.WriteLine("Choisissez un nombre entre 1 et 10 :");
                int choix = int.Parse(Console.ReadLine());

                if (choix < 1 || choix > 10)
                    throw new ArgumentOutOfRangeException("Saisissez un nombre compris entre [1, 10].");

                Console.WriteLine(choix == 5 ? "Vous avez gagné !" : "Vous avez perdu.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur : {ex.Message}");
            }
        }

        static void Etape2()
        {
            Console.WriteLine("\n--- Étape 2 : Gestion des erreurs et historique ---");
            bool jeuEnCours = true;

            while (jeuEnCours)
            {
                try
                {
                    Console.WriteLine("Choisissez un nombre entre 1 et 10 :");
                    int choix = int.Parse(Console.ReadLine());

                    if (choix < 1 || choix > 10)
                        throw new ArgumentOutOfRangeException("Saisissez un nombre compris entre [1, 10].");

                    if (choixFaits.Contains(choix))
                    {
                        Console.WriteLine("Vous avez déjà choisi ce nombre.");
                        continue;
                    }

                    choixFaits.Add(choix);
                    Console.WriteLine(choix == 5 ? "Vous avez gagné !" : "Vous avez perdu.");
                    jeuEnCours = choix == 5;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur : {ex.Message}");
                }

                Console.WriteLine("Vos choix : " + string.Join(", ", choixFaits));
            }
        }

        static void Etape3()
        {
            Console.WriteLine("\n--- Étape 3 : Personnalisation avec des bornes ---");

            List<int> choixFaitsEtape3 = new List<int>(); // Historique spécifique à l'étape 3

            try
            {
                // Définir les bornes
                Console.Write("Saisissez la borne minimale : ");
                bornesMin = int.Parse(Console.ReadLine());

                Console.Write("Saisissez la borne maximale : ");
                bornesMax = int.Parse(Console.ReadLine());

                if (bornesMin >= bornesMax)
                {
                    Console.WriteLine("Erreur : La borne minimale doit être inférieure à la borne maximale.");
                    return;
                }

                // Générer un nombre aléatoire
                nombreAleatoire = random.Next(bornesMin, bornesMax + 1);
                Console.WriteLine($"Un nombre a été généré entre {bornesMin} et {bornesMax}.");

                // Jeu principal
                bool jeuEnCours = true;
                while (jeuEnCours)
                {
                    Console.Write($"Choisissez un nombre entre {bornesMin} et {bornesMax} : ");
                    try
                    {
                        int choix = int.Parse(Console.ReadLine());

                        if (choix < bornesMin || choix > bornesMax)
                            throw new ArgumentOutOfRangeException($"Saisissez un nombre compris entre [{bornesMin}, {bornesMax}].");

                        if (choixFaitsEtape3.Contains(choix))
                        {
                            Console.WriteLine("Vous avez déjà choisi ce nombre.");
                            continue;
                        }

                        choixFaitsEtape3.Add(choix); // Ajouter le choix dans l'historique de l'étape 3
                        if (choix == nombreAleatoire)
                        {
                            Console.WriteLine("Félicitations, vous avez trouvé le bon nombre !");
                            jeuEnCours = false;
                        }
                        else
                        {
                            Console.WriteLine("Ce n'est pas le bon nombre. Essayez encore !");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erreur : {ex.Message}");
                    }

                    // Afficher uniquement les choix de l'étape 3
                    Console.WriteLine("Vos choix pour ce jeu : " + string.Join(", ", choixFaitsEtape3));
                }

                // Calculer la note
                double note = (double)(bornesMax - bornesMin + 1) / choixFaitsEtape3.Count;
                Console.WriteLine($"Votre note est : {note:F2}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur : {ex.Message}");
            }
        }
    }
}
