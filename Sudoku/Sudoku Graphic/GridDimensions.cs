using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Graphic
{
    public class GridDimensions
    {
        #region Attributes
        /// <summary>
        /// The number of cells in each column.
        /// </summary>
        private int gridSizeX;
        /// <summary>
        /// Gets and set the number of cells in each column.
        /// </summary>
        public int GridSizeX { get => gridSizeX; set => gridSizeX = value; }

        /// <summary>
        /// Gets and sets <see cref="GridDimensions.gridSizeX"/>
        /// </summary>
        private int gridSizeY;
        /// <summary>
        /// Gets and sets <see cref="GridDimensions.gridSizeY"/>
        /// </summary>
        public int GridSizeY { get => gridSizeY; set => gridSizeY = value; }

        /// <summary>
        /// The number of cells (column-wise) in a zone where no value can be duplicated in the sudoku.
        /// </summary>
        private int squareSizeX;
        /// <summary>
        /// Gets and sets <see cref="GridDimensions.squareSizeX"/>
        /// </summary>
        public int SquareSizeX { get => squareSizeX; set => squareSizeX = value; }

        /// <summary>
        /// The number of cells (line-wise) in a zone where no value can be duplicated in the sudoku.
        /// </summary>
        private int squareSizeY;
        /// <summary>
        /// Gets and sets <see cref="GridDimensions.squareSizeY"/>
        /// </summary>
        public int SquareSizeY { get => squareSizeY; set => squareSizeY = value; }
        #endregion

        #region Ctors
        /// <summary>
        /// Initializes an instance of <see cref="GridDimensions"/> given sizes of a sudoku grid
        /// </summary>
        /// <param name="_gridSizeX">The number of cells in each column.</param>
        /// <param name="_gridSizeY">The number of cells in each line.</param>
        /// <param name="_squareSizeX">The number of cells (column-wise) in a zone where no value can be duplicated in the sudoku.</param>
        /// <param name="_squareSizeY">The number of cells (line-wise) in a zone where no value can be duplicated in the sudoku.</param>
        public GridDimensions(int _gridSizeX, int _gridSizeY, int _squareSizeX, int _squareSizeY)
        {
            gridSizeX = _gridSizeX;
            gridSizeY = _gridSizeY;
            squareSizeX = _squareSizeX;
            squareSizeY = _squareSizeY;
        }

        #endregion

        #region Public methods
        /// <summary>
        /// Checks if the dimensions represent a square grid
        /// </summary>
        /// <returns>
        ///   <c>false</c> if <see cref="GridDimensions.gridSizeY"/> and <see cref="GridDimensions.gridSizeX"/> are not equal;
        ///   <c>false</c> if the grid isn't only composed of zones;
        ///   otherwise <c>false</c>.
        /// </returns>
        public bool IsValid()
        {
            return gridSizeX == gridSizeY &&
                gridSizeX % squareSizeX == 0 &&
                gridSizeY % squareSizeY == 0;
        }
        /// <summary>
        /// Returns the number of zones on each line.
        /// </summary>
        /// <returns>
        /// The number of zones on each line.
        /// </returns>
        public int NumberOfSquaresOnLine()
        {
            return gridSizeX / squareSizeX;
        }

        /// <summary>
        /// Returns the number of zones on each column.
        /// </summary>
        /// <returns>
        /// The number of zones on each column.
        /// </returns>
        public int NumberOfSquaresOnColumn()
        {
            return gridSizeY / squareSizeY;
        }
        #endregion
    }
}
