using Dinein_UserApp.Models;
using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dinein_UserApp.Services
{
    public class DataBase
    {
        public static string FirebaseClient = ("https://dine-in-54308-default-rtdb.firebaseio.com/");
        public static string FirebaseSecret = "1AO003FSpm2dGZn4321C88RKPu2T6DPnKLfBr1Dg";

        public FirebaseClient fc = new FirebaseClient(FirebaseClient,
        new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(FirebaseSecret) });

        public async Task<bool> Save(ReservationModel reservation)
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
    }
}
