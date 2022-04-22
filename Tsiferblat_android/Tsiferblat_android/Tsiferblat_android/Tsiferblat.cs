using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Tsiferblat_android
{
    static class Tsiferblat 
    {
        public static bool[] qwestion;//то, что необходимо получить
        public static bool[] answer;//то, что получается
        private static int MaxLVL=50;//максимальное количество уровней
        public static int lvl, score, max_score;//уровень сейчас, очки, (позже)максимальное кол-во очков за все время
        public static bool peremoga = false;//выиграл ли игрок игру
        private static bool[][] NuMbEr;//тут хранятся конфигурации всех десяти цифер в форме циферблатной записи:
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
        public static void init()
        {
            answer = new bool[7];
            qwestion = new bool[7];
            NuMbEr = new bool[10][];
            for (int i = 0; i < 10; i++)
                NuMbEr[i] = new bool[7];
            for (int i = 0; i < 7; i++)
            {
                answer[i] = false;
                qwestion[i] = false;
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
            NuMbEr[1][2] = true;
            NuMbEr[1][5] = true;
            NuMbEr[2][1] = false;
            NuMbEr[2][5] = false;
            NuMbEr[3][1] = false;
            NuMbEr[3][4] = false;
            NuMbEr[4][0] = false;
            NuMbEr[4][4] = false;
            NuMbEr[4][6] = false;
            NuMbEr[5][2] = false;
            NuMbEr[5][4] = false;
            NuMbEr[6][2] = false;
            NuMbEr[7][0] = true;
            NuMbEr[7][2] = true;
            NuMbEr[7][5] = true;
            NuMbEr[9][4] = false;
            NuMbEr[0][3] = false;
            SetQwestion();
            lvl = 1;
            score = 0;
        }
        public static bool[] CheckAnswer(int act,int digit)
        {
            if (!peremoga)
            {
                bool some_var = false;
                if (act >= 0)//&& digit >= 0)
                    some_var=Confirm(act, digit);
                //ShowAnswer();
                if (some_var)
                {
                    if (CheckAnswer())
                    {
                        score += 100;
                        lvl++;
                        if (lvl <= MaxLVL)
                            SetQwestion();
                        else
                        {
                            //ShowWin();
                            peremoga = true;
                        }
                    }
                    else
                        score--;
                }
                //if (lvl > WinLvlNum)
                //{//победа
                //    ShowWin();
                //    return;
                //}
                //else
                //{
                //    ShowQwestion();
                //}
            }
            else
            {
                //ShowWin();
            }
            return answer;
        }
        public static void Refresh()
        {//стираем то, что сейчас в answer-e
            for (int i = 0; i < 7; i++)
                answer[i] = false;
        }
        private static bool Confirm(int act, int digit)
        {
            if (act == 0)
            {//бинарная сумма (1+1=0; 1+0=0+1=1; 0+0=0)
                if (digit != -1) { Add(NuMbEr[digit]); return true; }
            }
            else if (act == 1)
            {//разница (0-1=0; 1-0=1; 0-0=1-1=0)
                if (digit != -1) { Minus(NuMbEr[digit]); return true; }
            }
            else if (act == 2)
            {//поставить в положение (поставить единицы и ноли в том положении, котором они должны быть)
                if (digit != -1) { Set(NuMbEr[digit]); return true; }
            }
            else if (act == 3)
            {//отрицание (1!1=0; 1!0=0; 0!1=1; 0!0=0)
                if (digit != -1) { Invert(NuMbEr[digit]); return true; }
                else { Invert(); return true; }
            }
            else if (act == 4)
            {//и
                if (digit != -1) { And(NuMbEr[digit]); return true; }
            }
            else if (act == 5)
            {//или
                if (digit != -1) { Or(NuMbEr[digit]); return true; }
            }
            return false;
        }
        private static void Add(bool[] number)
        {//на вход идет число
            int n = number.Length;
            for (int i = 0; i < n; i++)
            {
                answer[i] = !(answer[i] == number[i]);
                //1+1=0
                //1+0=1
                //0+1=1
                //0+0=0
            }
        }
        private static void Minus(bool[] number)
        {//на вход идет число
            int n = number.Length;
            for (int i = 0; i < n; i++)
            {
                answer[i] = !(number[i]) && answer[i];
                //1-1 = 0
                //1-0 = 1
                //0-1 = 0
                //0-0 = 0
            }
        }
        private static void Set(bool[] number)
        {
            int n = number.Length;
            for (int i = 0; i < n; i++)
            {
                answer[i] = number[i];
            }
        }
        private static void Invert(bool[] number)
        {//на вход идет число
            int n = number.Length;
            for (int i = 0; i < n; i++)
            {
                answer[i] = (answer[i] == number[i]);
                //1!1 = 1
                //1!0 = 0
                //0!1 = 0
                //0!0 = 1
            }
        }
        private static void Invert()
        {//инвертируем то, что есть
            for (int i = 0; i < 7; i++)
            {
                answer[i] = !answer[i];
            }
        }
        private static void And(bool[] number)
        {//инвертируем то, что есть
            for (int i = 0; i < 7; i++)
            {
                answer[i] = answer[i] && number[i];
                //1&1 = 1
                //1&0 = 0
                //0&1 = 0
                //0&0 = 0
            }
        }
        private static void Or(bool[] number)
        {//инвертируем то, что есть
            for (int i = 0; i < 7; i++)
            {
                answer[i] = answer[i] || number[i];
                //1|1 = 1
                //1|0 = 1
                //0|1 = 1
                //0|0 = 0
            }
        }
        private static void SetQwestion()
        {
            Random r = new Random();
            for (int i = 0; i < 7; i++)
            {
                qwestion[i] = Convert.ToBoolean(r.Next(2));
            }
        }
        private static bool CheckAnswer()
        {
            for (int i = 0; i < 7; i++)
                if (answer[i] != qwestion[i])
                    return false;
            return true;
        }
    }
}
