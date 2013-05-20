using System;
using System.Drawing;
using System.Windows.Forms;

namespace MO_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Neuron neuron;
        SymbolDrowed symbPic = new SymbolDrowed();
        static Bitmap btmBack = new Bitmap(10, 10);      //изображение
        static Bitmap btmFront = new Bitmap(10, 10);     //фон
        Graphics grBack = Graphics.FromImage(btmBack);
        Graphics grFront = Graphics.FromImage(btmFront);
        NeuronNet net = new NeuronNet(5, 10, 10, 1);

        /// <summary>
        /// Перечисление поддерживаемых для распознания символов.
        /// </summary>
        enum Alphabet {Г, И, Н, Я, Т};

        public void SaveToFile(string filename)
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter(filename);
            string[] lines = net.SetReadyToWrite();
            for (int i = 0; i < lines.Length; i++)
                writer.WriteLine(lines[i]);
            writer.Close();
        }

        public void LoadFromFile(string filename)
        {
            string[] lines = System.IO.File.ReadAllLines(filename);
            net = new NeuronNet(lines, 1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(System.IO.File.Exists("net.xml"))
                LoadFromFile("net.xml");
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void Обучить_Click(object sender, EventArgs e)
        {
          
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)//тоже рисуем или чистим
        {
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    grFront.FillRectangle(Brushes.Black, (float)(e.X / 25.6), (float)(e.Y / 25.6), (float)1, (float)1);
                    symbPic.SetAtXY((int)(e.X / 25.6), (int)(e.Y / 25.6));//добавляем точку к модели вх. данных
                    pictureBox1.Image = btmFront;
                    pictureBox1.BackgroundImage = btmBack;
                    pictureBox1.Refresh();
                    break;
                case System.Windows.Forms.MouseButtons.Right:
                    grFront.FillRectangle(Brushes.White, (float)(e.X / 25.6), (float)(e.Y / 25.6), (float)1, (float)1);
                    symbPic.ClearAtXY((int)(e.X / 25.6), (int)(e.Y / 25.6));//убираем точку
                    pictureBox1.Image = btmFront;
                    pictureBox1.BackgroundImage = btmBack;
                    pictureBox1.Refresh();
                    break;
            }
            if (symbPic.canIndicate)//вводим в сеть, и отображаем, что получилось...
            {
                net.SetInputData(symbPic.ReturnXs());
                bool[] res = net.CalcResult();
                listBox1.Items.Clear();
                for (int i = 0; i < res.Length; i++)
                    if (res[i])
                        listBox1.Items.Add((Alphabet)i);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            grFront.Clear(Color.White);
            symbPic.Clear();
            pictureBox1.Image = btmFront;
            pictureBox1.Refresh();
            net.Reset();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //механизм обучения
            switch (textBox1.Text)
            {
                case "Г": net.DeltaTrain((int)Alphabet.Г);
                    break;
                case "И": net.DeltaTrain((int)Alphabet.И);
                    break;
                case "Н": net.DeltaTrain((int)Alphabet.Н);
                    break;
                case "Т": net.DeltaTrain((int)Alphabet.Т);
                    break;
                case "Я": net.DeltaTrain((int)Alphabet.Я);
                    break;
            }
            SaveToFile("net.xml");
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)//рисуем или чистим
            {
                case System.Windows.Forms.MouseButtons.Left:
                    grFront.FillRectangle(Brushes.Black, (float)(e.X / 25.6), (float)(e.Y / 25.6), (float)1, (float)1);
                    symbPic.SetAtXY((int)(e.X / 25.6), (int)(e.Y / 25.6));//добавляем точку к модели вх. данных
                    pictureBox1.Image = btmFront;
                    pictureBox1.BackgroundImage = btmBack;
                    pictureBox1.Refresh();
                    break;
                case System.Windows.Forms.MouseButtons.Right:
                    grFront.FillRectangle(Brushes.White, (float)(e.X / 25.6), (float)(e.Y / 25.6), (float)1, (float)1);
                    symbPic.ClearAtXY((int)(e.X / 25.6), (int)(e.Y / 25.6));//убираем точку
                    pictureBox1.Image = btmFront;
                    pictureBox1.BackgroundImage = btmBack;
                    pictureBox1.Refresh();
                    break;
            }
            if (symbPic.canIndicate)
            {
                net.SetInputData(symbPic.ReturnXs());
                bool[] res = net.CalcResult();
                listBox1.Items.Clear();
                for (int i = 0; i < res.Length; i++)
                    if (res[i])
                        listBox1.Items.Add((Alphabet)i);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveToFile("net.xml");
        }
    }
}
