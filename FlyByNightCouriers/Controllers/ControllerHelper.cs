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
using System.Text;
using System.IO;
using System.Web.UI;

namespace FlyByNight.Controllers
{
    public class ControllerHelper
    {

        public static string CaptureViewOutput(Controller controller, ViewResult view)
        {
            var builder = new StringBuilder();
            using (var stringWriter = new StringWriter(builder))
            {
                using (var htmlTextWriter = new HtmlTextWriter(stringWriter))
                {
                    var currentContext = controller.ControllerContext.HttpContext;

                    try
                    {
                        // get the Request and User objects from the current, unchanged context
                        var currentRequest = HttpContext.Current.ApplicationInstance.Context.Request;
                        var currentUser = HttpContext.Current.ApplicationInstance.Context.User;

                        // create our new HttpResponse object containing our HtmlTextWriter
                        var newResponse = new HttpResponse(htmlTextWriter);

                        // create a new HttpContext object using our new Response object and the existing Request and User objects
                        var newContext = new HttpContextWrapper(
                                    new HttpContext(currentRequest, newResponse)
                                    {
                                        User = currentUser
                                    });

                        // swap in our new HttpContext object - output from this controller is now going to our HtmlTextWriter object
                        controller.ControllerContext.HttpContext = newContext;

                        // Run the ViewResult
                        view.ExecuteResult(controller.ControllerContext);

                        // flush the output
                        newResponse.Flush();
                        htmlTextWriter.Flush();
                        stringWriter.Flush();

                    }
                    finally
                    {
                        // and no matter what happens, set the context back!
                        controller.ControllerContext.HttpContext = currentContext;
                    }
                }
            }

            // our StringBuilder object now contains the output of the ViewResult object
            return builder.ToString();

        }

        public static void CaptureViewOutput(Controller controller, ViewResult view, StreamWriter streamWriter)
        {

            var currentContext = controller.ControllerContext.HttpContext;

            try
            {
                // get the Request and User objects from the current, unchanged context
                var currentRequest = HttpContext.Current.ApplicationInstance.Context.Request;
                var currentUser = HttpContext.Current.ApplicationInstance.Context.User;

                // create our new HttpResponse object containing our HtmlTextWriter
                var newResponse = new HttpResponse(streamWriter);

                // create a new HttpContext object using our new Response object and the existing Request and User objects
                var newContext = new HttpContextWrapper(
                            new HttpContext(currentRequest, newResponse)
                            {
                                User = currentUser
                            });

                // swap in our new HttpContext object - output from this controller is now going to our HtmlTextWriter object
                controller.ControllerContext.HttpContext = newContext;

                // Run the ViewResult
                view.ExecuteResult(controller.ControllerContext);

                // flush the output
                newResponse.Flush();

            }
            finally
            {
                // and no matter what happens, set the context back!
                controller.ControllerContext.HttpContext = currentContext;
            }

        }

        /// <summary>
        /// Generate a PDF from the View and Model and return it to the browser as a FileStreamResult object
        /// </summary>
        /// <param name="controller">The Controller</param>
        /// <param name="view">The View complete with initiliazed Model, if any</param>
        /// <returns>PDF document as FileStreamResult</returns>
        public static FileStreamResult PdfFileStreamResult(Controller controller, ViewResult view)
        {
            const string mimeType = "application/pdf";

            using (var stream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(stream))
                {
                    ControllerHelper.CaptureViewOutput(controller, view, streamWriter);
                    streamWriter.Flush();
                    stream.Flush();
                    stream.Seek(0, SeekOrigin.Begin);

                    var streamReader = new StreamReader(stream);
                    var contents = System.IO.File.ReadAllBytes(streamReader.ReadToEnd());
                    var pdfStream = new MemoryStream(contents);

                    return new FileStreamResult(pdfStream, mimeType);
                }
            }

        }

        public static MemoryStream StringToMemoryStream(Encoding encoding, string source)
        {
            var content = encoding.GetBytes(source);
            return new MemoryStream(content);
        }

    }
}