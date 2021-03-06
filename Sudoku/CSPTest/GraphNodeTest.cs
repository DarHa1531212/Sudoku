using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku_Graphic;
using System;
using System.Collections.Generic;

namespace GraphNodesTest
{
    [TestClass]
    class GraphNodeTest
    {
        [TestMethod]
        public void T_CSP()
        {
            // Arrange
            CSP csp = new CSP();

            // Act

            // Assert
            Assert.IsTrue(csp.Dimensions.GridSizeX == 9, "GridSizeX isn't 9.");
            Assert.IsTrue(csp.Dimensions.GridSizeY == 9, "GridSizeY isn't 9.");
            Assert.IsTrue(csp.Dimensions.SquareSizeX == 3, "SquareSizeX isn't 3.");
            Assert.IsTrue(csp.Dimensions.SquareSizeY == 3, "SquareSizeY isn't 3.");
            Assert.IsTrue(csp.Nodes.Count == 0, "Nodes is not empty.");
        }
    }
}
