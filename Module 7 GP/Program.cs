using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program
{

    // File path for storing the animal dictionary JSON data
    static string filePath = "animals.json";

// Main method to run the console application where users select options to manipulate the animal dictionary
    static void Main()
    {
        Dictionary<string, List<string>> animals = LoadAnimals();

        bool running = true;

        while (running)
        {
            Console.WriteLine("\n--** Animal Dictionary List **--");
            Console.WriteLine("1. Populate Dictionary");
            Console.WriteLine("2. Display Dictionary");
            Console.WriteLine("3. Remove Key");
            Console.WriteLine("4. Add New Key and Value");
            Console.WriteLine("5. Add Value to Existing Key");
            Console.WriteLine("6. Sort Keys");
            Console.WriteLine("7. Exit");
            Console.Write("Enter number choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {

                // Case 1: Populate the dictionary with user input for animal type and breed, then save to JSON file
                case "1":
                    Console.Write("Enter animal type (e.g., Dog): ");
                    string animal = Console.ReadLine();

                    Console.Write("Enter breed: ");
                    string breed = Console.ReadLine();

                    if (!animals.ContainsKey(animal))
                    {
                        animals[animal] = new List<string>();
                    }

                    animals[animal].Add(breed);
                    SaveAnimals(animals);
                    Console.WriteLine($"Added {breed} to {animal} successfully!");
                    break;

                // Case 2: Display the contents of the animal dictionary in a formatted manner
                case "2":
                    Console.WriteLine("\n--** Animal Dictionary **--");

                    foreach (var pair in animals)
                    {
                        Console.WriteLine($"\n{pair.Key}:");

                        foreach (var b in pair.Value)
                        {
                            Console.WriteLine($"  - {b}");
                        }
                    }

                    Console.WriteLine();
                    break;
                // Case 3: Remove an animal type (key) from the dictionary based on user input, then save the updated dictionary to the JSON file
                case "3":
                    Console.Write("Enter animal type to remove: ");
                    string removeKey = Console.ReadLine();

                    if (animals.Remove(removeKey))
                    {
                        SaveAnimals(animals);
                        Console.WriteLine("Removed successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Key not found.");
                    }
                    break;


        // case 4: Add a new animal type (key) and its breed (value) to the dictionary based on user input, then save the updated dictionary to the JSON file
                case "4":
                    Console.Write("Enter new animal type: ");
                    string newAnimal = Console.ReadLine();

                    Console.Write("Enter breed: ");
                    string newBreed = Console.ReadLine();

                    animals[newAnimal] = new List<string> { newBreed };
                    SaveAnimals(animals);
                    Console.WriteLine("Added successfully!");
                    break;


 // case 5: Add a new breed (value) to an existing animal type (key) in the dictionary based on user input, then save the updated dictionary to the JSON file
                case "5":
                    Console.Write("Enter animal type: ");
                    string existingAnimal = Console.ReadLine();

                    if (animals.ContainsKey(existingAnimal))
                    {
                        Console.Write("Enter breed to add: ");
                        string addBreed = Console.ReadLine();
                        animals[existingAnimal].Add(addBreed);
                        SaveAnimals(animals);
                        Console.WriteLine("Breed added!");
                    }
                    else
                    {
                        Console.WriteLine("Animal type not found.");
                    }
                    break;

                case "6":
                    Console.WriteLine("Sorting keys...");
                    var sorted = new SortedDictionary<string, List<string>>(animals);
                    animals = new Dictionary<string, List<string>>(sorted);
                    SaveAnimals(animals);
                    Console.WriteLine("Sorted!");
                    break;

                case "7":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid.");
                    break;
            }
        }
    }

        // Method to load the animal dictionary from a JSON file, or initialize with default values if the file does not exist
    static Dictionary<string, List<string>> LoadAnimals()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            var loadedAnimals = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json);

            if (loadedAnimals != null)
            {
                return loadedAnimals;
            }
        }

        // If file doesn't exist or is empty, return a default dictionary with some animal types and breeds so there is at least something to display when the user selects option 2
        return new Dictionary<string, List<string>>
        {
            ["Dog"] = new List<string> { "Bulldog", "Poodle", "Chihuahua", "Pug" },
            ["Cat"] = new List<string> { "Bengal", "Persian", "Siamese", "Maine Coon" },
            ["Bird"] = new List<string> { "Falcon", "Parrot", "Eagle", "Sparrow" }
        };
    }


// Method to save the animal dictionary to a JSON file with indentation for readability
    static void SaveAnimals(Dictionary<string, List<string>> animals)
    {
        string json = JsonSerializer.Serialize(animals, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(filePath, json);
    }
}