using System;

class Program
{
    static void Main()
    {
        // Stretch Challenge: Main game loop to allow replaying
        string playAgain = "yes";
        Random random = new Random();

        while (playAgain.ToLower() == "yes")
        {
            // Generate a random number from 1 to 100
            int magicNumber = random.Next(1, 101);
            int guess = -1; 
            int guessCount = 0; 

            Console.WriteLine("I have picked a magic number between 1 and 100. Try to guess it!");

            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine()); 
                guessCount++;
                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                    Console.WriteLine($"It took you {guessCount} guesses.");
                }
            }
            Console.Write("Do you want to play again (yes/no)? ");
            playAgain = Console.ReadLine();
        }

        Console.WriteLine("Thanks for playing!");
    }
}
