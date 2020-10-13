using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Тетрис.Model
{
    class MainFigura
    {
        public static Random rnd = new Random();
        public int tj { get; set; }
        public int ti { get; set; }
        public int N { get; set; }
        public int M { get; set; }
        public int CurrentPosition { get; set; }
        public int MaxPosition { get; set; }

        public int[][,] ArrayStatus;
        public int[,] CurrentFigurs;

        /// <summary>
        /// Выбор состояния фигуры.
        /// </summary>
        public void SelectStatus()
        {
            CurrentFigurs = ArrayStatus[CurrentPosition];

            N = CurrentFigurs.GetLength(0);
            M = CurrentFigurs.GetLength(1);
        }

        /// <summary>
        /// Возврат к прошлому состоянию.
        /// </summary>
        public void RotateLeft()
        {
            if (CurrentPosition > 0)
                CurrentPosition--;
            else
                CurrentPosition = MaxPosition - 1;

            SelectStatus();
        }

        /// <summary>
        /// Переход к следующиму состоянию.
        /// </summary>
        public void RoteteRight()
        {
            if (CurrentPosition < MaxPosition - 1)
                CurrentPosition++;
            else
                CurrentPosition = 0;

            SelectStatus();
        }
    }
}
