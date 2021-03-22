
namespace Zadanie2
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbPython = new System.Windows.Forms.TextBox();
            this.btnPython = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbNrInstancji = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbPlikDane = new System.Windows.Forms.TextBox();
            this.btnPlikDane = new System.Windows.Forms.Button();
            this.btnUruchom = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbPython);
            this.groupBox1.Controls.Add(this.btnPython);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 59);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Python";
            // 
            // tbPython
            // 
            this.tbPython.Location = new System.Drawing.Point(87, 30);
            this.tbPython.Name = "tbPython";
            this.tbPython.ReadOnly = true;
            this.tbPython.Size = new System.Drawing.Size(267, 20);
            this.tbPython.TabIndex = 1;
            // 
            // btnPython
            // 
            this.btnPython.Location = new System.Drawing.Point(6, 30);
            this.btnPython.Name = "btnPython";
            this.btnPython.Size = new System.Drawing.Size(75, 23);
            this.btnPython.TabIndex = 0;
            this.btnPython.Text = "Otwórz...";
            this.btnPython.UseVisualStyleBackColor = true;
            this.btnPython.Click += new System.EventHandler(this.btnPython_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbNrInstancji);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tbPlikDane);
            this.groupBox2.Controls.Add(this.btnPlikDane);
            this.groupBox2.Location = new System.Drawing.Point(12, 77);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(360, 88);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Plik z danymi";
            // 
            // tbNrInstancji
            // 
            this.tbNrInstancji.Location = new System.Drawing.Point(302, 62);
            this.tbNrInstancji.Name = "tbNrInstancji";
            this.tbNrInstancji.Size = new System.Drawing.Size(52, 20);
            this.tbNrInstancji.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(178, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Numer instancji:";
            // 
            // tbPlikDane
            // 
            this.tbPlikDane.Location = new System.Drawing.Point(87, 30);
            this.tbPlikDane.Name = "tbPlikDane";
            this.tbPlikDane.ReadOnly = true;
            this.tbPlikDane.Size = new System.Drawing.Size(267, 20);
            this.tbPlikDane.TabIndex = 1;
            // 
            // btnPlikDane
            // 
            this.btnPlikDane.Location = new System.Drawing.Point(6, 30);
            this.btnPlikDane.Name = "btnPlikDane";
            this.btnPlikDane.Size = new System.Drawing.Size(75, 23);
            this.btnPlikDane.TabIndex = 0;
            this.btnPlikDane.Text = "Otwórz...";
            this.btnPlikDane.UseVisualStyleBackColor = true;
            this.btnPlikDane.Click += new System.EventHandler(this.btnPlikDane_Click);
            // 
            // btnUruchom
            // 
            this.btnUruchom.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUruchom.Location = new System.Drawing.Point(135, 180);
            this.btnUruchom.Name = "btnUruchom";
            this.btnUruchom.Size = new System.Drawing.Size(90, 28);
            this.btnUruchom.TabIndex = 2;
            this.btnUruchom.Text = "Uruchom";
            this.btnUruchom.UseVisualStyleBackColor = true;
            this.btnUruchom.Click += new System.EventHandler(this.btnUruchom_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 222);
            this.Controls.Add(this.btnUruchom);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Gantt chart";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbPython;
        private System.Windows.Forms.Button btnPython;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbNrInstancji;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPlikDane;
        private System.Windows.Forms.Button btnPlikDane;
        private System.Windows.Forms.Button btnUruchom;
    }
}

