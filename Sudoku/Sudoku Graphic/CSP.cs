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
        int _gridSize = 9;
        int _squareSize = 3;
        #endregion

        #region Attributes
        private List<Cell> cells;
        public List<Cell> Cells { get => cells; set => cells = value; }

        private List<GraphArc> graphArcs;
        public List<GraphArc> GraphArcs { get => graphArcs; set => graphArcs = value; }
        #endregion

        #region Ctors
        public CSP()
        {
            cells = new List<Cell>();
            graphArcs = new List<GraphArc>();
        }
        #endregion

        #region Public Methods


        public void GenerateArcs()
        {
            foreach(Cell cell1 in cells)
            {
                int squareX1 = cell1.PosX / _squareSize;
                int squareY1 = cell1.PosY / _squareSize;
                foreach(Cell cell2 in cells)
                {
                    if(cell1.Equals(cell2))
                    {
                        continue;
                    }
                    int squareX2 = cell2.PosX / _squareSize;
                    int squareY2 = cell2.PosY / _squareSize;
                    if (cell1.PosX == cell2.PosX || cell1.PosY == cell2.PosY ||
                        (squareX1 == squareX2 && squareY1 ==squareY2))
                    {
                        GraphArc arc = new GraphArc(cell1, cell2);
                        graphArcs.Add(arc);
                    }
                }
            }
            RemoveDuplicateArcs();
        }

        public int RemoveDuplicateArcs()
        {
            int removed = 0;
            foreach(GraphArc arc1 in graphArcs)
            {
                foreach (GraphArc arc2 in graphArcs)
                {
                    if(arc1.IsDuplicata(arc2))
                    {
                        graphArcs.Remove(arc2);
                        removed++;
                    }
                }
            }
            return removed;
        }

        public void BacktrackingSearch()
        {
            //char[,] cellsAsChar = CellsAsChar();
            RecursiveBacktracking();
        }
        #endregion

        #region Private Methods

        private bool RecursiveBacktracking()
        {
            if (IsComplete()) {
                return true;
            }
            Cell chosenCell = SelectUnassignedVariable();

            foreach(char value in OrderDomainValues(chosenCell))
            {
                chosenCell.Value = value;
                if(IsConsistant())
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

        private Cell SelectUnassignedVariable()
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

        private List<char> GetRemainingPossibleValues(Cell cell)
        {
            List<char> remainingValues = new List<char>(cell.Domain);

            foreach(GraphArc arc in graphArcs)
            {
                remainingValues.Remove(arc.GetOtherCellValue(cell));
            }

            return remainingValues;
        }

        private int GetNumberOfConstraintsOnCell(Cell cell)
        {
            int constraintsOnCell = 0;
            foreach (GraphArc arc in graphArcs)
            {
                if(arc.IsCellIn(cell))
                {
                    constraintsOnCell++;
                }
            }
            return constraintsOnCell;
        }

        private List<char> OrderDomainValues(Cell cell)
        {
            return cell.Domain;
        }

        private List<char> LeastConstraingValue(Cell cell)
        {
            Dictionary<char, int> remainingMinimalValues = new Dictionary<char, int>();

            foreach(char c in cell.Domain)
            {
                cell.Value = c;
                int minRemainingValue = int.MaxValue;
                //------
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
