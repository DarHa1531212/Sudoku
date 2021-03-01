using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Graphic
{
    public class GraphNode
    {
        #region Attributes
        /// <summary>
        /// The sudoku cell represented by this node.
        /// </summary>
        private Cell cell;
        /// <summary>
        /// Gets and sets <see cref="GraphNode.cell"/>
        /// </summary>
        public Cell Cell { get => cell; set => cell = value; }

        /// <summary>
        /// The list of arcs connecting this node in the CSP graph.
        /// </summary>
        List<GraphArc> connectedArcs;
        /// <summary>
        /// Gets and sets <see cref="GraphNode.connectedArcs"/>
        /// </summary>
        public List<GraphArc> ConnectedArcs { get => connectedArcs; set => connectedArcs = value; }
        #endregion

        #region Ctors
        /// <summary>
        /// Initializes an instance of <see cref="GraphNode"/>.
        /// </summary>
        /// <param name="_cell">The <see cref="Cell"/> represented by the node.</param>
        public GraphNode(Cell _cell)
        {
            cell = _cell;
            connectedArcs = new List<GraphArc>();
        }
        #endregion

        /// <summary>
        /// Checks whether the given <see cref="GraphNode"/> is identical to this instance.
        /// </summary>
        /// <param name="node2">The tested <see cref="GraphNode"/>.</param>
        /// <returns>
        ///   <c>true</c> if both <see cref="GraphNode"/> are determined equal; otherwise <c>false</c>.
        /// </returns>
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
        /// Returns the hashcode of this instance of <see cref="GraphNode"/>.
        /// </summary>
        /// <returns>
        /// The hashcode of this instance of <see cref="GraphNode"/>.
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
