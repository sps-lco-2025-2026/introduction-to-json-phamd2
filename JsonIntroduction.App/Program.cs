using Newtonsoft.Json.Linq;
using System;
using System.Linq.Expressions;
using System.Net;
using System.Text.Json;


static void pokeInfo()
{
    string url = "https://raw.githubusercontent.com/Biuni/PokemonGO-Pokedex/master/pokedex.json";

    WebClient wc = new WebClient();
    string json = wc.DownloadString(url);


    JObject data = JObject.Parse(json);


    JArray pokemon = (JArray)data["pokemon"];

    Console.Write("Enter Pokemon number: ");
    string input = Console.ReadLine();

    bool found = false;

    for (int i = 0; i < pokemon.Count; i++)
    {
        string num = pokemon[i]["num"].ToString();
        string id = pokemon[i]["id"].ToString();

        if (input == num || input == id)
        {
            Console.WriteLine("Name: " + pokemon[i]["name"]);
            Console.WriteLine("Type: " + string.Join(", ", pokemon[i]["type"]));
            Console.WriteLine("Height: " + pokemon[i]["height"]);
            Console.WriteLine("Weight: " + pokemon[i]["weight"]);
            Console.WriteLine("Weaknesses: " + string.Join(", ", pokemon[i]["weaknesses"]));

            found = true;
            break;
        }
    }
    
    if (!found)
    {
        Console.WriteLine("Pokemon not found.");
    }

}


void guessthePokemon()
{
    string json;
    string url = "https://raw.githubusercontent.com/Biuni/PokemonGO-Pokedex/master/pokedex.json";
    
    
    using (var wc = new WebClient())
    {
        json = wc.DownloadString(url);
    }

    using var doc = JsonDocument.Parse(json);
    
    
    JsonElement pokemonList = doc.RootElement.GetProperty("pokemon");
    int totalPokemon = pokemonList.GetArrayLength();
    
    Random rnd = new Random();
    int score = 0;
    bool playing = true;

    Console.WriteLine("Pokemon Guessing Game");
    Console.WriteLine("Type the ID number for the given Pokemon.");

    while (playing)
    {
        
        int index = rnd.Next(totalPokemon);
        JsonElement targetPokemon = pokemonList[index];

        string name = targetPokemon.GetProperty("name").GetString();
        string correctNum = targetPokemon.GetProperty("num").GetString();

        Console.WriteLine($"\nWhat is the Pokedex number for: {name}?");
        Console.Write("Your Guess: ");
        string guess = Console.ReadLine();

        if (guess == correctNum)
        {
            score++;
            Console.WriteLine($"Correct! Score: {score}");
        }
        else
        {
            Console.WriteLine("GAME OVER");
            Console.WriteLine($"YOU GOT IT WRONG! The answer was {correctNum}.");
            Console.WriteLine($"Final Score: {score}");
            playing = false; 
        }
    }
}

guessthePokemon();