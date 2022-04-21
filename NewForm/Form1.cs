using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace NewForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Thread thread; //Создаём поток
        void ProgressChangeding()
        {
            while(progressBar1.Value < progressBar1.Maximum)
            {
                //Лямба-выражение - это создание функции во время выполнения другой функции
                progressBar1.Invoke(new Action(() =>    //Метод invoke позволяет работать с этим потоком
                                                        //из других потоков
                {
                    progressBar1.Value += 1;
                }));
                Thread.Sleep(50);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(thread == null || thread.IsAlive == false)   //Если поток пустой или "мёртвый"
            {
                progressBar1.Value = 0;
                thread = new Thread(ProgressChangeding); //Инициализируем поток тем методом, который хотим
                                                         //чтобы выполнялся в отдельном потоке
                thread.Start();     //Запускаем поток
                //thread.Abort();   //Использовать не стоит уже
            }
        }

        
    }
}
