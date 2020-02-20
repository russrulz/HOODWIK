using System.Collections.Generic;
using System;
using System.Drawing;

namespace HOODWIK
{
    class Board
    {

        public List<Space> spaces = new List<Space>();
        /// <summary>
        /// code to create the board
        /// </summary>
        public Board()
        {
            Addspaces();//creates the spaces for the board
            Linkspaces();//creates links between the spaces s they know what is connected for valid moves
            bool four = true;
            int x1 = 50, x2 = 250, x3 = 450, x4 = 650;
            int z1 = 150, z2 = 350, z3 = 550;
            for (int i = 0; i < spaces.Count; i++)//sets x values for the spaces to be able to draw them
            {
                if (four)
                {//if it's working on a set of four spaces them apart as a set of 4 points
                    spaces[i].setx(x1);
                    spaces[i + 1].setx(x2);
                    spaces[i + 2].setx(x3);
                    spaces[i + 3].setx(x4);
                    four = false;
                    i += 3;
                }
                else
                {//spcaes the 3 parts to be inbetween the 4's
                    spaces[i].setx(z1);
                    spaces[i + 1].setx(z2);
                    spaces[i + 2].setx(z3);
                    i += 2;
                    four = true;
                }
            }
            Setyvalues(x1, x2, x3, x4, z1, z2, z3);//sets yvalues
        }

        /// <summary>
        /// sets up the placments for the y values of each space
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="x3"></param>
        /// <param name="x4"></param>
        /// <param name="z1"></param>
        /// <param name="z2"></param>
        /// <param name="z3"></param>
        private void Setyvalues(int x1, int x2, int x3, int x4, int z1, int z2, int z3)//sets y values for the spaces appropriatly
        {
            spaces[0].sety(x1);
            spaces[1].sety(x1);
            spaces[2].sety(x1);
            spaces[3].sety(x1);

            spaces[4].sety(z1);
            spaces[5].sety(z1);
            spaces[6].sety(z1);

            spaces[7].sety(x2);
            spaces[8].sety(x2);
            spaces[9].sety(x2);
            spaces[10].sety(x2);

            spaces[11].sety(z2);
            spaces[12].sety(z2);
            spaces[13].sety(z2);

            spaces[14].sety(x3);
            spaces[15].sety(x3);
            spaces[16].sety(x3);
            spaces[17].sety(x3);

            spaces[18].sety(z3);
            spaces[19].sety(z3);
            spaces[20].sety(z3);

            spaces[21].sety(x4);
            spaces[22].sety(x4);
            spaces[23].sety(x4);
            spaces[24].sety(x4);
        }

        /// <summary>
        /// creates the links between the spaces
        /// </summary>
        private void Linkspaces()       //create links between the spaces where you can move along
        {
            spaces[0].link(spaces[1], "R");//A1 LINKS
            spaces[0].link(spaces[4], "DR");
            spaces[0].link(spaces[7], "D");

            spaces[1].link(spaces[0], "L");//A2 LINKS
            spaces[1].link(spaces[2], "R");
            spaces[1].link(spaces[4], "DL");
            spaces[1].link(spaces[5], "DR");
            spaces[1].link(spaces[8], "D");

            spaces[2].link(spaces[1], "L");//A3 LINKS
            spaces[2].link(spaces[3], "R");
            spaces[2].link(spaces[5], "DL");
            spaces[2].link(spaces[6], "DR");
            spaces[2].link(spaces[9], "D");

            spaces[3].link(spaces[2], "L");//A4 LINKS
            spaces[3].link(spaces[6], "DL");
            spaces[3].link(spaces[10], "D");

            spaces[4].link(spaces[0], "UL");//B1 LINKS
            spaces[4].link(spaces[1], "UR");
            spaces[4].link(spaces[7], "DL");
            spaces[4].link(spaces[8], "DR");

            spaces[5].link(spaces[1], "UL");//B2 LINKS
            spaces[5].link(spaces[2], "UR");
            spaces[5].link(spaces[8], "DL");
            spaces[5].link(spaces[9], "DR");

            spaces[6].link(spaces[2], "UL");//B3 LINKS
            spaces[6].link(spaces[3], "UR");
            spaces[6].link(spaces[9], "DL");
            spaces[6].link(spaces[10], "DR");

            spaces[7].link(spaces[0], "U"); //C1 LINKS
            spaces[7].link(spaces[4], "UR");
            spaces[7].link(spaces[8], "R");
            spaces[7].link(spaces[11], "DR");
            spaces[7].link(spaces[14], "D");

            spaces[8].link(spaces[1], "U");//C2 LINKS
            spaces[8].link(spaces[4], "UL");
            spaces[8].link(spaces[5], "UR");
            spaces[8].link(spaces[7], "L");
            spaces[8].link(spaces[9], "R");
            spaces[8].link(spaces[11], "DL");
            spaces[8].link(spaces[12], "DR");
            spaces[8].link(spaces[15], "D");

            spaces[9].link(spaces[2], "U");//C3 LINKS
            spaces[9].link(spaces[5], "UL");
            spaces[9].link(spaces[6], "UR");
            spaces[9].link(spaces[8], "L");
            spaces[9].link(spaces[10], "R");
            spaces[9].link(spaces[12], "DL");
            spaces[9].link(spaces[13], "DR");
            spaces[9].link(spaces[16], "D");

            spaces[10].link(spaces[3], "U");//C4 LINKS
            spaces[10].link(spaces[6], "UL");
            spaces[10].link(spaces[9], "L");
            spaces[10].link(spaces[13], "DL");
            spaces[10].link(spaces[17], "D");

            spaces[11].link(spaces[7], "UL");//D1 LINKS
            spaces[11].link(spaces[8], "UR");
            spaces[11].link(spaces[14], "DL");
            spaces[11].link(spaces[15], "DR");

            spaces[12].link(spaces[8], "UL");//D2 LINKS
            spaces[12].link(spaces[9], "UR");
            spaces[12].link(spaces[15], "DL");
            spaces[12].link(spaces[16], "DR");

            spaces[13].link(spaces[9], "UL");//D3 LINKS
            spaces[13].link(spaces[10], "UR");
            spaces[13].link(spaces[16], "DL");
            spaces[13].link(spaces[17], "DR");

            spaces[14].link(spaces[7], "U"); //E1 LINKS
            spaces[14].link(spaces[11], "UR");
            spaces[14].link(spaces[15], "R");
            spaces[14].link(spaces[18], "DR");
            spaces[14].link(spaces[21], "D");

            spaces[15].link(spaces[8], "U");//E2 LINKS
            spaces[15].link(spaces[11], "UL");
            spaces[15].link(spaces[12], "UR");
            spaces[15].link(spaces[14], "L");
            spaces[15].link(spaces[16], "R");
            spaces[15].link(spaces[18], "DL");
            spaces[15].link(spaces[19], "DR");
            spaces[15].link(spaces[22], "D");

            spaces[16].link(spaces[9], "U");//E3 LINKS
            spaces[16].link(spaces[12], "UL");
            spaces[16].link(spaces[13], "UR");
            spaces[16].link(spaces[15], "L");
            spaces[16].link(spaces[17], "R");
            spaces[16].link(spaces[19], "DL");
            spaces[16].link(spaces[20], "DR");
            spaces[16].link(spaces[23], "D");

            spaces[17].link(spaces[10], "U");//E4 LINKS
            spaces[17].link(spaces[13], "UL");
            spaces[17].link(spaces[16], "L");
            spaces[17].link(spaces[20], "DL");
            spaces[17].link(spaces[24], "D");

            spaces[18].link(spaces[14], "UL");//F1 LINKS
            spaces[18].link(spaces[15], "UR");
            spaces[18].link(spaces[21], "DL");
            spaces[18].link(spaces[22], "DR");

            spaces[19].link(spaces[15], "UL");//F2 LINKS
            spaces[19].link(spaces[16], "UR");
            spaces[19].link(spaces[22], "DL");
            spaces[19].link(spaces[23], "DR");

            spaces[20].link(spaces[16], "UL");//F3 LINKS
            spaces[20].link(spaces[17], "UR");
            spaces[20].link(spaces[23], "DL");
            spaces[20].link(spaces[24], "DR");

            spaces[21].link(spaces[14], "U");//G1 LINKS
            spaces[21].link(spaces[18], "UR");
            spaces[21].link(spaces[22], "R");

            spaces[22].link(spaces[15], "U");//G2 LINKS
            spaces[22].link(spaces[18], "UL");
            spaces[22].link(spaces[19], "UR");
            spaces[22].link(spaces[21], "L");
            spaces[22].link(spaces[23], "R");

            spaces[23].link(spaces[16], "U");//G3 LINKS
            spaces[23].link(spaces[19], "UL");
            spaces[23].link(spaces[20], "UR");
            spaces[23].link(spaces[22], "L");
            spaces[23].link(spaces[24], "R");

            spaces[24].link(spaces[17], "U");//G4 LINKS
            spaces[24].link(spaces[20], "UL");
            spaces[24].link(spaces[23], "L");

        }


        /// <summary>
        /// adds spaces to the board
        /// </summary>
        private void Addspaces()//adds spaces of board to a list
        {                                   //                              BOARD LAYOUT    mRed start A's AND C's blue E's AND G's
            spaces.Add(new Space("A1"));    //[0]                           A1  -----   A2  -----   A3  -----   A4
            spaces.Add(new Space("A2"));    //[1]                           |   \    /  |   \     /  |  \      / |
            spaces.Add(new Space("A3"));    //[2]                           |    B1     |     B2     |     B3    |      
            spaces.Add(new Space("A4"));    //[3]                           |   /    \  |   /    \   |  /     \  |
            //                                                              C1  -----   C2 -----    C3  -----  C4    
            spaces.Add(new Space("B1"));    //[4]                           |   \  /    |   \    /   |  \    /   |
            spaces.Add(new Space("B2"));    //[5]                           |    D1     |     D2     |    D3     |
            spaces.Add(new Space("B3"));    //[6]                           |   /  \    |   /   \    |  /   \    |
            //                                                              E1 -----    E2  -----   E3  -----   E4
            spaces.Add(new Space("C1"));    //[7]                           |   \   /   |   \   /    |   \   /   |
            spaces.Add(new Space("C2"));    //[8]                           |     F1    |     F2     |     F3    |
            spaces.Add(new Space("C3"));    //[9]                           |   /   \   |   /   \    |   /   \   |
            spaces.Add(new Space("C4"));    //[10]                          G1 -----    G2  -----   G3  -----   G4
            //
            spaces.Add(new Space("D1"));    //[11]
            spaces.Add(new Space("D2"));    //[12]
            spaces.Add(new Space("D3"));    //[13]
            //
            spaces.Add(new Space("E1"));    //[14]
            spaces.Add(new Space("E2"));    //[15]
            spaces.Add(new Space("E3"));    //[16]
            spaces.Add(new Space("E4"));    //[17]
            //
            spaces.Add(new Space("F1"));    //[18]
            spaces.Add(new Space("F2"));    //[19]
            spaces.Add(new Space("F3"));    //[20]
            //
            spaces.Add(new Space("G1"));    //[21]
            spaces.Add(new Space("G2"));    //[22]
            spaces.Add(new Space("G3"));    //[23]
            spaces.Add(new Space("G4"));    //[24]
        }

    
        internal void debug()//tseting to ensure it correctly set the x and y values
        {
            foreach (Space s in spaces)
            {
                Console.WriteLine(s.getx());
                Console.WriteLine(s.gety());
            }
        }

        /// <summary>
        /// loops through each space and calls it's draw method
        /// </summary>
        /// <param name="gp"></param>
        internal void draw(Graphics gp)//draws the board up using the spaces
        {
            foreach (Space s in spaces)
            {
                s.draw(gp);//tells each space to draw itself and all connections from itself
            }

        }
    }

}
