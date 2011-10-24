/*  Copyright Andrew Woodcock, 2011

    This file is part of Shaver.

    Shaver is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Shaver is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public License
    along with Shaver.  If not, see <http://www.gnu.org/licenses/>. 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlyByNight.Models;

namespace FlyByNight.Controllers
{
    public class ParcelsController : Controller
    {
        //
        // GET: /Parcels/

        internal void Setup()
        {
            if (Session["parcels"] == null)
            {
                var parcels = new List<ParcelModel>();
                parcels.Add(new ParcelModel()
                {
                    Sender = "Andrew",
                    SenderAddress = "My House, Cork",
                    Receiver = "Siobhan",
                    ReceiverAddress = "Your House, Cork",
                    Message = "Happy Birthday!",
                    Description = "1 dozen roses",
                    From = "andywoodcock11@gmail.com",
                    Subject = "Fly By Night Couriers: 1 dozen roses",
                    Recipients = new List<string> { "siobhan.m.woodcock@gmail.com", "andywoodcock11@gmail.com" }
                });
                parcels.Add(new ParcelModel()
                {
                    Sender = "Andrew",
                    SenderAddress = "My House, Cork",
                    Receiver = "Siobhan",
                    ReceiverAddress = "Your House, Cork",
                    Message = "Sorry I forgot your Birthday!",
                    Description = "2 dozen roses",
                    From = "andywoodcock11@gmail.com",
                    Subject = "Fly By Night Couriers: 2 dozen roses",
                    Recipients = new List<string> { "siobhan.m.woodcock@gmail.com", "andywoodcock11@gmail.com" }
                });

                Session.Add("parcels", parcels);
                
            }
        }

        internal List<ParcelModel> GetParcels()
        {
            Setup();
            return (List<ParcelModel>)Session["parcels"];
        }

        internal ParcelModel GetParcel(string id)
        {
            var parcels = GetParcels();
            var parcel = parcels.Where(p => p.Parcel == id).FirstOrDefault();
            return parcel;
        }

        internal void AddParcel(ParcelModel parcel)
        {
            Setup();
            parcel.From = "andywoodcock11@gmail.com";
            parcel.Subject = "Fly By Night Couriers: 1 dozen roses";
            parcel.Recipients = new List<string> { "siobhan.m.woodcock@gmail.com", "andywoodcock11@gmail.com" };
            ((List<ParcelModel>)Session["parcels"]).Add(parcel);
        }

        public ActionResult Index()
        {
            var parcels = GetParcels();
            return View(parcels);
        }

        public ActionResult Details(string id)
        {
            var parcel = GetParcel(id);
            var view = View(parcel);
            return view;
        }

        public ActionResult ParcelInvoice(string id)
        {
            var parcel = GetParcel(id);
            var view = View(parcel);
            return ControllerHelper.PdfFileStreamResult(this, view);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ParcelModel parcel)
        {
            AddParcel(parcel);
            return RedirectToAction("ParcelInvoice", new { id = parcel.Parcel });
        }

        public ActionResult Progress(string id)
        {
            var parcel = GetParcel(id);
            var view = View(parcel);
            return view;
        }

    }
}
