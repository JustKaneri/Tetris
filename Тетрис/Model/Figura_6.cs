using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Тетрис.Model
{
    class Figura_6:MainFigura
    {
        public Figura_6()
        {
            MaxPosition = 2;
            ArrayStatus = new int[2][,]
            {
               new int[,] { {7}, {7}, {7}, {7} },
               new int[,] { {7,7,7,7} }
            };

            CurrentPosition = rnd.Next(MaxPosition);

            SelectStatus();
        }
    }
}
