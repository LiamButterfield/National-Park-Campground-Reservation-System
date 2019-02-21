using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Capstone.Models;


namespace Capstone.DAL
{
    public class ReservationsSqlDAO : IReservationDAO
    {
        private string connectionString;

        public ReservationsSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public IList<Reservation> GetReservations(Park park)
        {
            List<Reservation> reservations = new List<Reservation>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"select *
                                                    from reservation
                                                    join site on reservation.site_id = site.site_id
                                                    join campground on site.campground_id = campground.campground_id
                                                    where from_date <= @from_date and 
                                                    campground.park_id = @park_id;",conn);

                    cmd.Parameters.AddWithValue("@park_id", park.ID);
                    cmd.Parameters.AddWithValue("@from_date", DateTime.Now.AddDays(30));

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Reservation reservation = ConvertReaderToReservation(reader);
                        reservations.Add(reservation);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error retreiving parks");
                Console.WriteLine(ex.Message);
            }

        }

        private Reservation ConvertReaderToReservation(SqlDataReader reader)
        {
            Reservation reservation = new Reservation();
            reservation.ID = Convert.ToInt32(reader["reservation_id"]);
            reservation.SiteID = Convert.ToInt32(reader["site_id"]);
            reservation.Name = Convert.ToString(reader["name"]);
            reservation.StartDate = Convert.ToDateTime(reader["from_date"]);
            reservation.EndDate = Convert.ToDateTime(reader["to_date"]);
            reservation.CreationDate = Convert.ToDateTime(reader["create_date"]);

            return reservation;
        }

        public int MakeReservation(Reservation newReservation)
        {
            int id = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("insert into reservation values (@name, @from_date, @to_date);", conn);
                    cmd.Parameters.AddWithValue("@name", newReservation.Name);
                    cmd.Parameters.AddWithValue("@from_date", newReservation.StartDate);
                    cmd.Parameters.AddWithValue("@to_date", newReservation.EndDate);                    

                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("select max(reservation_id) from reservation;", conn);

                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }
                
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error making a reservation.");
                Console.WriteLine(ex.Message);
            }
            return id;
        }
    }
}
