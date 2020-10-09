using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Тетрис.Model
{
    class Figura_2:MainFigura
    {
        /*     /\
              /  \
              ----
        */
        public Figura_2()
        {
            MaxPosition = 4;
            ArrayStatus = new int[4][,]
            {
            new int[,] { {0,3,0},
                         {3,3,3} },

            new int[,] { {3,0}, 
                         {3,3}, 
                         {3,0} },

            new int[,] { {3,3,3},
                         {0,3,0}},

            new int[,] { {0,3}, 
                         {3,3},
                         {0,3} }
            };

            CurrentPosition = rnd.Next(MaxPosition);
            SelectStatus();

        }
    }
}
