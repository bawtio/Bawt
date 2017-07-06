using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bawt
{
    /// <summary>
    /// This class represents the components of the packaet
    /// It contains the source and the message.
    /// </summary>
    public class Lemma
    {
        public string Source { get { return _source; } set { _source = value; } }
        public string Message { get { return _message; } set { _message = value; } }
        public List<int> Indices { get; set; }
        public List<char> Unknown { get; set; }

        string _message;
        string _source;

        public Lemma()
        {

        }

        public Lemma(string source, string message)
        {
            _source = Source;
            _message = Message;
        }
    }
}
