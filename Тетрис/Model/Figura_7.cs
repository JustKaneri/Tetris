using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Тетрис.Model
{
    class Figura_7:MainFigura
    {
        public Figura_7()
        {
            MaxPosition = 1;
            ArrayStatus = new int[1][,]
            {
                new int[,] 
                {
                    {8,0,8},
                    {0,8,0},
                    {8,0,8}
                }
            };

            CurrentPosition = rnd.Next(MaxPosition);
            SelectStatus();
        }
    }
}
