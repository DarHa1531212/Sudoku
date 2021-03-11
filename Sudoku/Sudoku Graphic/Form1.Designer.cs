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
            this.Debug = new System.Windows.Forms.Button();
            this.BtnChngStruct = new System.Windows.Forms.Button();
            this.BtnChngRegular = new System.Windows.Forms.Button();
            this.Sudoku.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnImport
            // 
            this.BtnImport.Location = new System.Drawing.Point(11, 25);
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
            this.Sudoku.Controls.Add(this.LabelWaitingGeneration);
            this.Sudoku.Location = new System.Drawing.Point(115, 25);
            this.Sudoku.Name = "Sudoku";
            this.Sudoku.Size = new System.Drawing.Size(362, 332);
            this.Sudoku.TabIndex = 2;
            this.Sudoku.Paint += new System.Windows.Forms.PaintEventHandler(this.Sudoku_Paint);
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
            this.BtnGenerate.Location = new System.Drawing.Point(11, 111);
            this.BtnGenerate.Margin = new System.Windows.Forms.Padding(2);
            this.BtnGenerate.Name = "BtnGenerate";
            this.BtnGenerate.Size = new System.Drawing.Size(99, 39);
            this.BtnGenerate.TabIndex = 4;
            this.BtnGenerate.Text = "Générer un plateau";
            this.BtnGenerate.UseVisualStyleBackColor = true;
            this.BtnGenerate.Click += new System.EventHandler(this.BtnGenerate_Click);
            // 
            // Debug
            // 
            this.Debug.Location = new System.Drawing.Point(11, 154);
            this.Debug.Margin = new System.Windows.Forms.Padding(2);
            this.Debug.Name = "Debug";
            this.Debug.Size = new System.Drawing.Size(99, 39);
            this.Debug.TabIndex = 5;
            this.Debug.Text = "Debug";
            this.Debug.UseVisualStyleBackColor = true;
            this.Debug.Click += new System.EventHandler(this.Debug_Click);
            // 
            // BtnChngStruct
            // 
            this.BtnChngStruct.Location = new System.Drawing.Point(11, 197);
            this.BtnChngStruct.Margin = new System.Windows.Forms.Padding(2);
            this.BtnChngStruct.Name = "BtnChngStruct";
            this.BtnChngStruct.Size = new System.Drawing.Size(99, 39);
            this.BtnChngStruct.TabIndex = 6;
            this.BtnChngStruct.Text = "Changer la structure";
            this.BtnChngStruct.UseVisualStyleBackColor = true;
            this.BtnChngStruct.Click += new System.EventHandler(this.BtnChngStruct_Click);
            // 
            // BtnChngRegular
            // 
            this.BtnChngRegular.Location = new System.Drawing.Point(11, 239);
            this.BtnChngRegular.Margin = new System.Windows.Forms.Padding(2);
            this.BtnChngRegular.Name = "BtnChngRegular";
            this.BtnChngRegular.Size = new System.Drawing.Size(99, 39);
            this.BtnChngRegular.TabIndex = 7;
            this.BtnChngRegular.Text = "Irrégulier / Régulier";
            this.BtnChngRegular.UseVisualStyleBackColor = true;
            this.BtnChngRegular.Click += new System.EventHandler(this.BtnChngRegular_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 549);
            this.Controls.Add(this.BtnChngRegular);
            this.Controls.Add(this.BtnChngStruct);
            this.Controls.Add(this.Debug);
            this.Controls.Add(this.BtnGenerate);
            this.Controls.Add(this.BtnResolve);
            this.Controls.Add(this.Sudoku);
            this.Controls.Add(this.BtnImport);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Sudoku.ResumeLayout(false);
            this.Sudoku.PerformLayout();
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
        private System.Windows.Forms.Label LabelWaitingGeneration;
    }
}

