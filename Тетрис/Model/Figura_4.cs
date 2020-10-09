using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Тетрис.Model
{
    class Figura_4:MainFigura
    {

        //См класс Figura_0.


        public Figura_4()
        {
            MaxPosition = 4;
            ArrayStatus = new int[4][,]
            {
                new int[,] { {0,5}, 
                             {0,5}, 
                             {5,5} },

                new int[,] { {5,5,5}, 
                             {0,0,5} },
                new int[,] { {5,5}, 
                             {5,0},
                             {5,0} },
                new int[,] { {5,0,0}, 
                             {5,5,5} }
            };

            CurrentPosition = rnd.Next(MaxPosition);
            SelectStatus();

        }
    }
}
