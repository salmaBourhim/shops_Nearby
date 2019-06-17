using projectTest.Models;
using projectTest.Session;
using projectTest.ViewModels;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Device.Location;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace projectTest.Controllers
{
    public class ShopController : Controller
    {
        private ShopsNearByDbContext _context = new ShopsNearByDbContext();

        // GET:List of Shops order by distance
        public ActionResult Index()
        {
            var data = _context.shops.ToList().OrderBy(x => x.Name);
            var result = new List<ShopOutputModel>();
            if (string.IsNullOrEmpty(Globals.GetCurrentLocation()))
            {
                foreach (var item in data)
                {
                    result.Add(new ShopOutputModel()
                    {
                        Name = item.Name, Latitude = item.Location.Latitude.Value,
                        Longitude = item.Location.Longitude.Value, ShopId = item.ShopId
                    });
                }

                result = result.OrderBy(x => x.Name).ToList();
            }
            else
            {
                foreach (var item in data)
                {
                    result.Add(new ShopOutputModel()
                    {
                        Name = item.Name, Latitude = item.Location.Latitude.Value,
                        Longitude = item.Location.Longitude.Value, ShopId = item.ShopId,
                        Distance = Calcule(item.Location.Latitude.Value, item.Location.Longitude.Value)
                    });
                }

                result = result.OrderBy(x => x.Distance).ToList();
            }
            TempData["result"] = result;
            return View(result);
        }

        [HttpPost]
        public ActionResult SetLocation(string Longitude, string Latitude)
        {
            try
            {
                Globals.SetCurrentLocation(Longitude + "|" + Latitude);
                return Json(new {status = HttpStatusCode.OK});
            }
            catch
            {
                return Json(new {status = HttpStatusCode.InternalServerError});
            }
        }

        [NonAction]
        private void SetObjects()
        {
            _context.shops.ToList().ForEach(loc => _context.shops.Remove(loc));
            _context.SaveChanges();
            var fshop = _context.shops.Add(new Shops()
            {
                Name = "My First shop",
                Location = DbGeography.FromText("POINT(-122.336106 47.605049)"),
            });
            var sshop = _context.shops.Add(new Shops()
            {
                Name = "My Second shop",
                Location = DbGeography.FromText("POINT(-121.906623 37.428272)"),
            });
            var tshop = _context.shops.Add(new Shops()
            {
                Name = "My third shop",
                Location = DbGeography.FromText("POINT(-121.906624 25.3294781)"),
            });
            var foshop = _context.shops.Add(new Shops()
            {
                Name = "My fourth shop",
                Location = DbGeography.FromText("POINT(-122.906623 27.428272)"),
            });
            _context.SaveChanges();
        }
        //Distance using longitude and latitude
        private double Calcule(double sLatitude, double sLongitude)
        {
            var sCoord = new GeoCoordinate(sLatitude, sLongitude);
            var CurrentLocation = Globals.GetCurrentLocation().Split('|');
            var eCoord = new GeoCoordinate(double.Parse(CurrentLocation[1], CultureInfo.InvariantCulture), double.Parse(CurrentLocation[0], CultureInfo.InvariantCulture));
            return sCoord.GetDistanceTo(eCoord);
        }
        //delete shop from main page to add it in preferred shop
        [HttpPost]
        public ActionResult DeleteFromMainPage(int? id)
        {
            var delshop = _context.shops.Find(id);
            _context.shops.Remove(delshop);
             _context.SaveChanges();
            TempData["id"] = id;
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        //my preferred shop
        public ActionResult Preferredshops()
        {
            var test = TempData["result"];
            var myid = int.Parse(TempData["id"].ToString());
            List<ShopOutputModel> mymodel = (List<ShopOutputModel>)test;
            var list=mymodel.Where(t => t.ShopId == myid).ToList();
            return View(list);
        }
    }
}