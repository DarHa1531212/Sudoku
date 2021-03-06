using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku_Graphic;
using System.Linq;

namespace Tests
{
    /// <summary>
    /// Description résumée pour CellTests
    /// </summary>
    [TestClass]
    public class CellTests
    {
        [TestMethod]
        public void T_Cell_Base_NoParameters()
        {
            // Arrange

            // Act
            Cell cell = new Cell();

            // Assert
            Assert.AreEqual(cell.PosX, 0, "Wrong PosX.");
            Assert.AreEqual(cell.PosY, 0, "Wrong PosY.");
            Assert.AreEqual(cell.Domain.Count, 9, "Wrong Domain.");
        }

        [TestMethod]
        public void T_Cell_Base_1Parameter()
        {
            // Arrange

            // Act
            Cell cell = new Cell(5);

            // Assert
            Assert.AreEqual(cell.PosX, 5, "Wrong PosX.");
            Assert.AreEqual(cell.PosY, 0, "Wrong PosY.");
            Assert.AreEqual(cell.Domain.Count, 9, "Wrong Domain.");
        }

        [TestMethod]
        public void T_Cell_Base_2Parameters()
        {
            // Arrange

            // Act
            Cell cell = new Cell(5, 8);

            // Assert
            Assert.AreEqual(cell.PosX, 5, "Wrong PosX.");
            Assert.AreEqual(cell.PosY, 8, "Wrong PosY.");
            Assert.AreEqual(cell.Domain.Count, 9, "Wrong Domain.");
        }

        [TestMethod]
        public void T_Cell_Base_3Parameters()
        {
            // Arrange

            // Act
            Cell cell = new Cell(5, 8, 13);

            // Assert
            Assert.AreEqual(cell.PosX, 5, "Wrong PosX.");
            Assert.AreEqual(cell.PosY, 8, "Wrong PosY.");
            Assert.AreEqual(cell.Domain.Count, 13, "Wrong Domain.");
        }

        [TestMethod]
        public void T_Cell_WithCustomDomain()
        {
            // Arrange
            List<char> domain = new List<char> { 'X', 'Y', 'Z' };

            // Act
            Cell cell = new Cell(5, 8, domain);

            // Assert
            Assert.AreEqual(cell.PosX, 5, "Wrong PosX.");
            Assert.AreEqual(cell.PosY, 8, "Wrong PosY.");
            CollectionAssert.AreEqual(
                cell.Domain, 
                new List<char> { 'X', 'Y', 'Z' }, 
                "Wrong Domain."
            );
        }

        [TestMethod]
        public void T_Cell_FromCell()
        {
            // Arrange
            Cell cell = new Cell(5, 8, 4);

            // Act
            Cell cell2 = new Cell(cell);

            // Assert
            Assert.AreEqual(cell2.PosX, 5, "Wrong PosX.");
            Assert.AreEqual(cell2.PosY, 8, "Wrong PosY.");
            Assert.AreEqual(cell2.Domain.Count, 4, "Wrong Domain.");
        }

        [TestMethod]
        public void T_RemoveFromDomain_First()
        {
            // Arrange
            List<char> domain = new List<char> { 'X', 'Y', 'Z' };
            Cell cell = new Cell(5, 8, domain);

            // Act
            cell.RemoveFromDomain('X');

            // Assert
            CollectionAssert.AreEqual(
                cell.Domain, 
                new List<char> { 'Y', 'Z' }, 
                "Wrong Domain."
            );
        }

        [TestMethod]
        public void T_RemoveFromDomain_Middle()
        {
            // Arrange
            List<char> domain = new List<char> { 'X', 'Y', 'Z' };
            Cell cell = new Cell(5, 8, domain);

            // Act
            cell.RemoveFromDomain('Y');

            // Assert
            CollectionAssert.AreEqual(
                cell.Domain,
                new List<char> { 'X', 'Z' },
                "Wrong Domain."
            );
        }

        [TestMethod]
        public void T_RemoveFromDomain_Last()
        {
            // Arrange
            List<char> domain = new List<char> { 'X', 'Y', 'Z' };
            Cell cell = new Cell(5, 8, domain);

            // Act
            cell.RemoveFromDomain('Z');

            // Assert
            CollectionAssert.AreEqual(
                cell.Domain,
                new List<char> { 'X', 'Y' },
                "Wrong Domain."
            );
        }

        [TestMethod]
        public void T_Equals_True()
        {
            // Arrange
            Cell cell = new Cell(5, 8, 12);
            Cell cell2 = new Cell(5, 8, 12);
            cell.Value = 'A';
            cell2.Value = 'A';

            // Act
            bool isEqual = cell.Equals(cell2);

            // Assert
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void T_Equals_False_PosX()
        {
            // Arrange
            Cell cell = new Cell(5, 8, 12);
            Cell cell2 = new Cell(6, 8, 12);
            cell.Value = 'A';
            cell2.Value = 'A';

            // Act
            bool isEqual = cell.Equals(cell2);

            // Assert
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void T_Equals_False_PosY()
        {
            // Arrange
            Cell cell = new Cell(5, 7, 12);
            Cell cell2 = new Cell(5, 8, 12);
            cell.Value = 'A';
            cell2.Value = 'A';

            // Act
            bool isEqual = cell.Equals(cell2);

            // Assert
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void T_Equals_False_Value()
        {
            // Arrange
            Cell cell = new Cell(0, 8, 12);
            Cell cell2 = new Cell(0, 8, 12);
            cell.Value = 'A';
            cell2.Value = '0';

            // Act
            bool isEqual = cell.Equals(cell2);

            // Assert
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void T_Equals_False_DomainSize()
        {
            // Arrange
            Cell cell = new Cell(0, 8, 9);
            Cell cell2 = new Cell(0, 8, 12);
            cell.Value = '0';
            cell2.Value = '0';

            // Act
            bool isEqual = cell.Equals(cell2);

            // Assert
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void T_Equals_False_DomainSizeReverse()
        {
            // Arrange
            Cell cell = new Cell(0, 8, 12);
            Cell cell2 = new Cell(0, 8, 9);
            cell.Value = '0';
            cell2.Value = '0';

            // Act
            bool isEqual = cell.Equals(cell2);

            // Assert
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void T_Equals_False_Domain()
        {
            // Arrange
            List<char> domain = new List<char> { '1', '2', '3' };
            List<char> domain2 = new List<char> { '2', '3', '4' };
            Cell cell = new Cell(0, 8, domain);
            Cell cell2 = new Cell(0, 8, domain2);
            cell.Value = '0';
            cell2.Value = '0';

            // Act
            bool isEqual = cell.Equals(cell2);

            // Assert
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void T_Equals_True_DomainUnsorted()
        {
            // Arrange
            List<char> domain = new List<char> { '1', '2', '3' };
            List<char> domain2 = new List<char> { '3', '2', '1' };
            Cell cell = new Cell(0, 8, domain);
            Cell cell2 = new Cell(0, 8, domain2);
            cell.Value = '0';
            cell2.Value = '0';

            // Act
            bool isEqual = cell.Equals(cell2);

            // Assert
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void T_Equals_WithObject_False_Null()
        {
            // Arrange
            Cell cell = new Cell();

            // Act
            bool isEqual = cell.Equals(null);

            // Assert
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void T_Equals_WithObject_False_Type()
        {
            // Arrange
            Cell cell = new Cell();

            // Act
            bool isEqual = cell.Equals(8);

            // Assert
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void T_Equals_WithObject_True_Object()
        {
            // Arrange
            Cell cell = new Cell();
            Object cell2 = new Cell();

            // Act
            bool isEqual = cell.Equals(cell2);

            // Assert
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void T_Equals_WithObject_True_SameReference()
        {
            // Arrange
            Cell cell = new Cell(0, 0);

            // Act
            bool isEqual = cell.Equals(cell);

            // Assert
            Assert.IsTrue(isEqual);
        }
    }
}
