using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Graphic
{
    public class GridDimensions
    {
        private int gridSizeX;
        private int gridSizeY;
        private int squareSizeX;
        private int squareSizeY;

        public int GridSizeX { get => gridSizeX; set => gridSizeX = value; }
        public int GridSizeY { get => gridSizeY; set => gridSizeY = value; }
        public int SquareSizeX { get => squareSizeX; set => squareSizeX = value; }
        public int SquareSizeY { get => squareSizeY; set => squareSizeY = value; }

        public GridDimensions(int _gridSizeX, int _gridSizeY, int _squareSizeX, int _squareSizeY)
        {
            gridSizeX = _gridSizeX;
            gridSizeY = _gridSizeY;
            squareSizeX = _squareSizeX;
            squareSizeY = _squareSizeY;
        }

        public bool IsValid()
        {
            return gridSizeX == GridSizeY &&
                gridSizeX % squareSizeX == 0 &&
                gridSizeY % squareSizeY == 0;
        }

        public int NumberOfSquaresOnLine()
        {
            return gridSizeX / squareSizeX;
        }

        public int NumberOfSquaresOnColumn()
        {
            return gridSizeY / squareSizeY;
        }
    }
}
