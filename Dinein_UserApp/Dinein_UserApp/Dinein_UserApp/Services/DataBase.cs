using Dinein_UserApp.Models;
using Firebase.Auth;
using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public async Task<bool> OrderSave(Order order)
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
        public async Task<bool> Register(string email, string name, string password)
        {
            var token = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password, name);
            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {
                return true;
            }
            return false;
        }
    }
}
