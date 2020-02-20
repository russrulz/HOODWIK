using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HOODWIK
{
    class Counter
    {
        Space current;//space occupied by counter
        bool red;//if red or not
        int id;//id value for the counter

        /// <summary>
        /// creates a counter with a id
        /// </summary>
        /// <param name="r">if the counter is to be red or not</param>
        /// <param name="i">id for the counter</param>
        public Counter(bool r, int i) //creates a counter with a id and a colour
        {
            red = r;
            id = i;
        }
        /// <summary>
        /// getter to know what space the coutner is on
        /// </summary>
        /// <returns></returns>
        public Space getCurrent()
        {
            return current;
        }
        /// <summary>
        /// sets the space the coutner is on
        /// </summary>
        /// <param name="s">space the counter is being placed on</param>
        public void setCurrent(Space s)
        {
            current = s;
        }

        /// <summary>
        /// varaible to allow checking if the counter is red or not
        /// </summary>
        /// <returns>true if it's red counter false otherwise</returns>
        public bool isred() 
        {
            return red;
        }

        /// <summary>
        /// draws a circle to represent the counter
        /// </summary>
        /// <param name="gp">graphics object to draw to</param>
        internal void draw(Graphics gp)//draws the circles to represent the counters
        {
            Brush b;
            if (red)//checks if this counter is red or not
            {
                b = Brushes.Red;//sets a brush to red if it is red
            }
            else
            {
                b = Brushes.Blue;//sets to blue if it's not red
            }
            gp.FillEllipse(b, current.getx() - 15, current.gety() - 15, 30, 30);//creates a cricle centered on the space the counter is occuping
        }

        /// <summary>
        /// change to how it changes the counter object to a string
        /// </summary>
        /// <returns>the string to write</returns>
        public override string ToString() {
            string st ="";
            st += "id: ";
            st += id;
            st += " Is Red: ";
            st += red;
            return st;
        }
    }
}
