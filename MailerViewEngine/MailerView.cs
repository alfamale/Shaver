﻿/*  Copyright Andrew Woodcock, 2011

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
using Shaver;
using System.Web.Mvc;
using System.IO;

namespace MailerViewEngine
{
    public class MailerView: ShaverView
    {

        private IView _view;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="controllerContext">ControllerContext of the calling Controller - contains ViewData, etc.</param>
        /// <param name="view">Instantiated RazorView</param>
        public MailerView(ControllerContext controllerContext, IView view)
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
            html = html.Replace("<!DOCTYPE html>", string.Empty);

            var viewData = ShaverView.GetViewData(controllerContext);
            var tempData = ShaverView.GetTempData(controllerContext);

            var mail = viewData.Model as IMail;
            mail.Content = html;

            Smtp.Send(mail.From, mail.Subject, mail.Content, mail.Recipients);

        }

        /// <summary>
        /// Method to output data to the TextWriter
        /// </summary>
        /// <param name="viewContext">ViewContext for the RazorView</param>
        /// <param name="writer">TextWriter to write output to</param>
        protected override void RenderView(ViewContext viewContext, TextWriter writer)
        {
            _view.Render(viewContext, writer);
        }

    }
}
