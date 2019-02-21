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

        public NPSystemController(IParkDAO parkDAO, ICampgroundDAO campgroundDAO, ISiteDAO siteDAO, IReservationDAO reservationDAO, MainMenuCLI mainMenu, ParkInfoMenuCLI parkInfoMenu)
        {
            this.parkDAO = parkDAO;
            this.campgroundDAO = campgroundDAO;
            this.siteDAO = siteDAO;
            this.reservationDAO = reservationDAO;
            this.mainMenu = mainMenu;
            this.parkInfoMenu = parkInfoMenu;
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

                IList<Campground> campgrounds = campgroundDAO.GetAllCampgroundsByPark(parkID);



            }
        }
    }
}
