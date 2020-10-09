using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Тетрис.Model
{
    class Figura_0 : MainFigura
    {
        /*  |
        *   |
        *   |__
        */

        public Figura_0()
        {
            MaxPosition = 4;

            ArrayStatus = new int[4][,]
            { new int[,] { {1,0},
                           {1,0 }, //Первое состояние.
                           {1,1} },

              new int[,] { {0,0,1}, //Второе состояние.
                           {1,1,1 }},

              new int[,] { {1,1},
                           {0,1}, //Третье состояние.
                           {0,1} },

              new int[,] { {1,1,1}, //Четвертое состояние.
                           {1,0,0 } }
            };

            CurrentPosition = rnd.Next(MaxPosition);
            SelectStatus();
        }
    }
}
