using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace In_Lec
{
    public partial class Form1 : Form
    {
        Bitmap off;

        _3D_Model arrow = new _3D_Model();

        List<Kwtsh> obj = new List<Kwtsh>();

        _3D_Model Cube = new _3D_Model();

        Camera cam = new Camera();


        _3D_Point v1 = new _3D_Point(0, 0, 0);
        _3D_Point v2 = new _3D_Point(0, 1, 0);

        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.Load += new EventHandler(Form1_Load);
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
   
        }

            
        void CreateCube(_3D_Model cb, float XS, float YS, float ZS)
        {
            float[] vert = 
            {  -100, 50, 50,
                100, 50, 50,
                100, 50,-50,
                -100,50,-50,

                -100,-50,   50,
                100, -50,   50,
                100, -50,  -50,
                -100,-50,  -50,

               
            };


            _3D_Point pnn;
            int j = 0;
            for (int i = 0; i < 8; i++)
            {
                pnn = new _3D_Point(vert[j] + XS, vert[j + 1] + YS, vert[j + 2] + ZS);
                j += 3;
                cb.AddPoint(pnn);
            }


            int[] Edges = {
                0,1 ,
                1,2 ,
                2,3 ,
                3,0 , 

                4,5,
                5,6,
                6,7,
                7,4,

                0,4,
                1,5,
                2,6,
                3,7

            };
            j = 0;
            Color[] cl = { Color.Aquamarine, Color. Aqua, Color.Blue, Color.Yellow};
            for (int i = 0; i < 12; i++)
            {
                if (i < 7)
                {
                    cb.AddEdge(Edges[j], Edges[j + 1], cl[2]);
                }
                else

                    cb.AddEdge(Edges[j], Edges[j + 1], cl[3]);
                j += 2;

            }
            //cb.cam = Cam;
        }

        void CreateColumntaweel(_3D_Model C, float XS, float YS, float ZS, Color vvv)
        {
            float[] vert3 =
            {
                -100,5, 25,
                100, 5, 25,
                100, 5,-25,
                -100,5,-25,

                -50, -25,  25,
                50,  -25,  25,
                50,  -25, -25,
                -50, -25, -25,

            };

            _3D_Point pnn2;
            int j = 0;
            for (int i = 0; i < 8; i++)
            {

                pnn2 = new _3D_Point(vert3[j] + XS, vert3[j + 1] + YS, vert3[j + 2] + ZS);
                j += 3;

                C.AddPoint(pnn2);

            }
            int[] Edges = {
                0,1,
                1,2,
                2,3,
                3,0,
                4,5,
                5,6,
                6,7,
                7,4,
                0,4,
                3,7,
                2,6,
                1,5
            };
            j = 0;
            //Color[] cl = { Color.Red, Color.Yellow, Color.Black, Color.Blue };
            for (int i = 0; i < 12; i++)
            {

                C.AddEdge(Edges[j], Edges[j + 1], vvv); //cl[i % 4]);

                j += 2;
            }
        }

        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.X:
                    break;
                case Keys.Y:
                    break;
                case Keys.Z:
                    break;
                
                case Keys.Right:
                    //Cube.RotateAroundEdge(3, 1);
                    //Cube.RotateAroundEdge(3, -1);

                    Transformation.RotateArbitrary(Cube.L_3D_Pts, v1, v2, -1);

                    for (int i = 0; i < obj.Count(); i++)
                    {


                        Transformation.RotateArbitrary(obj[i].L_3D_Pts, v1, v2, -1);
                    }

                    Transformation.RotateArbitrary(arrow.L_3D_Pts, v1, v2, -1);
                    break;


                case Keys.Left:

                    Transformation.RotateArbitrary(Cube.L_3D_Pts, v1, v2, 1);

                    for (int i = 0; i < obj.Count(); i++)
                    {


                        Transformation.RotateArbitrary(obj[i].L_3D_Pts, v1, v2, 1);
                    }

                    Transformation.RotateArbitrary(arrow.L_3D_Pts, v1, v2, 1);
                    break;

                case Keys.Up:
                    float diffX = Cube.L_3D_Pts[1].X - Cube.L_3D_Pts[0].X;
                    float diffY = Cube.L_3D_Pts[1].Y - Cube.L_3D_Pts[0].Y;
                    float diffZ = Cube.L_3D_Pts[1].Z - Cube.L_3D_Pts[0].Z;

                    float step = 0.01f;
                    Cube.TransX(diffX * step);
                    Cube.TransY(diffY * step);
                    Cube.TransZ(diffZ * step);
                    for (int i = 0; i < obj.Count; i++)
                    {
                        obj[i].TransX(diffX * step);
                        obj[i].TransY(diffY * step);
                        obj[i].TransZ(diffZ * step);
                    }
                    arrow.TransX(diffX * step);
                    arrow.TransY(diffY * step);
                    arrow.TransZ(diffZ * step);

                   

                    break;
                case Keys.Down:
                   

                    break;

                case Keys.Space:
                    cam.lookAt.X ++;
                    cam.BuildNewSystem();
                    break;
            }

            DrawDubble(this.CreateGraphics());
        }

        void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(this.ClientSize.Width , this.ClientSize.Height);


            int cx = 400;
            int cy = 400;
            cam.ceneterX = (this.ClientSize.Width / 2);
            cam.ceneterY = (this.ClientSize.Height / 2);
            cam.cxScreen = cx;
            cam.cyScreen = cy;            
            cam.BuildNewSystem();


           Kwtsh pnn;

           
           arrow.cam = cam;
           CreateColumntaweel(arrow, 50,50 , 0, Color.White);
            
           arrow.RotateAroundEdge(1, -45);
           Cube.cam = cam;
           CreateCube(Cube, 50, 100, 0);

            for (int i = 0; i < 4; i++)
            {
              
               pnn = new Kwtsh();
               pnn.XS = (int)Cube.L_3D_Pts[i].X ;

               pnn.YS = (int)Cube.L_3D_Pts[i].Y;

               pnn.ZS = (int)Cube.L_3D_Pts[i].Z - (int)pnn.RadSmall;

                pnn.cam = cam;
                pnn.Design();
                obj.Add(pnn);
                
            }

        }

        void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubble(e.Graphics);
        }

        void DrawScene(Graphics g)
        {
            g.Clear(Color.Black);

            for (int i = 0; i < obj.Count(); i++)
            {
                obj[i].DrawYourSelf(g);
            }

            Cube.DrawYourSelf(g);
            arrow.DrawYourSelf(g);


            
        }

        void DrawDubble(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
    }
}
