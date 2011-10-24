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
using log4net;
using log4net.Config;
using Shaver;

namespace LoggingViewEngine
{
    public class LoggingViewEngine: ShaverViewEngine
    {

        public LoggingViewEngine()
        {
            // This is where we tell MVC where to look for our files. This says
            // to look for a file at "Views/{Controller}/{Action}.cslog"
            base.ViewLocationFormats = new string[] { "~/Views/{1}/{0}.cslog", "~/Views/Shared/{0}.cslog" };
            base.PartialViewLocationFormats = base.ViewLocationFormats;
        }

        /// <summary>
        /// Method to create a new IView from the current Controller Context and the RazorView instantiated in the call to 
        /// CreateView
        /// </summary>
        /// <param name="controllerContext">ControllerContext of the calling Controller - contains ViewData, etc.</param>
        /// <param name="view">The instantiated RazorView</param>
        /// <returns>A LoggingView object</returns>
        protected override IView CreateReturnView(ControllerContext controllerContext, IView view)
        {
            return new LoggingView(controllerContext, view);
        }

    }
}
