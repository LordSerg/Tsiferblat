using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tsiferblat_android
{
    public partial class MainPage : Shell//ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Tsiferblat.init();
            Draw_tsifers();
            s_input = "---";
            act = -1;
            digit = -1;
        }
        string s_input;
        int act;
        int digit;
        private void Draw_tsifers()
        {
            if (Tsiferblat.qwestion[0]) r0q.Fill = SolidColorBrush.Black; else r0q.Fill = SolidColorBrush.White;
            if (Tsiferblat.qwestion[1]) r1q.Fill = SolidColorBrush.Black; else r1q.Fill = SolidColorBrush.White;
            if (Tsiferblat.qwestion[2]) r2q.Fill = SolidColorBrush.Black; else r2q.Fill = SolidColorBrush.White;
            if (Tsiferblat.qwestion[3]) r3q.Fill = SolidColorBrush.Black; else r3q.Fill = SolidColorBrush.White;
            if (Tsiferblat.qwestion[4]) r4q.Fill = SolidColorBrush.Black; else r4q.Fill = SolidColorBrush.White;
            if (Tsiferblat.qwestion[5]) r5q.Fill = SolidColorBrush.Black; else r5q.Fill = SolidColorBrush.White;
            if (Tsiferblat.qwestion[6]) r6q.Fill = SolidColorBrush.Black; else r6q.Fill = SolidColorBrush.White;

            if (Tsiferblat.answer[0]) r0a.Fill = SolidColorBrush.Black; else r0a.Fill = SolidColorBrush.White;
            if (Tsiferblat.answer[1]) r1a.Fill = SolidColorBrush.Black; else r1a.Fill = SolidColorBrush.White;
            if (Tsiferblat.answer[2]) r2a.Fill = SolidColorBrush.Black; else r2a.Fill = SolidColorBrush.White;
            if (Tsiferblat.answer[3]) r3a.Fill = SolidColorBrush.Black; else r3a.Fill = SolidColorBrush.White;
            if (Tsiferblat.answer[4]) r4a.Fill = SolidColorBrush.Black; else r4a.Fill = SolidColorBrush.White;
            if (Tsiferblat.answer[5]) r5a.Fill = SolidColorBrush.Black; else r5a.Fill = SolidColorBrush.White;
            if (Tsiferblat.answer[6]) r6a.Fill = SolidColorBrush.Black; else r6a.Fill = SolidColorBrush.White;
            ShowLabels();
        }
        private void bt1_num(object sender, EventArgs e) { SetNumberFromInput(1);}
        private void bt2_num(object sender, EventArgs e) { SetNumberFromInput(2);}
        private void bt3_num(object sender, EventArgs e) { SetNumberFromInput(3);}
        private void bt4_num(object sender, EventArgs e) { SetNumberFromInput(4);}
        private void bt5_num(object sender, EventArgs e) { SetNumberFromInput(5);}
        private void bt6_num(object sender, EventArgs e) { SetNumberFromInput(6);}
        private void bt7_num(object sender, EventArgs e) { SetNumberFromInput(7);}
        private void bt8_num(object sender, EventArgs e) { SetNumberFromInput(8);}
        private void bt9_num(object sender, EventArgs e) { SetNumberFromInput(9);}
        private void bt0_num(object sender, EventArgs e) { SetNumberFromInput(0); }
        private void SetNumberFromInput(int i)
        {

            if (s_input.Length == 1 || s_input == "&&")
            {//просто действие - добавляем число
                digit = i;
                s_input += i.ToString();
            }
            else if (s_input.Length == 2 || s_input.Length == 4 || s_input[0] == '&')
            {//действие и число - меняем число
                digit = i;
                string s = s_input;
                s = s.Remove(s.Length - 1) + i.ToString();
                s_input = s;
            }
            else if (s_input == "set")
            {//действие "set" - добавляем число
                digit = i;
                s_input += i.ToString();
            }
            ShowLabels();
        }
        private void bt_plus(object sender, EventArgs e) 
        {
            act = 0;
            s_input = "+";
            digit = -1;
            ShowLabels();
        }
        private void bt_minus(object sender, EventArgs e) 
        {
            act = 1;
            s_input = "-";
            digit = -1;
            ShowLabels();
        }
        private void bt_set(object sender, EventArgs e) 
        {
            act = 2;
            s_input = "set";
            digit = -1;
            ShowLabels();
        }
        private void bt_not(object sender, EventArgs e) 
        {
            act = 3;
            s_input = "!";
            digit = -1;
            ShowLabels();
        }
        private void bt_and(object sender, EventArgs e) 
        {
            act = 4;
            s_input = "&&";
            digit = -1;
            ShowLabels();
        }
        private void bt_or(object sender, EventArgs e) 
        {
            act = 5;
            s_input = "|";
            digit = -1;
            ShowLabels();
        }
        private void ShowLabels()
        {
            if (Tsiferblat.peremoga)
            {
                label_lvl.Text = "LvL: " + Tsiferblat.lvl.ToString();
                label_scr.Text = "Score: " + Tsiferblat.score.ToString();
                label_act.Text = "YOU WIN!!!!";
            }
            else
            {
                label_lvl.Text = "LvL: " + Tsiferblat.lvl.ToString();
                label_scr.Text = "Score: " + Tsiferblat.score.ToString();
                label_act.Text = "Action: " + s_input;
            }
        }
        private void bt_OK(object sender, EventArgs e)
        {
            Tsiferblat.CheckAnswer(act, digit);
            Draw_tsifers();
        }
        
        protected override void OnSizeAllocated(double width, double height)
        {//меняем трошки расположение одного из стеков, чтоб красиво было и при горизонтали, и при вертикали
            base.OnSizeAllocated(width, height);
            if (width != this.Width || height != this.Height)
            {
                this.WidthRequest = width;
                this.HeightRequest = height;
            }
            if (width > height)
            {
                SL_toChange.Orientation = StackOrientation.Horizontal;
            }
            else
                SL_toChange.Orientation = StackOrientation.Vertical;
            //double k;
            //if (grid1.Height > grid1.Width)
            //    k = (double)grid1.Width / (double)12;
            //else
            //    k = (double)grid1.Height / (double)12;
            //grid1 = new Grid
            //{
            //    RowDefinitions =
            //            {
            //                new RowDefinition { Height = new GridLength(k) },
            //                new RowDefinition { Height = new GridLength(k) },
            //                new RowDefinition { Height = new GridLength(k*3) },
            //                new RowDefinition { Height = new GridLength(k) },
            //                new RowDefinition { Height = new GridLength(k*3) },
            //                new RowDefinition { Height = new GridLength(k) },
            //                new RowDefinition { Height = new GridLength(k) }
            //            },
            //    ColumnDefinitions =
            //            {
            //                new ColumnDefinition { Width = new GridLength(k*3) },
            //                new ColumnDefinition { Width = new GridLength(k) },
            //                new ColumnDefinition { Width = new GridLength(k*3) },
            //                new ColumnDefinition { Width = new GridLength(k) },
            //                new ColumnDefinition { Width = new GridLength(k*3) }
            //            },
            //    ColumnSpacing = 0,
            //    RowSpacing = 0
            //};
            //if (grid2.Height > grid2.Width)
            //    k = (double)grid2.Width / (double)12;
            //else
            //    k = (double)grid2.Height / (double)12;
            //grid2 = new Grid
            //{
            //    RowDefinitions =
            //            {
            //                new RowDefinition { Height = new GridLength(k) },
            //                new RowDefinition { Height = new GridLength(k) },
            //                new RowDefinition { Height = new GridLength(k*3) },
            //                new RowDefinition { Height = new GridLength(k) },
            //                new RowDefinition { Height = new GridLength(k*3) },
            //                new RowDefinition { Height = new GridLength(k) },
            //                new RowDefinition { Height = new GridLength(k) }
            //            },
            //    ColumnDefinitions =
            //            {
            //                new ColumnDefinition { Width = new GridLength(k*3) },
            //                new ColumnDefinition { Width = new GridLength(k) },
            //                new ColumnDefinition { Width = new GridLength(k*3) },
            //                new ColumnDefinition { Width = new GridLength(k) },
            //                new ColumnDefinition { Width = new GridLength(k*3) }
            //            },
            //    ColumnSpacing = 0,
            //    RowSpacing = 0
            //};

        }
        
    }
}
