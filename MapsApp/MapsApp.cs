﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapsApp
{
    public partial class MapsApp : Form
    {
        List<string> coordinates;
        string adr;
        int pV;

        public MapsApp()
        {
            InitializeComponent();
            
        }
        PolygonCoordinates pc = new PolygonCoordinates();
        
        private void button1_Click(object sender, EventArgs e)
        {
            resultConnect.Text = string.Empty;
            adr = adress.Text;
            if (!Int32.TryParse(pointValue.Text, out pV))
            {
                MessageBox.Show("Неверное значение точек");
                return;
            }

            coordinates = pc.GetCoordinates(adr, pV);
            resultConnect.Text = "Done";
        }

        private async void save(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var name = saveFileDialog.FileName;
                using (StreamWriter stream = new StreamWriter(name, false, Encoding.UTF8))
                {
                    foreach (string s in coordinates)
                    {
                        await stream.WriteAsync(s + "\n");
                    }
                }
            }
        }

    }
}
