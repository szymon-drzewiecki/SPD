﻿using Silnik;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        private int nrInstancji;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnPython_Click(object sender, EventArgs e)
        {
            OpenFileDialog wyborPliku = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Wybierz plik python.exe",
                DefaultExt = "exe"
            };
            wyborPliku.ShowDialog();

            sciezkaPython = wyborPliku.FileName;
            tbPython.Text = sciezkaPython;
        }

        private void btnPlikDane_Click(object sender, EventArgs e)
        {
            OpenFileDialog wyborPliku = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Wybierz plik z danymi w formacie neh",
                DefaultExt = "txt"
            };
            wyborPliku.ShowDialog();

            sciezkaPlikDane = wyborPliku.FileName;
            tbPlikDane.Text = sciezkaPlikDane;
        }

        /* Glowna funkcja programu. Wszystkie operacje zaczynaja sie od tego miejsca */
        private void btnUruchom_Click(object sender, EventArgs e)
        {
            if (!Int32.TryParse(tbNrInstancji.Text, out nrInstancji))
            {
                MessageBox.Show("Złe dane w polu instancji!");
            }
            else
            {
                Algorytmy algorytmy = new Algorytmy();
                KolejkaZadan kz = new KolejkaZadan(sciezkaPlikDane, nrInstancji);
                var kolejnoscZadan = new int[kz.zadania.Length];
                for (int x = 0; x < kz.zadania.Length; x++)
                {
                    kolejnoscZadan[x] = x + 1;
                }
                int[,] macierzZadan = Algorytmy.pasreMacierzZadan(kz, kolejnoscZadan);
                if (cbAlgorytm.SelectedIndex == 0)
                {
                    //Przeglad zupelny
                    PrzegladZupelny przegladZupelny = new PrzegladZupelny();
                    int[] sekwencja = przegladZupelny.Przeglad(kz);
                    int Cmax = Algorytmy.calculateTotalspan(kz, macierzZadan, sekwencja);
                    kz.WypiszRezultaty(Cmax, sekwencja);
                }
                else if (cbAlgorytm.SelectedIndex == 1)
                {
                    //Johnson
                    int[] sekwencja = algorytmy.AlgorytmJohnsona(kz);
                    int Cmax = Algorytmy.calculateTotalspan(kz, macierzZadan,sekwencja);
                    kz.WypiszRezultaty(Cmax, sekwencja);
                }
                else
                {
                    //Neh
                    List<Tuple<int, int>> posortowane = Algorytmy.ZwrocPosortowanePriorytety(kz);
                    List<int> sekwencja = Neh.NehBasic(kz, posortowane);
                    int Cmax = Algorytmy.calculateTotalspan(kz, macierzZadan, sekwencja.ToArray());
                    kz.WypiszRezultaty(Cmax, sekwencja.ToArray());
                }

                //Wizualizacja
                if (rbVisualize.Checked)
                {
                    var proces = new Process();
                    proces.StartInfo.FileName = sciezkaPython;
                    proces.StartInfo.Arguments = @"w_gantt.py";
                    proces.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    proces.Start();
                }
            }
        }
    }
}
