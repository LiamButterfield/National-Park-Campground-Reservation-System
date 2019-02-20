using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    public class SitesSqlDAO : ISiteDAO
    {
        private string connectionString;

        public SitesSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public IList<Site> GetAvailableSites(int park_id, int? campground_id, DateTime startingDate, DateTime endingDate)
        {
            List<Site> sites = new List<Site>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from park;", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site site = ConvertReaderToSite(reader);
                        sites.Add(site);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error retreiving parks");
                Console.WriteLine(ex.Message);
            }

            return sites;
        }

        private Site ConvertReaderToSite(SqlDataReader reader)
        {
            Site site = new Site();

            site.ID = Convert.ToInt32(reader["site_id"]);
            site.CampgroundID = Convert.ToInt32(reader["campground_id"]);
            site.SiteNumber = Convert.ToInt32(reader["site_number"]);
            site.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
            site.Accessible = Convert.ToBoolean(reader["accessible"]);
            site.MaxRVLength = Convert.ToInt32(reader["max_rv_length"]);
            site.Utilities = Convert.ToBoolean(reader["utilities"]);

            return site;
        }
    }
    
}
