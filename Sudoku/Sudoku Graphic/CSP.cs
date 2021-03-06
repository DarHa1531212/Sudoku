using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Graphic
{
    public class CSP
    {
        #region Constants

        const ushort _defaultSize = 9;
        const ushort _defaultSquareSize = 3;

        #endregion

        #region Attributes

        private GridDimensions dimensions;
        private List<GraphNode> nodes;
        private List<GraphArc> graphArcs;
        public GridDimensions Dimensions { get => dimensions; set => dimensions = value; }
        public List<GraphNode> Nodes { get => nodes; set => nodes = value; }
        public List<GraphArc> GraphArcs { get => graphArcs; set => graphArcs = value; }

        #endregion

        #region Ctors
        /// <summary>
        /// Initializes a new instance of <see cref="CSP"/>
        /// </summary>
        public CSP()
        {
            Dimensions = new GridDimensions(
                _defaultSize, _defaultSize, 
                _defaultSquareSize, _defaultSquareSize
            );
            nodes = new List<GraphNode>();
            graphArcs = new List<GraphArc>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Generates the constraints (populates each <see cref="GraphNode"/> of <see cref="CSP.nodes"/>) between the 
        /// different <see cref="GraphNode"/> in <see cref="CSP.nodes"/> using classic sudoku's rules.
        /// Note than each constraint will appear "twice" : one from every node to the other
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
                        GraphArc newArc = new GraphArc(node1, node2);
                        node1.ConnectedArcs.Add(newArc);
                        graphArcs.Add(newArc);
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
            // We first check if the grid respects the rules.
            if(!IsConsistant())
            {
                return false;
            }
            GenerateDomains();
            return RecursiveBacktracking();
        }

        /// <summary>
        /// Clears <see cref="CSP.nodes"/>.
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
            if (ForwardChecking())
            {
               return false;
            }

            GraphNode chosenNode = SelectUnassignedVariable();
            List<char> completeDomain = new List<char>(chosenNode.Cell.Domain); 
            foreach(char value in OrderDomainValues(chosenNode))
            {
                List<char> oldDomain = new List<char>(chosenNode.Cell.Domain);
                chosenNode.Cell.Value = value;
                chosenNode.Cell.Domain = new List<char>(new char[] { value });
                List<Cell> modifiedCells = ModifyNeighbouringCells(chosenNode);
                //if (IsConsistant(chosenNode))
                //{
                bool success = RecursiveBacktracking();
                if(success)
                {
                    return true;
                }
               // }
                chosenNode.Cell.Domain = new List<char>(oldDomain);
                chosenNode.Cell.RemoveFromDomain(value);
                foreach(Cell cell in modifiedCells)
                {
                    cell.Domain.Add(value);
                }
                chosenNode.Cell.Value = '.';
            }
            chosenNode.Cell.Domain = new List<char>(completeDomain);
            return false;
        }

        private bool ForwardChecking()
        {
            foreach (GraphNode node in nodes)
            {
                if (node.Cell.Domain.Count == 0)
                {
                    return true;
                }
            }
            return false;
        }

        private void AC3()
        {
            Queue<GraphArc> arcs = new Queue<GraphArc>(graphArcs);

            while(arcs.Count != 0)
            {
                GraphArc current = arcs.Dequeue();
                if (RemoveInconsistentValues(current))
                {
                    foreach (var arc in current.GetFirstNode().ConnectedArcs)
                    {
                        arcs.Enqueue(arc.GetReverseArc());
                    }
                }
            }
        }

        private bool RemoveInconsistentValues(GraphArc arc)
        {
            bool removed = false;
            Cell cell = arc.GetFirstNode().Cell;
            foreach (var valueBeginning in cell.Domain)
            {
                if (ValidConstraint(arc))
                {
                    cell.RemoveFromDomain(valueBeginning);
                    removed = true;
                }
            }
            return removed;
        }

        private bool RemoveInconsistentValues_OtherImplementation(GraphArc arc)
        {
            Cell otherCell = arc.GetReverseArc().GetFirstNode().Cell;
            if(cell.Domain.Count != 1)
            {
                return false;
            }
            return arc.GetFirstNode().Cell.Domain.Remove(cell.Domain[0]);
        }

        private bool ValidConstraint(GraphArc arc)
        {
            GraphNode nodeBeginning = arc.GetFirstNode();
            GraphNode nodeEnding = arc.GetOtherNode(nodeBeginning);
            char valueNodeEnding = nodeBeginning.Cell.Value;
            foreach (var valueEnding in nodeEnding.Cell.Domain)
            {
                nodeEnding.Cell.Value = valueEnding;
                bool isConsistant = IsConsistant(nodeBeginning);
                nodeEnding.Cell.Value = valueNodeEnding;
                if (isConsistant)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the actual entire CSP assignment is consistent.
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

        /// <summary>
        /// Checks if the <see cref="GraphNode.cell"/> of a given <see cref="GraphNode"/> respects all constraints.
        /// </summary>
        /// <param name="node"></param>
        /// <returns>
        ///   <c>true</c> if every constraint in the <see cref="GraphNode.connectedArcs"/> of the given
        ///   <paramref name="node"></paramref> is respected; otherwise <c>false</c>.
        /// </returns>
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
        /// Checks whether the actual assignment of the <see cref="CSP"/> is complete.
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
        /// Searchs for a <see cref="GraphNode"/> in <see cref="CSP.nodes"/> with a <see cref="Cell"/> whose <see cref="Cell.value"/> of '.'.
        /// </summary>
        /// <returns>
        /// The <see cref="GraphNode"/> that satisfies the most the conditions defined in the method.
        /// </returns>
        private GraphNode SelectUnassignedVariable()
        {
            return SelectFirstUnassignedVariable();
        }

        /// <summary>
        /// Searchs for a <see cref="GraphNode"/> in <see cref="CSP.nodes"/> with a <see cref="Cell"/> whose <see cref="Cell.value"/> of '.'.
        /// </summary>
        /// <returns>
        /// The first <see cref="GraphNode"/> found that satisfies the condition; <see cref="null"/> if no matching <see cref="GraphNode"/> found.
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
        /// Iterates over <see cref="CSP.nodes"/> and returns the <see cref="GraphNode"/> with the <see cref="Cell"/> with the minimum number of
        /// <see cref="Cell.value"/> that wouldn't make the assignment inconsistent.
        /// </summary>
        /// <returns>
        /// The <see cref="GraphNode"/> that satisfies this condition.
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
        /// Iterates over <see cref="CSP.nodes"/> and returns the <see cref="GraphNode"/> with the maximul number of
        /// constraints. The constraints are represented in <see cref="GraphNode.connectedArcs"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="GraphNode"/> that satisfies this condition.
        /// </returns>
        private GraphNode DegreeHeuristic()
        {
            int maximumConstraintsCount = int.MinValue;
            GraphNode returnedNode = null;
            foreach (GraphNode node in nodes)
            {
                int cellRemainingRestraintsCount = GetNumberOfConstraintsOnNode(node);
                if (cellRemainingRestraintsCount > maximumConstraintsCount)
                {
                    returnedNode = node;
                    maximumConstraintsCount = cellRemainingRestraintsCount;
                }
            }
            return returnedNode;

        }

        /// <summary>
        /// Given a <see cref="GraphNode"/>, returns a list of every possible <see cref="Cell.value"/> in its 
        /// <see cref="Cell"/>'s <see cref="Cell.domain"/> that wouldn't make the assignment inconsistent.
        /// </summary>
        /// <param name="node">The <see cref="GraphNode"/> we want to give a value to.</param>
        /// <returns>
        /// The list of <see cref="char"/> representing the list of <see cref="Cell.value"/>.
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
        /// Given a <see cref="GraphNode"/>, returns the number of "open" constraints on this <see cref="GraphNode"/>.
        /// </summary>
        /// <param name="node">The tested <see cref="GraphNode"/>.</param>
        /// <returns>
        /// The number of <see cref="GraphArc"/> in <see cref="GraphNode.connectedArcs"/> 
        /// where the other <see cref="GraphNode"/>'s <see cref="GraphNode.cell"/>'s <see cref="Cell.value"/> is equal to '.'.
        /// </returns>
        private int GetNumberOfConstraintsOnNode(GraphNode node)
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
        /// <param name="node">The <see cref="GraphNode"/> whose <see cref="Cell"/>'s <see cref="Cell.value"/>
        /// will be ordered.</param>
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
        /// <param name="node">The given <see cref="GraphNode"/>.</param>
        /// <returns>
        /// <param name="node">'s <see cref="Cell"/>'s <see cref="Cell.domain"/>.
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
        /// <param name="node">The <see cref="GraphNode"/> whose <see cref="GraphNode.cell"/> is tested.</param>
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

        private void GenerateDomains()
        {
            foreach(GraphNode node in nodes)
            {
                if(node.Cell.Value != '.')
                {
                    node.Cell.Domain = new List<char>(new char[] { node.Cell.Value });
                } else
                {
                    foreach(GraphArc arc in node.ConnectedArcs)
                    {
                        node.Cell.RemoveFromDomain(arc.GetOtherCellValue(node.Cell));
                    }
                }
            }
        }

        private List<Cell> ModifyNeighbouringCells(GraphNode node)
        {
            List<Cell> cellsWithChangedDomains = new List<Cell>();
            foreach(GraphArc arc in node.ConnectedArcs)
            {
                Cell neighbouringCell = arc.GetOtherNode(node).Cell;
                if (neighbouringCell.Domain.Remove(node.Cell.Value))
                {
                    cellsWithChangedDomains.Add(neighbouringCell);
                }
            }
            return cellsWithChangedDomains;
        } 
        #endregion
    }
}
