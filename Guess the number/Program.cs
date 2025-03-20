using System;

namespace GuessTheNumberGame
{
 
    public interface IRandomNumberGenerator
    {
        int Generate(int min, int max);
    }

   
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        private Random _random = new Random();

        public int Generate(int min, int max)
        {
            return _random.Next(min, max); 
        }
    }

    public interface IGuessingGame
    {
        void Start();
    }

 
    public class GuessingGame : IGuessingGame
    {
        private readonly IRandomNumberGenerator _randomNumberGenerator; 
        private int _numberToGuess;

        public GuessingGame(IRandomNumberGenerator randomNumberGenerator)
        {
            _randomNumberGenerator = randomNumberGenerator;
        }

        public void Start()
        {
            _numberToGuess = _randomNumberGenerator.Generate(1, 100); 
            int guess = 0;
            int attempts = 0;

            Console.WriteLine("Guess the number between 1 and 100!"); 

            while (guess != _numberToGuess) 
            {
                Console.Write("Enter your guess: ");
                guess = Convert.ToInt32(Console.ReadLine()); 
                attempts++;

                if (guess < _numberToGuess)
                {
                    Console.WriteLine("Too low. Try again.");
                }
                else if (guess > _numberToGuess)
                {
                    Console.WriteLine("Too high. Try again.");
                }
            }

            Console.WriteLine($"Congratulations! You guessed the number {_numberToGuess} in {attempts} attempts."); 
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            IRandomNumberGenerator randomNumberGenerator = new RandomNumberGenerator();
            IGuessingGame game = new GuessingGame(randomNumberGenerator);
            game.Start(); 
        }
    }
}
