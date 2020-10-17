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
        /// <summary>
        /// Координата верхнего левого угла по Y.
        /// </summary>
        public int tj { get; set; }
        /// <summary>
        /// Координата верхнего левого угла по X.
        /// </summary>
        public int ti { get; set; }
        /// <summary>
        /// Кол-во строк массива фигуры.
        /// </summary>
        public int N { get; set; }
        /// <summary>
        /// Кол-во столбцов массива фигуры.
        /// </summary>
        public int M { get; set; }
        /// <summary>
        /// Выбраная позиция.
        /// </summary>
        public int CurrentPosition { get; set; }
        /// <summary>
        /// Максимальное кол-во позиций.
        /// </summary>
        public int MaxPosition { get; set; }

        /// <summary>
        /// Массив состояний фигуры.
        /// </summary>
        public int[][,] ArrayStatus;
        /// <summary>
        /// Выбраное состояние фигуры.
        /// </summary>
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
