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
            this.LabelWaitingGeneration = new System.Windows.Forms.Label();
            this.BtnResolve = new System.Windows.Forms.Button();
            this.BtnGenerate = new System.Windows.Forms.Button();
            this.BtnChngStruct = new System.Windows.Forms.Button();
            this.BtnChngRegular = new System.Windows.Forms.Button();
            this.SliderCellsGenerated = new System.Windows.Forms.TrackBar();
            this.NumberOfCells = new System.Windows.Forms.Label();
            this.Sudoku.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderCellsGenerated)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnImport
            // 
            this.BtnImport.Location = new System.Drawing.Point(11, 25);
            this.BtnImport.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.Sudoku.Controls.Add(this.LabelWaitingGeneration);
            this.Sudoku.Location = new System.Drawing.Point(115, 25);
            this.Sudoku.Name = "Sudoku";
            this.Sudoku.Size = new System.Drawing.Size(362, 332);
            this.Sudoku.TabIndex = 2;
            // 
            // LabelWaitingGeneration
            // 
            this.LabelWaitingGeneration.AutoSize = true;
            this.LabelWaitingGeneration.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelWaitingGeneration.Location = new System.Drawing.Point(24, 43);
            this.LabelWaitingGeneration.Name = "LabelWaitingGeneration";
            this.LabelWaitingGeneration.Size = new System.Drawing.Size(301, 17);
            this.LabelWaitingGeneration.TabIndex = 0;
            this.LabelWaitingGeneration.Text = "Le \"sudoku\" est en cours de génération.";
            this.LabelWaitingGeneration.Visible = false;
            // 
            // BtnResolve
            // 
            this.BtnResolve.Location = new System.Drawing.Point(11, 68);
            this.BtnResolve.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnResolve.Name = "BtnResolve";
            this.BtnResolve.Size = new System.Drawing.Size(99, 39);
            this.BtnResolve.TabIndex = 3;
            this.BtnResolve.Text = "Résoudre le sudoku";
            this.BtnResolve.UseVisualStyleBackColor = true;
            this.BtnResolve.Click += new System.EventHandler(this.BtnResolve_Click);
            // 
            // BtnGenerate
            // 
            this.BtnGenerate.Location = new System.Drawing.Point(11, 318);
            this.BtnGenerate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnGenerate.Name = "BtnGenerate";
            this.BtnGenerate.Size = new System.Drawing.Size(99, 39);
            this.BtnGenerate.TabIndex = 4;
            this.BtnGenerate.Text = "Générer un plateau";
            this.BtnGenerate.UseVisualStyleBackColor = true;
            this.BtnGenerate.Click += new System.EventHandler(this.BtnGenerate_Click);
            // 
            // BtnChngStruct
            // 
            this.BtnChngStruct.Location = new System.Drawing.Point(11, 111);
            this.BtnChngStruct.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnChngStruct.Name = "BtnChngStruct";
            this.BtnChngStruct.Size = new System.Drawing.Size(99, 39);
            this.BtnChngStruct.TabIndex = 6;
            this.BtnChngStruct.Text = "Changer la structure";
            this.BtnChngStruct.UseVisualStyleBackColor = true;
            this.BtnChngStruct.Click += new System.EventHandler(this.BtnChngStruct_Click);
            // 
            // BtnChngRegular
            // 
            this.BtnChngRegular.Location = new System.Drawing.Point(11, 154);
            this.BtnChngRegular.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnChngRegular.Name = "BtnChngRegular";
            this.BtnChngRegular.Size = new System.Drawing.Size(99, 39);
            this.BtnChngRegular.TabIndex = 7;
            this.BtnChngRegular.Text = "Irrégulier / Régulier";
            this.BtnChngRegular.UseVisualStyleBackColor = true;
            this.BtnChngRegular.Click += new System.EventHandler(this.BtnChngRegular_Click);
            // 
            // SliderCellsGenerated
            // 
            this.SliderCellsGenerated.Location = new System.Drawing.Point(11, 279);
            this.SliderCellsGenerated.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SliderCellsGenerated.Maximum = 50;
            this.SliderCellsGenerated.Minimum = 17;
            this.SliderCellsGenerated.Name = "SliderCellsGenerated";
            this.SliderCellsGenerated.Size = new System.Drawing.Size(99, 45);
            this.SliderCellsGenerated.TabIndex = 8;
            this.SliderCellsGenerated.Text = "Cellules générées";
            this.SliderCellsGenerated.Value = 17;
            this.SliderCellsGenerated.ValueChanged += new System.EventHandler(this.Value_Changed);
            // 
            // NumberOfCells
            // 
            this.NumberOfCells.AutoSize = true;
            this.NumberOfCells.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumberOfCells.Location = new System.Drawing.Point(49, 258);
            this.NumberOfCells.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.NumberOfCells.Name = "NumberOfCells";
            this.NumberOfCells.Size = new System.Drawing.Size(26, 17);
            this.NumberOfCells.TabIndex = 9;
            this.NumberOfCells.Text = "17";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 408);
            this.Controls.Add(this.BtnChngRegular);
            this.Controls.Add(this.BtnChngStruct);
            this.Controls.Add(this.BtnGenerate);
            this.Controls.Add(this.BtnResolve);
            this.Controls.Add(this.Sudoku);
            this.Controls.Add(this.BtnImport);
            this.Controls.Add(this.SliderCellsGenerated);
            this.Controls.Add(this.NumberOfCells);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Sudoku";
            this.Sudoku.ResumeLayout(false);
            this.Sudoku.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderCellsGenerated)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BtnImport;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel Sudoku;
        private System.Windows.Forms.Button BtnResolve;
        private System.Windows.Forms.Button BtnGenerate;
        private System.Windows.Forms.Button BtnChngStruct;
        private System.Windows.Forms.Button BtnChngRegular;
        private System.Windows.Forms.Label LabelWaitingGeneration;
        private System.Windows.Forms.TrackBar SliderCellsGenerated;
        private System.Windows.Forms.Label NumberOfCells;
    }
}

