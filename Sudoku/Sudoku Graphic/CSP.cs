using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Graphic
{
    public class CSP
    {
        #region Attributes
        /// <summary>
        /// Contains all the <see cref="Cell"/> representing the cells of the sudoku grid.
        /// </summary>
        private List<Cell> cells;
        /// <summary>
        /// Gets and sets <see cref="CSP.cells"/>.
        /// </summary>
        public List<Cell> Cells { get => cells; set => cells = value; }

        /// <summary>
        /// Contains all the <see cref="GraphArc"/> representing the binary constraints between the <see cref="Cell"/>.
        /// </summary>
        private List<GraphArc> graphArcs;

        public GridDimensions Dimensions { get => dimensions; set => dimensions = value; }
        private GridDimensions dimensions;
        #endregion

        #region Ctors
        /// <summary>
        /// Initializes a new instance of <see cref="CSP"/>
        /// </summary>
        public CSP()
        {
            cells = new List<Cell>();
            graphArcs = new List<GraphArc>();
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
            foreach(Cell cell1 in cells)
            {
                int squareX1 = cell1.PosX / dimensions.SquareSizeX;
                int squareY1 = cell1.PosY / dimensions.SquareSizeY;
                foreach(Cell cell2 in cells)
                {
                    if(cell1.Equals(cell2))
                    {
                        continue;
                    }
                    int squareX2 = cell2.PosX / dimensions.SquareSizeX;
                    int squareY2 = cell2.PosY / dimensions.SquareSizeY;
                    if (cell1.PosX == cell2.PosX || cell1.PosY == cell2.PosY ||
                        (squareX1 == squareX2 && squareY1 ==squareY2))
                    {
                        graphArcs.Add(new GraphArc(cell1, cell2));
                    }
                }
            }
            RemoveDuplicateArcs();
        }

        /// <summary>
        /// Removes any duplicated <see cref="GraphArc"/> in <see cref="CSP.graphArcs"/>. What is a duplicated arc is defined
        /// in the <see cref="GraphArc.IsDuplicata(GraphArc)"/> method.
        /// </summary>
        /// <returns>
        /// The number of removed <see cref="GraphArc"/>.
        /// </returns>
        public int RemoveDuplicateArcs()
        {
            int removed = 0;
            List<GraphArc> nonDuplicatedArcs = new List<GraphArc>(graphArcs);
            foreach(GraphArc arc1 in graphArcs)
            {
                foreach (GraphArc arc2 in graphArcs)
                {
                    if(arc1.Equals(arc2))
                    {
                        continue;
                    }
                    if(arc1.IsDuplicata(arc2) && nonDuplicatedArcs.Contains(arc1))
                    {
                        nonDuplicatedArcs.Remove(arc2);
                        removed++;
                    }
                }
            }
            graphArcs = new List<GraphArc>(nonDuplicatedArcs);
            return removed;
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
            return RecursiveBacktracking();
        }

        /// <summary>
        /// Clears <see cref="CSP.cells"/> and <see cref="CSP.graphArcs"/>.
        /// </summary>
        public void ClearLists()
        {
            cells.Clear();
            graphArcs.Clear();
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
            Cell chosenCell = SelectUnassignedVariable();

            foreach(char value in OrderDomainValues(chosenCell))
            {
                chosenCell.Value = value;
                if (IsConsistant())
                {
                    bool success = RecursiveBacktracking();
                    if(success)
                    {
                        return true;
                    }
                }
                chosenCell.Value = '.';
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
            foreach (GraphArc arc in graphArcs)
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
            foreach (Cell cell in cells)
            {
                if (cell.Value == '.')
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
        private Cell SelectUnassignedVariable()
        {
            return SelectFirstUnassignedVariable();
        }

        /// <summary>
        /// Searchs for a <see cref="Cell"/> in <see cref="CSP.cells"/> with a <see cref="Cell.value"/> of '.'.
        /// </summary>
        /// <returns>
        /// The first <see cref="Cell"/> found that satisfies the condition; <see cref="null"/> if no matching <see cref="Cell"/> found.
        /// </returns>
        private Cell SelectFirstUnassignedVariable()
        {
            foreach(Cell cell in cells)
            {
                if(cell.Value == '.')
                {
                    return cell;
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
        private Cell MRV()
        {
            int minimumValueCount = int.MaxValue;
            Cell returnedCell = null;
            foreach(Cell cell in cells)
            {
                int cellRemainingValueCount = GetRemainingPossibleValues(cell).Count;
                if(cellRemainingValueCount < minimumValueCount)
                {
                    returnedCell = cell;
                    minimumValueCount = cellRemainingValueCount;
                }
            }
            return returnedCell;
        }

        /// <summary>
        /// Iterates over <see cref="CSP.cells"/> and returns the cell with the maximul number of
        /// constraints. The constraints are represented in <see cref="CSP.graphArcs"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="Cell"/> that satisfies this condition.
        /// </returns>
        private Cell DegreeHeuristic()
        {
            int maximumConstraintsCount = int.MinValue;
            Cell returnedCell = null;
            foreach (Cell cell in cells)
            {
                int cellRemainingRestraintsCount = GetNumberOfConstraintsOnCell(cell);
                if (cellRemainingRestraintsCount > maximumConstraintsCount)
                {
                    returnedCell = cell;
                    maximumConstraintsCount = cellRemainingRestraintsCount;
                }
            }
            return returnedCell;

        }

        /// <summary>
        /// Given a <see cref="Cell"/>, returns a list of every possible <see cref="Cell.value"/> in its 
        /// <see cref="Cell.domain"/> that wouldn't make the assignment inconsistent.
        /// </summary>
        /// <param name="cell">The <see cref="Cell"/> we want to give a value to.</param>
        /// <returns>
        /// The list of <see cref="char"/> representing the lit of <see cref="Cell.value"/>.
        /// </returns>
        private List<char> GetRemainingPossibleValues(Cell cell)
        {
            List<char> remainingValues = new List<char>(cell.Domain);

            foreach(GraphArc arc in graphArcs)
            {
                remainingValues.Remove(arc.GetOtherCellValue(cell));
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
        private int GetNumberOfConstraintsOnCell(Cell cell)
        {
            int constraintsOnCell = 0;
            foreach (GraphArc arc in graphArcs)
            {
                if(arc.IsCellIn(cell) && arc.GetOtherCellValue(cell) == '.')
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
        private List<char> OrderDomainValues(Cell cell)
        {
            return GetUnorderedCellValues(cell);
        }

        /// <summary>
        /// Returns a given <see cref="Cell"/>'s <see cref="Cell.domain"/>.
        /// </summary>
        /// <param name="cell">The given <see cref="Cell"/>.</param>
        /// <returns>
        /// <param name="cell">'s <see cref="Cell.domain"/>.
        /// </returns>
        private List<char> GetUnorderedCellValues(Cell cell)
        {
            return cell.Domain;
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
        private List<char> LeastConstraingValue(Cell cell)
        {
            Dictionary<char, int> remainingMinimalValues = new Dictionary<char, int>();

            foreach(char c in cell.Domain)
            {
                cell.Value = c;
                int minRemainingValue = int.MaxValue;
                foreach(Cell cell2 in cells)
                {
                    if(cell2.Value == '.')
                    {
                        int cell2RemainingValues = GetRemainingPossibleValues(cell2).Count;
                        if (cell2RemainingValues < minRemainingValue)
                        {
                            minRemainingValue = cell2RemainingValues;
                        }
                    }
                }
                remainingMinimalValues.Add(c, minRemainingValue);
            }
            cell.Value = '.';

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
