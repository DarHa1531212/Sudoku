using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku_Graphic
{
    public partial class Form1 : Form
    {
        Grid grid = new Grid();

        public Form1()
        {
            InitializeComponent();
            this.AutoSize = true;
            createCells();
            Sudoku.AutoSize = true;
        }

        Label[,] cells = new Label[9, 9];
        
        private void createCells()
        {
            // design inspired by code found at https://playwithcsharpdotnet.blogspot.com/
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i, j] = new Label();
                    cells[i, j].Font = new Font(SystemFonts.DefaultFont.FontFamily, 20);
                    cells[i, j].Font = new Font(SystemFonts.DefaultFont.FontFamily, 20);
                    cells[i, j].Size = new Size(40, 40);
                    cells[i, j].BorderStyle = BorderStyle.Fixed3D;
                    cells[i, j].TextAlign = ContentAlignment.MiddleCenter;
                    cells[i, j].ForeColor = SystemColors.ControlDarkDark;
                    cells[i, j].Location = new Point(i * 40, j * 40);
                    cells[i, j].BackColor = ((i / 3) + (j / 3)) % 2 == 0 ? SystemColors.Control : Color.LightGray;
                    
                    Sudoku.Controls.Add(cells[i, j]);
                }
            }
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            // code found at https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "ss files (*.ss)|*.ss|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                    
                    DecodeGrid(fileContent);

                    UpdateGridDisplay();
                }
            }
        }

        private void DecodeGrid(string gridContent)
        {
            int actualIndex = 0;
            for (int i = 0; i < 11; i++)
            {
                string columnSubstring = gridContent.Substring(12 * i, 11)
                    .Replace("!", "")
                    .Replace("-", "");

                if (columnSubstring != String.Empty)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        grid.SudokuGrid[actualIndex, j].Value = Convert.ToChar(columnSubstring.Substring(j, 1));
                    }
                    actualIndex++;
                }
            }
        }

        private void UpdateGridDisplay()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    cells[row, column].Text = grid.SudokuGrid[column, row].Value.ToString();
                }
            }
        }

        private void BtnResolve_Click(object sender, EventArgs e)
        {
            if(cells[0, 0].Text != String.Empty)
            {
                Cell[,] solvedSudoku = grid.BacktrackingSearch();
                grid.SudokuGrid = solvedSudoku;
                UpdateGridDisplay();
            }
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {

        }
    }
}
