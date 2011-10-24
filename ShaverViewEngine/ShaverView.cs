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
using System.Web.UI;

namespace Shaver
{
    public abstract class ShaverView: IView
    {

        /// <summary>
        /// Calls the following abstract methods in order: Setup, CaptureHtml and ActOnHtml
        /// </summary>
        /// <param name="controllerContext">ControllerContext of the calling Controller - contains ViewData, etc.</param>
        /// <param name="view">Instantiated RazorView</param>
        public ShaverView(ControllerContext controllerContext, IView view)
        {
            Setup(controllerContext, view);
            var html = CaptureHtml(controllerContext, view);
            ActOnHtml(html, controllerContext, view);
        }

        /// <summary>
        /// An abstract method to perform any setup tasks. First method called from the ctor
        /// </summary>
        /// <param name="controllerContext">ControllerContext of the calling Controller - contains ViewData, etc.</param>
        /// <param name="view">Instantiated RazorView</param>
        protected abstract void Setup(ControllerContext controllerContext, IView view);

        /// <summary>
        /// Abstract method to capture and return HTML from the ControllerContext and RazorView
        /// </summary>
        /// <param name="controllerContext">ControllerContext of the calling Controller - contains ViewData, etc.</param>
        /// <param name="view">Instantiated RazorView</param>
        /// <returns>HTML</returns>
        protected virtual string CaptureHtml(ControllerContext controllerContext, IView view)
        {
            var builder = new StringBuilder();
            using (var stringWriter = new StringWriter(builder))
            {
                using (var htmlTextWriter = new HtmlTextWriter(stringWriter))
                {

                    // build our view context - we need this to be able to render the view and capture the output
                    var viewContext = new ViewContext(
                        controllerContext,                          // the ControllerContext we are in
                        view,                                       // the IView object - created by the RazorViewEngine above
                        GetViewData(controllerContext),             // ViewData contains the model - without this model data would not be rendered
                        GetTempData(controllerContext),             // TempData contains the Controller's TempData            
                        htmlTextWriter);                            // the TextWriter the output is being written to - we control this!

                    // now render the view - because we own htmlTextWriter, we can now capture all the rendered output
                    view.Render(viewContext, htmlTextWriter);

                    // don't forget to flush!
                    htmlTextWriter.Flush();
                    stringWriter.Flush();
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// Abstract method to perform tasks on captured HTML
        /// </summary>
        /// <param name="html">HTML to act on - returned from CaptureHtml</param>
        /// <param name="controllerContext">ControllerContext of the calling Controller - contains ViewData, etc.</param>
        /// <param name="view">Instantiated RazorView</param>
        protected abstract void ActOnHtml(string html, ControllerContext controllerContext, IView view);

        /// <summary>
        /// Calls the abstract method RenderView passing the ViewContext and the TextWriter
        /// </summary>
        /// <param name="viewContext">ViewContext for the RazorView</param>
        /// <param name="writer">TextWriter to write output to</param>
        public void Render(ViewContext viewContext, TextWriter writer)
        {
            RenderView(viewContext, writer);
        }

        /// <summary>
        /// Abstract method to output data to the TextWriter
        /// </summary>
        /// <param name="viewContext">ViewContext for the RazorView</param>
        /// <param name="writer">TextWriter to write output to</param>
        protected abstract void RenderView(ViewContext viewContext, TextWriter writer);

        /// <summary>
        /// Extract the ViewData from the ControllerContext
        /// </summary>
        /// <param name="controllerContext">Controller Context</param>
        /// <returns>View Data</returns>
        protected static ViewDataDictionary GetViewData(ControllerContext controllerContext)
        {
            return controllerContext.Controller.ViewData;
        }

        /// <summary>
        /// Extract the ViewData from the ControllerContext
        /// </summary>
        /// <param name="controllerContext">Controller Context</param>
        /// <returns>View Data</returns>
        protected static TempDataDictionary GetTempData(ControllerContext controllerContext)
        {
            return controllerContext.Controller.TempData;
        }

    }
}
