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
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //code found at https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog?view=net-5.0
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
                }
            }
            decodeGrid(fileContent);

            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    textBox1.Text += grid.SudokuGrid[column, row];
                }
                textBox1.Text += (char)8;
            }

        }

        private void decodeGrid(string gridContent)
        {
            int actualIndex = 0;
            for (int i = 0; i < 11; i++)
            {
                string columnSubstring = gridContent.Substring(13 * i, 11)
                    .Replace("|", "")
                    .Replace("-", "");

                if (columnSubstring != String.Empty)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        grid.SudokuGrid[j, actualIndex] = Convert.ToChar(columnSubstring.Substring(j, 1));
                    }
                    actualIndex++;
                }
            }
        }
    }
}
