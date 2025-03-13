// 1 
/*
  using System;
  using System.Collections.Generic;

 class Program
{
    static List<string> animals = new List<string>();
    static void AddAnimals()
    {
        Console.WriteLine("Enter 5 animals:");
        int count = 0;
        while (count < 5)
        {
            Console.Write($"Animal #{count + 1}: ");
            string animal = Console.ReadLine();
            if (!animals.Contains(animal))  
            {
                animals.Add(animal);
                count++;
            }
            else
            {
                Console.WriteLine("This animal has already been added, try a different one.");
            }
        }
    }

    static void RemoveAnimal()
    {
        while (true)
        {
            Console.WriteLine("\nDo you want to remove an animal by index (I) or name (N)?");
            string choice = Console.ReadLine().ToLower();
            if (choice == "i")
            {
                try
                {
                    Console.Write("Enter the animal index (1-5): ");
                    int index = int.Parse(Console.ReadLine()) - 1;
                    if (index >= 0 && index < animals.Count)
                    {
                        string removed = animals[index];
                        animals.RemoveAt(index);
                        Console.WriteLine($"Animal '{removed}' has been removed.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid index.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a valid number.");
                }
            }
            else if (choice == "n")
            {
                Console.Write("Enter the animal name: ");
                string name = Console.ReadLine();
                if (animals.Contains(name))
                {
                    animals.Remove(name);
                    Console.WriteLine($"Animal '{name}' has been removed.");
                    break;
                }
                else
                {
                    Console.WriteLine($"Animal '{name}' not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    }
    static void Main()
    {
        AddAnimals();

        Console.WriteLine("\nYour animals:");
        for (int i = 0; i < animals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {animals[i]}");
        }
        RemoveAnimal();
        Console.WriteLine("\nRemaining animals:");
        for (int i = 0; i < animals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {animals[i]}");
        }
    }
}
*/


/*   2
using System;
using System.Collections.Generic;

public class HelloWorld
{
    public static void Main()
    {
        Dictionary<string, string> countriesAndCapitals = new Dictionary<string, string>
        {
            {"USA", "Washington, D.C."},
            {"Germany", "Berlin"},
            {"France", "Paris"},
            {"Italy", "Rome"},
            {"Japan", "Tokyo"}
        };

        Console.WriteLine("Here are the available countries:");
        int i = 1;
        foreach (var countryName in countriesAndCapitals.Keys) 
        {
            Console.WriteLine($"{i}. {countryName}");
            i++;
        }

        Console.Write("\nEnter the name of a country: ");
        string country = Console.ReadLine();  

        if (countriesAndCapitals.ContainsKey(country))
        {
            Console.WriteLine($"The capital of {country} is {countriesAndCapitals[country]}.");
        }
        else
        {
            Console.WriteLine("Sorry, I don't know the capital of that country.");
        }
    }
}

*/


//3

using System;

public class RandomWords
{
    public static void Main()
    {
        string[] randomWords = new string[10];
        Random rand = new Random();

        for (int i = 0; i < randomWords.Length; i++)
        {
            randomWords[i] = GenerateRandomWord(rand);
            Console.WriteLine(randomWords[i]);
        }

        Console.WriteLine("Enter a word:");
        string userInput = Console.ReadLine();

        for (int i = 0; i < randomWords.Length; i++)
        {
            if (i % 2 == 0)
            {
                randomWords[i] = userInput + randomWords[i];
            }
            else
            {
                randomWords[i] = randomWords[i] + userInput;
            }
        }

        Console.WriteLine("Modified words:");
        foreach (string word in randomWords)
        {
            Console.WriteLine(word);
        }
    }

    static string GenerateRandomWord(Random rand)
    {
        char[] word = new char[5];
        for (int i = 0; i < word.Length; i++)
        {
            word[i] = (char)rand.Next(65, 122);
        }
        return new string(word);
    }
}
