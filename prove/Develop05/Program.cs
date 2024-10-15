using System;
using System.Threading;

namespace MindfulnessProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Mindfulness Program");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        BreathingActivity();
                        break;
                    case "2":
                        ReflectionActivity();
                        break;
                    case "3":
                        ListingActivity();
                        break;
                    case "4":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        // Breathing Activity
        static void BreathingActivity()
        {
            Console.Clear();
            Console.WriteLine("Breathing Activity");
            Console.WriteLine("This activity will help you relax by guiding you through breathing in and out slowly.");
            int duration = GetDuration();

            for (int i = 0; i < duration / 6; i++)
            {
                Console.WriteLine("Breathe in...");
                Pause(3);
                Console.WriteLine("Breathe out...");
                Pause(3);
            }

            EndActivity("Breathing", duration);
        }

        // Reflection Activity
        static void ReflectionActivity()
        {
            Console.Clear();
            Console.WriteLine("Reflection Activity");
            Console.WriteLine("This activity will help you reflect on times when you showed strength and resilience.");
            int duration = GetDuration();

            string[] prompts = {
                "Think of a time when you stood up for someone else.",
                "Think of a time when you did something really difficult.",
                "Think of a time when you helped someone in need.",
                "Think of a time when you did something selfless."
            };

            string[] questions = {
                "Why was this experience meaningful to you?",
                "How did you get started?",
                "How did you feel when it was complete?",
                "What did you learn from this experience?"
            };

            Random random = new Random();
            Console.WriteLine(prompts[random.Next(prompts.Length)]);

            for (int i = 0; i < duration / 6; i++)
            {
                Console.WriteLine(questions[random.Next(questions.Length)]);
                Pause(6);
            }

            EndActivity("Reflection", duration);
        }

        // Listing Activity
        static void ListingActivity()
        {
            Console.Clear();
            Console.WriteLine("Listing Activity");
            Console.WriteLine("This activity will help you list positive things in your life.");
            int duration = GetDuration();

            string[] prompts = {
                "List people you appreciate.",
                "List personal strengths you have.",
                "List times you helped someone recently."
            };

            Random random = new Random();
            Console.WriteLine(prompts[random.Next(prompts.Length)]);

            Console.WriteLine("You have 5 seconds to prepare.");
            Pause(5);

            int itemCount = 0;
            var endTime = DateTime.Now.AddSeconds(duration);
            while (DateTime.Now < endTime)
            {
                Console.Write("Enter an item: ");
                Console.ReadLine();
                itemCount++;
            }

            Console.WriteLine($"You listed {itemCount} items.");
            EndActivity("Listing", duration);
        }

        // Utility function to get duration from user
        static int GetDuration()
        {
            Console.Write("Enter the duration of the activity in seconds: ");
            return int.Parse(Console.ReadLine());
        }

        // Utility function to pause for a few seconds with a spinner
        static void Pause(int seconds)
        {
            for (int i = 0; i < seconds; i++)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }

        // Common ending message for all activities
        static void EndActivity(string activityName, int duration)
        {
            Console.WriteLine($"Good job! You completed the {activityName} activity for {duration} seconds.");
            Pause(3);
        }
    }
}
