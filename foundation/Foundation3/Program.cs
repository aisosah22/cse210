using System;
using System.Collections.Generic;

// Base Activity class
abstract class Activity
{
    protected DateTime Date;
    protected int Minutes;

    public Activity(DateTime date, int minutes)
    {
        Date = date;
        Minutes = minutes;
    }

    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    public virtual string GetSummary()
    {
        return $"{Date:dd MMM yyyy} {GetType().Name} ({Minutes} min) - " +
               $"Distance: {GetDistance():0.0} km, " +
               $"Speed: {GetSpeed():0.0} kph, " +
               $"Pace: {GetPace():0.0} min per km";
    }
}

// Running class
class Running : Activity
{
    private double Distance;

    public Running(DateTime date, int minutes, double distance) : base(date, minutes)
    {
        Distance = distance;
    }

    public override double GetDistance() => Distance;

    public override double GetSpeed() => (Distance / Minutes) * 60;

    public override double GetPace() => Minutes / Distance;
}

// Cycling class
class Cycling : Activity
{
    private double Speed;

    public Cycling(DateTime date, int minutes, double speed) : base(date, minutes)
    {
        Speed = speed;
    }

    public override double GetDistance() => (Speed * Minutes) / 60;

    public override double GetSpeed() => Speed;

    public override double GetPace() => 60 / Speed;
}

// Swimming class
class Swimming : Activity
{
    private int Laps;
    private const double LapDistanceKm = 50.0 / 1000; // 50 meters in kilometers

    public Swimming(DateTime date, int minutes, int laps) : base(date, minutes)
    {
        Laps = laps;
    }

    public override double GetDistance() => Laps * LapDistanceKm;

    public override double GetSpeed() => (GetDistance() / Minutes) * 60;

    public override double GetPace() => Minutes / GetDistance();
}

// Program to create and display activity summaries
class Program
{
    static void Main()
    {
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2022, 11, 3), 30, 4.8),       // Running 4.8 km in 30 min
            new Cycling(new DateTime(2022, 11, 3), 40, 15.0),      // Cycling at 15 kph for 40 min
            new Swimming(new DateTime(2022, 11, 3), 25, 20)        // Swimming 20 laps in 25 min
        };

        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
