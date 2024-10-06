using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        // Load scriptures from a file
        List<Scripture> scriptures = LoadScripturesFromFile("scriptures.txt");

        if (scriptures.Count == 0)
        {
            Console.WriteLine("No scriptures found.");
            return;
        }

        // Randomly select a scripture
        Random random = new Random();
        Scripture selectedScripture = scriptures[random.Next(scriptures.Count)];

        // Clear the console and display the initial scripture
        Console.Clear();
        selectedScripture.Display();

        // Prompt the user to press enter or type "quit"
        Console.WriteLine("\nPress enter to continue or type 'quit' to exit.");

        string input = Console.ReadLine();

        while (input != "quit" && !selectedScripture.AllWordsHidden)
        {
            // Hide a few random words in the scripture
            selectedScripture.HideRandomWords();

            // Clear the console and display the modified scripture
            Console.Clear();
            selectedScripture.Display();

            // Check if all words are hidden
            if (selectedScripture.AllWordsHidden)
                break;

            // Prompt the user again
            Console.WriteLine("\nPress enter to continue or type 'quit' to exit.");
            input = Console.ReadLine();
        }
    }

    static List<Scripture> LoadScripturesFromFile(string filePath)
    {
        List<Scripture> scriptures = new List<Scripture>();

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 2)
                {
                    string reference = parts[0].Trim();
                    string text = parts[1].Trim();
                    scriptures.Add(new Scripture(reference, text));
                }
            }
        }

        return scriptures;
    }
}

class Scripture
{
    private Reference reference;
    private List<Word> words;
    private Random random;

    public bool AllWordsHidden { get { return words.TrueForAll(w => w.Hidden); } }

    public Scripture(string referenceString, string text)
    {
        this.reference = new Reference(referenceString);
        this.words = GetWordsFromText(text);
        this.random = new Random();
    }

    public void Display()
    {
        Console.WriteLine(reference.ToString() + ":");
        foreach (Word word in words)
        {
            if (word.Hidden)
                Console.Write("____ ");
            else
                Console.Write(word.Text + " ");
        }
        Console.WriteLine();
    }

    public void HideRandomWords()
    {
        List<Word> visibleWords = words.FindAll(w => !w.Hidden);
        int index = random.Next(visibleWords.Count);
        visibleWords[index].Hidden = true;
    }

    private List<Word> GetWordsFromText(string text)
    {
        List<Word> words = new List<Word>();
        string[] wordArray = text.Split(' ');

        foreach (string word in wordArray)
        {
            words.Add(new Word(word));
        }

        return words;
    }
}

class Word
{
    public string Text { get; }
    public bool Hidden { get; set; }

    public Word(string text)
    {
        Text = text;
        Hidden = false;
    }
}

class Reference
{
    private string book;
    private int chapter;
    private int startVerse;
    private int endVerse;

    public Reference(string referenceString)
    {
        string[] parts = referenceString.Split(':');
        if (parts.Length == 2)
        {
            string[] bookParts = parts[0].Split(' ');
            if (bookParts.Length > 1)
            {
                book = bookParts[1];
            }

            string[] verseParts = parts[1].Split('-');
            if (verseParts.Length == 1)
            {
                if (int.TryParse(verseParts[0], out int verse))
                {
                    startVerse = verse;
                    endVerse = verse;
                }
            }
            else if (verseParts.Length == 2)
            {
                if (int.TryParse(verseParts[0], out int start) && int.TryParse(verseParts[1], out int end))
                {
                    startVerse = start;
                    endVerse = end;
                }
            }
        }
    }

    public override string ToString()
    {
        string referenceString = "";
        if (!string.IsNullOrEmpty(book))
        {
            referenceString += book + " ";
        }
        referenceString += chapter + ":" + startVerse;
        if (startVerse != endVerse)
        {
            referenceString += "-" + endVerse;
        }
        return referenceString;
    }
}
