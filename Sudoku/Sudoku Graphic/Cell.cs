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

        private int posX;
        public int PosX { get => posX; set => posX = value; }

        private int posY;
        public int PosY { get => posY; set => posY = value; }

        private char value;
        public char Value { get => value; set => this.value = value; }

        private List<char> domain;
        public List<char> Domain { get => domain; set => domain = value; }
        #endregion

        #region Ctors
        public Cell(int x = 0, int y = 0)
        {
            posX = x;
            posY = y;
            domain = new List<char>(new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' });
        }

        public Cell(Cell cell)
        {
            posX = cell.PosX;
            posY = cell.PosY;
            value = cell.Value;
            domain = new List<char>(cell.Domain);
        }
        #endregion

        #region Operators

        protected bool Equals(Cell cell2)
        {
            return posX == cell2.PosX &&
                posY == cell2.PosY &&
                value == cell2.Value &&
                domain.All(cell2.Domain.Contains);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Cell)obj);
        }

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
