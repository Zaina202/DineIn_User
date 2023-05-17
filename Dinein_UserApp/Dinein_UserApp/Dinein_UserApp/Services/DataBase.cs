using Dinein_UserApp.Models;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Menu = Dinein_UserApp.Models.Menu;


namespace Dinein_UserApp.Services
{
    public class DataBase
    {
        public static string FirebaseClient = "https://dine-in2-default-rtdb.firebaseio.com/";
        public static string FirebaseSecret = "pVOv2WoG1nNrDAZsbmzV8OPS51oPcgdntCXDqjHK";

        public FirebaseClient fc = new FirebaseClient(FirebaseClient,
        new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(FirebaseSecret) });
        private static readonly string webAPIkey = "\r\nAIzaSyCOwJmK-r_qQ5lKDjcjPZYV6s4WHHW7fH4";
        private readonly FirebaseAuthProvider authProvider;
        public DataBase()
        {
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(webAPIkey));
        }
        public async Task<bool> ReservationModelUpdate(ReservationModel reservation)
        {
            try
            {

                await fc.Child(nameof(ReservationModel)).PutAsync(JsonConvert.SerializeObject(reservation));

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> ReservationModelSave(ReservationModel reservation)
        {
            try
            {
                await fc.Child(nameof(ReservationModel)).PostAsync(JsonConvert.SerializeObject(reservation));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }
        public async Task<bool> OrderSave(BillOrder order)
        {
            try
            {
                await fc.Child(nameof(BillOrder)).PostAsync(JsonConvert.SerializeObject(order));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }
        public async Task<List<Menu>> GetAll()
        {
            var MenuList = new List<Menu>();
            var menu = await fc.Child(nameof(Menu)).OnceAsync<Menu>();
            foreach (var item in menu)
            {
                var Meal = new Menu()
                {
                    Name = item.Object.Name,
                    Price = item.Object.Price,
                    ImageUrl=item.Object.ImageUrl,
                };
                MenuList.Add(Meal);
            }
            return MenuList;
        }
        public async Task<int> GetReservationCountByTime(string Time)
        {
            int count = 0;
            try
            {
                if (Time == null)
                {
                    return 0;
                }
                else
                {
                    var reservations = await fc.Child(nameof(ReservationModel)).OnceAsync<ReservationModel>();
                    foreach (var reservation in reservations)
                    {
                        if (reservation.Object.TimePicker == Time)
                        {
                            count++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving reservation count by time: {ex.Message}");
            }
            return count;
        }
        public async Task<int> GetReservationCountByUserID(string id)
        {
            int count = 0;
            try
            {
                if (id == null)
                {
                    return 0;
                }
                else
                {
                    var reservations = await fc.Child(nameof(ReservationModel)).OnceAsync<ReservationModel>();
                    foreach (var reservation in reservations)
                    {
                        if (reservation.Object.UserId == id)
                        {
                            count++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving reservation count by id: {ex.Message}");
            }
            return count;
        }

    
        public async Task<List<BillOrder>> GetOrderById(string userId)
        {
            try
            {
                var orderQueryResult = await fc.Child("BillOrder")
                    .OnceAsync<BillOrder>();
                return orderQueryResult.Where(el => el.Object.UserId == userId).Select(el => el.Object).ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting the order by ID: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UserSave(Users user)
        {
            try
            {
                await fc.Child(nameof(Users)).PostAsync(JsonConvert.SerializeObject(user));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }
        public async Task<string> SignIn(string email, string password)
        {
            var authLink = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
            if (!string.IsNullOrEmpty(authLink.FirebaseToken))
            {
                Application.Current.Properties["UID"] = authLink.User.LocalId;
                return authLink.FirebaseToken;
            }
            else
            {
                return "";
            }
        }
        public async Task<bool> Register(string email, string name, string password)
        {
            var token = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password, name);
            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {
                Application.Current.Properties["UID"] = token.User.LocalId;

                return true;
            }
            return false;
        }
        public async Task<bool> ResetPassword(string email)
        {
            await authProvider.SendPasswordResetEmailAsync(email);
            return true;

        }
        public async Task<Users> GetUserById(string userId)
        {
            var userQueryResult = await fc.Child("Users")
                    .OrderBy("Id")
                    .EqualTo(userId)
                    .OnceAsync<Users>();

            if (userQueryResult.Any())
            {
                var user = userQueryResult.First().Object;
                return user;
            }
            return null;
        }



        public async Task DeleteOrderAsync(string userId)
        {
            bool response = await App.Current.MainPage.DisplayAlert("Alert", "Do you want to delete this order?", "Yes", "No");

            if (response)
            {
                var toDeleteOrder = await fc
                          .Child("BillOrder")
                          .OnceAsync<Order>();

                foreach (var x in toDeleteOrder)
                {
                    if (x.Object.UserId == userId)
                    {
                        await fc
                            .Child("BillOrder")
                            .Child(x.Key)
                           .DeleteAsync();
                    }
                }
                await App.Current.MainPage.DisplayAlert("Success", "Deletion Succeeded", "Ok");

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Failed", "Delete Failed", "Ok");
            }
        }

        public async Task DeleteReservationAsync(string userId)
        {
            var toDeleteRes = await fc
                      .Child("ReservationModel")
                      .OnceAsync<Order>();

            foreach (var x in toDeleteRes)
            {
                if (x.Object.UserId == userId)
                {
                    await fc
                        .Child("ReservationModel")
                        .Child(x.Key)
                       .DeleteAsync();
                }
            }
        }

        public async Task<ReservationModel> GetCurrentReservation(string userId)
        {
            try
            {
                var reservations = await fc.Child(nameof(ReservationModel)).OrderBy(nameof(ReservationModel.UserId)).EqualTo(userId).OnceAsync<ReservationModel>();

                if (reservations.Any())
                {
                    var latestReservation = reservations.OrderByDescending(r => r.Key).FirstOrDefault();
                    if (latestReservation != null && latestReservation.Object != null)
                    {
                        var reservation = latestReservation.Object;
                        reservation.UserId = latestReservation.Key;
                        reservation.ReservationId = latestReservation.Key;

                        Console.WriteLine($"Retrieved current reservation: {reservation.UserId}, {reservation.ReservationId}, {reservation.TimePicker}, {reservation.NumberOfPeople}");

                        return reservation;
                    }
                }
                return null;
            }
            catch (FirebaseException ex)
            {
                Console.WriteLine($"Firebase Exception: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> HasExistingOrder(string userId)
        {
            try
            {
                var orders = await fc.Child("BillOrder")
                    .OrderBy(nameof(BillOrder.UserId))
                    .EqualTo(userId)
                    .OnceAsync<BillOrder>();

                return orders.Any();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while checking for existing order: {ex.Message}");
                return false;
            }
        }
 
    }
}
