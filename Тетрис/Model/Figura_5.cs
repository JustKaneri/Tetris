using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Тетрис.Model
{
    class Figura_5:MainFigura
    {
        public Figura_5()
        {
            MaxPosition = 1;
            ArrayStatus = new int[1][,]
            {
                new int[,] { {6,6},
                             {6,6} }
            };

            CurrentPosition = rnd.Next(MaxPosition);

            SelectStatus();

        }
    }
}
