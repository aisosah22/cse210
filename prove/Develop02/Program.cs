using System;
using System.Collections.Generic;
using System.IO;

class Entry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }
}

class Journal
{
    private List<Entry> entries;

    public Journal()
    {
        entries = new List<Entry>();
    }

    public void AddEntry(Entry entry)
    {
        entries.Add(entry);
    }

    public void DisplayEntries()
    {
        Console.WriteLine("Journal Entries:");
        foreach (var entry in entries)
        {
            Console.WriteLine($"Date: {entry.Date}");
            Console.WriteLine($"Prompt: {entry.Prompt}");
            Console.WriteLine($"Response: {entry.Response}");
            Console.WriteLine();
        }
    }

    public void SaveToFile(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (var entry in entries)
            {
                writer.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
            }
        }
        Console.WriteLine("Journal saved successfully!");
    }

    public void LoadFromFile(string fileName)
    {
        entries.Clear();
        string line;
        using (StreamReader reader = new StreamReader(fileName))
        {
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 3)
                {
                    Entry entry = new Entry
                    {
                        Date = parts[0],
                        Prompt = parts[1],
                        Response = parts[2]
                    };
                    entries.Add(entry);
                }
            }
        }
        Console.WriteLine("Journal loaded successfully!");
    }
}

class Program
{
    private static Journal journal;

    static void Main(string[] args)
    {
        journal = new Journal();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");
            Console.WriteLine("Select an option:");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    WriteNewEntry();
                    break;
                case "2":
                    DisplayJournal();
                    break;
                case "3":
                    SaveJournalToFile();
                    break;
                case "4":
                    LoadJournalFromFile();
                    break;
                case "5":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void WriteNewEntry()
    {
        Console.WriteLine("Enter your response:");
        string response = Console.ReadLine();

        Console.WriteLine("Enter the prompt:");
        string prompt = Console.ReadLine();

        string date = DateTime.Now.ToString("yyyy-MM-dd");

        Entry entry = new Entry
        {
            Prompt = prompt,
            Response = response,
            Date = date
        };

        journal.AddEntry(entry);

        Console.WriteLine("Entry added successfully!");
    }

    static void DisplayJournal()
    {
        journal.DisplayEntries();
    }

    static void SaveJournalToFile()
    {
        Console.WriteLine("Enter the filename:");
        string fileName = Console.ReadLine();

        journal.SaveToFile(fileName);
    }

    static void LoadJournalFromFile()
    {
        Console.WriteLine("Enter the filename:");
        string fileName = Console.ReadLine();

        journal.LoadFromFile(fileName);
    }
}
