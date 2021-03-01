using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Graphic
{
    public class GraphNode
    {
        private Cell cell;
        public Cell Cell { get => cell; set => cell = value; }

        List<GraphArc> connectedArcs;
        public List<GraphArc> ConnectedArcs { get => connectedArcs; set => connectedArcs = value; }

        public GraphNode(Cell _cell)
        {
            cell = _cell;
            connectedArcs = new List<GraphArc>();
        }

        #region Operators
        protected bool Equals(GraphNode node2)
        {
            return cell.Equals(cell) &&
                connectedArcs.All(node2.ConnectedArcs.Contains);
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
            int hash = cell.GetHashCode() ^ 13;
            foreach (var arc in connectedArcs)
            {
                hash += 2*arc.GetHashCode() ^ 5;
            }
            return hash;
        }

        #endregion
    }
}
