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
        /// Gets and sets <see cref="Cell.posX"/>.
        /// </summary>
        public int PosX { get => posX; set => posX = value; }

        /// <summary>
        /// The y position of the cell in the sudoku grid.
        /// </summary>
        private int posY;
        /// <summary>
        /// Gets and sets <see cref="Cell.posY"/>.
        /// </summary>
        public int PosY { get => posY; set => posY = value; }

        /// <summary>
        /// The character inside the cell. If the cell is empty, it is represented by a '.'
        /// </summary>
        private char value;
        /// <summary>
        /// Gets and sets <see cref="Cell.value"/>.
        /// </summary>
        public char Value { get => value; set => this.value = value; }

        /// <summary>
        /// The possible values (different than '.') that the field value can take. 
        /// </summary>
        private List<char> domain;
        /// <summary>
        /// Gets and sets <see cref="Cell.domain"/>.
        /// </summary>
        public List<char> Domain { get => domain; set => domain = value; }

        /// <summary>
        /// Idicates in which zone (equivalent of the 3*3 squares on a normal 9*9 sudoku) the cell is.
        /// </summary>
        private int zoneNumber;
        /// <summary>
        /// Gets and sets <see cref="Cell.zoneNumber"/>.
        /// </summary>
        public int ZoneNumber { get => zoneNumber; set => zoneNumber = value; }

        #endregion

        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="x">The x position of the cell in the grid.</param>
        /// <param name="y">The y position of the cell in the grid.</param>
        /// <param name="domainSize">The number of possible values of the cell.</param>
        public Cell(int x = 0, int y = 0, int domainSize = 9)
        {
            posX = x;
            posY = y;
            if(domainSize < 1)
            {
                throw new ArgumentException("La taille du domaine doit être comprise entre 1 et 9");
            }
            domain = new List<char>();
            for(int i = 1; i <= Math.Min(9, domainSize); ++i)
            {
                domain.Add((char)(i + '0'));
            }
            for(int i = 0; i < Math.Max(domainSize - 9, 0); ++i)
            {
                domain.Add((char)(i + 'A'));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class with a given position
        /// and domain.
        /// </summary>
        /// <param name="x">The x position of the cell in the grid.</param>
        /// <param name="y">The y position of the cell in the grid.</param>
        /// <param name="_domain">The domain of the cell.</param>
        public Cell(int x, int y, List<char> _domain)
        {
            posX = x;
            posY = y;
            domain = new List<char>(_domain);
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
            zoneNumber = cell.ZoneNumber;
            domain = new List<char>(cell.Domain);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Removes a value from the <see cref="Cell.domain"/>.
        /// </summary>
        /// <param name="value">The value to remove.</param>
        public void RemoveFromDomain(char value)
        {
            domain.Remove(value);
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
                zoneNumber == cell2.ZoneNumber &&
                domain.Count == cell2.Domain.Count &&
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
            int hash = posX.GetHashCode() ^ 5 +
                8 * posY.GetHashCode() ^ 17 +
                6*zoneNumber.GetHashCode()^5;
            //foreach (char c in domain)
            //{
            //    hash += c.GetHashCode() ^ 9;
            //}
            return hash;
        }

        #endregion
    }
}
