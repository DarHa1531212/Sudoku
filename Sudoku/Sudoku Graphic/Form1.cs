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

        enum State
        {
            INITIAL_STATE, // Can change struct, load sudoku
            STRUCT_CHANGED, // Can load sudoku or change struct
            SUDOKU_LOADED // Can solve sudoku, change struct
        }

        List<Color> colors = new List<Color>(new Color[]{
                Color.LightYellow,
                Color.LightBlue,
                Color.Pink,
                Color.LightCyan,
                Color.Lime,
                Color.Red,
                Color.Blue,
                Color.Magenta,
                Color.Orange,
                Color.LightGray,
                Color.LightGreen,
                Color.Maroon,
                Color.Cyan,
                Color.ForestGreen,
                Color.MistyRose,
                Color.PaleTurquoise
        });

        Grid grid = new Grid();
        CSP csp = new CSP();
        //public CSP Csp { get => csp; set => csp = value; }

        State state;

        bool asCSP;

        bool irregularSudoku;

        GridDimensions actualDimensions;

        public Form1()
        {
            InitializeComponent();
            this.AutoSize = true;

            actualDimensions = new GridDimensions(9, 9, 3, 3);
            createCells();
            Sudoku.AutoSize = true;

            state = State.INITIAL_STATE;
            asCSP = true;
            irregularSudoku = false;
        }

        Label[,] cells = new Label[9, 9];

        private void recreateCells(string[] zones = null)
        {
            disposeOfCells();
            createCells(zones);
        }

        private void disposeOfCells()
        {
            for (int i = 0; i < cells.GetLength(0); ++i)
            {
                for (int j = 0; j < cells.GetLength(1); ++j)
                {
                    cells[j, i].Dispose();
                }
            }
        }

        private void createCells(string[] zones = null)
        {
            int size = actualDimensions.GridSizeX;
            cells = new Label[size, size];
            // design inspired by code found at https://playwithcsharpdotnet.blogspot.com/
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    cells[i, j] = new Label();
                    cells[i, j].Font = new Font(SystemFonts.DefaultFont.FontFamily, 180 / size);
                    //cells[i, j].Font = new Font(SystemFonts.DefaultFont.FontFamily, 180 / size);
                    cells[i, j].Size = new Size(360 / size, 360 / size);
                    cells[i, j].BorderStyle = BorderStyle.Fixed3D;
                    cells[i, j].TextAlign = ContentAlignment.MiddleCenter;
                    cells[i, j].ForeColor = SystemColors.ControlDarkDark;
                    cells[i, j].Location = new Point(i * 360 / size, j * 360 / size);
                    if (zones == null)
                    {
                        cells[i, j].BackColor = ((i / actualDimensions.NumberOfSquaresOnLine()) + (j / actualDimensions.NumberOfSquaresOnColumn())) % 2 == 0 ? SystemColors.Control : Color.LightGray;
                    }
                    else
                    {
                        int value;
                        char zoneChar = zones[i][j];
                        if (zoneChar >= 'A')
                        {
                            value = 9 + zoneChar - 'A';
                        }
                        else
                        {
                            value = zoneChar - '1';
                        }
                        cells[i, j].BackColor = colors[value];
                    }

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
                    if (!irregularSudoku)
                    {
                        if (DecodeGrid_Regular(fileContent))
                        {
                            state = State.SUDOKU_LOADED;
                            UpdateGridDisplay_Regular();
                        }
                    }
                    else
                    {
                        if (DecodeGrid_Irregular(fileContent))
                        {
                            state = State.SUDOKU_LOADED;
                            UpdateGridDisplay_Irregular();
                        }

                    }
                }
            }
        }

        private bool DecodeGrid_Regular(string gridContent)
        {
            gridContent = gridContent.Replace("\r", "")
                    .Replace(" ", "");
            // Find if the grid is valid
            GridDimensions dimensions = FindGridDimensions_Regular(gridContent);
            if (dimensions == null)
            {
                return false;
            }

            if (!asCSP && dimensions.GridSizeY != 9)
            {
                MessageBox.Show("La taille de sudoku supportée est de 9*9 uniquement avec une structure de tableau.");
                return false;
            }

            if (asCSP)
            {
                csp.ClearLists();
                csp.Dimensions = dimensions;
            }
            actualDimensions = dimensions;
            recreateCells();

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
                        cell.ZoneNumber = dimensions.NumberOfSquaresOnLine() * (actualIndex / dimensions.SquareSizeX) + j / dimensions.SquareSizeY;
                        GraphNode node = new GraphNode(cell);
                        csp.Nodes.Add(node);
                    }
                }
                actualIndex++;

            }
            csp.GenerateArcs();
            return true;
        }

        private bool DecodeGrid_Irregular(string gridContent)
        {
            if (!asCSP)
            {
                MessageBox.Show("Les sudokus irréguliers ne sont solvables qu'avec un CSP.");
                return false;
            }
            gridContent = gridContent.Replace("\r", "")
                    .Replace(" ", "");
            // Find size of grid
            GridDimensions dimensions = FindGridDimensions_Irregular(gridContent);
            if (dimensions == null)
            {
                return false;
            }
            if (dimensions.GridSizeX > colors.Count)
            {
                MessageBox.Show("Les sudokus irréguliers sont pour le moment limités à une taille de " 
                    + colors.Count.ToString() 
                    + "x" 
                    + colors.Count.ToString()
                    + ".");
                return false;
            }

            // Recuperate zones
            string[] zones = GetZones(gridContent, dimensions.GridSizeX);
            if (zones == null)
            {
                return false;
            }

            actualDimensions = dimensions;
            csp.ClearLists();
            recreateCells();

            string cleanContent = gridContent.Replace("!", "")
                .Replace(" ", "")
                .Replace("-", "");
            string[] columns = cleanContent.Split('\n');

            for (int i = 0; i < dimensions.GridSizeX; ++i)
            {
                for (int j = 0; j < dimensions.GridSizeX; ++j)
                {
                    Cell cell = new Cell(i, j, dimensions.GridSizeX);
                    cell.Value = Convert.ToChar(columns[i][j]);
                    int value;
                    char zoneChar = zones[i][j];
                    if (zoneChar >= 'A')
                    {
                        value = 9 + zoneChar - 'A';
                    }
                    else
                    {
                        value = zoneChar - '1';
                    }
                    cell.ZoneNumber = value;
                    GraphNode node = new GraphNode(cell);
                    csp.Nodes.Add(node);
                }
            }
            csp.GenerateArcs();
            return true;
        }

        private void UpdateGridDisplay_Regular()
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
                foreach (GraphNode node in csp.Nodes)
                {
                    Cell cell = node.Cell;
                    //string value;
                    //if (cell.Value >= 'A')
                    //{
                    //    int realInteger = 10 + cell.Value - 'A';
                    //    Console.WriteLine(realInteger);
                    //    value = realInteger.ToString();
                    //    Console.WriteLine(value);
                    //}
                    //else
                    //{
                    //    value = cell.Value.ToString();
                    //}
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

        private void UpdateGridDisplay_Irregular()
        {
            foreach (GraphNode node in csp.Nodes)
            {
                Cell cell = node.Cell;
                cells[cell.PosY, cell.PosX].Text = cell.Value.ToString();
                cells[cell.PosY, cell.PosX].BackColor = colors[cell.ZoneNumber];
            }
        }

        private GridDimensions FindGridDimensions_Regular(string gridContent)
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

        private GridDimensions FindGridDimensions_Irregular(string gridContent)
        {
            string[] columns = gridContent.Split('\n');
            // We need to know the size m*m of the grid. We're gonna ask for the grid to be simple (no ! or -)
            // An irregular grid is just characters.
            int possibleSize = 0;
            int index = 0;
            foreach (string column in columns)
            {
                if (column.Length == 0)
                {
                    break;
                }
                if (possibleSize == 0)
                {
                    possibleSize = column.Length;
                }
                else if (possibleSize != column.Length) // Not a square
                {
                    MessageBox.Show("Invalid grid !");
                    return null;
                }
                index++;
            }

            if (index != possibleSize) // Not a square
            {
                MessageBox.Show("Invalid grid !");
                return null;
            }

            return new GridDimensions(index, index, -1, -1);
        }



        private string[] GetZones(string gridContent, int gridSize)
        {
            string[] columns = gridContent.Split('\n');
            // First we don't need the grid we want to find the empty cell

            Dictionary<char, int> characterCount = new Dictionary<char, int>();
            int parsedColumns = 0;
            for (int index = gridSize + 1; index < columns.Length; ++index)
            {
                if (columns[index].Length == 0)
                {
                    break;
                }
                else if (gridSize != columns[index].Length) // Not a square
                {
                    MessageBox.Show("Invalid grid 1 !");
                    return null;
                }

                int count;
                for (int j = 0; j < gridSize; ++j)
                {
                    char number = columns[index][j];
                    characterCount.TryGetValue(number, out count);
                    if (count == 0)
                    {
                        characterCount.Add(number, 1);
                    }
                    else
                    {
                        characterCount[number] = ++count;
                    }
                }
                parsedColumns++;
            }
            if (parsedColumns != gridSize)
            {
                MessageBox.Show("Invalid grid 2 !");
                return null;
            }
            if (characterCount.Count != gridSize)
            {
                MessageBox.Show("Invalid grid 3 !");
                return null;
            }

            foreach (char key in characterCount.Keys)
            {
                if (characterCount[key] != gridSize)
                {
                    MessageBox.Show("Invalid grid 4 !");
                    return null;
                }
            }

            string[] zones = new string[gridSize];
            Array.Copy(columns, gridSize + 1, zones, 0, gridSize);
            return zones;
        }


        private void BtnResolve_Click(object sender, EventArgs e)
        {
            if (state == State.STRUCT_CHANGED)
            {
                MessageBox.Show("Structure changée. Rechargez un sudoku !");
                return;
            }
            if (state == State.INITIAL_STATE)
            {
                MessageBox.Show("Chargez d'abord un sudoku !");
                return;
            }
            if (!asCSP)
            {
                if (cells[0, 0].Text != String.Empty)
                {
                    Cell[,] solvedSudoku = grid.BacktrackingSearch();
                    if (solvedSudoku == null)
                    {
                        MessageBox.Show("Backtracking failed.");
                    }
                    else
                    {
                        grid.SudokuGrid = solvedSudoku;
                        UpdateGridDisplay_Regular();
                    }
                }

            }
            else
            {
                if (cells[0, 0].Text != String.Empty)
                {
                    if (csp.BacktrackingSearch())
                    {
                        if (!irregularSudoku)
                        {
                            UpdateGridDisplay_Regular();
                        }
                        else
                        {
                            UpdateGridDisplay_Irregular();
                        }
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
            LabelWaitingGeneration.Visible = true;
            LabelWaitingGeneration.Update();
            actualDimensions = new GridDimensions(9, 9, 3, 3);
            csp.Dimensions = actualDimensions;
            csp.ClearLists();
            recreateCells();
            csp.GenerateSudoku((25.0f / 100.0f) * (actualDimensions.GridSizeX * actualDimensions.GridSizeY));
            LabelWaitingGeneration.Visible = false;
            UpdateGridDisplay_Regular();
        }

        private void Debug_Click(object sender, EventArgs e)
        {
            // Arrange
            CSP csp2 = new CSP();
            GraphNode gn0 = new GraphNode(new Cell(0, 0));
            GraphNode gn1 = new GraphNode(new Cell(0, 2));
            GraphNode gn2 = new GraphNode(new Cell(0, 6));
            csp2.Nodes.Add(gn0);
            csp2.Nodes.Add(gn1);
            csp2.Nodes.Add(gn2);

            GraphArc ga01 = new GraphArc(gn0, gn1);
            GraphArc ga02 = new GraphArc(gn0, gn2);
            GraphArc ga10 = new GraphArc(gn1, gn0);
            GraphArc ga12 = new GraphArc(gn1, gn2);
            GraphArc ga20 = new GraphArc(gn2, gn0);
            GraphArc ga21 = new GraphArc(gn2, gn1);

            // Act
            csp2.GenerateArcs();
        }

        private void Sudoku_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnChngStruct_Click(object sender, EventArgs e)
        {
            asCSP = !asCSP;
            state = State.STRUCT_CHANGED;
            csp.ClearLists();
            if (asCSP)
            {
                MessageBox.Show("Structure utilisée : CSP");
            }
            else
            {
                MessageBox.Show("Structure utilisée : Tableau");
            }

        }

        private void BtnChngRegular_Click(object sender, EventArgs e)
        {
            irregularSudoku = !irregularSudoku;
            state = State.STRUCT_CHANGED;
            csp.ClearLists();

            if (irregularSudoku)
            {
                MessageBox.Show("Type de sudoku : irrégulier");
            }
            else
            {
                MessageBox.Show("Type de sudoku : régulier");
            }
        }
    }
}
