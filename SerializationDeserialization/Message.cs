using System;
using System.Collections.Generic;
using System.Text;

namespace SerializationDeserialization
{
    public class Message
    {
        public DateTime Date { get; set; }
        public string Info { get; set; }

        public override string ToString()
        {
            return $"{Date} {Info}";
        }
    }
}
