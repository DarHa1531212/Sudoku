using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Graphic
{
    public class GraphArc
    {
        #region Attributes
        private Cell cell1;

        private Cell cell2;
        #endregion

        #region Ctors
        public GraphArc(Cell cell1, Cell cell2)
        {
            this.cell1 = cell1;
            this.cell2 = cell2;
        }
        #endregion

        #region Public Methods
        public bool IsConsistant()
        {
            return cell1.Value == cell2.Value;
        }

        public bool IsDuplicata(GraphArc arc2)
        {
            return this.cell1 == arc2.cell1 &&
                this.cell2 == arc2.cell2;
        }


        public bool IsCellIn(Cell cell)
        {
            return cell1 == cell || cell2 == cell;
        }

        public char GetOtherCellValue(Cell cell)
        {
            if(cell == cell1)
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
    }
}
