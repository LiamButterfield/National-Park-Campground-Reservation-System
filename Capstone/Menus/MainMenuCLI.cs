﻿using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.Menus
{
    public class MainMenuCLI
    {
        public string DisplayMenu(IList<Park> parks)
        {
            string input = null;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the National Park Campsite Reservation System");
                Console.WriteLine("**----------------------------------------------------**");
                foreach (Park park in parks)
                {
                    Console.WriteLine($"{park.ID}: {park.Name}");
                }
                Console.WriteLine("Q: Quit");
                Console.Write("Please select a park for Further Details: ");
                input = Console.ReadLine();

                int inputInt = 0;
                int.TryParse(input, out inputInt);

                if (input.ToLower() == "q" || (parks.Count >= inputInt) && (inputInt > 0))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("That was not a valid input, please try again");
                    Console.WriteLine("Press enter to continue");
                    Console.ReadLine();
                    Console.Clear();
                }
            }

            return input;
        }
    }
}