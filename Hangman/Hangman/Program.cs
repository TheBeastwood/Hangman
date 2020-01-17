using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Hangman";

            //Variables involving event handling to ensure correct input for difficulty select screen
            string userInput = "";
            int difficulty = -1;
            int difficultyCheck = -1;
            //Variables involving creation of char array holding mystery word letters
            string guessWord = "";
            int maxLevel = 3;
            char[] guessWordArray;
            int randomNumber;
            string[,] storedWordArray = new string[3, 5] { { "FAT", "DOG", "CAT", "CAR", "BOX" },
                                                           { "GLASS", "CLOTH", "SHOWER", "RADIO", "BOOK"},
                                                           { "COMBUSTION", "CONGLOMERATE", "LAWYER", "SICKLE", "FLABERGASTED" } };
            //Game stats variables
            string[,] statusArray = new string[6, 7] { { "____  ", "____  ", "____  ", "____  ", "____  ", "____  ", "____  "},
                                                       { "|  |  ", "|  |  ", "|  |  ", "|  |  ", "|  |  ", "|  |  ", "|  |  "},
                                                       { "|     ", "|  O  ", "|  O  ", "|  O  ", "|  O  ", "|  O  ", "|  O  "},
                                                       { "|     ", "|     ", "|  |  ", "| /|  ", "| /|] ", "| /|] ", "| /|] "},
                                                       { "|     ", "|     ", "|     ", "|     ", "|     ", "| /   ", "| / ] "},
                                                       { "______", "______", "______", "______", "______", "______", "______"} }; 
            char guessLetter;
            int miss = 0;
            int points = 0;
            List<char> triedLetters = new List<char>();

            //Select Difficulty
            do
            {
                Console.WriteLine("Select game difficulty: " + Environment.NewLine);
                if (maxLevel >= 0)
                {
                    Console.WriteLine("(0) Player vs. Player");
                }
                for (int i = 1; i<= maxLevel; i++)
                {
                    Console.WriteLine("(" + i + ") " + "Level " + i);
                }
                userInput = Console.ReadLine();
                Console.Clear();
                //Check correct input
                if (int.TryParse(userInput, out difficultyCheck) && difficultyCheck >= 0 && difficultyCheck <= maxLevel)
                {
                    difficulty = difficultyCheck;
                }
                else
                {
                    Console.WriteLine("Invalid entry. Please select option between 0 and " + maxLevel + Environment.NewLine);
                }
            } while (difficulty == -1);

            //Process word for game (either random word from selected difficulty or custom word)
            if (difficulty >= 1)
            {
                Random random = new Random();
                randomNumber =random.Next(0, 4);
                guessWord = storedWordArray[difficulty - 1, randomNumber];
            }
            else
            {
                Console.WriteLine("Enter a word: ");
                guessWord = Console.ReadLine();
            }
            guessWord = guessWord.ToUpper();
            guessWordArray = guessWord.ToCharArray();

            //Play game
            do
            {
                Console.Clear();
                //Set up Graphic (displayed body parts)
                for (int i = 0; i <= 5; i++)
                {
                    Console.WriteLine(statusArray[i, miss]);
                }
                Console.WriteLine();
                //Set up hidden and shown letters
                foreach (char letter in guessWordArray)
                {
                    if (triedLetters.Contains(letter))
                    {
                        Console.Write(letter);
                    }
                    else
                    {
                        Console.Write("*");
                    }
                }

                //Show tried letters, number of misses, points and ask for letter input
                Console.Write(Environment.NewLine + "Tried Letters: ");
                foreach (char tried in triedLetters)
                {
                    Console.Write(tried + " ");
                }
                Console.WriteLine(Environment.NewLine + "Misses: " + Convert.ToString(miss) + Environment.NewLine + "Points: " + Convert.ToString(points) + Environment.NewLine + "Please enter a letter: ");
                userInput = "";
                userInput = Console.ReadLine();
                userInput = userInput.ToUpper();

                //Verification that input is a single letter/number that hasn't been entered before. Game stats modified.
                if (char.TryParse(userInput, out guessLetter))
                {
                    if (!triedLetters.Contains(guessLetter))
                    {
                        triedLetters.Add(guessLetter);
                        if (guessWordArray.Contains(guessLetter))
                        {
                            foreach (char letter in guessWordArray)
                            {
                                if (letter == guessLetter)
                                {
                                    points = points + 1;
                                }
                            }
                        }
                        else
                        {
                            miss = miss + 1;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Letter has already been guessed." + Environment.NewLine + "Press any key to return to game.");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Please input only a single letter." + Environment.NewLine + "Press any key to return to game.");
                    Console.ReadLine();
                }
            } while (miss < 6 && points < guessWordArray.Length);
            Console.Clear();

            //Conditions of game ending
            if (points == guessWordArray.Length)
            {
                Console.WriteLine("Congratulations! You won!");
            }
            else
            {
                Console.WriteLine("Sorry. You lost.");
            }
            Console.WriteLine(Environment.NewLine + "Final Misses: " + Convert.ToString(miss) + Environment.NewLine + "Final Points: " + Convert.ToString(points) + Environment.NewLine + "Press any key to exit game.");
            Console.ReadLine();
        }
    }
}
