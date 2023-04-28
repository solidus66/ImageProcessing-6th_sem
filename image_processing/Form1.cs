using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ipbak23
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(@"C:\Users\solidus66\OneDrive\ВГУ\3 курс 2 сем\Обработка изображений\tasks\project\images\cus.jpg");
            pictureBox1.Image = bmp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)pictureBox1.Image,
                bmp1 = new Bitmap(bmp.Width, bmp.Height);
            for (int y = 0; y < bmp.Height; ++y)
                for (int x = 0; x < bmp.Width; ++x)
                {
                    Color cl = bmp.GetPixel(x, y);
                    int r, g, b;
                    r = cl.R; g = cl.G; b = cl.B;
                    g = 0; b = 0;
                    if (r < 0) r = 0; else if (r > 255) r = 255;
                    if (g < 0) g = 0; else if (g > 255) g = 255;
                    if (b < 0) b = 0; else if (b > 255) b = 255;

                    //int br = (r+g+b)/3;
                    //if (br < 0) br = 0; else if (br > 255) br = 255
                    //cl = Color.FromArgb(br, br, br);

                    cl = Color.FromArgb(r, g, b);
                    bmp1.SetPixel(x, y, cl);
                };
            pictureBox2.Image = bmp1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double d = 100;
            Random rnd = new Random();
            Bitmap bmp = (Bitmap)pictureBox1.Image,
                bmp1 = new Bitmap(bmp.Width, bmp.Height);
            for (int y = 0; y < bmp.Height; ++y)
                for (int x = 0; x < bmp.Width; ++x)
                {
                    Color cl = bmp.GetPixel(x, y);
                    int r, g, b;
                    r = cl.R; g = cl.G; b = cl.B;

                    double ns = 0;
                    for (int i = 0; i < 12; ++i)
                        ns += rnd.NextDouble();
                    ns -= 6;

                    //r += Convert.ToInt32(rnd.NextDouble() * d - d/2);
                    //g += Convert.ToInt32(rnd.NextDouble() * d - d/2);
                    //b += Convert.ToInt32(rnd.NextDouble() * d - d/2);

                    // для чб шума убрать цикл у r & g , тк для чб шума достаточно одного числа
                    // работаем с шумом: 1) аддитивный; 2) гаусовский; 3) белый;
                    r += Convert.ToInt32(ns * d);
                    ns = 0;
                    for (int i = 0; i < 12; ++i)
                        ns += rnd.NextDouble();
                    ns -= 6;
                    g += Convert.ToInt32(ns * d);
                    ns = 0;
                    for (int i = 0; i < 12; ++i)
                        ns += rnd.NextDouble();
                    ns -= 6;
                    b += Convert.ToInt32(ns * d);

                    if (r < 0) r = 0; else if (r > 255) r = 255;
                    if (g < 0) g = 0; else if (g > 255) g = 255;
                    if (b < 0) b = 0; else if (b > 255) b = 255;

                    cl = Color.FromArgb(r, g, b);
                    bmp1.SetPixel(x, y, cl);
                };
            pictureBox2.Image = bmp1;
        }
        // Фильтр низких частот
        private void button4_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)pictureBox1.Image,
                bmp1 = new Bitmap(bmp.Width, bmp.Height);

            for (int y = 2; y < bmp.Height - 2; ++y)
                for (int x = 2; x < bmp.Width - 2; ++x)
                {
                    double rs = 0, gs = 0, bs = 0;
                    int r, g, b; Color cl;

                    for (int i = -2; i <= 2; ++i)
                        for (int j = -2; j <= 2; ++j)
                        {
                            cl = bmp.GetPixel(x + i, y + j);
                            r = cl.R; g = cl.G; b = cl.B;
                            rs += r * 0.04;
                            gs += g * 0.04;
                            bs += b * 0.04;
                        }

                    r = Convert.ToInt32(rs);
                    g = Convert.ToInt32(gs);
                    b = Convert.ToInt32(bs);

                    if (r < 0) r = 0; else if (r > 255) r = 255;
                    if (g < 0) g = 0; else if (g > 255) g = 255;
                    if (b < 0) b = 0; else if (b > 255) b = 255;

                    cl = Color.FromArgb(r, g, b);
                    bmp1.SetPixel(x, y, cl);
                };
            pictureBox2.Image = bmp1;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // Линейная Низкочастотная Фильтрация - Коэффициенты 1
        private void button5_Click(object sender, EventArgs e)
        {
            double d = Double.Parse(textBox1.Text); // создаем окошко для удобного ввода любого d 

            Random rnd = new Random();
            Bitmap bmp = (Bitmap)pictureBox1.Image;
            Bitmap bmp1 = new Bitmap(bmp.Width, bmp.Height);

            for (int y = 0; y < bmp.Height; ++y) // циклы как мы делали в шаблоне - для зашумления 
                for (int x = 0; x < bmp.Width; ++x)
                {
                    Color cl = bmp.GetPixel(x, y);
                    int r, g, b;
                    r = cl.R;
                    g = cl.G;
                    b = cl.B;
                    double ns = 0;

                    for (int i = 0; i < 12; ++i)
                        ns += rnd.NextDouble() * d - d / 2;
                    ns -= 6;
                    r += Convert.ToInt32(ns);
                    g += Convert.ToInt32(ns);
                    b += Convert.ToInt32(ns);

                    if (r < 0) r = 0; else if (r > 255) r = 255;
                    if (g < 0) g = 0; else if (g > 255) g = 255;
                    if (b < 0) b = 0; else if (b > 255) b = 255;

                    cl = Color.FromArgb(r, g, b);
                    bmp1.SetPixel(x, y, cl);
                };
            pictureBox1.Image = (Bitmap)bmp1.Clone();

            double w3 = 0.000, w2 = 0.070, w1 = 0.440; // коэффициенты из методички

            double[,] h = { { w3, w3, w3, w3, w3 },
                            { w3, w2, w2, w2, w3 },
                            { w3, w2, w1, w2, w3 },
                            { w3, w2, w2, w2, w3 },
                            { w3, w3, w3, w3, w3 }
                          };


            for (int y = 2; y < bmp.Height - 2; ++y)
                for (int x = 2; x < bmp.Width - 2; ++x)
                {
                    double rs = 0, gs = 0, bs = 0;
                    Color cl;
                    int r, g, b;

                    for (int i = -2; i <= 2; ++i)
                        for (int j = -2; j <= 2; ++j)
                        {
                            cl = bmp.GetPixel(x + i, y + j);
                            r = cl.R; g = cl.G; b = cl.B;

                            rs += r * h[i + 2, j + 2];
                            gs += g * h[i + 2, j + 2];
                            bs += b * h[i + 2, j + 2];

                        }

                    r = Convert.ToInt32(rs);
                    g = Convert.ToInt32(gs);
                    b = Convert.ToInt32(bs);

                    if (r < 0) r = 0; else if (r > 255) r = 255;
                    if (g < 0) g = 0; else if (g > 255) g = 255;
                    if (b < 0) b = 0; else if (b > 255) b = 255;

                    cl = Color.FromArgb(r, g, b);
                    bmp1.SetPixel(x, y, cl);
                };
            pictureBox2.Image = bmp1;
        }

        // Линейная Низкочастотная Фильтрация - Коэффициенты 2, для большей зашумленности
        private void button6_Click(object sender, EventArgs e)
        {
            double d = Double.Parse(textBox1.Text);

            Random rnd = new Random();
            Bitmap bmp = (Bitmap)pictureBox1.Image;
            Bitmap bmp1 = new Bitmap(bmp.Width, bmp.Height);
            for (int y = 0; y < bmp.Height; ++y)
                for (int x = 0; x < bmp.Width; ++x)
                {
                    Color cl = bmp.GetPixel(x, y);
                    int r, g, b;
                    r = cl.R;
                    g = cl.G;
                    b = cl.B;
                    double ns = 0;

                    for (int i = 0; i < 12; ++i)
                        ns += rnd.NextDouble() * d - d / 2;
                    ns -= 6;
                    r += Convert.ToInt32(ns);
                    g += Convert.ToInt32(ns);
                    b += Convert.ToInt32(ns);

                    if (r < 0) r = 0; else if (r > 255) r = 255;
                    if (g < 0) g = 0; else if (g > 255) g = 255;
                    if (b < 0) b = 0; else if (b > 255) b = 255;

                    cl = Color.FromArgb(r, g, b);
                    bmp1.SetPixel(x, y, cl);
                };
            pictureBox1.Image = (Bitmap)bmp1.Clone();

            double w3 = 0.025, w2 = 0.060, w1 = 0.060;

            double[,] h = { { w3, w3, w3, w3, w3 },
                            { w3, w2, w2, w2, w3 },
                            { w3, w2, w1, w2, w3 },
                            { w3, w2, w2, w2, w3 },
                            { w3, w3, w3, w3, w3 }
                          };

            for (int y = 2; y < bmp.Height - 2; ++y)
                for (int x = 2; x < bmp.Width - 2; ++x)
                {
                    double rs = 0, gs = 0, bs = 0;
                    Color cl;
                    int r, g, b;

                    for (int i = -2; i <= 2; ++i)
                        for (int j = -2; j <= 2; ++j)
                        {
                            cl = bmp.GetPixel(x + i, y + j);
                            r = cl.R; g = cl.G; b = cl.B;

                            rs += r * h[i + 2, j + 2];
                            gs += g * h[i + 2, j + 2];
                            bs += b * h[i + 2, j + 2];

                        }

                    r = Convert.ToInt32(rs);
                    g = Convert.ToInt32(gs);
                    b = Convert.ToInt32(bs);

                    if (r < 0) r = 0; else if (r > 255) r = 255;
                    if (g < 0) g = 0; else if (g > 255) g = 255;
                    if (b < 0) b = 0; else if (b > 255) b = 255;

                    cl = Color.FromArgb(r, g, b);
                    bmp1.SetPixel(x, y, cl);
                };
            pictureBox2.Image = bmp1;
        }

        // ЛНФ с "цветным" шумом
        private void button7_Click(object sender, EventArgs e)
        {
            double d = Double.Parse(textBox1.Text);

            Random rnd = new Random();
            Bitmap bmp = (Bitmap)pictureBox1.Image;
            Bitmap bmp1 = new Bitmap(bmp.Width, bmp.Height);

            for (int y = 0; y < bmp.Height; ++y)
                for (int x = 0; x < bmp.Width; ++x)
                {
                    Color cl = bmp.GetPixel(x, y);
                    int r, g, b;
                    r = cl.R; g = cl.G; b = cl.B;

                    double ns = 0;
                    for (int i = 0; i < 12; ++i)
                        ns += rnd.NextDouble();
                    ns -= 6;

                    r += Convert.ToInt32(ns * d);
                    ns = 0;
                    for (int i = 0; i < 12; ++i)
                        ns += rnd.NextDouble();
                    ns -= 6;
                    g += Convert.ToInt32(ns * d);
                    ns = 0;
                    for (int i = 0; i < 12; ++i)
                        ns += rnd.NextDouble();
                    ns -= 6;
                    b += Convert.ToInt32(ns * d);

                    if (r < 0) r = 0; else if (r > 255) r = 255;
                    if (g < 0) g = 0; else if (g > 255) g = 255;
                    if (b < 0) b = 0; else if (b > 255) b = 255;

                    cl = Color.FromArgb(r, g, b);
                    bmp1.SetPixel(x, y, cl);
                };
            pictureBox1.Image = (Bitmap)bmp1.Clone();

            double w3 = 0.000, w2 = 0.070, w1 = 0.440;

            double[,] h = { { w3, w3, w3, w3, w3 },
                            { w3, w2, w2, w2, w3 },
                            { w3, w2, w1, w2, w3 },
                            { w3, w2, w2, w2, w3 },
                            { w3, w3, w3, w3, w3 }
                          };

            for (int y = 2; y < bmp.Height - 2; ++y)
                for (int x = 2; x < bmp.Width - 2; ++x)
                {
                    double rs = 0, gs = 0, bs = 0;
                    Color cl;
                    int r, g, b;

                    for (int i = -2; i <= 2; ++i)
                        for (int j = -2; j <= 2; ++j)
                        {
                            cl = bmp.GetPixel(x + i, y + j);
                            r = cl.R; g = cl.G; b = cl.B;

                            rs += r * h[i + 2, j + 2];
                            gs += g * h[i + 2, j + 2];
                            bs += b * h[i + 2, j + 2];

                        }

                    r = Convert.ToInt32(rs);
                    g = Convert.ToInt32(gs);
                    b = Convert.ToInt32(bs);

                    if (r < 0) r = 0; else if (r > 255) r = 255;
                    if (g < 0) g = 0; else if (g > 255) g = 255;
                    if (b < 0) b = 0; else if (b > 255) b = 255;

                    cl = Color.FromArgb(r, g, b);
                    bmp1.SetPixel(x, y, cl);
                };
            pictureBox2.Image = bmp1;
        }

        // треугольник максвелла
        private void button8_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(256, 256);
            for (int y = 0; y < 256; ++y)
                for (int x = 0; x < 256 - y; ++x)
                {
                    int r = x, g = y, b = 255 - x - y;
                    Color cl = Color.FromArgb(r, g, b);
                    bmp.SetPixel(x, y, cl);
                };
            pictureBox1.Image = bmp;
        }

        // шум соль-перец
        private void button9_Click(object sender, EventArgs e)
        {
            int d = 500000;
            Random rnd = new Random();
            Bitmap bmp = (Bitmap)pictureBox1.Image,
                bmp1 = bmp;
            for (int i = 0; i < d; ++i)
            {
                int x = Convert.ToInt32(rnd.NextDouble() * (bmp.Width - 1));
                int y = Convert.ToInt32(rnd.NextDouble() * (bmp.Height - 1));

                if (rnd.NextDouble() > 0.5)
                    bmp1.SetPixel(x, y, Color.Black); // перец
                else
                    bmp1.SetPixel(x, y, Color.White); // соль

            }
            pictureBox2.Image = bmp1;
        }

        // медианный фильтр
        private void button10_Click(object sender, EventArgs e)
        {
            {
                int d = int.Parse(textBox1.Text); // создаем окошко для удобного ввода любого d 

                Random rnd = new Random();
                Bitmap bmp = (Bitmap)pictureBox1.Image,
                bmp1 = bmp;

                // ШУМ
                for (int i = 0; i < d; ++i)
                {
                    int x = Convert.ToInt32(rnd.NextDouble() * (bmp.Width - 1));
                    int y = Convert.ToInt32(rnd.NextDouble() * (bmp.Height - 1));

                    if (rnd.NextDouble() > 0.5)
                        bmp1.SetPixel(x, y, Color.Black);
                    else
                        bmp1.SetPixel(x, y, Color.White);

                }
                pictureBox1.Image = (Bitmap)bmp1.Clone();

                // [25] т.к. маска 5x5
                int[] ra = new int[25];
                int[] ga = new int[25];
                int[] ba = new int[25];

                // ФИЛЬТР
                for (int y = 2; y < bmp.Height - 2; ++y)
                    for (int x = 2; x < bmp.Width - 2; ++x)
                    {
                        int r, g, b, k = 0; Color cl;
                        for (int i = -2; i <= 2; ++i)
                            for (int j = -2; j <= 2; ++j)
                            {
                                cl = bmp.GetPixel(x + i, y + j);
                                r = cl.R; g = cl.G; b = cl.B;
                                ra[k] = r;
                                ga[k] = g;
                                ba[k++] = b;
                            }

                        Array.Sort(ra); Array.Sort(ga); Array.Sort(ba);

                        // [12] т.к. выбираем эл-т расположенный по середине
                        int rm = ra[12], gm = ga[12], bm = ba[12];

                        r = Convert.ToInt32(rm);
                        g = Convert.ToInt32(gm);
                        b = Convert.ToInt32(bm);

                        if (r < 0) r = 0; else if (r > 255) r = 255;
                        if (g < 0) g = 0; else if (g > 255) g = 255;
                        if (b < 0) b = 0; else if (b > 255) b = 255;

                        cl = Color.FromArgb(r, g, b);
                        bmp1.SetPixel(x, y, cl);
                    };
                pictureBox2.Image = bmp1;
            }
        }

        // Подчёркивание границ (+ шум цветной)
        private void button11_Click(object sender, EventArgs e)
        {
            double d = Double.Parse(textBox1.Text);

            Random rnd = new Random();
            Bitmap bmp = (Bitmap)pictureBox1.Image,
            bmp1 = new Bitmap(bmp.Width, bmp.Height);

            // ШУМ
            for (int y = 0; y < bmp.Height; ++y)
                for (int x = 0; x < bmp.Width; ++x)
                {
                    Color cl = bmp.GetPixel(x, y);
                    int r, g, b;
                    r = cl.R; g = cl.G; b = cl.B;

                    double ns = 0;
                    for (int i = 0; i < 12; ++i)
                        ns += rnd.NextDouble();
                    ns -= 6;

                    r += Convert.ToInt32(ns * d);
                    ns = 0;
                    for (int i = 0; i < 12; ++i)
                        ns += rnd.NextDouble();
                    ns -= 6;
                    g += Convert.ToInt32(ns * d);
                    ns = 0;
                    for (int i = 0; i < 12; ++i)
                        ns += rnd.NextDouble();
                    ns -= 6;
                    b += Convert.ToInt32(ns * d);

                    if (r < 0) r = 0; else if (r > 255) r = 255;
                    if (g < 0) g = 0; else if (g > 255) g = 255;
                    if (b < 0) b = 0; else if (b > 255) b = 255;

                    cl = Color.FromArgb(r, g, b);
                    bmp1.SetPixel(x, y, cl);
                };
            pictureBox1.Image = (Bitmap)bmp1.Clone();

            // ФИЛЬТР
            for (int y = 1; y < bmp.Height - 1; ++y)
                for (int x = 1; x < bmp.Width - 1; ++x)
                {
                    double rs = 0, gs = 0, bs = 0;
                    int r, g, b; Color cl;

                    double[,] filter = new double[,] { { -1, -1, -1 },
                                                       { -1,  9, -1 },
                                                       { -1, -1, -1 }
                                                     };

                    for (int i = -1; i <= 1; ++i)
                        for (int j = -1; j <= 1; ++j)
                        {
                            cl = bmp.GetPixel(x + i, y + j);
                            r = cl.R; g = cl.G; b = cl.B;
                            rs += r * filter[i + 1, j + 1];
                            gs += g * filter[i + 1, j + 1];
                            bs += b * filter[i + 1, j + 1];
                        }

                    r = Convert.ToInt32(rs);
                    g = Convert.ToInt32(gs);
                    b = Convert.ToInt32(bs);

                    if (r < 0) r = 0; else if (r > 255) r = 255;
                    if (g < 0) g = 0; else if (g > 255) g = 255;
                    if (b < 0) b = 0; else if (b > 255) b = 255;

                    cl = Color.FromArgb(r, g, b);
                    bmp1.SetPixel(x, y, cl);
                }
            pictureBox2.Image = bmp1;
        }

        private void button11_Click_1(object sender, EventArgs e)
        {

        }
    }
}