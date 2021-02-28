﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Graphic
{
    public class GraphArc
    {
        #region Attributes
        /// <summary>
        /// The first <see cref="Cell"/> connected by this instance of <see cref="GraphArc"/> in a graph.
        /// </summary>
        private Cell cell1;

        /// <summary>
        /// The second <see cref="Cell"/> connected by this instance of <see cref="GraphArc"/> in a graph.
        /// </summary>
        private Cell cell2;
        #endregion

        #region Ctors
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphArc"/> class.
        /// </summary>
        /// <param name="cell1">The first instance of <see cref="Cell"/> connected by this <see cref="GraphArc"/></param>
        /// <param name="cell2">The second instance of <see cref="Cell"/> connected by this <see cref="GraphArc"/></param>
        public GraphArc(Cell cell1, Cell cell2)
        {
            this.cell1 = cell1;
            this.cell2 = cell2;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Checks whether the constraint represented by this instance of <see cref="GraphArc"/> is respected.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the constraint is respected; otherwise, <c>false</c>
        /// </returns>
        public bool IsConsistant()
        {
            if(cell1.Value == '.' || cell2.Value == '.')
            {
                return true;
            }
            return cell1.Value != cell2.Value;
        }

        /// <summary>
        /// Checks whether or not this <see cref="GraphArc"/> and another 
        /// instance of <see cref="GraphArc"/> connect the same two instances of <see cref="Cell"/>. 
        /// </summary>
        /// <param name="arc2">The second instance of <see cref="GraphArc"/> to test.</param>
        /// <returns>
        ///   <c>true</c> if both instances connect the same cells; otherwise, <c>false</c>
        /// </returns>
        public bool IsDuplicata(GraphArc arc2)
        {
            return this.cell1 == arc2.cell1 &&
                this.cell2 == arc2.cell2;
        }

        /// <summary>
        /// Checks if the given instance of <see cref="Cell"/> is one of the two <see cref="Cell"/>
        /// connected by this arc.
        /// </summary>
        /// <param name="cell">The instance of the <see cref="Cell"/></param>
        /// <returns>
        ///   <c>true</c> if the <see cref="Cell"/> is either equal to <see cref="GraphArc.cell1"/> or <see cref="GraphArc.cell2"/>;
        ///   otherwise, <c>false</c>
        /// </returns>
        public bool IsCellIn(Cell cell)
        {
            return cell1 == cell || cell2 == cell;
        }

        /// <summary>
        /// Given one instance of a <see cref="Cell"/>, returns the value of the other <see cref="Cell"/>
        /// connected by this arc.
        /// </summary>
        /// <param name="cell">The first instance of <see cref="Cell"/> connected to this arc.</param>
        /// <returns>
        /// The field <see cref="Cell.value"/> of <see cref="GraphArc.cell1"/> if <param name="cell"> is equal to <see cref="GraphArc.cell2"/>;
        /// The field <see cref="Cell.value"/> of <see cref="GraphArc.cell2"/> if <param name="cell"> is equal to <see cref="GraphArc.cell2"/>;
        /// ' ' otherwise.
        /// </returns>
        public char GetOtherCellValue(Cell cell)
        {
            if (cell == cell1)
            {
                return cell2.Value;
            }
            if (cell == cell2)
            {
                return cell1.Value;
            }
            return ' ';
        }
        #endregion

        #region Operators

        /// <summary>
        /// Tests the equality between the fields of two <see cref="GraphArc"/> instances.
        /// </summary>
        /// <param name="arc2">The <see cref="GraphArc"/> on which the test is realized.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="GraphArc"/>'s fields are equal to this instance; otherwise, <c>false</c>
        /// </returns>
        protected bool Equals(GraphArc arc2)
        {
            return cell1.Equals(arc2.cell1) &&
                cell2.Equals(arc2.cell2);
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
            return Equals((GraphArc)obj);
        }

        /// <summary>
        /// Returns the hashcode of this instance of <see cref="GraphArc"/>.
        /// </summary>
        /// <returns>
        /// The hashcode of this instance of <see cref="GraphArc"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return cell1.GetHashCode() + 15 * cell2.GetHashCode() ^ 2;
        }
        #endregion
    }
}