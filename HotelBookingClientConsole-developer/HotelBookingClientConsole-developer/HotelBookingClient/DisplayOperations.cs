using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookingClient.HotelClient;
using HotelBookingClient.RoomClient;

namespace HotelBookingClient
{
    public class DisplayOperations
    {
        public static void DisplayHotels(Hotel[] hotels)
        {
            foreach (var hotel in hotels)
            {
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("Hotel Id : {0} ", hotel.Id);
                Console.WriteLine("Hotel Name : {0} ", hotel.Name);
                Console.WriteLine("Hotel Area : {0}", hotel.Area);
                Console.WriteLine("Minimum Rate : {0}", hotel.RateMin);
                Console.WriteLine("Maximum Rate : {0}", hotel.RateMax);
                Console.WriteLine("Ratings : {0}", hotel.Rating);
                Console.WriteLine("Amenities : {0}", hotel.Amenities);
                Console.WriteLine("EmailId : {0}", hotel.Email);
                Console.WriteLine("------------------------------------------------------");
            }
        }

        public static void DisplayRooms(Room[] rooms)
        {
            foreach (Room room in rooms)
            {
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("Room Id: {0}", room.id);
                Console.WriteLine("Room rate: {0}", room.rate);
                Console.WriteLine("Room type: {0}", room.type);
                Console.WriteLine("Room amenities: {0}", room.amenities);
                Console.WriteLine("----------------------------------------------------------");
            }
        }
    }
}

