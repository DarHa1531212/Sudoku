using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Graphic
{

    public class Cell
    {
        #region Attributes

        /// <summary>
        /// The x position of the cell in the sudoku grid.
        /// </summary>
        private int posX;
        /// <summary>
        /// Gets and sets the x position of the cell.
        /// </summary>
        public int PosX { get => posX; set => posX = value; }

        /// <summary>
        /// The y position of the cell in the sudoku grid.
        /// </summary>
        private int posY;
        /// <summary>
        /// Gets and sets the y position of the cell.
        /// </summary>
        public int PosY { get => posY; set => posY = value; }

        /// <summary>
        /// The character inside the cell. If the cell is empty, it is represented by a '.'
        /// </summary>
        private char value;
        /// <summary>
        /// Gets and sets the value of the cell.
        /// </summary>
        public char Value { get => value; set => this.value = value; }

        /// <summary>
        /// The possible values (different than '.') that the field value can take. 
        /// </summary>
        private List<char> domain;
        /// <summary>
        /// Gets and sets the domain of the cell.
        /// </summary>
        public List<char> Domain { get => domain; set => domain = value; }
        #endregion

        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="x">The x position of the cell in the grid.</param>
        /// <param name="y">The y position of the cell in the grid.</param>
        public Cell(int x = 0, int y = 0)
        {
            posX = x;
            posY = y;
            domain = new List<char>(new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' });
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class by copying
        /// another <see cref="Cell"/> instance.
        /// </summary>
        /// <param name="cell">The cell to copy.</param>
        public Cell(Cell cell)
        {
            posX = cell.PosX;
            posY = cell.PosY;
            value = cell.Value;
            domain = new List<char>(cell.Domain);
        }
        #endregion

        #region Operators
        /// <summary>
        /// Tests the equality between the fields of two <see cref="Cell"/> instances.
        /// </summary>
        /// <param name="cell2">The <see cref="Cell"/> on which the test is realized.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="Cell"/>'s fields are equal to this instance; otherwise, <c>false</c>
        /// </returns>
        protected bool Equals(Cell cell2)
        {
            return posX == cell2.PosX &&
                posY == cell2.PosY &&
                value == cell2.Value &&
                domain.All(cell2.Domain.Contains);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Cell)obj);
        }

        /// <summary>
        /// Returns the hashcode of this instance of <see cref="Cell"/>.
        /// </summary>
        /// <returns>
        /// The hashcode of this instance of <see cref="Cell"/>.
        /// </returns>
        public override int GetHashCode()
        {
            int hash = posX.GetHashCode()^5 +
                8*posY.GetHashCode()^17 +
                6*value.GetHashCode()^5;
            foreach (char c in domain)
            {
                hash += c.GetHashCode() ^ 9;
            }
            return hash;
        }
        #endregion
    }
}
