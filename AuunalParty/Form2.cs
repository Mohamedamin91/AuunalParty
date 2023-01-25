﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuunalParty
{
    public partial class Form2 : Form
    {
        int counter = 6;
        public Form2()
        {
            InitializeComponent();
            lblCount.Font = new Font("Arial", 125, FontStyle.Bold);
            label2.Font = new Font("Times New Roman", 60, FontStyle.Bold);

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer1 = new Timer();
            timer1.Interval = 1000; // trigger every 100 milliseconds 
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (counter > 0)
            {
                counter--;
                lblCount.Text = counter.ToString();
            }
            else
            {
                timer1.Stop();
                this.Close();
            }
        }
    }
}