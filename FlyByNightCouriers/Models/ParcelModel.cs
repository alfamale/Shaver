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
using System.ComponentModel.DataAnnotations;
using MailerViewEngine;

namespace FlyByNight.Models
{
    public class ParcelModel: IMail
    {

        private string _parcel = Guid.NewGuid().ToString();
        public string Parcel { get { return _parcel; } }

        private double _cost = 9.95;
        public double Cost { get { return _cost; } }

        [Required]
        public string Sender { get; set; }
        [Required]
        public string SenderAddress { get; set; }

        [Required]
        public string Receiver { get; set; }
        [Required]
        public string ReceiverAddress { get; set; }

        public string Message { get; set; }
        public string Description { get; set; }

        private DateTime _deliveryDate = DateTime.Now.AddDays(2);
        public DateTime DeliveryDate { get { return _deliveryDate; } }

        public string From { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public List<string> Recipients { get; set; }

        public List<string> Attachments { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

    }
}