﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Graphic
{
    public class CSP
    {
        #region Attributes

        public GridDimensions Dimensions { get => dimensions; set => dimensions = value; }
        private GridDimensions dimensions;

        public List<GraphNode> Nodes { get => nodes; set => nodes = value; }
        private List<GraphNode> nodes;

        #endregion

        #region Ctors
        /// <summary>
        /// Initializes a new instance of <see cref="CSP"/>
        /// </summary>
        public CSP()
        {
            nodes = new List<GraphNode>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Generates the constraints (populates graphArcs with <see cref="GraphArc"/>) between the 
        /// different <see cref="Cell"/> in <see cref="CSP.cells"/> using classic sudoku's rules. Ensures there will be
        /// no duplicated constraint.
        /// </summary>
        public void GenerateArcs()
        {
            foreach(GraphNode node1 in nodes)
            {
                Cell cell1 = node1.Cell;
                int squareX1 = cell1.PosX / dimensions.SquareSizeX;
                int squareY1 = cell1.PosY / dimensions.SquareSizeY;
                foreach(GraphNode node2 in nodes)
                {
                    Cell cell2 = node2.Cell;
                    if(cell1.Equals(cell2))
                    {
                        continue;
                    }
                    int squareX2 = cell2.PosX / dimensions.SquareSizeX;
                    int squareY2 = cell2.PosY / dimensions.SquareSizeY;
                    if (cell1.PosX == cell2.PosX || cell1.PosY == cell2.PosY ||
                        (squareX1 == squareX2 && squareY1 ==squareY2))
                    {
                        node1.ConnectedArcs.Add(new GraphArc(node1, node2));
                    }
                }
            }
        }
        /// <summary>
        /// Launches the backtracking-search algorithm to try and solve the sudoku.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the sudoku has been resolved; otherwise <c>false</c>.
        /// </returns>
        public bool BacktrackingSearch()
        {
            //char[,] cellsAsChar = CellsAsChar();
            if(!IsConsistant())
            {
                return false;
            }
            return RecursiveBacktracking();
        }

        /// <summary>
        /// Clears <see cref="CSP.cells"/> and <see cref="CSP.graphArcs"/>.
        /// </summary>
        public void ClearLists()
        {
            nodes.Clear();
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// The recursive part of the backtracking-search algorithm. Iterates over the possible <see cref="Cell.value"/> of every
        /// <see cref="Cell"/> whose <see cref="Cell.value"/> is equal to '.' until either a solution is found or every possible state
        /// has been tested.
        /// </summary>
        /// <returns>
        ///  <c>true</c> if a solution has been found;
        ///  <c>false</c> when the tested state leads to a invalid constraint.
        /// </returns>
        private bool RecursiveBacktracking()
        {
            if (IsComplete()) {
                return true;
            }
            GraphNode chosenNode = SelectUnassignedVariable();

            foreach(char value in OrderDomainValues(chosenNode))
            {
                chosenNode.Cell.Value = value;
                if (IsConsistant(chosenNode))
                {
                    bool success = RecursiveBacktracking();
                    if(success)
                    {
                        return true;
                    }
                }
                chosenNode.Cell.Value = '.';
            }
            return false;
        }


        /// <summary>
        /// Checks if the actual CSP assignment is consistent.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if every constraint in the <see cref="CSP.graphArcs"/> is respected.
        /// </returns>
        private bool IsConsistant()
        {
            foreach (GraphNode node in nodes)
            {
                foreach(GraphArc arc in node.ConnectedArcs)
                {
                    if(!arc.IsConsistant())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool IsConsistant(GraphNode node)
        {
            foreach(GraphArc arc in node.ConnectedArcs)
            {
                if (!arc.IsConsistant())
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks whether the actual assignment of the <see cref="CSP"/> is respected.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the actual assignment of this instance of <see cref="CSP"/> is complete;
        ///   otherwise <c>false</c>.
        /// </returns>
        private bool IsComplete()
        {
            foreach (GraphNode node in nodes)
            {
                if (node.Cell.Value == '.')
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Searchs for a <see cref="Cell"/> in <see cref="CSP.cells"/> with a <see cref="Cell.value"/> of '.'.
        /// </summary>
        /// <returns>
        /// The <see cref="Cell"/> that satisfies the most the conditions defined in the method.
        /// </returns>
        private GraphNode SelectUnassignedVariable()
        {
            return SelectFirstUnassignedVariable();
        }

        /// <summary>
        /// Searchs for a <see cref="Cell"/> in <see cref="CSP.cells"/> with a <see cref="Cell.value"/> of '.'.
        /// </summary>
        /// <returns>
        /// The first <see cref="Cell"/> found that satisfies the condition; <see cref="null"/> if no matching <see cref="Cell"/> found.
        /// </returns>
        private GraphNode SelectFirstUnassignedVariable()
        {
            foreach(GraphNode node in nodes)
            {
                if(node.Cell.Value == '.')
                {
                    return node;
                }
            }
            return null;
        }

        /// <summary>
        /// Iterates over <see cref="CSP.cells"/> and returns the cell with the minimum number of
        /// <see cref="Cell.value"/> that wouldn't make the assignment inconsistent.
        /// </summary>
        /// <returns>
        /// The <see cref="Cell"/> that satisfies this condition.
        /// </returns>
        private GraphNode MRV()
        {
            int minimumValueCount = int.MaxValue;
            GraphNode returnedNode = null;
            foreach(GraphNode node in nodes)
            {
                int cellRemainingValueCount = GetRemainingPossibleValues(node).Count;
                if(cellRemainingValueCount < minimumValueCount)
                {
                    returnedNode = node;
                    minimumValueCount = cellRemainingValueCount;
                }
            }
            return returnedNode;
        }

        /// <summary>
        /// Iterates over <see cref="CSP.cells"/> and returns the cell with the maximul number of
        /// constraints. The constraints are represented in <see cref="CSP.graphArcs"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="Cell"/> that satisfies this condition.
        /// </returns>
        private GraphNode DegreeHeuristic()
        {
            int maximumConstraintsCount = int.MinValue;
            GraphNode returnedNode = null;
            foreach (GraphNode node in nodes)
            {
                int cellRemainingRestraintsCount = GetNumberOfConstraintsOnCell(node);
                if (cellRemainingRestraintsCount > maximumConstraintsCount)
                {
                    returnedNode = node;
                    maximumConstraintsCount = cellRemainingRestraintsCount;
                }
            }
            return returnedNode;

        }

        /// <summary>
        /// Given a <see cref="Cell"/>, returns a list of every possible <see cref="Cell.value"/> in its 
        /// <see cref="Cell.domain"/> that wouldn't make the assignment inconsistent.
        /// </summary>
        /// <param name="cell">The <see cref="Cell"/> we want to give a value to.</param>
        /// <returns>
        /// The list of <see cref="char"/> representing the lit of <see cref="Cell.value"/>.
        /// </returns>
        private List<char> GetRemainingPossibleValues(GraphNode node)
        {
            List<char> remainingValues = new List<char>(node.Cell.Domain);

            foreach(GraphArc arc in node.ConnectedArcs)
            {
                remainingValues.Remove(arc.GetOtherCellValue(node.Cell));
            }

            return remainingValues;
        }

        /// <summary>
        /// Given a <see cref="Cell"/>, returns the number of "open" constraints on this cell.
        /// </summary>
        /// <param name="cell">The tested <see cref="Cell"/>.</param>
        /// <returns>
        /// The number of <see cref="GraphArc"/> in <see cref="CSP.graphArcs"/> whose attribute 
        /// <see cref="GraphArc.cell1"/> or <see cref="GraphArc.cell2"/> is equal <param name="cell"> and
        /// where the other attribute's <see cref="Cell.value"/> is equal to '.'.
        /// </returns>
        private int GetNumberOfConstraintsOnCell(GraphNode node)
        {
            int constraintsOnCell = 0;
            foreach (GraphArc arc in node.ConnectedArcs)
            {
                if(arc.GetOtherCellValue(node.Cell) == '.')
                {
                    constraintsOnCell++;
                }
            }
            return constraintsOnCell;
        }

        /// <summary>
        /// Orders every <see cref="Cell.value"/> in a <see cref="Cell"/>'s <see cref="Cell.domain"/>.
        /// </summary>
        /// <param name="cell">The <see cref="Cell"/> whose values will be ordered.</param>
        /// <returns>
        /// A list of char representing the ordered list of <see cref="Cell.value"/>.
        /// </returns>
        private List<char> OrderDomainValues(GraphNode node)
        {
            return GetUnorderedCellValues(node);
        }

        /// <summary>
        /// Returns a given <see cref="Cell"/>'s <see cref="Cell.domain"/>.
        /// </summary>
        /// <param name="cell">The given <see cref="Cell"/>.</param>
        /// <returns>
        /// <param name="cell">'s <see cref="Cell.domain"/>.
        /// </returns>
        private List<char> GetUnorderedCellValues(GraphNode node)
        {
            return node.Cell.Domain;
        }

        /// <summary>
        /// Orders the <see cref="Cell.value"/> in a <see cref="Cell"/>'s <see cref="Cell.domain"/> in a descending order,
        /// starting with the <see cref="Cell.value"/> that, if given to the <see cref="Cell"/> <paramref name="cell"/>, would result in
        /// the amount of remaining values (keeping the assignment consistent in the cell with the least amount of values) being the highest.
        /// </summary>
        /// <param name="cell">The tested <see cref="Cell"/>.</param>
        /// <returns>
        /// An ordered list of char representing the ordered <see cref="Cell.value"/>.
        /// </returns>
        private List<char> LeastConstraingValue(GraphNode node)
        {
            Dictionary<char, int> remainingMinimalValues = new Dictionary<char, int>();

            foreach(char c in node.Cell.Domain)
            {
                node.Cell.Value = c;
                int minRemainingValue = int.MaxValue;
                foreach(GraphArc arc in node.ConnectedArcs) {
                    if(arc.GetOtherCellValue(node.Cell) == '.')
                    {
                        int cell2RemainingValues = GetRemainingPossibleValues(arc.GetOtherNode(node)).Count;
                        if (cell2RemainingValues < minRemainingValue)
                        {
                            minRemainingValue = cell2RemainingValues;
                        }
                    }
                }
                remainingMinimalValues.Add(c, minRemainingValue);
            }
            node.Cell.Value = '.';

            List<char> orderedValues = new List<char>();
            // Trier le dictionnaire selon la valeur décroissante
            foreach (KeyValuePair<char, int> item in remainingMinimalValues.OrderByDescending(key => key.Value))
            {
                orderedValues.Add(item.Key);
            }

            return orderedValues;
        }


        #endregion
    }
}
