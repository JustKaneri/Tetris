using System;
using System.Drawing;
using System.Windows.Forms;
using Тетрис.Controler;

namespace Тетрис
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Tetris tetris;

        /// <summary>
        /// Загрузка формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            tetris = new Tetris(Graphics.FromHwnd(this.Handle),20,10);

            this.Width = tetris.Dx + tetris.ColCount * tetris.CellWidth + tetris.CellWidth * 6 + 25;
            this.Height = tetris.Dy + tetris.RowCount * tetris.CellWidth + 37;

            lPause.Left = tetris.Dx + (tetris.ColCount * tetris.CellWidth - lPause.Width) / 2;
            lPause.Top = tetris.Dy + (tetris.RowCount * tetris.CellWidth - lPause.Height) / 2;

            this.Controls.Add(tetris.lLevel);
            this.Controls.Add(tetris.lLines);
            this.Controls.Add(tetris.lScope);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Down:
                    if(!tetris.RestoreInterval)
                    {
                        tetris.BaceInterval = tetris.timer.Interval;
                        tetris.timer.Interval = 10;
                        tetris.RestoreInterval = true;
                    }
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            bool f;

            switch(e.KeyCode)
            {
                case Keys.Left:

                    f = true;

                    if (tetris.currenFigura == null || tetris.Pause)
                    {
                        f = false;
                    }
                    else 
                    if (tetris.currenFigura.tj == 0)
                    {
                        f = false;
                    }
                    else
                    {
                        for (int i = 0; i < tetris.currenFigura.N; i++)
                        {
                            if(tetris.currenFigura.ti + i >= 0)
                            {
                                for (int j = 0; j < tetris.currenFigura.M; j++)
                                {
                                    if (tetris.currenFigura.CurrentFigurs[i, j] != 0 && 
                                        tetris.GameMap[tetris.currenFigura.ti + i, tetris.currenFigura.tj + j - 1] != 0)
                                        f = false;

                                }
                            }
                        }

                        
                    }

                    if(f)
                    {
                        tetris.currenFigura.tj = tetris.currenFigura.tj - 1;
                        tetris.Show();
                    }

                    break;

                case Keys.Right://Отжата клавиша ВПРАВО
                                //проверка и движение фигуры вправо (аналогично движению влево)
                    f = true;
                    if (tetris.currenFigura == null || tetris.Pause)
                        f = false;
                    else
                    if (tetris.currenFigura.tj + tetris.currenFigura.M == tetris.ColCount)
                        f = false;
                    else
                    {
                        for (int i = 0; i < tetris.currenFigura.N; i++)
                            if (tetris.currenFigura.ti + i >= 0)
                                for (int j = 0; j < tetris.currenFigura.M; j++)
                                    if (tetris.currenFigura.CurrentFigurs[i, j] != 0 && tetris.GameMap[tetris.currenFigura.ti + i, tetris.currenFigura.tj + j + 1] != 0)
                                        f = false;
                    }
                    if (f)
                    {
                        tetris.currenFigura.tj = tetris.currenFigura.tj + 1;
                        tetris.Show();
                    }
                    break;

                case Keys.Down:
                    tetris.timer.Interval = tetris.BaceInterval;
                    tetris.RestoreInterval = false;
                    break;


                case Keys.Up:

                    if (tetris.currenFigura != null && !tetris.Pause)
                    {
                        if(tetris.currenFigura.N != tetris.currenFigura.M)
                        {
                            int Ci, 
                                Cj, 
                                Li = 0, 
                                Lj = 0,
                                Pi,
                                Pj,
                                K=0;

                            Ci = tetris.currenFigura.ti + tetris.currenFigura.N / 2;
                            Cj = tetris.currenFigura.tj + tetris.currenFigura.M / 2;

                            Li = Ci - tetris.currenFigura.M / 2;
                            Lj = Cj - tetris.currenFigura.N / 2;

                            Pi = Li + tetris.currenFigura.M - 1;
                            Pj = Lj + tetris.currenFigura.N - 1;

                            if (Li >= 0 && Lj >= 0 && Pi < tetris.RowCount && Pj < tetris.ColCount)
                            {
                                for (int i = Li; i <= Pi; i++)
                                {
                                    for (int j = Lj; j <= Pj; j++)
                                    {
                                        if (tetris.GameMap[i, j] > 0)
                                            K++;
                                    }
                                }

                                if(K == 0)
                                {
                                    tetris.currenFigura.ti = Li;
                                    tetris.currenFigura.tj = Lj;
                                    //вращаем фигуру по часовой стрелке
                                    tetris.currenFigura.RoteteRight();
                                    //показываем повернутую фигуру
                                    tetris.Show();

                                }
                            }
                        }
                    }

                        break;


                case Keys.Space:

                    tetris.Show();
                    tetris.PreviewFigura(tetris.Nextfigura);
                    tetris.timer.Enabled = !tetris.timer.Enabled;
                    tetris.Pause = !tetris.timer.Enabled;
                    lPause.Visible = !tetris.timer.Enabled;
                    break;


            }

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (tetris.currenFigura != null)
            {
                tetris.Show();
                tetris.PreviewFigura(tetris.Nextfigura);
            }

        }
    }
}
