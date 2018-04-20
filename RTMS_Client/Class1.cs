using RTMS_API;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTMS_Client
{
    public class Class1
    {
        public static void Test()
        {
            RTMS_API.Container context = new Container(new Uri("http://localhost:31934/"));

            context.AddToSensorCodes(new SensorCode
            {

            });


            DateTime trev = DateTime.Now.AddYears(-10);
            List<WifiActivity> everything = context.WifiActivities.ToList();
            List<WifiActivity> query = context.WifiActivities.Where(x => x.mac == "b4:ce:d0:38:a8:d2").ToList();

        }
    }
}
