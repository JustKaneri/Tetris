using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Тетрис.Model
{
    class Figura_1:MainFigura
    {
        /*      |
         *      |__
         *         |
                   |  
        */

        public Figura_1()
        {
            MaxPosition = 2;

            ArrayStatus = new int[2][,]
            {
                new int[,]{ {0,2},
                            {2,2},
                            {2,0}},

                new int[,]{ {2,2,0},
                            {0,2,2}}
            };

            CurrentPosition = rnd.Next(MaxPosition);
            SelectStatus();
        }
    }
}
