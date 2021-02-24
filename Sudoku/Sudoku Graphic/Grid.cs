using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Graphic
{
    class Grid
    {
        const int gridSize = 9;
        private char[,] sudokuGrid = new char[gridSize, gridSize];
        public Grid(){
        }

        public char[,] SudokuGrid { get => sudokuGrid; set => sudokuGrid = value; }
    }
}
