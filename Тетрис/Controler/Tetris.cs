using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Тетрис.Model;

namespace Тетрис.Controler
{
    class Tetris
    {
        private static Random rnd = new Random();
        private const int CountFigur = 8;

        public int[,] GameMap;
        public int RowCount,
                   ColCount;
        public int CellWidth = 25;//ширина одной ячейки игрового поля
        public int Dx = 0,
                   Dy = 0;//отступы.
        public int BaceInterval;//поле для сохранения старого значения интервала таймера при сбросе фигуры
        public int Level = 1;
        public int Scope = 0;
        public int CountDestroyLine = 0;//количество уничтоженных линий
        public int NextLevel;//количество баллов для перехода на следующий уровень
        
        //работы таймера после сброса фигуры
        public bool RestoreInterval = false;
        public bool Pause = false;
        public bool GameOver = false;
        public Timer timer;
        public MainFigura currenFigura = null;
        public MainFigura Nextfigura = null;

        public Label lLevel,
                     lLines,
                     lScope;
        Graphics g;
        Brush[] ArrayBrush = { Brushes.White,
                               Brushes.Red,
                               Brushes.Blue,
                               Brushes.Green,
                               Brushes.CornflowerBlue,
                               Brushes.Brown,
                               Brushes.BlueViolet,
                               Brushes.Coral,
                               Brushes.Chocolate};
        Bitmap[] ArrayBitmap;

        public Tetris(Graphics pov, int Row, int Col)
        {
            g = pov;
            RowCount = Row;
            ColCount = Col;

            GameMap = new int[RowCount, ColCount];

            int i, j;
            for (i = 0; i < RowCount; i++)
                for (j = 0; j < ColCount; j++) GameMap[i, j] = 0;

            ArrayBitmap = new Bitmap[ArrayBrush.Length];

            for (i = 0; i < ArrayBitmap.Length; i++)
            {
                ArrayBitmap[i] = new Bitmap(CellWidth, CellWidth);

                Graphics rectBitmap = Graphics.FromImage(ArrayBitmap[i]);
                //на поверности квадратика рисуем закрашенный прямоугольник соответсвующего цвета
                rectBitmap.FillRectangle(ArrayBrush[i], 0, 0, CellWidth, CellWidth);
                //вокру прямоугольника рисуем черную рамку
                rectBitmap.DrawRectangle(Pens.Black, 0, 0, CellWidth, CellWidth);

            }

            NextLevel = ColCount * Level * 10;
            //случайным образом создаем следующую фигуру
            switch (rnd.Next(CountFigur))
            {
                case 0:
                    {
                        Nextfigura = new Figura_0(); break;
                    }
                case 1:
                    {
                        Nextfigura = new Figura_1(); break;
                    }
                case 2:
                    {
                        Nextfigura = new Figura_2(); break;
                    }
                case 3:
                    {
                        Nextfigura = new Figura_3(); break;
                    }
                case 4:
                    {
                        Nextfigura = new Figura_4(); break;
                    }
                case 5:
                    {
                        Nextfigura = new Figura_5(); break;
                    }
                case 6:
                    {
                        Nextfigura = new Figura_6(); break;
                    }
                case 7:
                    {
                        Nextfigura = new Figura_7();
                        break;
                    }
            }

            PreviewFigura(Nextfigura);
            //располаем созданную фигуру выше игрового поля
            Nextfigura.tj = -Nextfigura.N;
            Nextfigura.ti = (ColCount - Nextfigura.M) / 2;
            //создаем определяем параметры меток для вывода информации об уровне, количестве линий и баллов
            Font font = new Font("Arial", 14, FontStyle.Bold);
            int h = (RowCount * CellWidth - 6 * CellWidth) / 4;
            int y = Dy + 6 * CellWidth + h;
            lLevel = new Label();
            lLevel.Font = font;
            lLevel.AutoSize = true;
            lLevel.Left = Dx + ColCount * CellWidth + 10;
            lLevel.Top = y;
            lLines = new Label();
            lLines.Font = font;
            lLines.AutoSize = true;

            lLines.Left = lLevel.Left;
            lLines.Top = y + h;
            lScope = new Label();
            lScope.Font = font;
            lScope.AutoSize = true;
            lScope.Left = lLevel.Left;
            lScope.Top = y + 2 * h;
            //выводим в созданные метки стартовые значения
            ShowLabel();
            timer =  new Timer();//создаем таймер
            timer.Enabled = false;//выключаем таймер
            timer.Interval = 500;//устанавливаем начальный интервал срабатывания таймера
            timer.Tick += Timer_Tick;//подключаем обработчик таймера
            timer.Enabled = true;
        }

        /// <summary>
        /// Таймер.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (currenFigura == null)
            {

                //в текущей сделать следующую фигуру
                currenFigura = Nextfigura;
                //случайно выбрать новую следующую фигуру
                switch (rnd.Next(CountFigur))
                {
                    case 0:
                        {
                            Nextfigura = new Figura_0();
                            break;
                        }
                    case 1:
                        {
                            Nextfigura = new Figura_1();
                            break;
                        }
                    case 2:
                        {
                            Nextfigura = new Figura_2();
                            break;
                        }
                    case 3:
                        {
                            Nextfigura = new Figura_3();
                            break;
                        }
                    case 4:
                        {
                            Nextfigura = new Figura_4();
                            break;
                        }
                    case 5:
                        {
                            Nextfigura = new Figura_5();
                            break;
                        }
                    case 6:
                        {
                            Nextfigura = new Figura_6();
                            break;
                        }
                    case 7:
                        {
                            Nextfigura = new Figura_7();
                            break;
                        }

                }

                PreviewFigura(Nextfigura);

                currenFigura.ti = -currenFigura.N;
                currenFigura.tj = (ColCount - currenFigura.M) / 2;
            }
            else
            {

                bool f = true;
                int i, j;
                if (currenFigura.ti + currenFigura.N == RowCount)
                    f = false;
                else
                {
                    //перебираем двумерный массив фигуры
                    for (i = 0; i < currenFigura.N; i++)
                        for (j = 0; j < currenFigura.M; j++)
                            //если перебираемая строка массива фигуры уже показалась или сейчас покажется в игровом поле, то
                            if (i + currenFigura.ti + 1 >= 0)
                                //если в игровом поле внизу клетки с квадратиком фигуры есть другой квадратик, то
                                if (GameMap[i + currenFigura.ti + 1, j + currenFigura.tj] > 0 && currenFigura.CurrentFigurs[i, j] > 0)
                                    f = false;//дальше фигура двигаться не может
                }

                if (f)
                {
                    currenFigura.ti = currenFigura.ti + 1;
                    Show();
                }
                else
                {
                    for (i = 0; i < currenFigura.N; i++)
                        for (j = 0; j < currenFigura.M; j++)
                        {
                            if (currenFigura.CurrentFigurs[i, j] > 0 && currenFigura.ti + i >= 0)
                                GameMap[currenFigura.ti + i, currenFigura.tj + j] = currenFigura.CurrentFigurs[i, j];

                            currenFigura.CurrentFigurs[i, j] = 0;
                        }


                    i = RowCount - 1;
                    while (i >= 0)
                    {
                        int k = 0;

                        for (j = 0; j < ColCount; j++)
                            if (GameMap[i, j] == 0) k++;//подсчитываем количество пустых клеток в строке

                        if (k == 0)
                        {
                            //заполняем нулями клетки удалямой строки
                            for (j = 0; j < ColCount; j++)
                                GameMap[i, j] = 0;
                            //посчитываем количество баллов и линий
                            Scope += ColCount * Level;
                            CountDestroyLine += 1;
                            //если количество баллов превышает новый уровень, то
                            if (Scope >= NextLevel)
                            {
                                //если интервал таймера не меньше 100 мс, то уменьшаем скорость таймера на 50 мс
                                if (timer.Interval >= 100)
                                    timer.Interval -= 50;
                                Level += 1;
                                NextLevel *= 2;
                            }


                            ShowLabel();

                            for (k = i; k > 0; k--)
                                for (j = 0; j < ColCount; j++)
                                    GameMap[k, j] = GameMap[k - 1, j];

                            Show();//перерисовываем игровое поле
                        }
                        else i--;
                    }

                    if (currenFigura.ti <= 0)
                    {
                        timer.Enabled = false;
                        var rez = MessageBox.Show("Хотите начать новую игру?", "Игра закончена", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                        if (rez == DialogResult.Yes)
                        {
                            //то очищаем игровое поле
                            for (i = 0; i < RowCount; i++)
                                for (j = 0; j < ColCount; j++)
                                    GameMap[i, j] = 0;
                            //восстанавливаем начальные параметры игры
                            currenFigura = null;
                            timer.Enabled = true;
                            timer.Interval = 500;
                            Level = 1;
                            Scope = 0;
                            CountDestroyLine = 0;
                            ShowLabel();
                        }
                        else Application.Exit();//выход из игры
                        Show();//если не вышли, то началась новая игра, и вызваем функцию прорисовки игрового поля
                    }
                    currenFigura = null;//уничтожаем упавшую фигуру
                }
            }

        }

        /// <summary>
        /// Показ следующей фигуры.
        /// </summary>
        /// <param name="f"></param>
        public void PreviewFigura(MainFigura figura)
        {

            int x1 = Dx + ColCount * CellWidth + 10,
                y1 = Dy;

            g.FillRectangle(Brushes.White, x1, y1, 6 * CellWidth, 6 * CellWidth);

            int otstx = x1 + (6 - figura.M) * CellWidth / 2, otsty = Dy + (6 - figura.N) * CellWidth / 2;
            //перебор ячеек массива, хранящего информацию о фигуре
            for (int i = 0; i < figura.N; i++)
                for (int j = 0; j < figura.M; j++)
                    if (figura.CurrentFigurs[i, j] > 0)//если ячейка не пустая, то
                        g.DrawImage(ArrayBitmap[figura.CurrentFigurs[i, j]], otstx + j * CellWidth, otsty + i * CellWidth, CellWidth, CellWidth);
        }

        /// <summary>
        /// Рисование игрового поля.
        /// </summary>
        public void Show()
        {
            //перерисовываем массив
            int i, j;
            int nb;

            for (i = 0; i < RowCount; i++)
                for (j = 0; j < ColCount; j++)
                {
                    if (currenFigura == null)
                        nb = GameMap[i, j];
                    else
                        if (i >= currenFigura.ti && i < currenFigura.ti + currenFigura.N && j >= currenFigura.tj && j < currenFigura.tj + currenFigura.M)
                        //если клетка фигуры не пустая, то
                            if (currenFigura.CurrentFigurs[i - currenFigura.ti, j - currenFigura.tj] > 0)
                             nb = currenFigura.CurrentFigurs[i - currenFigura.ti, j - currenFigura.tj];

                        else
                            nb = GameMap[i, j];
                    else
                        //иначе, если выводимая клетка не попадает на фигуру, то
                        //цвет квадрата также определяем по клетке игрового поля
                        nb = GameMap[i, j];

                    g.DrawImage(ArrayBitmap[nb], j * CellWidth + Dx, i * CellWidth + Dy);
                }
        }

        /// <summary>
        /// Изменение надписи.
        /// </summary>
        public void ShowLabel()
        {
            lLevel.Text = "Уровень: " + Convert.ToString(Level);
            lLines.Text = "Линий: " + Convert.ToString(CountDestroyLine);
            lScope.Text = "Очки: " + Convert.ToString(Scope);
        }

    }
}