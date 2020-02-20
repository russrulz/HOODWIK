using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace HOODWIK
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Board board; //instance of the board
        Graphics gp;   
        List<Counter> red;
        List<Counter> blue;
        bool redturn;
        bool inprogress;
        Point mouseclick1;//variable for point of every 1st mouse click
        Point mouseclick2;//variable for every 2nd mouse click
        int mouseclicks = 0;//variable to track how many times mouse has been clicked 

        private readonly BackgroundWorker worker = new BackgroundWorker();//background worker to ensure clicks are registered

        private void Form1_Load(object sender, EventArgs e)
        {

            this.MouseClick += new MouseEventHandler(this.Mouseclickhandler);//set mouseclicks on the form to use mouseclickhandler for it's method
            worker.DoWork += worker_DoWork;            //sets the code for the background worker to do
            gp = this.CreateGraphics();                 //sets up  clean graphics for drawing
        }

        /// <summary>
        /// takes mouse click and puts it's x and y in the apropriate point based on if it's a odd click or even click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mouseclickhandler(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)//checks to ensure it was a left click
            {
                if (mouseclicks < 1)//if it's the first click sets mouse1 to that and increments mouseclicks
                {
                    mouseclick1 = new Point(e.X, e.Y);
                    mouseclicks++;
                }
                else if (mouseclicks < 2)//if a even click number sets mouse2 
                {
                    mouseclick2 = new Point(e.X, e.Y);
                    mouseclicks++;
                }
                else//if it's a odd number resets to 1 and sets mouseclick1 while clearing mouseclick2
                {
                    mouseclicks = 1;
                    mouseclick1 = new Point(e.X, e.Y);
                    mouseclick2 = new Point();
                }
            }

        }
        
        /// <summary>
        /// creates a new board and counters and begins the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startToolStripMenu_Click(object sender, EventArgs e)
        {
            try
            {
                board = new Board();//creates a new board
                red = new List<Counter>();//creates a empty list for tracking red counters
                blue = new List<Counter>();//creates list for tracking blue counters

                setupCounters();//sets up the counters

                Draw();//draws the board and coutners

                //run();
                worker.RunWorkerAsync();//start the worker runing the main program loop
            }
            catch(Exception ex) {//catch unforseen errors to prevent outright crashing
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// adds 8 counters to each list of coutners and setup thier positions on the board
        /// </summary>
        private void setupCounters()
        {

            for (int i = 1; i <= 8; i++)
            {
                red.Add(new Counter(true, i));
                blue.Add(new Counter(false, i));
            }
            red[0].setCurrent(board.spaces[0]);
            board.spaces[0].setocc(red[0]);
            red[1].setCurrent(board.spaces[1]);
            board.spaces[1].setocc(red[1]);
            red[2].setCurrent(board.spaces[2]);
            board.spaces[2].setocc(red[2]);
            red[3].setCurrent(board.spaces[3]);
            board.spaces[3].setocc(red[3]);
            red[4].setCurrent(board.spaces[7]);
            board.spaces[7].setocc(red[4]);
            red[5].setCurrent(board.spaces[8]);
            board.spaces[8].setocc(red[5]);
            red[6].setCurrent(board.spaces[9]);
            board.spaces[9].setocc(red[6]);
            red[7].setCurrent(board.spaces[10]);
            board.spaces[10].setocc(red[7]);

            blue[0].setCurrent(board.spaces[14]);
            board.spaces[14].setocc(blue[0]);
            blue[1].setCurrent(board.spaces[15]);
            board.spaces[15].setocc(blue[1]);
            blue[2].setCurrent(board.spaces[16]);
            board.spaces[16].setocc(blue[2]);
            blue[3].setCurrent(board.spaces[17]);
            board.spaces[17].setocc(blue[3]);
            blue[4].setCurrent(board.spaces[21]);
            board.spaces[21].setocc(blue[4]);
            blue[5].setCurrent(board.spaces[22]);
            board.spaces[22].setocc(blue[5]);
            blue[6].setCurrent(board.spaces[23]);
            board.spaces[23].setocc(blue[6]);
            blue[7].setCurrent(board.spaces[24]);
            board.spaces[24].setocc(blue[7]);
        }


        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            // run all background tasks here
            run();
        }

        /// <summary>
        /// main game run loop
        /// </summary>
        private void run()
        {
            inprogress = true;//is game inprogress
            redturn = false;//is it reds turn
            while (inprogress)
            {//game loop

                if (redturn)//checks if redsturn
                {
                    //run computer ai stuff 
                    runRed();
                    redturn = false;
                }
                else
                {
                    //runBlue();//run pc for blue
                    bool valid = false;
                    while (!valid)//loop untill player has selected a valid move to make
                    {
                        valid = runBlueplayer();//check if the move selected was valid
                    }
                    redturn = true;//sets turn value to red
                }
                Draw();//draws the board and coutners
               
            }
        }



        /// <summary>
        /// code to run players turn
        /// </summary>
        private bool runBlueplayer()
        {
            //wait for player click             
            Console.Out.WriteLine(mouseclicks + "-" + mouseclick1 + "-" + mouseclick2);
            Point pt,pt2;//points for making checks easyier later
            Point p = new Point(0, 0);//a black point for comparision 

            if(mouseclick1 != p && mouseclick2 != p){//checks to make sure there is both a 1st and second mouseclick
                foreach (Counter c in blue)//goes through the counters for blue
                {
                    pt = new Point(c.getCurrent().getx(), c.getCurrent().gety());//sets a point to the current counters location
                    if (inrange(mouseclick1, pt, 15f))//checks if the mouse click was within range of the current coutner
                    {
                        Console.Out.WriteLine("selected counter: " + c);//prints that the counter has been selected
                        foreach (Connection cn in c.getCurrent().getconnections())//loops through each of the connections of the space the counter is on
                        {
                            Space sp = cn.getspace();//saves the space of the connection to a variable
                            pt2 = new Point(sp.getx(), sp.gety());//sets a point to the spaces location

                            if (sp.getocc() == null)//checks to see if the space is occupied by a counter
                            {
                                if (inrange(mouseclick2, pt2, 12.5f))//if the mouseclick was in range of the space
                                {
                                    counterMove(c, sp);//move the counter to the space
                                    return true;        //return true to say a valid move has been made
                                }
                            }
                            else if (sp.getocc().isred())//if the space is occupied check if it's a red token occupiding it or not
                            {
                                foreach (Connection con in sp.getconnections())//for each connection of the occupied space
                                {
                                    Space spc = con.getspace();//store the space as variable
                                    pt2 = new Point(spc.getx(), spc.gety());//store a point for the spaces location
                                    if (spc.getocc() == null && con.getDirection() == cn.getDirection())//check if the space is on the same line or not from the origianl coutner
                                    {
                                        if (inrange(mouseclick2, pt2, 12.5f))//check if the mouse clicked on the space
                                        {
                                            jumpMove(c, cn.getspace().getocc(), con.getspace());//execute a move to jump the red counter
                                            return true;//return true to indicate a valid move was made
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }

            //check if it was valid blue token clicked
            //show valid spaces for moves and jumps
            //wait for a valid movespace click
            //check if it was a valid move
            //execute the move


            return false;

        }
        
        /// <summary>
        /// code to check if a point is within a range of another
        /// </summary>
        /// <param name="m">centeral point</param>
        /// <param name="pt">point to check if in range</param>
        /// <param name="r">range to check</param>
        /// <returns>true if it's in range otherwise false</returns>
        private bool inrange(Point m, Point pt, float r)
        {
            return ((pt.X - r) < m.X && m.X < (pt.X + r) && ((pt.Y - r) < m.Y && m.Y < pt.Y + r));           
        }


        /// <summary>
        /// code for pc to run blues turn (not in use)
        /// </summary>
        private void runBlue()
        {
            List<Counter> moves = new List<Counter>();
            List<Space> movesp = new List<Space>();
            List<Counter> jumps = new List<Counter>();
            List<Counter> jumped = new List<Counter>();
            List<Space> jumpsp = new List<Space>();

            foreach (Counter c in blue)
            {
                foreach (Connection cn in c.getCurrent().getconnections())
                {
                    Space s = cn.getspace();
                    if (s.getocc() == null)
                    {// checks each connection of the counters current space to see if empty if it is adds to list of valid moves
                        moves.Add(c);
                        movesp.Add(s);
                    }
                    else if (s.getocc().isred())
                    { //checks if the coutner occuping space is not red
                        foreach (Connection con in s.getconnections())
                        {
                            if (con.getDirection() == cn.getDirection() && con.getspace().getocc() == null)
                            {
                                jumps.Add(c);
                                jumped.Add(s.getocc());
                                jumpsp.Add(con.getspace());
                            }
                        }
                    }
                }
            }
            Random ran = new Random();
            if (jumps.Count > 0)//if there are valid jumps
            { //pick one from 0 to jumps.Count - 1

                int count = jumps.Count;
                int r = ran.Next(0, count);
                jumpMove(jumps[r], jumped[r], jumpsp[r]);
            }
            else if (moves.Count > 0)
            {//pick random move from list

                int count = moves.Count;
                int r = ran.Next(0, count);
                counterMove(moves[r], movesp[r]);
            }
            // else { 
            //      MessageBox.Show("Out of moves?");
            //  }
        }

        /// <summary>
        /// redraws the board
        /// </summary>
        private void Draw()
        {
            gp.Clear(Color.White);//clears the graphics 
            board.draw(gp);//draws the board with spaces and the connection lines between the spaces
            DrawCounters();//draws the counters on top of the board
        }

        /// <summary>
        /// executes code for red turn (pc)
        /// </summary>
        private void runRed()
        {
            List<Counter> moves = new List<Counter>();//list of counters with valid moves
            List<Space> movespace = new List<Space>();//list for spaces to move to
            List<Counter> jumper = new List<Counter>();//list of coutner making jump
            List<Counter> jumped = new List<Counter>();//list of counter to be jumped
            List<Space> jumpspace = new List<Space>();//list of landing space after jump

            foreach (Counter c in red)
            {
                foreach (Connection cn in c.getCurrent().getconnections())
                {
                    Space s = cn.getspace();
                    if (s.getocc() == null)
                    {// checks each connection of the counters current space to see if empty if it is adds to list of valid moves
                        moves.Add(c);
                        movespace.Add(s);
                    }
                    else if (!s.getocc().isred())
                    { //checks if the coutner occuping space is not red
                        foreach (Connection con in s.getconnections())
                        {
                            if ((con.getDirection() == cn.getDirection()) && con.getspace().getocc() == null)
                            {
                                jumper.Add(c);
                                jumped.Add(s.getocc());
                                jumpspace.Add(con.getspace());
                            }
                        }
                    }
                }
            }
            Random ran = new Random();
            if (jumper.Count > 0)//if there are valid jumps
            { //pick one from 0 to jumps.Count - 1
                int count = jumper.Count;
                int r = ran.Next(0, count);
                jumpMove(jumper[r], jumped[r], jumpspace[r]);
            }
            else
            {//pick random move from list
                int count = moves.Count;
                int r = ran.Next(0, count);
                counterMove(moves[r], movespace[r]);
            }
        }

        /// <summary>
        /// execute the move
        /// </summary>
        /// <param name="counter">counter to move</param>
        /// <param name="space">space to move to</param>
        private void counterMove(Counter counter, Space space)
        {
            counter.getCurrent().setocc(null);//remvoes link to coutner form the space it moved from
            counter.setCurrent(space);//sets coutners current value to the new space
            space.setocc(counter);//sets the new spaces coutner variable to makesure it knows there is a counter on it
            foreach (Space s in board.spaces) { //goes through each of the spaces of the board 
                if (s.getID() == counter.getCurrent().getID())//if the current space is the space that is being moved from
                {
                    s.setocc(null);//set it to null to indicate counter isn't currently there anymore
                }
                if (s.getID() == space.getID())//if space s is the space being moved to
                {
                    s.setocc(counter);//sets it's occupied variable to the counter thats moving
                }
            }
        }

        /// <summary>
        /// executes jump move
        /// </summary>
        /// <param name="counter">Counter to move</param>
        /// <param name="counter_2">Counter being jumped</param>
        /// <param name="space">space to move to</param>
        private void jumpMove(Counter counter, Counter counter_2, Space space)
        {
            space.setocc(counter);//sets the space the counter is moving to know that the coutner is not on it
            counter.getCurrent().setocc(null);//sets the space being mvoed from to null so that checks for a counter on that space know it has none
            counter.setCurrent(space);//sets the counters varaible for knowing where it is to the space it's moving to
            foreach (Space s in board.spaces)//goes though each space on the board
            {
                if (s.getID() == counter.getCurrent().getID())//if it is the space being moved from
                {
                    s.setocc(null);//set occupied varaible to null
                }
                if (s.getID() == space.getID())//if it is the space being moved to
                {
                    s.setocc(counter);//set occupied varaible to the counter thats moving
                }
                if (s.getocc() == counter_2)//if the space currently says it has the counter being jumped occupiing it 
                {
                    s.setocc(null);//set it to null to ensure that there's not coutner there
                }
            }
            if (redturn)//if it's reds turn
            {
                blue.Remove(counter_2);//remove jumped counter from blues coutners
                
                if (blue.Count == 0)//checks if all counters from blue are removed if so end game
                {
                    //end game
                    inprogress = false;
                    Draw();
                    MessageBox.Show("RED WINS");
                    //display player loses
                }
            }
            else//it's blues turn
            {
                red.Remove(counter_2);//remove jumped counter
                if (red.Count == 0)//checks if red still has counters remaining
                {
                    inprogress = false;
                    //display player wins 
                    Draw();
                    MessageBox.Show("BLUE WINS");// dispaly that player wins
                }
            }

        }

        /// <summary>
        /// Draws each counter
        /// </summary>
        private void DrawCounters()
        {
            foreach (Counter c in red)//goes through each red counter
            {
                c.draw(gp);//calls it's draw method
            }
            foreach (Counter c in blue)//goes through each blue counter
            {
                c.draw(gp);//calls draw method of the coutner
            }
        }

        /// <summary>
        /// closes the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}