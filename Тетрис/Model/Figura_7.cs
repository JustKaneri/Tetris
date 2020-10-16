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
            MaxPosition = 3;
            ArrayStatus = new int[3][,]
            {
                new int[,] 
                { 
                    {0,8,0},
                    {8,0,8}
                },
                new int[,]
                {
                    {8,0},
                    {0,8},
                    {8,0}
                },
                new int[,]
                {
                    {8,0,8},
                    {0,8,0}
                }
            };

            CurrentPosition = rnd.Next(MaxPosition);
            SelectStatus();
        }
    }
}
