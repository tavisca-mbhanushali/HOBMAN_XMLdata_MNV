using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookingClient.BookingClient;
using HotelBookingClient.HotelClient;
using HotelBookingClient.RoomClient;

namespace HotelBookingClient
{
    class Program
    {
        static void Main(string[] args)
        {
            int menuChoice;
            do
            {
                Console.Write("1.Book room\n2.Cancle booking\n3.Exit\nEnter your choice: ");
                menuChoice = int.Parse(Console.ReadLine());
                switch (menuChoice)
                {
                    case 1:
                        HotelClient.HotelClient client = new HotelClient.HotelClient();

                        HotelFilter filterHotel = new HotelFilter();

                        Console.Write("Enter City Name: ");
                        string cityname = Console.ReadLine();
                        filterHotel.cityName = cityname;
                        HotelStatus status = client.GetHotels(filterHotel);

                        /* display hotels */
                        DisplayOperations.DisplayHotels(status.hotels);

                        /* hotel search with filter */
                        HotelSearch(client, status, filterHotel);

                        /* room search */
                        RoomFilter roomFilter = new RoomFilter();
                        Console.WriteLine("Enter hotel id to book room: ");
                        roomFilter.hotelId = Console.ReadLine();
                        roomFilter.cityName = cityname;
                        RoomClient.RoomClient roomClient = new RoomClient.RoomClient();

                        RoomStatus roomStatus = roomClient.GetRooms(roomFilter);
                        DisplayOperations.DisplayRooms(roomStatus.rooms);

                        Console.Write("Do you Want To Add Filters to room (Y/N):");
                        string choiceRoomFilter = Console.ReadLine();
                        if (choiceRoomFilter.ToLower() == "y")
                        {


                            int choiceOfFilter = 0;
                            do
                            {
                                Console.WriteLine("Room Filters :-");
                                Console.WriteLine("1. Rates");
                                Console.WriteLine("2. Amenities");
                                Console.WriteLine("3. See result");
                                Console.WriteLine("4. Book room");
                                Console.Write("Enter Your Choice");

                                choiceOfFilter = int.Parse((Console.ReadLine()));
                                switch (choiceOfFilter)
                                {
                                    case 1:
                                        Console.WriteLine("Enter Your Minimum rate");
                                        roomFilter.rate = Console.ReadLine();
                                        break;
                                    case 2:
                                        Console.WriteLine("Enter Your amenities");
                                        string[] amenities = Console.ReadLine().Split(',');
                                        roomFilter.amenities = amenities;
                                        break;
                                    case 3:
                                        roomStatus = roomClient.GetRooms(roomFilter);
                                        DisplayOperations.DisplayRooms(roomStatus.rooms);
                                        break;
                                    case 4:
                                        BookRoom(cityname);
                                        break;
                                    default:
                                        Console.WriteLine("\nInvalid choice");
                                        break;
                                }
                            } while (choiceOfFilter != 4);
                        }
                        else
                        {
                            BookRoom(cityname);
                        }

                        Console.ReadLine();
                        break;
                    case 2:
                        BookingDeatils bookingDeatils = new BookingDeatils();
                        Console.Write("Enter booking id to cancle booking: ");
                        bookingDeatils.BookingId = Console.ReadLine();
                        BookingServiceClient bookingServiceClient = new BookingServiceClient();
                        BookingStatus bookingStatus =  bookingServiceClient.CancleBooking(bookingDeatils);

                        Console.WriteLine("\n{0}",bookingStatus.errorMessage);

                        break;
                    case 3:
                        Console.WriteLine("\nThank you come again.");
                        break;
                    default:
                        Console.WriteLine("\nInvalid choice !!!");
                        break;
                }
            } while (menuChoice != 3);
        }

        private static void HotelSearch(HotelClient.HotelClient client, HotelStatus status, HotelFilter filterHotel)
        {
            Console.Write("Do you Want To Add Filters Y/N: ");
            string choice = Console.ReadLine();
            if (choice.ToLower() == "y")
            {
                int choiceOfFilter = 0;
                do
                {
                    Console.WriteLine("\nHotel Filters :-");
                    Console.WriteLine("1.Filter By Minmum rate :");
                    Console.WriteLine("2.Filter By rating :");
                    Console.WriteLine("3.Filter By Amenities :");
                    Console.WriteLine("4.Search Hotel");

                    Console.Write("Enter Your Choice: ");
                    choiceOfFilter = int.Parse((Console.ReadLine()));
                    switch (choiceOfFilter)
                    {
                        case 1:
                            Console.Write("Enter Your Minimum rate");
                            filterHotel.minRate = Console.ReadLine();
                            break;
                        case 2:
                            Console.Write("Enter Your rating");
                            filterHotel.rating = Console.ReadLine();
                            break;
                        case 3:
                            Console.Write("Enter Your amenities");
                            string[] amenities = Console.ReadLine().Split(',');
                            filterHotel.amenities = amenities;
                            break;
                        case 4:
                            status = client.GetHotels(filterHotel);
                            DisplayOperations.DisplayHotels(status.hotels);
                            break;
                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    }
                } while (choiceOfFilter != 4);
            }
        }


        private static void BookRoom(string cityname)
        {
            BookingDeatils bookingDeatils = new BookingDeatils();

            Console.Write("Enter room id to book room: ");
            bookingDeatils.RoomId = Console.ReadLine();
            Console.Write("Enter check in date (DD/MM/YYYY) :");
            bookingDeatils.CheckInDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter check out date (DD/MM/YYYY): ");
            bookingDeatils.CheckOutDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter number of guests: ");
            bookingDeatils.numberOfGuests = int.Parse(Console.ReadLine());
            Console.Write("Enter number of rooms to book: ");
            bookingDeatils.numberOfRooms = int.Parse(Console.ReadLine());
            bookingDeatils.City = cityname;

            Customer customer = new Customer();
            Console.Write("Enter first name: ");
            customer.firstName = Console.ReadLine();
            Console.Write("Enter last name: ");
            customer.lastName = Console.ReadLine();
            Console.Write("Enter email id: ");
            customer.emailId = Console.ReadLine();
            Console.Write("Enter phone number: ");
            customer.phoneNumber = Console.ReadLine();

            bookingDeatils.customer = customer;

            BookingServiceClient bookingServiceClient = new BookingServiceClient();

            BookingStatus bookingStatus = bookingServiceClient.BookRoom(bookingDeatils);
            if (bookingStatus.bookingDetails.BookingId != null)
                Console.WriteLine("Booking succesful\nBooking id : {0}", bookingStatus.bookingDetails.BookingId);
            else
                Console.WriteLine("Booking failed due to {0}", bookingStatus.errorMessage);
        }
    }
}
