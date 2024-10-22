using System;
using System.Collections.Generic;
using System.IO;

// Base class for all goals
class Goal
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }

    public Goal(string name, string description, int points)
    {
        Name = name;
        Description = description;
        Points = points;
    }

    public virtual void RecordEvent() { }
    public virtual bool IsComplete() { return false; }
    public virtual string GetDetails() { return $"{Name}: {Description}"; }
}

// Simple goal that can be marked complete
class SimpleGoal : Goal
{
    private bool _isComplete = false;

    public SimpleGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override void RecordEvent()
    {
        _isComplete = true;
    }

    public override bool IsComplete()
    {
        return _isComplete;
    }

    public override string GetDetails()
    {
        return $"{base.GetDetails()} - Complete: {(_isComplete ? "[X]" : "[ ]")} - Points: {Points}";
    }
}

// Eternal goal that is never fully completed
class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override void RecordEvent()
    {
        // No completion, only points awarded
    }

    public override string GetDetails()
    {
        return $"{base.GetDetails()} - Infinite progress - Points per record: {Points}";
    }
}

// Checklist goal with a target and bonus points
class ChecklistGoal : Goal
{
    public int Target { get; set; }
    public int CompletedCount { get; set; }
    public int BonusPoints { get; set; }

    public ChecklistGoal(string name, string description, int points, int target, int bonus)
        : base(name, description, points)
    {
        Target = target;
        BonusPoints = bonus;
        CompletedCount = 0;
    }

    public override void RecordEvent()
    {
        if (CompletedCount < Target)
        {
            CompletedCount++;
        }
    }

    public override bool IsComplete()
    {
        return CompletedCount >= Target;
    }

    public override string GetDetails()
    {
        return $"{base.GetDetails()} - Progress: {CompletedCount}/{Target} - Points: {Points}, Bonus: {BonusPoints}";
    }
}

// Goal manager to handle the list of goals and scoring
class GoalManager
{
    private List<Goal> goals = new List<Goal>();
    private int score = 0;

    public void AddGoal(Goal goal)
    {
        goals.Add(goal);
    }

    public void RecordGoal(int goalIndex)
    {
        if (goalIndex < 0 || goalIndex >= goals.Count)
        {
            Console.WriteLine("Invalid goal index.");
            return;
        }

        Goal goal = goals[goalIndex];
        goal.RecordEvent();

        score += goal.Points;
        if (goal is ChecklistGoal checklistGoal && checklistGoal.IsComplete())
        {
            score += checklistGoal.BonusPoints;
        }
    }

    public void DisplayGoals()
    {
        Console.WriteLine("\nGoals:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i+1}. {goals[i].GetDetails()}");
        }
        Console.WriteLine($"Score: {score}\n");
    }

    public void SaveGoals(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine(score);
            foreach (Goal goal in goals)
            {
                writer.WriteLine($"{goal.GetType().Name}:{goal.Name},{goal.Description},{goal.Points}");
            }
        }
    }

    public void LoadGoals(string filename)
    {
        if (File.Exists(filename))
        {
            string[] lines = File.ReadAllLines(filename);
            score = int.Parse(lines[0]);
            goals.Clear();

            foreach (string line in lines[1..])
            {
                string[] parts = line.Split(":");
                string[] details = parts[1].Split(",");

                switch (parts[0])
                {
                    case "SimpleGoal":
                        goals.Add(new SimpleGoal(details[0], details[1], int.Parse(details[2])));
                        break;
                    case "EternalGoal":
                        goals.Add(new EternalGoal(details[0], details[1], int.Parse(details[2])));
                        break;
                    case "ChecklistGoal":
                        goals.Add(new ChecklistGoal(details[0], details[1], int.Parse(details[2]), 5, 100));  // Adjust as needed
                        break;
                }
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();

        // Example goals
        manager.AddGoal(new SimpleGoal("Run 5k", "Complete a 5k race", 500));
        manager.AddGoal(new EternalGoal("Read Scriptures", "Read daily", 100));
        manager.AddGoal(new ChecklistGoal("Temple Visits", "Visit temple 10 times", 50, 10, 500));

        manager.DisplayGoals();

        // Record some events
        manager.RecordGoal(0);  // Completing 5k race
        manager.RecordGoal(1);  // Reading scriptures once
        manager.DisplayGoals();

        // Save and load example
        manager.SaveGoals("goals.txt");
        manager.LoadGoals("goals.txt");
        manager.DisplayGoals();
    }
}
