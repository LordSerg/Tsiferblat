using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tsiferblat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            bit = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bit2 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            g = Graphics.FromImage(bit);
            g2 = Graphics.FromImage(bit2);
            res1 = new bool[7];
            qwestion = new bool[7];
            NuMbEr = new bool[10][];
            for (int i = 0; i < 10; i++)
                NuMbEr[i] = new bool[7];
            //buttons = new List<Button>();
            //buttons.Add(button1);
            //buttons.Add(button2);
            //buttons.Add(button16);
            //buttons.Add(button3);
            //buttons.Add(button4);
            //buttons.Add(button5);
            //buttons.Add(button6);
            //buttons.Add(button7);
            //buttons.Add(button8);
            //buttons.Add(button9);
            //buttons.Add(button10);
            //buttons.Add(button11);
            //buttons.Add(button12);
            //buttons.Add(button13);
            //for (int i = 3; i < buttons.Count(); i++)
            //    buttons[i].Enabled = false;
            for (int i = 0; i < 7; i++)
            {
                res1[i] = false;
                NuMbEr[1][i] = false;
                NuMbEr[2][i] = true;//
                NuMbEr[3][i] = true;//
                NuMbEr[4][i] = true;//
                NuMbEr[5][i] = true;//
                NuMbEr[6][i] = true;//
                NuMbEr[7][i] = false;
                NuMbEr[8][i] = true;//
                NuMbEr[9][i] = true;//
                NuMbEr[0][i] = true;//
            }

            /*
             ____0____
            |         |
           1|        2|
            |         |
            -----3-----
            |         |
           4|        5|
            |____6____|
             */
            NuMbEr[1][2]= true;
            NuMbEr[1][5]= true;
            NuMbEr[2][1]= false;
            NuMbEr[2][5]= false;
            NuMbEr[3][1]= false;
            NuMbEr[3][4]= false;
            NuMbEr[4][0]= false;
            NuMbEr[4][4]= false;
            NuMbEr[4][6]= false;
            NuMbEr[5][2]= false;
            NuMbEr[5][4]= false;
            NuMbEr[6][2]= false;
            NuMbEr[7][0]= true;
            NuMbEr[7][2]= true;
            NuMbEr[7][5]= true;
            NuMbEr[9][4] = false;
            NuMbEr[0][3] = false;
            ShowAnswer();
            SetQwestion();
            ShowQwestion();
            lvl = 1;
            score = 0;
            label5.Text = score.ToString();
            label7.Text = lvl.ToString();
            toolTip1.SetToolTip(button1, "1+1 = 0\n1+0 = 1\n0+1 = 1\n0+0 = 0");//+
            toolTip1.SetToolTip(button2,"1-1 = 0\n1-0 = 1\n0-1 = 0\n0-0 = 0");//-
            toolTip1.SetToolTip(button3,"Sets the selected digit to answer");//set
            toolTip1.SetToolTip(button16,"1!1 = 1\n1!0 = 0\n0!1 = 0\n0!0 = 1\nIf digit isn't selected:\n1 -> 0\n0 -> 1");//!
            toolTip1.SetToolTip(button17, "1&&1 = 1\n1&&0 = 0\n0&&1 = 0\n0&&0 = 0");//&&
            toolTip1.SetToolTip(button18, "1|1 = 1\n1|0 = 1\n0|1 = 1\n0|0 = 0");//||
        }
        //List<Button> buttons;
        Bitmap bit,bit2;
        Graphics g,g2;
        bool[][] NuMbEr;
        bool[] res1;
        int score;
        int lvl;
        //bool[][] qwestion;
        bool[] qwestion;
        int act = -1;
        int digit = -1;

        //private void CheckOutInput()
        //{
        //    List<char> s = new List<char>();
        //    s = textBox1.Text.ToList<char>();
        //    for (int i = 0; i < s.Count(); i++)
        //    {
        //        if (s[i] != '+' && s[i] != '-' && (s[i] < '0' || s[i] > '9'))
        //        {
        //            s.RemoveAt(i);
        //            i--;
        //        }
        //    }
        //    //textBox1.Text = (s).ToString();
        //    string str="";
        //    foreach (char ch in s)
        //        str += ch;
        //    textBox1.Text = str;
        //}
        private void ShowAnswer()
        {//будем выводить
            g.Clear(Color.White);
            float x_c = pictureBox1.Width / 2, y_c = pictureBox1.Height / 2;
            float min = pictureBox1.Width > pictureBox1.Height ? pictureBox1.Height : pictureBox1.Width;
            float size_big = min / 4, size_small = min / 20, k = min / 100;

            if (res1[0])
                g.FillRectangle(new SolidBrush(Color.Black), x_c - (size_big / 2), y_c - size_big - size_small - k, size_big, size_small);//0
            else
                g.DrawRectangle(new Pen(Color.Black), x_c - (size_big / 2), y_c - size_big - size_small - k, size_big, size_small);//0

            if (res1[1])
                g.FillRectangle(new SolidBrush(Color.Black), x_c - (size_big / 2) - size_small - k, y_c - size_big, size_small, size_big);//1
            else
                g.DrawRectangle(new Pen(Color.Black), x_c - (size_big / 2) - size_small - k, y_c - size_big, size_small, size_big);//1

            if (res1[2])
                g.FillRectangle(new SolidBrush(Color.Black), x_c + (size_big / 2) + k, y_c - size_big, size_small, size_big);//2
            else
                g.DrawRectangle(new Pen(Color.Black), x_c + (size_big / 2) + k, y_c - size_big, size_small, size_big);//2

            if (res1[3])
                g.FillRectangle(new SolidBrush(Color.Black), x_c - (size_big / 2), y_c, size_big, size_small);//3
            else
                g.DrawRectangle(new Pen(Color.Black), x_c - (size_big / 2), y_c, size_big, size_small);//3

            if (res1[4])
                g.FillRectangle(new SolidBrush(Color.Black), x_c - (size_big / 2) - size_small - k, y_c + size_small, size_small, size_big);//4
            else
                g.DrawRectangle(new Pen(Color.Black), x_c - (size_big / 2) - size_small - k, y_c + size_small, size_small, size_big);//4

            if (res1[5])
                g.FillRectangle(new SolidBrush(Color.Black), x_c + (size_big / 2) + k, y_c + size_small, size_small, size_big);//5
            else
                g.DrawRectangle(new Pen(Color.Black), x_c + (size_big / 2) + k, y_c + size_small, size_small, size_big);//5

            if (res1[6])
                g.FillRectangle(new SolidBrush(Color.Black), x_c - (size_big / 2), y_c + size_big + size_small + k, size_big, size_small);//6
            else
                g.DrawRectangle(new Pen(Color.Black), x_c - (size_big / 2), y_c + size_big + size_small + k, size_big, size_small);//6

            pictureBox1.Image = bit;
        }
        private void ShowQwestion()
        {//будем выводить
            g2.Clear(Color.White);
            float x_c = pictureBox2.Width / 2, y_c = pictureBox2.Height / 2;
            float min = pictureBox2.Width > pictureBox2.Height ? pictureBox2.Height : pictureBox2.Width;
            float size_big = min / 4, size_small = min / 20, k = min / 100;

            if (qwestion[0])
                g2.FillRectangle(new SolidBrush(Color.Black), x_c - (size_big / 2), y_c - size_big - size_small - k, size_big, size_small);//0
            else
                g2.DrawRectangle(new Pen(Color.Black), x_c - (size_big / 2), y_c - size_big - size_small - k, size_big, size_small);//0

            if (qwestion[1])
                g2.FillRectangle(new SolidBrush(Color.Black), x_c - (size_big / 2) - size_small - k, y_c - size_big, size_small, size_big);//1
            else
                g2.DrawRectangle(new Pen(Color.Black), x_c - (size_big / 2) - size_small - k, y_c - size_big, size_small, size_big);//1

            if (qwestion[2])
                g2.FillRectangle(new SolidBrush(Color.Black), x_c + (size_big / 2) + k, y_c - size_big, size_small, size_big);//2
            else
                g2.DrawRectangle(new Pen(Color.Black), x_c + (size_big / 2) + k, y_c - size_big, size_small, size_big);//2

            if (qwestion[3])
                g2.FillRectangle(new SolidBrush(Color.Black), x_c - (size_big / 2), y_c, size_big, size_small);//3
            else
                g2.DrawRectangle(new Pen(Color.Black), x_c - (size_big / 2), y_c, size_big, size_small);//3

            if (qwestion[4])
                g2.FillRectangle(new SolidBrush(Color.Black), x_c - (size_big / 2) - size_small - k, y_c + size_small, size_small, size_big);//4
            else
                g2.DrawRectangle(new Pen(Color.Black), x_c - (size_big / 2) - size_small - k, y_c + size_small, size_small, size_big);//4

            if (qwestion[5])
                g2.FillRectangle(new SolidBrush(Color.Black), x_c + (size_big / 2) + k, y_c + size_small, size_small, size_big);//5
            else
                g2.DrawRectangle(new Pen(Color.Black), x_c + (size_big / 2) + k, y_c + size_small, size_small, size_big);//5

            if (qwestion[6])
                g2.FillRectangle(new SolidBrush(Color.Black), x_c - (size_big / 2), y_c + size_big + size_small + k, size_big, size_small);//6
            else
                g2.DrawRectangle(new Pen(Color.Black), x_c - (size_big / 2), y_c + size_big + size_small + k, size_big, size_small);//6

            pictureBox2.Image = bit2;
        }
        private void ShowWin()
        {
            g2.Clear(Color.White);
            g.Clear(Color.White);
            g.DrawString("ПОЗДРАВЛЯЮ!!! ВЫ ВЫИГРАЛИ!!!", new Font("Arial", 16), new SolidBrush(Color.Black), 0, 0);
            //g.DrawString("что-то красивое придумать, и чтобы выводилось что-то в роде 'Них * я себе, а ты крутой'", new Font("Arial", 8), new SolidBrush(Color.Black), 0, 20);
        }
        private void Add(bool[] number)
        {//на вход идет число
            int n = number.Length;
            for (int i = 0; i < n; i++)
            {
                res1[i] = !(res1[i] == number[i]);
                //1+1=0
                //1+0=1
                //0+1=1
                //0+0=0
            }
        }
        private void Minus(bool[] number)
        {//на вход идет число
            int n = number.Length;
            for (int i = 0; i < n; i++)
            {
                res1[i] = !(number[i])&&res1[i];
                //1-1 = 0
                //1-0 = 1
                //0-1 = 0
                //0-0 = 0
            }
        }
        private void Set(bool[] number)
        {
            int n = number.Length;
            for (int i = 0; i < n; i++)
            {
                res1[i] = number[i];
            }
        }
        private void Invert(bool[] number)
        {//на вход идет число
            int n = number.Length;
            for (int i = 0; i < n; i++)
            {
                res1[i] = (res1[i] == number[i]);
                //1!1 = 1
                //1!0 = 0
                //0!1 = 0
                //0!0 = 1
            }
        }
        private void Invert()
        {//инвертируем то, что есть
            for (int i = 0; i < 7; i++)
            {
                res1[i] = !res1[i];
            }
        }
        private void And(bool[] number)
        {//инвертируем то, что есть
            for (int i = 0; i < 7; i++)
            {
                res1[i] = res1[i]&&number[i];
                //1&1 = 1
                //1&0 = 0
                //0&1 = 0
                //0&0 = 0
            }
        }
        private void Or(bool[] number)
        {//инвертируем то, что есть
            for (int i = 0; i < 7; i++)
            {
                res1[i] = res1[i]||number[i];
                //1|1 = 1
                //1|0 = 1
                //0|1 = 1
                //0|0 = 0
            }
        }
        private void ChangeAnswer()
        {
            if (act == 0)
            {//бинарная сумма (1+1=0; 1+0=0+1=1; 0+0=0)
                if (digit!=-1)Add(NuMbEr[digit]);
            }
            else if (act == 1)
            {//разница (0-1=0; 1-0=1; 0-0=1-1=0)
                if (digit != -1) Minus(NuMbEr[digit]);
            }
            else if (act == 2)
            {//поставить в положение (поставить единицы и ноли в том положении, котором они должны быть)
                if (digit != -1) Set(NuMbEr[digit]);
            }
            else if (act == 3)
            {//отрицание (1!1=0; 1!0=0; 0!1=1; 0!0=0)
                if (digit != -1) Invert(NuMbEr[digit]);
                else Invert();
            }
            else if (act == 4)
            {//и
                if (digit != -1) And(NuMbEr[digit]);
            }
            else if (act == 5)
            {//или
                if (digit != -1) Or(NuMbEr[digit]);
            }
        }
        private bool CheckAnswer()
        {
            for(int i=0;i<7;i++)
                if (res1[i] != qwestion[i])
                    return false;
            return true;
        }
        private void SetQwestion()
        {
            Random r = new Random();
            for (int i = 0; i < 7; i++)
            {
                qwestion[i] = Convert.ToBoolean(r.Next(2));
            }
        }
        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            if (pictureBox1.Width > 0 && pictureBox1.Height > 0)
            {
                bit = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                g = Graphics.FromImage(bit);
                    ShowAnswer();
                if (lvl > 50)
                    ShowWin();
            }
        }
        //private void t0()
        //{//инвертируем
        //    int n = buttons.Count();
        //    for (int i = 0; i < n; i++)
        //        buttons[i].Enabled = !buttons[i].Enabled;
        //}
        //private void t1()
        //{//убираем все, кроме "+", "-" и "set"
        //    int n = buttons.Count();
        //    for (int i = 3; i < n; i++)
        //        buttons[i].Enabled = false;
        //}

        private void button1_Click(object sender, EventArgs e)
        {//+
            act = 0;
            label1.Text = "+";
            digit = -1;
        }
        private void button2_Click(object sender, EventArgs e)
        {//-
            act = 1;
            label1.Text = "-";
            digit = -1;
        }
        private void button3_Click(object sender, EventArgs e)
        {//set
            act = 2;
            label1.Text = "set";
            digit = -1;
        }
        private void button16_Click(object sender, EventArgs e)
        {//инверсия
            act = 3;
            label1.Text = "!";
            digit = -1;
        }
        private void button17_Click(object sender, EventArgs e)
        {//&&
            act = 4;
            label1.Text = "&&";
            digit = -1;
        }
        private void button18_Click(object sender, EventArgs e)
        {//||
            act = 5;
            label1.Text = "|";
            digit = -1;
        }
        private void button14_Click(object sender, EventArgs e)
        {//OK
            if (label1.Text != "---")
                ChangeAnswer();
            ShowAnswer();
            if (CheckAnswer())
            {
                score += 100;
                lvl++;
                if (lvl <= 50)
                    SetQwestion();
            }
            else
                score--;
            if (lvl > 50)
            {//победа
                ShowWin();
                
            }
            else
            {
                ShowQwestion();
            }
            label5.Text=score.ToString();
            label7.Text=lvl.ToString();
        }
        private void button15_Click(object sender, EventArgs e)
        {//Cancel
            label1.Text = "---";
            act = -1;
            digit = -1;
            for (int i = 0; i < 7; i++)
                res1[i] = false;
            ShowAnswer();
        }
        private void SetNumberFromInput(int i)
        {

            if (label1.Text.Length == 1 || label1.Text == "&&")
            {//просто действие - добавляем число
                digit = i;
                label1.Text += i.ToString();
            }
            else if (label1.Text.Length == 2|| label1.Text.Length == 4|| label1.Text[0] == '&')
            {//действие и число - меняем число
                digit = i;
                string s = label1.Text;
                s=s.Remove(s.Length - 1)+ i.ToString();
                label1.Text = s;
            }
            else if(label1.Text=="set")
            {//действие "set" - добавляем число
                digit = i;
                label1.Text += i.ToString();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            SetNumberFromInput(1);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            SetNumberFromInput(2);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            SetNumberFromInput(3);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            SetNumberFromInput(4);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            SetNumberFromInput(5);
        }
        private void button9_Click(object sender, EventArgs e)
        {
            SetNumberFromInput(6);
        }
        private void button10_Click(object sender, EventArgs e)
        {
            SetNumberFromInput(7);
        }
        private void button11_Click(object sender, EventArgs e)
        {
            SetNumberFromInput(8);
        }

        private void pictureBox2_SizeChanged(object sender, EventArgs e)
        {
            if (pictureBox2.Width > 0 && pictureBox2.Height > 0)
            {
                bit2 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
                g2 = Graphics.FromImage(bit2);
                ShowQwestion();
                if (lvl > 50)
                    ShowWin();
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //230, 80 - min pict. size
            //700, 430 - min min form size
            //if (this.Width > 0 && this.Height > 0)
            pictureBox2.Size=new Size(230,80+(this.Height-430)/2);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            What f = new What();
            f.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            SetNumberFromInput(9);
        }
        private void button13_Click(object sender, EventArgs e)
        {
            SetNumberFromInput(0);
        }
    }
}
