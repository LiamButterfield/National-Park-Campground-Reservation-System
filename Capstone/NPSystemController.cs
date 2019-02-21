using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;
using Capstone.Models;
using Capstone.Menus;

namespace Capstone
{
    public class NPSystemController
    {
        private IParkDAO parkDAO;
        private ICampgroundDAO campgroundDAO;
        private ISiteDAO siteDAO;
        private IReservationDAO reservationDAO;
        private MainMenuCLI mainMenu;
        private ParkInfoMenuCLI parkInfoMenu;
        private ParkCampgroundsMenuCLI parkCampgrounds;
        private ReservationMenuCLI reservationMenu;

        public NPSystemController(IParkDAO parkDAO, ICampgroundDAO campgroundDAO, ISiteDAO siteDAO, IReservationDAO reservationDAO, MainMenuCLI mainMenu, ParkInfoMenuCLI parkInfoMenu, ParkCampgroundsMenuCLI parkCampgrounds, ReservationMenuCLI reservationMenu)
        {
            this.parkDAO = parkDAO;
            this.campgroundDAO = campgroundDAO;
            this.siteDAO = siteDAO;
            this.reservationDAO = reservationDAO;
            this.mainMenu = mainMenu;
            this.parkInfoMenu = parkInfoMenu;
            this.parkCampgrounds = parkCampgrounds;
            this.reservationMenu = reservationMenu;
        }

        public void Run()
        {
            IList<Park> parks = parkDAO.GetAllParks();

            while (true)
            {
                string mMInput = mainMenu.DisplayMenu(parks);

                if(mMInput.ToLower() == "q")
                {
                    break;
                }

                int parkID = int.Parse(mMInput);

                Park userPark = parks[parkID - 1];
                while (true)
                {
                    IList<Campground> campgrounds = campgroundDAO.GetAllCampgroundsByPark(parkID);
                    int pIInput = parkInfoMenu.DisplayMenu(userPark);
                    int pCInput = 0;
                    if (pIInput == 1)
                    {                    
                    pCInput = parkCampgrounds.DisplayMenu(userPark, campgrounds);
                    }
                    if (pIInput == 2 || pCInput == 1)
                    {
                        int? campgroundID = null;
                        DateTime requestedStart = new DateTime(1753, 01, 01);
                        DateTime requestedEnd = new DateTime(1753, 01, 01);
                        var reservationRequest = (campground: campgroundID, from: requestedStart, to: requestedEnd);
                        reservationRequest = reservationMenu.DisplayMenu(userPark, campgrounds);
                        IList<Site> sites = siteDAO.GetAvailableSites(parkID, reservationRequest.campground, reservationRequest.from, reservationRequest.to);
                        int selectedSite = 0;
                        string camperName = "";
                        var camperAndSite = (site: selectedSite, camper: camperName);
                        camperAndSite = reservationMenu.MakeReservation(sites, campgrounds);
                    }
                    else if (pIInput == 3)
                    {
                        break;
                    }
                }
            }
        }
    }
}
