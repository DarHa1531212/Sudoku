using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Graphic
{
    class Grid
    {
        #region Constants

        const int gridSize = 9;

        #endregion

        #region Attributs

        private char[,] sudokuGrid = new char[gridSize, gridSize];
        public char[,] SudokuGrid { get => sudokuGrid; set => sudokuGrid = value; }

        #endregion

        #region Ctor

        public Grid ()
        {

        }

        #endregion

        #region Public methods

        public char[,] BacktrackingSearch()
        {
            return RecursiveBacktracking(SudokuGrid);
        }

        public char[,] RecursiveBacktracking(char[,] grid)
        {
            if (IsComplete(grid))
            {
                return grid;
            }
            Tuple<int, int> selectedVariable = SelectUnassignedVariable(grid);

            foreach (var value in OrderDomainValues(selectedVariable, grid))
            {
                if (IsConsistent(value, grid))
                {
                    grid[selectedVariable.Item1, selectedVariable.Item2] = value;
                    char[,] result = RecursiveBacktracking(grid);
                    if (result != null)
                    {
                        return result;
                    }
                    grid[selectedVariable.Item1, selectedVariable.Item2] = '.';
                }
            }

            return null;
        }

        #endregion

        #region Private methods

        private bool IsComplete(char[,] grid)
        {
            foreach (var cell in grid)
            {
                if (cell == '.')
                {
                    return false;
                }
            }
            return true;
        }

        private Tuple<int, int> SelectUnassignedVariable(char[,] grid)
        {
            // TODO: modifier la façon dont est selectionnée la case
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (grid[j, i] == '.')
                    {
                        return new Tuple<int, int>(j, i);
                    }
                }
            }
            return null;
        }

        private List<char> OrderDomainValues(Tuple<int, int> position, char[,] grid)
        {
            // TODO: modifier pour les ordonner
            List<char> values = new List<char>();
            for(char v = '1'; v == '9'; v++)
            {
                values.Add(v);
            }
            return values;
        }

        private bool IsConsistent(char value, char[,] grid)
        {
            // TODO: ajouter la vérification des contraintes
            return true;
        }

        #endregion
    }
}
