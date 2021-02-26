using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Graphic
{
    public class Grid
    {
        #region Constants

        const int gridSize = 9;
        const int squareSize = 3;

        #endregion

        #region Attributs

        private Cell[,] sudokuGrid = new Cell[gridSize, gridSize];
        public Cell[,] SudokuGrid { get => sudokuGrid; set => sudokuGrid = value; }

        #endregion

        #region Ctor

        public Grid()
        {
            for(int i = 0; i < gridSize; ++i)
            {
                for (int j = 0; j < gridSize; ++j)
                {
                    sudokuGrid[j, i] = new Cell(j, i);
                    sudokuGrid[j, i].Value = '.';
                }

            }
        }

        #endregion

        #region Public methods

        public Cell[,] BacktrackingSearch()
        {
            char[,] cellsAsChar = CellsAsChar();
            return RecursiveBacktracking(sudokuGrid);
        }

        public Cell[,] RecursiveBacktracking(Cell[,] grid)
        {
            if (IsComplete(grid))
            {
                return grid;
            }
            Tuple<int, int> selectedVariable = SelectUnassignedVariable(grid);

            foreach (var value in OrderDomainValues(selectedVariable, grid))
            {
                if (IsConsistent(selectedVariable, value, grid))
                {
                    grid[selectedVariable.Item1, selectedVariable.Item2].Value = value;
                    Cell[,] result = RecursiveBacktracking(grid);
                    if (result != null)
                    {
                        return result;
                    }
                    grid[selectedVariable.Item1, selectedVariable.Item2].Value = '.';
                }
            }
            return null;
        }

        #endregion

        #region Private methods

        private char[,] CellsAsChar()
        {

            char[,] cellsAsValue = new char[gridSize, gridSize];
            for (int i = 0; i < gridSize; ++i)
            {
                for (int j = 0; j < gridSize; ++j)
                {
                    cellsAsValue[i, j] = sudokuGrid[i, j].Value;
                }
            }
            return cellsAsValue;
        }

        private bool IsComplete(Cell[,] grid)
        {
            foreach (var cell in grid)
            {
                if (cell.Value == '.')
                {
                    return false;
                }
            }
            return true;
        }

        private Tuple<int, int> SelectUnassignedVariable(Cell[,] grid)
        {
            // TODO: modifier la façon dont est selectionnée la case
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (grid[j, i].Value == '.')
                    {
                        return new Tuple<int, int>(j, i);
                    }
                }
            }
            return null;
        }

        private Tuple<int, int> MRV(Cell[,] grid)
        {
            int minRemainingValues = int.MaxValue;
            Tuple<int, int> chosenVar = new Tuple<int, int>(-1, -1);
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (grid[j, i].Value == '.')
                    {
                        Tuple<int, int> testedVar = new Tuple<int, int>(j, i);
                        int remainingValuesCount = getRemainingPossibleValues(testedVar, grid).Count;
                        if (remainingValuesCount < minRemainingValues)
                        {
                            minRemainingValues = remainingValuesCount;
                            chosenVar = testedVar;
                        }
                    }
                }
            }
            return chosenVar;
        }

        private Tuple<int, int> DegreeHeuristic(Cell[,] grid)
        {
            int maxRemainingConstraints = int.MinValue;
            Tuple<int, int> chosenVar = new Tuple<int, int>(-1, -1);
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (grid[j, i].Value == '.')
                    {
                        Tuple<int, int> testedVar = new Tuple<int, int>(j, i);
                        int constraintsOfVar = getRemainingNumberOfConstraints(testedVar, grid);
                        if (constraintsOfVar > maxRemainingConstraints)
                        {
                            maxRemainingConstraints = constraintsOfVar;
                            chosenVar = testedVar;
                        }

                    }
                }
            }
            return chosenVar;
        }

        // Attention, bien donner le tuple sous format (x,y) / (j,i)
        private List<char> getRemainingPossibleValues(Tuple<int, int> gridLocation, Cell[,] grid)
        {
            List<char> remainingValues = new List<char>(
                new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' });
            // First, remove all values on the same line and colum
            for (int index = 0; index < gridSize; ++index)
            {
                remainingValues.Remove(grid[gridLocation.Item1, index].Value);
                remainingValues.Remove(grid[index, gridLocation.Item2].Value);
            }

            int squareNumberX = gridLocation.Item2 / squareSize;
            int squareNumberY = gridLocation.Item1 / squareSize;

            for (int i = 0 + squareNumberX * squareSize; i < 0 + squareNumberX * squareSize + 3; ++i)
            {
                for (int j = 0 + squareNumberY * squareSize; i < 0 + squareNumberY * squareSize + 3; ++j)
                {
                    remainingValues.Remove(grid[j, i].Value);
                }
            }
            return remainingValues;
        }

        // Attention, bien donner le tuple sous format (x,y) / (j,i)
        private int getRemainingNumberOfConstraints(Tuple<int, int> gridLocation, Cell[,] grid)
        {
            int constraintsRemaining = 0;
            // First, remove all values on the same line and colum
            for (int index = 0; index < gridSize; ++index)
            {
                if (index != gridLocation.Item1)
                {
                    if (grid[index, gridLocation.Item2].Value == '.')
                    {
                        constraintsRemaining++;
                    }
                }
                if (index != gridLocation.Item2)
                {
                    if (grid[gridLocation.Item1, index].Value == '.')
                    {
                        constraintsRemaining++;
                    }
                }
            }

            int squareNumberX = gridLocation.Item2 / squareSize;
            int squareNumberY = gridLocation.Item1 / squareSize;

            // Attention à ne pas ajouter 1 pour les contraintes déjà trouvées avec les lignes/colonnes
            for (int i = 0 + squareNumberX * squareSize; i < 0 + squareNumberX * squareSize + 3; ++i)
            {
                for (int j = 0 + squareNumberY * squareSize; i < 0 + squareNumberY * squareSize + 3; ++j)
                {
                    if (i != gridLocation.Item2 && j != gridLocation.Item1)
                    {
                        if (grid[j, i].Value == '.')
                        {
                            constraintsRemaining++;
                        }
                    }
                }
            }
            return constraintsRemaining;
        }

        private List<char> OrderDomainValues(Tuple<int, int> position, Cell[,] grid)
        {
            // TODO: modifier pour les ordonner
            /*
            List<char> values = new List<char>();
            for (char v = '1'; v == '9'; v++)
            {
                values.Add(v);
            }
            return values;
            */
            return grid[position.Item1, position.Item2].Domain;
        }

        private List<char> LeastConstraingValue(Tuple<int, int> position, Cell[,] grid)
        {
            // Question : est-ce qu'il faut prendre le minimum des valeurs possibles restantes ou bien les sommer ?
            // Dans le doute je prends le minimum
            Dictionary<char, int> remainingMinimalValues = new Dictionary<char, int>();

            foreach (char v in grid[position.Item2, position.Item1].Domain)
            {
                // Etape 1 : Créer la nouvelle grille
                Cell[,] testGrid = new Cell[gridSize, gridSize];
                Array.Copy(grid, 0, testGrid, gridSize * gridSize - 1, gridSize * gridSize);

                testGrid[position.Item2, position.Item1].Value = v;
                // Etape 2 : Itérer sur les éléments non remplis pour trouver le minimum de valeurs possibles
                int minRemainingValues = int.MaxValue;
                for (int i = 0; i < gridSize; ++i)
                {
                    for (int j = 0; j < gridSize; ++j)
                    {
                        if(grid[j,i].Value == '.')
                        {
                            Tuple<int, int> testedPosition = new Tuple<int, int>(j, i);
                            int remainingValues = getRemainingPossibleValues(testedPosition, testGrid).Count;
                            if(remainingValues < minRemainingValues)
                            {
                                minRemainingValues = remainingValues;
                            }
                        }
                    }
                }

                // Etape 3 : Insérer dans le dictionnaire
                remainingMinimalValues.Add(v, minRemainingValues);
            }

            List<char> orderedValues = new List<char>();
            // Trier le dictionnaire selon la valeur décroissante
            foreach (KeyValuePair<char, int> item in remainingMinimalValues.OrderByDescending(key => key.Value))
            {
                orderedValues.Add(item.Key);
            }

            return orderedValues;

        }

        private bool IsConsistent(Tuple<int, int> position, char value, Cell[,] grid)
        {
            // TODO: ajouter la vérification des contraintes
            for (int index = 0; index < gridSize; ++index)
            {
                if (index != position.Item1)
                {
                    if (grid[index, position.Item2].Value == value)
                    {
                        return false;
                    }
                }
                if (index != position.Item2)
                {
                    if (grid[position.Item1, index].Value == value)
                    {
                        return false;
                    }
                }
            }

            int squareNumberX = position.Item2 / squareSize;
            int squareNumberY = position.Item1 / squareSize;

            // Attention à ne pas ajouter 1 pour les contraintes déjà trouvées avec les lignes/colonnes
            for (int i = 0 + squareNumberX * squareSize; i < 0 + squareNumberX * squareSize + 3; ++i)
            {
                for (int j = 0 + squareNumberY * squareSize; i < 0 + squareNumberY * squareSize + 3; ++j)
                {
                    if (i != position.Item2 && j != position.Item1)
                    {
                        if (grid[j, i].Value == value)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        #endregion
    }
}
