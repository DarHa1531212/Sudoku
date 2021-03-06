using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku_Graphic;

namespace Tests
{
    /// <summary>
    /// Description résumée pour GridTests
    /// </summary>
    [TestClass]
    public class GridTests
    {
        [TestMethod]
        public void T_Grid()
        {
            // Arrange
            Cell[,] sudoku = new Cell[,] {
                {
                    new Cell(0, 0),
                    new Cell(0, 1),
                    new Cell(0, 2),
                    new Cell(0, 3),
                    new Cell(0, 4),
                    new Cell(0, 5),
                    new Cell(0, 6),
                    new Cell(0, 7),
                    new Cell(0, 8)
                },
                {
                    new Cell(1, 0),
                    new Cell(1, 1),
                    new Cell(1, 2),
                    new Cell(1, 3),
                    new Cell(1, 4),
                    new Cell(1, 5),
                    new Cell(1, 6),
                    new Cell(1, 7),
                    new Cell(1, 8)
                },
                {
                    new Cell(2, 0),
                    new Cell(2, 1),
                    new Cell(2, 2),
                    new Cell(2, 3),
                    new Cell(2, 4),
                    new Cell(2, 5),
                    new Cell(2, 6),
                    new Cell(2, 7),
                    new Cell(2, 8)
                },
                {
                    new Cell(3, 0),
                    new Cell(3, 1),
                    new Cell(3, 2),
                    new Cell(3, 3),
                    new Cell(3, 4),
                    new Cell(3, 5),
                    new Cell(3, 6),
                    new Cell(3, 7),
                    new Cell(3, 8)
                },
                {
                    new Cell(4, 0),
                    new Cell(4, 1),
                    new Cell(4, 2),
                    new Cell(4, 3),
                    new Cell(4, 4),
                    new Cell(4, 5),
                    new Cell(4, 6),
                    new Cell(4, 7),
                    new Cell(4, 8)
                },
                {
                    new Cell(5, 0),
                    new Cell(5, 1),
                    new Cell(5, 2),
                    new Cell(5, 3),
                    new Cell(5, 4),
                    new Cell(5, 5),
                    new Cell(5, 6),
                    new Cell(5, 7),
                    new Cell(5, 8)
                },
                {
                    new Cell(6, 0),
                    new Cell(6, 1),
                    new Cell(6, 2),
                    new Cell(6, 3),
                    new Cell(6, 4),
                    new Cell(6, 5),
                    new Cell(6, 6),
                    new Cell(6, 7),
                    new Cell(6, 8)
                },
                {
                    new Cell(7, 0),
                    new Cell(7, 1),
                    new Cell(7, 2),
                    new Cell(7, 3),
                    new Cell(7, 4),
                    new Cell(7, 5),
                    new Cell(7, 6),
                    new Cell(7, 7),
                    new Cell(7, 8)
                },
                {
                    new Cell(8, 0),
                    new Cell(8, 1),
                    new Cell(8, 2),
                    new Cell(8, 3),
                    new Cell(8, 4),
                    new Cell(8, 5),
                    new Cell(8, 6),
                    new Cell(8, 7),
                    new Cell(8, 8)
                }
            };
            sudoku[0, 0].Value = '.';
            sudoku[1, 0].Value = '.';
            sudoku[2, 0].Value = '.';
            sudoku[3, 0].Value = '.';
            sudoku[4, 0].Value = '.';
            sudoku[5, 0].Value = '.';
            sudoku[6, 0].Value = '.';
            sudoku[7, 0].Value = '.';
            sudoku[8, 0].Value = '.';
            sudoku[0, 1].Value = '.';
            sudoku[1, 1].Value = '.';
            sudoku[2, 1].Value = '.';
            sudoku[3, 1].Value = '.';
            sudoku[4, 1].Value = '.';
            sudoku[5, 1].Value = '.';
            sudoku[6, 1].Value = '.';
            sudoku[7, 1].Value = '.';
            sudoku[8, 1].Value = '.';
            sudoku[0, 2].Value = '.';
            sudoku[1, 2].Value = '.';
            sudoku[2, 2].Value = '.';
            sudoku[3, 2].Value = '.';
            sudoku[4, 2].Value = '.';
            sudoku[5, 2].Value = '.';
            sudoku[6, 2].Value = '.';
            sudoku[7, 2].Value = '.';
            sudoku[8, 2].Value = '.';
            sudoku[0, 3].Value = '.';
            sudoku[1, 3].Value = '.';
            sudoku[2, 3].Value = '.';
            sudoku[3, 3].Value = '.';
            sudoku[4, 3].Value = '.';
            sudoku[5, 3].Value = '.';
            sudoku[6, 3].Value = '.';
            sudoku[7, 3].Value = '.';
            sudoku[8, 3].Value = '.';
            sudoku[0, 4].Value = '.';
            sudoku[1, 4].Value = '.';
            sudoku[2, 4].Value = '.';
            sudoku[3, 4].Value = '.';
            sudoku[4, 4].Value = '.';
            sudoku[5, 4].Value = '.';
            sudoku[6, 4].Value = '.';
            sudoku[7, 4].Value = '.';
            sudoku[8, 4].Value = '.';
            sudoku[0, 5].Value = '.';
            sudoku[1, 5].Value = '.';
            sudoku[2, 5].Value = '.';
            sudoku[3, 5].Value = '.';
            sudoku[4, 5].Value = '.';
            sudoku[5, 5].Value = '.';
            sudoku[6, 5].Value = '.';
            sudoku[7, 5].Value = '.';
            sudoku[8, 5].Value = '.';
            sudoku[0, 6].Value = '.';
            sudoku[1, 6].Value = '.';
            sudoku[2, 6].Value = '.';
            sudoku[3, 6].Value = '.';
            sudoku[4, 6].Value = '.';
            sudoku[5, 6].Value = '.';
            sudoku[6, 6].Value = '.';
            sudoku[7, 6].Value = '.';
            sudoku[8, 6].Value = '.';
            sudoku[0, 7].Value = '.';
            sudoku[1, 7].Value = '.';
            sudoku[2, 7].Value = '.';
            sudoku[3, 7].Value = '.';
            sudoku[4, 7].Value = '.';
            sudoku[5, 7].Value = '.';
            sudoku[6, 7].Value = '.';
            sudoku[7, 7].Value = '.';
            sudoku[8, 7].Value = '.';
            sudoku[0, 8].Value = '.';
            sudoku[1, 8].Value = '.';
            sudoku[2, 8].Value = '.';
            sudoku[3, 8].Value = '.';
            sudoku[4, 8].Value = '.';
            sudoku[5, 8].Value = '.';
            sudoku[6, 8].Value = '.';
            sudoku[7, 8].Value = '.';
            sudoku[8, 8].Value = '.';

            // Act
            Grid grid = new Grid();

            // Assert
            CollectionAssert.AreEqual(grid.SudokuGrid, sudoku);
        }
    }
}
