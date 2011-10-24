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
using System.Net.Mail;

namespace MailerViewEngine
{
    /// <summary>
    /// Send email using SMTP.
    /// SMTP details are taken from the app.config or web.config file
    /// </summary>
    public class Smtp
    {

        /// <summary>
        /// send the email
        /// </summary>
        /// <param name="from">Sender</param>
        /// <param name="subject">Email subject line</param>
        /// <param name="content">Email contents</param>
        /// <param name="recipients">List of recipients. 
        /// This is a generic list of strings representing email addresses</param>
        /// <returns>True for success, false for failure</returns>
        public static Boolean Send(string from, string subject, string content, List<string> recipients)
        {

            var message = SmtpMail(from, subject, content);

            foreach (string recipient in recipients)
            {
                try
                {
                    message.To.Add(new MailAddress(recipient));
                }
                catch (Exception ex)
                {
                    throw new SmtpFailedRecipientException("Could not add the recipient", ex);
                }
            }

            return SendSmtpMail(message);

        }


        /// <summary>
        /// send the email with attachments
        /// </summary>
        /// <param name="from">Sender</param>
        /// <param name="subject">Email subject line</param>
        /// <param name="content">Email contents</param>
        /// <param name="recipients">List of recipients. 
        /// This is a generic list of strings representing email addresses</param>
        /// <param name="attachments">List of attachments</param>
        /// <returns>True for success, false for failure</returns>
        public static Boolean Send(string from, string subject, string content, List<string> recipients, List<string> attachments)
        {

            var message = SmtpMail(from, subject, content);

            foreach (string recipient in recipients)
            {
                try
                {
                    message.To.Add(new MailAddress(recipient));
                }
                catch (Exception ex)
                {
                    throw new SmtpFailedRecipientException("Could not add the recipient '" + recipient + "'", ex);
                }
            }

            foreach (string attachment in attachments)
            {
                try
                {
                    message.Attachments.Add(new Attachment(attachment));
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not add the attachment '" + attachment + "'", ex);
                }
            }

            return SendSmtpMail(message);

        }

        /// <summary>
        /// send the email
        /// </summary>
        /// <param name="from">Sender</param>
        /// <param name="subject">Email subject line</param>
        /// <param name="content">Email contents</param>
        /// <param name="recipients">List of recipients. 
        /// This is a generic list of strings representing email addresses</param>
        /// <param name="host">SMTP host</param>
        /// <param name="port">Port on the host (usually 25)</param>
        /// <returns>True for success, false for failure</returns>
        public static Boolean Send(string from, string subject, string content, List<string> recipients, string host, int port)
        {

            var message = SmtpMail(from, subject, content);

            foreach (string recipient in recipients)
            {
                try
                {
                    message.To.Add(new MailAddress(recipient));
                }
                catch (Exception ex)
                {
                    throw new SmtpFailedRecipientException("Could not add the recipient", ex);
                }
            }

            return SendSmtpMail(message, host, port);

        }

        /// <summary>
        /// send the email
        /// </summary>
        /// <param name="from">Sender</param>
        /// <param name="subject">Email subject line</param>
        /// <param name="content">Email contents</param>
        /// <param name="recipients">List of recipients. 
        /// This is a generic list of strings representing email addresses</param>
        /// <param name="attachments">List of attachments</param>
        /// <param name="host">SMTP host</param>
        /// <param name="port">Port on the host (usually 25)</param>
        /// <returns>True for success, false for failure</returns>
        public static Boolean Send(string from, string subject, string content, List<string> recipients, List<string> attachments, string host, int port)
        {

            var message = SmtpMail(from, subject, content);

            foreach (string recipient in recipients)
            {
                try
                {
                    message.To.Add(new MailAddress(recipient));
                }
                catch (Exception ex)
                {
                    throw new SmtpFailedRecipientException("Could not add the recipient", ex);
                }
            }

            foreach (string attachment in attachments)
            {
                try
                {
                    message.Attachments.Add(new Attachment(attachment));
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not add the attachment '" + attachment + "'", ex);
                }
            }

            return SendSmtpMail(message, host, port);

        }

        internal static MailMessage SmtpMail(string from, string subject, string content, bool isBodyHtml = true)
        {
            return new MailMessage()
            {
                Subject = subject,
                From = new MailAddress(from),
                Body = content,
                IsBodyHtml = isBodyHtml,
                BodyEncoding = Encoding.UTF8,
                SubjectEncoding = Encoding.UTF8
            };
        }

        internal static Boolean SendSmtpMail(MailMessage message)
        {
            try
            {
                var client = new SmtpClient();
                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                throw new SmtpException("Could not send mail", ex);
            }
        }

        internal static Boolean SendSmtpMail(MailMessage message, string host, int port)
        {
            try
            {
                var client = new SmtpClient(host, port);
                client.UseDefaultCredentials = true;
                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                throw new SmtpException("Could not send mail", ex);
            }
       }

    }
}
