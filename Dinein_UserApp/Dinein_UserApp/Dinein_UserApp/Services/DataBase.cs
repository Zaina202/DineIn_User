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
        public static string FirebaseClient = "https://dine-in-54308-default-rtdb.firebaseio.com/";
        public static string FirebaseSecret = "1AO003FSpm2dGZn4321C88RKPu2T6DPnKLfBr1Dg";

        public FirebaseClient fc = new FirebaseClient(FirebaseClient,
        new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(FirebaseSecret) });
        static string webAPIkey = "\r\nAIzaSyBs4FwBJ8G5xNjnRKdFDYpv_lPuvSVWCyA";
        FirebaseAuthProvider authProvider;
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

        public async Task<bool> OrderSave(List<Order> order)
        {
            try
            {
                await fc.Child(nameof(Order)).PostAsync(JsonConvert.SerializeObject(order));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
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
        public async Task<int> GetTotalPrice()
        {
            var orders = await fc.Child("Order").OnceAsync<Order>();
            int totalPrice = 0;
            foreach (var order in orders)
            {
                totalPrice += order.Object.TotalPrice;
            }
            return totalPrice;
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
        public async Task<Users> GetUser(string userId)
        {
            {
                try
                {
                    var userSnapshot = await fc.Child(nameof(Users)).OnceAsync<Users>();
                    var userFirebaseObject = userSnapshot.FirstOrDefault();
                    if (userFirebaseObject != null)
                    {
                        var user = userFirebaseObject.Object;
                        user.Id = userFirebaseObject.Key;
                        return user;
                    }
                    else
                    {
                        return null;
                    }
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
    }
}
