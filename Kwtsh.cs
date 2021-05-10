using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace In_Lec
{
    class Kwtsh : _3D_Model
    {
        public float Rad = 40;
        public float RadSmall = 20;
        public int XS, YS, ZS;

        public void Design()
        {
            float xx, yy, ZZ = 10 ;
            int i=0;
            float inc = 20;
            int steps = (int)(360 / inc);
            int iStart;
      

                for (int k = 0; k < 2; k++)
                {
                    iStart = i;
                    for (float th = 0; th < 360; th += inc)
                    {
                        xx = (float) (Math.Cos(th * Math.PI / 180) * Rad);
                        yy = (float) (Math.Sin(th * Math.PI / 180) * Rad);

                        L_3D_Pts.Add(new _3D_Point(xx + XS , yy+ YS, ZZ+ ZS));

                        //if (i > 0)
                        if (th > 0)
                        {
                            AddEdge(i, i - 1, Color.Brown);
                        }

                        AddEdge(i, i + steps, Color.Brown);

                        if (k == 0)
                            AddEdge(i, i + 2 * steps, Color.Brown);
                        i++;
                    }

                    AddEdge(i - 1, iStart, Color.Brown);
                    int j = 0;
                    for (float th = 0; th < 360; th += inc)
                    {
                        xx = (float) (Math.Cos(th * Math.PI / 180) * RadSmall);
                        yy = (float) (Math.Sin(th * Math.PI / 180) * RadSmall);

                        L_3D_Pts.Add(new _3D_Point(xx + XS, yy+YS, ZZ+ZS));

                        if (j > 0)
                        {
                            AddEdge(i, i - 1, Color.Brown);
                        }

                        i++;
                        j++;
                    }

                    AddEdge(i - 1, i - j, Color.Brown);

                    ZZ += 30;
                }

    




        }
    }
}
