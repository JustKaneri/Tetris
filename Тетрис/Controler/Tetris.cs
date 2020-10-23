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
        /// <summary>
        /// Максимальное кол-во фигур.
        /// </summary>
        private const int CountFigur = 8;

        /// <summary>
        /// Игровая карта.
        /// </summary>
        public int[,] GameMap;

        /// <summary>
        /// Кол-во строк и столбцов.
        /// </summary>
        public int RowCount,
                   ColCount;

        /// <summary>
        /// Размер ячейки
        /// </summary>
        public int CellWidth = 25;

        /// <summary>
        /// Отступы.
        /// </summary>
        public int Dx = 0,
                   Dy = 0;

        /// <summary>
        /// Старый интервал таймера.
        /// </summary>
        public int BaceInterval;
        public int Level = 1;

        /// <summary>
        /// Рекорд.
        /// </summary>
        public int Scope = 0;

        /// <summary>
        /// Кол-во уничтоженных линий.
        /// </summary>
        public int CountDestroyLine = 0;
        
        /// <summary>
        /// Кол-во балов для следующего уровня.
        /// </summary>
        public int NextLevel;
        
        //работы таймера после сброса фигуры
        public bool RestoreInterval = false;
        public bool Pause = false;
        public bool GameOver = false;
        public Timer timer;

        /// <summary>
        /// Выбранная фигура.
        /// </summary>
        public MainFigura currenFigura = null;

        /// <summary>
        /// Следующая фигура.
        /// </summary>
        public MainFigura Nextfigura = null;

        public Label lLevel,
                     lLines,
                     lScope;
        Graphics g;

        /// <summary>
        /// Массив кистей.
        /// </summary>
        Brush[] ArrayBrush = { Brushes.LightBlue,
                               Brushes.Red,
                               Brushes.Blue,
                               Brushes.Green,
                               Brushes.CornflowerBlue,
                               Brushes.Brown,
                               Brushes.BlueViolet,
                               Brushes.Coral,
                               Brushes.Chocolate};

        /// <summary>
        /// Массив цветных кубиков.
        /// </summary>
        Bitmap[] ArrayBitmap;

        public Tetris(Graphics pov, int Row, int Col)
        {
            g = pov;
            RowCount = Row;
            ColCount = Col;

            GameMap = new int[RowCount, ColCount];

            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                    GameMap[i, j] = 0;

            ArrayBitmap = new Bitmap[ArrayBrush.Length];

            for (int i = 0; i < ArrayBitmap.Length; i++)
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
                currenFigura = Nextfigura;

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
            int x1 = Dx + ColCount * CellWidth + 10;
            int y1 = Dy;

            g.FillRectangle(Brushes.White, x1, y1, 6 * CellWidth, 6 * CellWidth);

            int otstX = x1 + (6 - figura.M) * CellWidth / 2;
            int otstY = y1 + (6 - figura.N) * CellWidth / 2;

            for (int i = 0; i < figura.N; i++)
                for (int j = 0; j < figura.M; j++)
                    if (figura.CurrentFigurs[i, j] > 0)
                        g.DrawImage(ArrayBitmap[figura.CurrentFigurs[i, j]], otstX + j * CellWidth, otstY + i * CellWidth, CellWidth, CellWidth);
        }

        /// <summary>
        /// Рисование игрового поля.
        /// </summary>
        public void Show()
        {
            //Номер выбраного блока.
            int NumberBlock;

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColCount; j++)
                {
                    if (currenFigura == null)
                        NumberBlock = GameMap[i, j];
                    else
                    {
                        if (i >= currenFigura.ti && i < currenFigura.ti + currenFigura.N &&
                            j >= currenFigura.tj && j < currenFigura.tj + currenFigura.M)
                        {
                            if (currenFigura.CurrentFigurs[i - currenFigura.ti, j - currenFigura.tj] > 0)
                                NumberBlock = currenFigura.CurrentFigurs[i - currenFigura.ti, j - currenFigura.tj];
                            else
                                NumberBlock = GameMap[i, j];
                        }
                        else
                            NumberBlock = GameMap[i, j];
                    }


                    g.DrawImage(ArrayBitmap[NumberBlock], j * CellWidth + Dx, i * CellWidth + Dy);
                }
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