﻿namespace Sudoku_Graphic
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
            this.Debug = new System.Windows.Forms.Button();
            this.BtnChngStruct = new System.Windows.Forms.Button();
            this.BtnChngRegular = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnImport
            // 
            this.BtnImport.Location = new System.Drawing.Point(15, 15);
            this.BtnImport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnImport.Name = "BtnImport";
            this.BtnImport.Size = new System.Drawing.Size(132, 48);
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
            this.Sudoku.Location = new System.Drawing.Point(153, 15);
            this.Sudoku.Margin = new System.Windows.Forms.Padding(4);
            this.Sudoku.Name = "Sudoku";
            this.Sudoku.Size = new System.Drawing.Size(483, 409);
            this.Sudoku.TabIndex = 2;
            this.Sudoku.Paint += new System.Windows.Forms.PaintEventHandler(this.Sudoku_Paint);
            // 
            // BtnResolve
            // 
            this.BtnResolve.Location = new System.Drawing.Point(15, 68);
            this.BtnResolve.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnResolve.Name = "BtnResolve";
            this.BtnResolve.Size = new System.Drawing.Size(132, 48);
            this.BtnResolve.TabIndex = 3;
            this.BtnResolve.Text = "Résoudre le sudoku";
            this.BtnResolve.UseVisualStyleBackColor = true;
            this.BtnResolve.Click += new System.EventHandler(this.BtnResolve_Click);
            // 
            // BtnGenerate
            // 
            this.BtnGenerate.Location = new System.Drawing.Point(15, 121);
            this.BtnGenerate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnGenerate.Name = "BtnGenerate";
            this.BtnGenerate.Size = new System.Drawing.Size(132, 48);
            this.BtnGenerate.TabIndex = 4;
            this.BtnGenerate.Text = "Générer un plateau";
            this.BtnGenerate.UseVisualStyleBackColor = true;
            this.BtnGenerate.Click += new System.EventHandler(this.BtnGenerate_Click);
            // 
            // Debug
            // 
            this.Debug.Location = new System.Drawing.Point(15, 174);
            this.Debug.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Debug.Name = "Debug";
            this.Debug.Size = new System.Drawing.Size(132, 48);
            this.Debug.TabIndex = 5;
            this.Debug.Text = "Debug";
            this.Debug.UseVisualStyleBackColor = true;
            this.Debug.Click += new System.EventHandler(this.Debug_Click);
            // 
            // BtnChngStruct
            // 
            this.BtnChngStruct.Location = new System.Drawing.Point(15, 226);
            this.BtnChngStruct.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnChngStruct.Name = "BtnChngStruct";
            this.BtnChngStruct.Size = new System.Drawing.Size(132, 48);
            this.BtnChngStruct.TabIndex = 6;
            this.BtnChngStruct.Text = "Changer la structure";
            this.BtnChngStruct.UseVisualStyleBackColor = true;
            this.BtnChngStruct.Click += new System.EventHandler(this.BtnChngStruct_Click);
            // 
            // BtnChngRegular
            // 
            this.BtnChngRegular.Location = new System.Drawing.Point(15, 278);
            this.BtnChngRegular.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnChngRegular.Name = "BtnChngRegular";
            this.BtnChngRegular.Size = new System.Drawing.Size(132, 48);
            this.BtnChngRegular.TabIndex = 7;
            this.BtnChngRegular.Text = "Irrégulier / Régulier";
            this.BtnChngRegular.UseVisualStyleBackColor = true;
            this.BtnChngRegular.Click += new System.EventHandler(this.BtnChngRegular_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 676);
            this.Controls.Add(this.BtnChngRegular);
            this.Controls.Add(this.BtnChngStruct);
            this.Controls.Add(this.Debug);
            this.Controls.Add(this.BtnGenerate);
            this.Controls.Add(this.BtnResolve);
            this.Controls.Add(this.Sudoku);
            this.Controls.Add(this.BtnImport);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
        private System.Windows.Forms.Button Debug;
        private System.Windows.Forms.Button BtnChngStruct;
        private System.Windows.Forms.Button BtnChngRegular;
    }
}

