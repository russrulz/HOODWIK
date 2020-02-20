using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HOODWIK
{
    class Connection
    {
        string d;//string for the direction of the connection
        Space s;//space that is connected to

        /// <summary>
        /// creates a connections with a space to connect to and a direction for the connection
        /// </summary>
        /// <param name="space"></param>
        /// <param name="dir"></param>
        public Connection(Space space, string dir)
        {
            this.d = dir;
            this.s = space;
        }

        internal Space getspace()
        {
            return s;
        }

        internal string getDirection()
        {
            return d;
        }
    }
}
