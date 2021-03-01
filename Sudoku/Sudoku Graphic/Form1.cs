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
        CSP csp = new CSP();

        bool asCSP = true;

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

                    if (DecodeGrid(fileContent))
                    {

                        UpdateGridDisplay();

                    }
                }
            }
        }

        private bool DecodeGrid(string gridContent)
        {
            gridContent = gridContent.Replace("\r", "")
                    .Replace(" ", "");

            // Find if the grid is valid
            GridDimensions dimensions = FindGridDimensions(gridContent);
            if (dimensions == null)
            {
                return false;
            }

            if (!asCSP && dimensions.GridSizeY != 9)
            {
                MessageBox.Show("La taille de sudoku supportée est de 9*9 uniquement avec une structure de tableau.");
                return false;
            }

            if (dimensions.GridSizeX > 9)
            {
                MessageBox.Show("La taille maximale supportée est de 9*9");
                return false;
            }

            if (asCSP)
            {
                csp.ClearLists();
                csp.Dimensions = dimensions;
            }

            string cleanContent = gridContent.Replace("!", "")
                .Replace(" ", "")
                .Replace("-", "");
            string[] columns = cleanContent.Split('\n');
            int actualIndex = 0;
            foreach (string column in columns)
            {
                if (column.Length == 0)
                {
                    continue;
                }
                for (int j = 0; j < column.Length; ++j)
                {
                    if (!asCSP)
                    {
                        grid.SudokuGrid[actualIndex, j].Value = Convert.ToChar(column[j]);
                    }
                    else
                    {
                        Cell cell = new Cell(actualIndex, j, dimensions.GridSizeX);
                        cell.Value = Convert.ToChar(column[j]);
                        GraphNode node = new GraphNode(cell);
                        csp.Nodes.Add(node);
                    }
                }
                actualIndex++;

            }

            /*
            for (int i = 0; i < dimensions.GridSizeX + dimensions.NumberOfSquaresOnLine() - 1; i++)
            {
                string columnSubstring = gridContent.Substring(12 * i, 11)
                    .Replace("!", "")
                    .Replace(" ", "")
                    .Replace("-", "");

                if (columnSubstring != String.Empty)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        grid.SudokuGrid[actualIndex, j].Value = Convert.ToChar(columnSubstring.Substring(j, 1));

                        Cell cell = new Cell(actualIndex, j);
                        cell.Value = Convert.ToChar(columnSubstring.Substring(j, 1));
                        csp.Cells.Add(cell);
                    }
                    actualIndex++;
                }
            }
            */
            csp.GenerateArcs();
            return true;
        }

        private void UpdateGridDisplay()
        {
            if (!asCSP)
            {
                for (int row = 0; row < 9; row++)
                {
                    for (int column = 0; column < 9; column++)
                    {
                        cells[row, column].Text = grid.SudokuGrid[column, row].Value.ToString();
                        int squareY = row / 3;
                        int squareX = column / 3;

                        if ((squareX + squareY) % 2 == 0)
                        {
                            cells[row, column].BackColor = Color.LightGray;
                        }
                        else
                        {
                            cells[row, column].BackColor = Color.White;
                        }
                    }
                }
            }
            else
            {
                // Step 1 : Making the whole grid black
                for (int i = 0; i < 9; ++i)
                {
                    for (int j = 0; j < 9; ++j)
                    {
                        cells[i, j].Text = " ";
                        cells[i, j].BackColor = Color.Black;
                    }

                }
                // Step 2 : Writing on the cells
                foreach (GraphNode node in csp.Nodes)
                {
                    Cell cell = node.Cell;
                    cells[cell.PosY, cell.PosX].Text = cell.Value.ToString();
                    int squareY = cell.PosY / csp.Dimensions.SquareSizeY;
                    int squareX = cell.PosX / csp.Dimensions.SquareSizeX;

                    if ((squareX + squareY) % 2 == 0)
                    {
                        cells[cell.PosY, cell.PosX].BackColor = Color.White;
                    }
                    else
                    {
                        cells[cell.PosY, cell.PosX].BackColor = Color.LightGray;
                    }
                }
            }
        }

        private GridDimensions FindGridDimensions(string gridContent)
        {
            string[] columns = gridContent.Split('\n');
            int squareSizeX = -1;
            int numberOfSquaresX = 1;
            int squareSizeY = -1;
            int numberOfSquaresY = 0;
            int nonEmptyColumns = 0;
            int numberOfLinesBeforeLimit = 0;
            foreach (string column in columns)
            {
                if (column.Length == 0)
                {
                    continue;
                }
                nonEmptyColumns++;
                string[] columnSplitInSquares = column.Split('!');

                if (numberOfSquaresY == 0)
                {
                    numberOfSquaresY = columnSplitInSquares.Length;
                }
                else if (numberOfSquaresY != columnSplitInSquares.Length)
                {
                    MessageBox.Show("Invalid grid !");
                    return null;
                }

                foreach (string splitColumn in columnSplitInSquares)
                {
                    if (squareSizeY == -1)
                    {
                        squareSizeY = splitColumn.Length;
                    }
                    else if (squareSizeY != splitColumn.Length)
                    {
                        MessageBox.Show("Invalid grid !");
                        return null;
                    }
                }

                if (column[0] == '-')
                {
                    numberOfSquaresX++;
                    if (squareSizeX == -1)
                    {
                        squareSizeX = numberOfLinesBeforeLimit;
                    }
                    else if (squareSizeX != numberOfLinesBeforeLimit)
                    {
                        MessageBox.Show("Invalid grid !");
                        return null;
                    }
                    numberOfLinesBeforeLimit = 0;
                }
                else
                {
                    numberOfLinesBeforeLimit++;
                }
            }
            if (squareSizeX != numberOfLinesBeforeLimit)
            {
                MessageBox.Show("Invalid grid !");
                return null;
            }

            int sizeY = numberOfSquaresY * squareSizeY;
            int sizeX = numberOfSquaresX * squareSizeX;

            Console.WriteLine("Size X : " + sizeX.ToString());
            Console.WriteLine("Size Y : " + sizeY.ToString());
            Console.WriteLine("Square Size X : " + squareSizeX.ToString());
            Console.WriteLine("Square Size Y : " + squareSizeY.ToString());

            GridDimensions dimensions = new GridDimensions(sizeX, sizeY, squareSizeX, squareSizeY);

            if (dimensions.IsValid())
            {
                return dimensions;
            }
            else
            {
                MessageBox.Show("Grille Invalide");
                return null;
            }
        }

        private void BtnResolve_Click(object sender, EventArgs e)
        {
            if (!asCSP)
            {
                if (cells[0, 0].Text != String.Empty)
                {
                    Cell[,] solvedSudoku = grid.BacktrackingSearch();
                    if(solvedSudoku == null)
                    {
                        MessageBox.Show("Backtracking failed.");
                    } else
                    {
                        grid.SudokuGrid = solvedSudoku;
                        UpdateGridDisplay();
                    }
                }

            }
            else
            {
                if (cells[0, 0].Text != String.Empty)
                {
                    if (csp.BacktrackingSearch())
                    {
                        UpdateGridDisplay();
                    }
                    else
                    {
                        MessageBox.Show("Backtracking failed.");
                    }
                }

            }
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {

        }
    }
}
