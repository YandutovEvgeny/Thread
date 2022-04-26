using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Summator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string Summa(string a, string b)
        {
            string result = "";

            if(a.Length < b.Length) //первая строка всегда больше
            {
                string buffer = a;
                a = b;
                b = buffer;
            }
            a = new string(a.Reverse().ToArray());
            b = new string(b.Reverse().ToArray());

            bool ostatok = false;
            for (int i = 0; i < a.Length; i++)
            {
                int sum = 0;
                if (i < b.Length)
                    //Чтобы перевести char в int нам достаточно от char отнять 48
                    sum = a[i] + b[i] - 96;
                else
                    //48 - порядковый номер нуля в char
                    sum = a[i] - 48;
                if (ostatok)    //Если есть остаток к сумме прибавляется 1
                    sum++;
                if (sum >= 10)  //Если результат сложения двух цифр = 10
                {
                    sum -= 10;  //в ответе 0
                    ostatok = true; //в остаток 1
                }
                else  //Иначе
                {
                    ostatok = false;    //остатка нет
                }
                result += sum.ToString();    
            }
            if (ostatok)
                result += "1";
            result = new string(result.Reverse().ToArray());
            return result;
        }
        string Multy(string a, int b)
        {
            string result = "0";
            for (int i = 0; i < b; i++)
            {
                result = Summa(result, a);
            }
            return result;
        }

        void Fact(object N) //Чтобы запустить поток с параметрами наша функция должна быть void и
                            //параметры должны быть типа object
        {
            int n = (int)N;
            string result = "1";
            for (int i = 1; i <= n; i++)
            {
                result = Multy(result, i);
                Invoke(new Action(() => progressBar1.Value++));
            }
            Invoke (new Action(()=> MessageBox.Show(result)));
            progressBar1.Value = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Summa(textBox1.Text, textBox2.Text));
            MessageBox.Show(Multy(textBox1.Text, Convert.ToInt32(textBox2.Text)));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(Fact)); //Поток с параметрами
            thread.Start(Convert.ToInt32(textBox3.Text));   //Запускаем поток с параметрами
            progressBar1.Maximum = Convert.ToInt32(textBox3.Text);
            //MessageBox.Show(Fact(Convert.ToInt32(textBox3.Text)));
        }
    }
}
