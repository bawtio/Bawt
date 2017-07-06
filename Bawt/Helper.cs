using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;

namespace Bawt
{
    public static class Helper
    {
        /// <summary>
        /// Checks if message is encoded
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool IsEncoded(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException();
            }
            if (message.Trim() == String.Empty)
            {
                throw new ArgumentException("message cannot be empty");
            }

            var outcome = String.Empty;

            try
            {
                var dMessage = Security.Decrypt(message, "warispeace");
                var items = dMessage.Split('|');
                string valid = items[0].ToString();

                if (valid == "bawt")
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// Validates URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsValidUrl(string url)
        {
            if (url == null)
            {
                throw new ArgumentNullException();
            }
            if (url.Trim() == String.Empty)
            {
                throw new ArgumentException("url cannot be empty");
            }

            try

            {
                if (!Uri.TryCreate(url, UriKind.Absolute, out Uri uri) || null == uri)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }

            return true;
        }

        /// <summary>
        /// Returns the page content
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetContent(string url)
        {

            if (url == null)
            {
                throw new ArgumentNullException();
            }
            if (url.Trim() == String.Empty)
            {
                throw new ArgumentException("url cannot be empty");
            }
            if (Helper.IsValidUrl(url) == false)
            {
                throw new ArgumentException("not a valid url");
            }

            string page = String.Empty;

            try
            {
                var httpClient = new HttpClient();
                page = httpClient.GetStringAsync(url).Result;
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }

            return Clean(page);
        }

        /// <summary>
        /// Cleans the input
        /// </summary>
        /// <param name="message"></param>
        /// <returns>String with replaced characters</returns>
        public static string Clean(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException();
            }
            if (message.Trim() == String.Empty)
            {
                throw new ArgumentException("message cannot be empty");
            }

            var regex = String.Empty;

            try
            {
                regex = Regex.Replace(message, @"[*]+", "");
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }

            return regex;
        }

    }
}
