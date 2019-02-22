using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using System.Linq;

namespace Capstone.Menus
{
    public class ReservationMenuCLI
    {
        public (int, DateTime, DateTime) DisplayMenu(Park park, IList<Campground> campgrounds)
        {
            int campgroundID = 0;
            DateTime requestedStart = new DateTime(1753, 01, 01);
            DateTime requestedEnd = new DateTime(1753, 01, 01);
            var reservationRequest = (campground: campgroundID, from: requestedStart, to: requestedEnd);

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{park.Name} National Park Campgrounds");
                Console.WriteLine();
                Console.WriteLine("      Name                               Open           Close          Daily Fee");
                foreach (Campground campground in campgrounds)
                {
                    string openingMonth = intToMonth(campground.OpeningMonth);
                    string closingMonth = intToMonth(campground.ClosingMonth);
                    Console.WriteLine($"#{campground.ID,-5}{campground.Name,-35}{openingMonth,-15}{closingMonth,-15}{campground.DailyFee,-15:C2}");
                }
                Console.WriteLine();
                string input = "";
                while (true)
                {
                     Console.Write("Which campground? (enter 0 to search the whole park, enter X to cancel): ");
                     input = Console.ReadLine();
                     int.TryParse(input, out campgroundID);
                     if (input.ToLower() == "x")
                     {
                        break;
                     }
                     else if(campgrounds.Any(c => c.ID == campgroundID))
                     {
                        reservationRequest.campground = int.Parse(input);
                        break;
                     }
                     else if(input == "0")
                     {
                       // Allowing input to equal 0 for full park search.
                        break;
                     }
                     else
                     {
                        Console.WriteLine("That was not a valid input, please try again");
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();                    
                     }
                }
                if (input.ToLower() == "x")
                {
                    break;
                }
                Console.Write("What is the arrival date?: ");
                reservationRequest.from = DateTime.Parse(Console.ReadLine());
                Console.Write("What is the departure date?: ");
                reservationRequest.to = DateTime.Parse(Console.ReadLine());
                break;
            }
            return reservationRequest;
        }

        public (int, string) MakeReservation(IList<Site> sites, IList<Campground> campgrounds)
        {
            int selectedSite = 0;
            string camperName = "";
            var camperAndSite = (site: selectedSite, camper: camperName);

            Console.WriteLine("Results matching your search criteria:");
            Console.WriteLine("Campground Site No. Max Occup. Accessible? RV Len Utility Cost");


            foreach (Site site in sites)
            {
                List<Campground> camps = new List<Campground> (campgrounds.Where(c => c.ID == site.CampgroundID));
                Campground campground = camps[0];

                string accessible = "No";
                if (site.Accessible)
                {
                    accessible = "Yes";
                }

                string rVLength = "N/A";
                if(site.MaxRVLength > 0)
                {
                    rVLength = site.MaxRVLength.ToString();
                }

                string utility = "No";
                if (site.Utilities)
                {
                    accessible = "Yes";
                }

                Console.WriteLine($"{campground.Name}{site.ID}{site.SiteNumber}{site.MaxOccupancy}{accessible}{rVLength}{utility}{campground.DailyFee}");
            }
            while (true)
            {
                Console.Write("Which site should be reserved (enter x to cancel: ");
                string input = Console.ReadLine();
                int.TryParse(input, out selectedSite);
                if (input.ToLower() == "x")
                {
                    break;
                }
                else if (sites.Any(s => s.ID == selectedSite))
                {
                    camperAndSite.site = int.Parse(input);
                }
                else
                {
                    Console.WriteLine("That was not a valid input, please try again");
                    Console.WriteLine("Press enter to continue");
                    Console.ReadLine();                    
                }
            }
            return camperAndSite;
        }

        private string intToMonth(int month)
        {
            switch (month)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
                default:
                    return "How is this possible?";
            }
        }
    }
}
