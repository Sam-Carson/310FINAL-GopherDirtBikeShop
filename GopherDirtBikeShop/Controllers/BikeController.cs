using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GopherDirtBikeShop.Controllers
{
    public class BikeController : ApiController
    {
        BikesModelContainer DBBikes = new BikesModelContainer();
        public class ClientSideBike
        {
            public int BikeID { get; set; }
            public int ComponentID { get; set; }
            public string Type { get; set; }
            public string Brand { get; set; }
            public string Model { get; set; }
            public string Size { get; set; }
            public string Color { get; set; }
            public string Description { get; set; }
        }

        public IEnumerable<object> GetBikes()
        {
            // return bikes
            // return WebApiConfig.Bikes

            var BikeCollection = from bike in DBBikes.Bikes
                                 select new
                                 {
                                     bike.BikeID,
                                     bike.ComponentID,
                                     bike.Type,
                                     bike.Brand,
                                     bike.Model,
                                     bike.Size,
                                     bike.Color,
                                     bike.Description
                                 };
            var BikeData = BikeCollection;

            return BikeCollection.ToList();
        }

        public IHttpActionResult GetBike(int id)
        {
            try
            {
                var findBike = (from searchedBike in DBBikes.Bikes
                               where searchedBike.BikeID == id
                               select new
                               {
                                   searchedBike.BikeID,
                                   searchedBike.Components,
                                   searchedBike.Type,
                                   searchedBike.Brand,
                                   searchedBike.Model,
                                   searchedBike.Size,
                                   searchedBike.Color,
                                   searchedBike.Description
                               }).First();
                return Ok(findBike);
            }
            catch (Exception)
            {
                return NotFound();
            }

        }


        [HttpPost]
        public ClientSideBike Save(ClientSideBike tempBike)
        {
            Bike newBike = new Bike();
            //newBike.BikeID = ClientBike.BikeID;  SQL will make up the ID
            newBike.ComponentID = tempBike.ComponentID;
            newBike.Type = tempBike.Type;
            newBike.Brand = tempBike.Brand;
            newBike.Model = tempBike.Model;
            newBike.Size = tempBike.Size;
            newBike.Color = tempBike.Color;
            newBike.Description = tempBike.Description;
            // navigation property - an object out of another list ie: ComponentID
            var GetComponentObject = (from component in DBBikes.Components
                                      where component.ComponentID == newBike.ComponentID
                                      select component).First();
            newBike.Components = (ICollection<Component>)GetComponentObject; // collection cast necassary due to a 'collection' - multiple properties are being returned
            DBBikes.Bikes.Add(newBike);
            DBBikes.SaveChanges();

            return tempBike;
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var bikeToDelete = (from deletingBike in DBBikes.Bikes
                                    where deletingBike.BikeID == id
                                    select deletingBike).First();

                Bike deleteThisBike = bikeToDelete;
                DBBikes.Bikes.Remove(deleteThisBike);
                DBBikes.SaveChanges();
                HttpResponseMessage goodResponse = new HttpResponseMessage();
                goodResponse.StatusCode = HttpStatusCode.OK;
                return goodResponse;
            }
            catch (Exception)
            {
                HttpResponseMessage badResponse = new HttpResponseMessage();
                badResponse.StatusCode = HttpStatusCode.BadRequest;
                return badResponse;
            }
        }
    }
}
