using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;

namespace Bawt
{
    public class Engine
    {

        private Lemma lemma = new Lemma();

        /// <summary>
        /// Encodes the string
        /// </summary>
        /// <param name="message">Message to encode</param>
        /// <param name="url">Source url</param>
        /// <returns>Ended string</returns>
        public string Encode(string message, string url)
        {
             
            if (message == null || url == null)
            {
                throw new ArgumentNullException();
            }
            if (message.Trim() == String.Empty)
            {
                throw new ArgumentException("message cannot be empty");
            }
            if (Helper.IsValidUrl(url) == false)
            {
                throw new ArgumentException("not a valid url");
            }

            var siteUrl = url;
            var html = Helper.GetContent(siteUrl);
            var indices = String.Empty;
            var serialized = String.Empty;
            var header = "bawt|";

            try
            {
                lemma.Source = siteUrl;
                lemma.Message = html;

                var contains = message;

                var random = new Random();
                lemma.Indices = new List<int>();
                lemma.Unknown = new List<char>();

                var count = 0;
                char curChar;

                var tempList = new List<int>();
                var unknownList = new List<int>();

                foreach (char c in contains)
                {
                    count = 0;
                    tempList.Clear();
                    unknownList.Clear();
                    curChar = c;

                    foreach (char m in lemma.Message)
                    {
                        count++;
                        if (c == m)
                        {
                            tempList.Add(count);
                        }
                    }

                    if (tempList.Count == 0)
                    {
                        lemma.Unknown.Add(curChar);
                        lemma.Indices.Add(-1);
                    }
                    else
                    {
                        var rand = new Random(Guid.NewGuid().GetHashCode());
                        var result = tempList[rand.Next(0, tempList.Count)];

                        lemma.Indices.Add(result);
                    }
                }

                try
                {
                    foreach (var i in lemma.Indices)
                    {
                        indices += "|" + i.ToString();
                    }

                    serialized = Security.Encrypt(header + lemma.Source + indices, "warispeace");
                }
                catch (Exception ex)
                {
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }

            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }


            return serialized;
        }

        /// <summary>
        /// Decodes a Message
        /// </summary>
        /// <param name="message">Encoded message</param>
        /// <returns>Decoded message</returns>
        public string Decode(string message)
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
                var outputMessage = Security.Decrypt(message, "warispeace");
                var items = outputMessage.Split('|');
                var valid = items[0].ToString();
                var header = "bawt";

                if (valid == header)
                {
                    var source = Helper.GetContent(items[1].ToString());
                    var source2 = Helper.Clean(source);

                    for (int i = 2; i < items.Count(); i++)
                    {

                        var result = Int32.TryParse(items[i], out int s);

                        if (s == -1 || result == false)
                        {
                            outcome += " ";
                        }
                        else
                        {
                            outcome += source2.Substring(s - 1, 1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }

            return outcome;
        }

    }

}
