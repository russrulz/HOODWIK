using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HOODWIK
{
    class Space
    {

        string ID;  //varaible for id of the space
        int x;      //xvalue of spaces location
        int y;      //yvalue of spaces location
        
        List<Connection> connections = new List<Connection>();//list of connections the space has
        Counter ocu;

        /// <summary>
        /// code to create the space ensuring that it's id gets set and it starts with not counter in occupied variable
        /// </summary>
        /// <param name="s">id of this space</param>
        public Space(string s)
        {
            this.ID = s;
            ocu = null;
        }

        /// <summary>
        /// creates connection between a space and another space while also adding the direction which the connection is
        /// </summary>
        /// <param name="space">space being linked to</param>
        /// <param name="dir">direction of the link</param>
        internal void link(Space space, string dir)
        {
            connections.Add(new Connection(space, dir));
        }

        internal void setx(int x1)
        {
            this.x = x1;
        }
        internal int getx()
        {
            return x;
        }
        internal void sety(int y1)
        {
            this.y = y1;
        }
        internal int gety()
        {
            return y;
        }

        /// <summary>
        /// method to draw out the space and also create lines to represent it's connections
        /// </summary>
        /// <param name="gp"> the graphics object to draw to</param>
        internal void draw(Graphics gp)//draws a circle centered on the x and y values and a line for each connection to other spaces
        {
            int rad = 25;//diameter of the circle to draw
            Pen p = Pens.Black;
            Brush b = Brushes.Black;
            gp.DrawEllipse(p, (x - (rad / 2)), (y - (rad / 2)), rad, rad);//draws a cricle for the space
            gp.FillEllipse(b, (x - (rad / 2)), (y - (rad / 2)), rad, rad);//creates a filled black circle to represent it
            foreach (Connection c in connections)//goes through the connections of the space
            {
                Space sp = c.getspace();//temp variable of space to connect to
                gp.DrawLine(p, x, y, sp.x, sp.y);//draws connection line from itself to connected space
            }

        }

        /// <summary>
        /// //sets variable to have space know if there is a counter on it or not
        /// </summary>
        /// <param name="counter">Counter that is on the space</param>
        internal void setocc(Counter counter)
        {
            ocu = counter;
        }


        internal List<Connection> getconnections()
        {
            return connections;
        }

        internal Counter getocc()
        {
            return ocu;
        }

        internal string getID()
        {
            return ID;
        }
    }
}
