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
    public abstract class ShaverViewEngine: RazorViewEngine
    {

        /// <summary>
        /// Overrides the RazorViewEngine's CreateView method. Instantiates a new RazorView and then passes that view and the ControllerContext
        /// to the abstract method CreateReturnView
        /// </summary>
        /// <param name="controllerContext">ControllerContext of the calling Controller - contains ViewData, etc.</param>
        /// <param name="viewPath">Path to the view</param>
        /// <param name="masterPath">Path to the master file</param>
        /// <returns>IView object instantiated by abstract CreateReturnView</returns>
        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            var view = base.CreateView(controllerContext, viewPath, masterPath);
            return CreateReturnView(controllerContext, view);
        }

        /// <summary>
        /// Abstract method to create a new IView from the current Controller Context and the RazorView instantiated in the call to 
        /// CreateView
        /// </summary>
        /// <param name="controllerContext">ControllerContext of the calling Controller - contains ViewData, etc.</param>
        /// <param name="view">The instantiated RazorView</param>
        /// <returns>An IView object</returns>
        protected virtual IView CreateReturnView(ControllerContext controllerContext, IView view)
        {
            return view;
        }

    }
}
