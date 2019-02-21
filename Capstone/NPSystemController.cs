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
        private ParkCampgroundsCLI parkCampgrounds;

        public NPSystemController(IParkDAO parkDAO, ICampgroundDAO campgroundDAO, ISiteDAO siteDAO, IReservationDAO reservationDAO, MainMenuCLI mainMenu, ParkInfoMenuCLI parkInfoMenu, ParkCampgroundsCLI parkCampgrounds)
        {
            this.parkDAO = parkDAO;
            this.campgroundDAO = campgroundDAO;
            this.siteDAO = siteDAO;
            this.reservationDAO = reservationDAO;
            this.mainMenu = mainMenu;
            this.parkInfoMenu = parkInfoMenu;
            this.parkCampgrounds = parkCampgrounds;
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
                    else if (pIInput == 2 || pCInput == 1)
                    {
                        ;
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
