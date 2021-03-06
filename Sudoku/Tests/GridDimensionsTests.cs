using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku_Graphic;

namespace Tests
{
    /// <summary>
    /// Description résumée pour GridDimensionsTests
    /// </summary>
    [TestClass]
    public class GridDimensionsTests
    {
        [TestMethod]
        public void T_GridDimensions()
        {
            // Arrange

            // Act
            GridDimensions gridDimensions = new GridDimensions(
                10, 20, 1, 2
            );

            // Assert
            Assert.AreEqual(gridDimensions.GridSizeX, 10, "Wrong GridSizeX.");
            Assert.AreEqual(gridDimensions.GridSizeY, 20, "Wrong GridSizeY.");
            Assert.AreEqual(gridDimensions.SquareSizeX, 1, "Wrong SquareSizeX");
            Assert.AreEqual(gridDimensions.SquareSizeY, 2, "Wrong SquareSizeY.");
        }

        [TestMethod]
        public void T_IsValid_True()
        {
            // Arrange
            GridDimensions gridDimensions = new GridDimensions(
                9, 9, 3, 3
            );

            // Act
            bool isValid = gridDimensions.IsValid();

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void T_IsValid_False_GridSize()
        {
            // Arrange
            GridDimensions gridDimensions = new GridDimensions(
                9, 12, 3, 4
            );

            // Act
            bool isValid = gridDimensions.IsValid();

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void T_IsValid_False_SquareSizeX()
        {
            // Arrange
            GridDimensions gridDimensions = new GridDimensions(
                9, 9, 4, 3
            );

            // Act
            bool isValid = gridDimensions.IsValid();

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void T_IsValid_False_SquareSizeY()
        {
            // Arrange
            GridDimensions gridDimensions = new GridDimensions(
                9, 9, 3, 4
            );

            // Act
            bool isValid = gridDimensions.IsValid();

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void T_NumberOfSquaresOnLine()
        {
            // Arrange
            GridDimensions gridDimensions = new GridDimensions(
                9, 9, 3, 3
            );

            // Act
            int numberOfSquaresOnLine = gridDimensions.NumberOfSquaresOnLine();

            // Assert
            Assert.AreEqual(numberOfSquaresOnLine, 3);
        }

        [TestMethod]
        public void T_NumberOfSquaresOnColumn()
        {
            // Arrange
            GridDimensions gridDimensions = new GridDimensions(
                12, 12, 3, 3
            );

            // Act
            int numberOfSquaresOnColumn = gridDimensions.NumberOfSquaresOnColumn();

            // Assert
            Assert.AreEqual(numberOfSquaresOnColumn, 4);
        }
    }
}
