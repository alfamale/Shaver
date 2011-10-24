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
using System.Text;
using System.Web.Mvc;
using System.IO;
using APWebGrbNET;
using System.Web.UI;
using Shaver;
using System.Web;

namespace PdfViewEngine
{
    public class PdfView: ShaverView
    {

        internal const string Host = "127.0.0.1";
        internal const long Port = 60034;
        public const string Directory = @"C:\PDF\";

        private IView _view;

        internal string DocId = Guid.NewGuid().ToString() + ".pdf";

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="controllerContext">ControllerContext of the calling Controller - contains ViewData, etc.</param>
        /// <param name="view">Instantiated RazorView</param>
        public PdfView(ControllerContext controllerContext, IView view)
            : base(controllerContext, view)
        {
        }
       
        /// <summary>
        /// Method to perform any setup tasks. First method called from the ctor
        /// </summary>
        /// <param name="controllerContext">ControllerContext of the calling Controller - contains ViewData, etc.</param>
        /// <param name="view">Instantiated RazorView</param>
        protected override void Setup(ControllerContext controllerContext, IView view)
        {
            _view = view;
        }

        /// <summary>
        /// Method to perform tasks on captured HTML
        /// </summary>
        /// <param name="html">HTML to act on - returned from CaptureHtml</param>
        /// <param name="controllerContext">ControllerContext of the calling Controller - contains ViewData, etc.</param>
        /// <param name="view">Instantiated RazorView</param>
        protected override void ActOnHtml(string html, ControllerContext controllerContext, IView view)
        {
            // WebGrabber doesn't like this
            html = html.Replace("<!DOCTYPE html>", string.Empty);

            // create the activePDF WebGrabber object
            var grabber = new APWebGrabber()
            {
                CreateFromHTMLText = html,              // the HTML to convert
                OutputDirectory = Directory,            // the directory the file will be written to
                NewDocumentName = DocId,                // the name of the PDF document
                UserStyleSheetUrl = Css(),              // style the HTML - CSS must be Base64-encoded for some reason and is NOT the URL
                Orientation = 1                         // 0 = portrait, 1 = landscape
            };

            // generate the PDF
            var result = grabber.DoPrint(Host, Port);

            // clean up after ourselves
            grabber.CleanUp(Host, Port);
        }

        /// <summary>
        /// Method to output data to the TextWriter
        /// Render the View - in our case, we just return the name and location of the PDF file we just generated
        /// </summary>
        /// <param name="viewContext">ViewContext for the RazorView</param>
        /// <param name="writer">TextWriter to write output to</param>
        protected override void RenderView(ViewContext viewContext, TextWriter writer)
        {
            writer.Write(Directory + DocId);
        }

        /// <summary>
        /// Get the contents of the CSS file
        /// </summary>
        /// <returns>Base64-encoded CSS - activePDF wierdness here</returns>
        internal string Css()
        {
            const string headers = "data:text/css;charset=utf-8;base64,";
            var base64Css = new StringBuilder(headers);
            using (var reader = new StreamReader(HttpContext.Current.Server.MapPath(@"~/Content/Site.css")))
            {
                var css = Encoding.UTF8.GetBytes(reader.ReadToEnd());
                base64Css.Append(Convert.ToBase64String(css));
            }
            return base64Css.ToString();
        }

    }
}
