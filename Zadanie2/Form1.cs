using Silnik;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zadanie2
{
    public partial class Form1 : Form
    {
        private string sciezkaPython;
        private string sciezkaPlikDane;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnPython_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = @"C:\";
            fileDialog.Title = "Wybierz plik python.exe";
            fileDialog.DefaultExt = "exe";
            fileDialog.ShowDialog();

            sciezkaPython = fileDialog.FileName;
            tbPython.Text = sciezkaPython;
        }

        private void btnPlikDane_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = @"C:\";
            fileDialog.Title = "Wybierz plik z danymi w formacie neh";
            fileDialog.DefaultExt = "txt";
            fileDialog.ShowDialog();

            sciezkaPlikDane = fileDialog.FileName;
            tbPlikDane.Text = sciezkaPlikDane;
        }

        private void btnUruchom_Click(object sender, EventArgs e)
        {
            /* Glowna funkcja programu. Wszystkie operacje zaczynaja sie od tego miejsca */
            //algorytmy chlopakow
            //wizualizacja
        }
    }
}
