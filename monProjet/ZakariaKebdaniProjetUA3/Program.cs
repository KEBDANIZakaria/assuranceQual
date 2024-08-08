using System;
using System.Collections.Generic;
using System.IO;

namespace ZakariaKebdaniProjetUA3
{
    internal class Program
    {
        static List<Etudiant> etudiants = new List<Etudiant>();
        static List<Cours> cours = new List<Cours>();
        static List<Notes> notes = new List<Notes>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Choisissez parmis les option suivantes:");
                Console.WriteLine("1. Ajouter un étudiant.");
                Console.WriteLine("2. Ajouter un cours.");
                Console.WriteLine("3. Ajouter une note.");
                Console.WriteLine("4. Afficher le relevé de notes d'un étudiant.");
                Console.WriteLine("5. Quitter.");

                int choix = int.Parse(Console.ReadLine());

                switch (choix)
                {
                    case 1:
                        AjouterEtudiant();
                        break;
                    case 2:
                        AjouterCours();
                        break;
                    case 3:
                        AjouterNote();
                        break;
                    case 4:
                        AfficherReleve();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Choix invalide.");
                        break;
                }
            }
        }

        static void AjouterEtudiant()
        {
            Console.Write("Numéro d'étudiant: ");
            int numero = int.Parse(Console.ReadLine());
            Console.Write("Nom: ");
            string nom = Console.ReadLine();
            Console.Write("Prénom: ");
            string prenom = Console.ReadLine();

            etudiants.Add(new Etudiant(numero, nom, prenom));
            EnregistrerEtudiants();
        }

        static void EnregistrerEtudiants()
        {
            foreach (var etudiant in etudiants)
            {
                string fileName = $"etudiant_{etudiant.NumeroEtudiant}.txt";
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.WriteLine(etudiant.ToString());
                }
            }
        }

        static void AjouterCours()
        {
            Console.Write("Numéro du cours: ");
            int numero = int.Parse(Console.ReadLine());
            Console.Write("Code du cours: ");
            string code = Console.ReadLine();
            Console.Write("Titre du cours: ");
            string titre = Console.ReadLine();

            cours.Add(new Cours(numero, code, titre));
        }

        static void AjouterNote()
        {
            Console.Write("Numéro d'étudiant: ");
            int numeroEtudiant = int.Parse(Console.ReadLine());
            Console.Write("Numéro du cours: ");
            int numeroCours = int.Parse(Console.ReadLine());
            Console.Write("Note obtenue: ");
            double valeur = double.Parse(Console.ReadLine());

            notes.Add(new Notes(numeroEtudiant, numeroCours, valeur));
            EnregistrerNotes();
        }

        static void EnregistrerNotes()
        {
            foreach (var etudiant in etudiants)
            {
                string fileName = $"notes_etudiant_{etudiant.NumeroEtudiant}.txt";
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    foreach (var note in notes)
                    {
                        if (note.NumeroEtudiant == etudiant.NumeroEtudiant)
                        {
                            sw.WriteLine(note.ToString());
                        }
                    }
                }
            }
        }

        static void AfficherReleve()
        {
            Console.Write("Numéro d'étudiant: ");
            int numero = int.Parse(Console.ReadLine());
            AfficherReleveDeNotes(numero);
        }

        static void AfficherReleveDeNotes(int numeroEtudiant)
        {
            string fileName = $"notes_etudiant_{numeroEtudiant}.txt";
            if (File.Exists(fileName))
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            else
            {
                Console.WriteLine("Relevé de notes introuvable.");
            }
        }
    }

    public class Etudiant
    {
        public int NumeroEtudiant { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }

        public Etudiant(int numeroEtudiant, string nom, string prenom)
        {
            NumeroEtudiant = numeroEtudiant;
            Nom = nom;
            Prenom = prenom;
        }

        public override string ToString()
        {
            return $"{NumeroEtudiant}: {Prenom} {Nom}";
        }
    }

    public class Cours
    {
        public int NumeroCours { get; set; }
        public string Titre { get; set; }
        public string Code { get; set; }

        public Cours(int numeroCours, string code, string titre)
        {
            NumeroCours = numeroCours;
            Code = code;
            Titre = titre;
        }

        public override string ToString()
        {
            return $"{NumeroCours}: {Titre} - {Code}";
        }
    }

    public class Notes
    {
        public int NumeroEtudiant { get; set; }
        public int NumeroCours { get; set; }
        public double Mark { get; set; }

        public Notes(int numeroEtudiant, int numeroCours, double mark)
        {
            NumeroEtudiant = numeroEtudiant;
            NumeroCours = numeroCours;
            Mark = mark;
        }

        public override string ToString()
        {
            return $"Etudiant {NumeroEtudiant}, Cours {NumeroCours}: {Mark}";
        }
    }
}