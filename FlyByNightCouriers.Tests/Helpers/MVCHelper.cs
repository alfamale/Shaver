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
using System.Web;
using System.Web.UI;
using Moq;
using System.Web.Routing;
using System.Security.Principal;

namespace FlyByNight.Tests.Helpers
{
    public class MVCHelper
    {

        public static HtmlTextWriter GetHtmlWriter(StringBuilder builder)
        {
            var stringWriter = new StringWriter(builder);
            var htmlWriter = new HtmlTextWriter(stringWriter);
            return htmlWriter;
        }

        public static RequestContext MockRequestContext()
        {
            return new Mock<RequestContext>().Object;
        }

        public static HttpRequestBase MockHttpRequest()
        {
            return new Mock<HttpRequestBase>().Object;
        }

        public static HttpResponseBase MockHttpResponse()
        {
            return new Mock<HttpResponseBase>().Object;
        }

        public static Mock<HttpContextBase> MockHttpContext(StringBuilder builder)
        {
            var request = MockHttpRequest();
            var response = new Mock<HttpResponseBase>();
            response.SetupGet(r => r.Output).Returns(GetHtmlWriter(builder));

            return MockHttpContext(request, response.Object);
        }

        public static Mock<HttpContextBase> MockHttpContext(HttpRequestBase request, HttpResponseBase response)
        {
            var context = new Mock<HttpContextBase>();
            context.SetupGet(c => c.Request).Returns(request);
            context.SetupGet(c => c.Response).Returns(response);
            return context;
        }

        public static ControllerContext MockControllerContext(StringBuilder builder)
        {
            return new ControllerContext(new Mock<RequestContext>().Object, new Mock<ControllerBase>().Object)
            {
                HttpContext = MockHttpContext(builder).Object
            };
        }

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
    }
}
