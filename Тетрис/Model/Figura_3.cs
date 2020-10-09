using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Тетрис.Model
{
    class Figura_3:MainFigura
    {
        /*      |
         *      |__
         *         |
         *         |  
        */
        public Figura_3()
        {
            MaxPosition = 2;
            ArrayStatus = new int[2][,]
            {
                new int[,] { {4,0}, 
                             {4,4}, 
                             {0,4} },

                new int[,] { {0,4,4}, 
                             {4,4,0} }
            };

            CurrentPosition = rnd.Next(MaxPosition);
            SelectStatus();

        }
    }
}
