using System;
using System.Collections.Generic;
using System.Text;

namespace ShadeRadio
{
    class Station
    {
        public string name;
        public string url;

        public Station() { }
        public Station(string name,string url)
        {
            this.name = name;
            this.url = url;
        }
    }
}
