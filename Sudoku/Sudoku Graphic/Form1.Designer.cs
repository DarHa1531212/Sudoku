namespace Sudoku_Graphic
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnImport = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Sudoku = new System.Windows.Forms.Panel();
            this.BtnResolve = new System.Windows.Forms.Button();
            this.BtnGenerate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnImport
            // 
            this.BtnImport.Location = new System.Drawing.Point(11, 12);
            this.BtnImport.Margin = new System.Windows.Forms.Padding(2);
            this.BtnImport.Name = "BtnImport";
            this.BtnImport.Size = new System.Drawing.Size(99, 39);
            this.BtnImport.TabIndex = 1;
            this.BtnImport.Text = "Importer un plateau (.ss)";
            this.BtnImport.UseVisualStyleBackColor = true;
            this.BtnImport.Click += new System.EventHandler(this.BtnImport_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Sudoku
            // 
            this.Sudoku.Location = new System.Drawing.Point(115, 12);
            this.Sudoku.Name = "Sudoku";
            this.Sudoku.Size = new System.Drawing.Size(362, 332);
            this.Sudoku.TabIndex = 2;
            // 
            // BtnResolve
            // 
            this.BtnResolve.Location = new System.Drawing.Point(11, 55);
            this.BtnResolve.Margin = new System.Windows.Forms.Padding(2);
            this.BtnResolve.Name = "BtnResolve";
            this.BtnResolve.Size = new System.Drawing.Size(99, 39);
            this.BtnResolve.TabIndex = 3;
            this.BtnResolve.Text = "Résoudre le sudoku";
            this.BtnResolve.UseVisualStyleBackColor = true;
            this.BtnResolve.Click += new System.EventHandler(this.BtnResolve_Click);
            // 
            // BtnGenerate
            // 
            this.BtnGenerate.Location = new System.Drawing.Point(11, 98);
            this.BtnGenerate.Margin = new System.Windows.Forms.Padding(2);
            this.BtnGenerate.Name = "BtnGenerate";
            this.BtnGenerate.Size = new System.Drawing.Size(99, 39);
            this.BtnGenerate.TabIndex = 4;
            this.BtnGenerate.Text = "Générer un plateau";
            this.BtnGenerate.UseVisualStyleBackColor = true;
            this.BtnGenerate.Click += new System.EventHandler(this.BtnGenerate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 549);
            this.Controls.Add(this.BtnGenerate);
            this.Controls.Add(this.BtnResolve);
            this.Controls.Add(this.Sudoku);
            this.Controls.Add(this.BtnImport);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button BtnImport;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel Sudoku;
        private System.Windows.Forms.Button BtnResolve;
        private System.Windows.Forms.Button BtnGenerate;
    }
}

