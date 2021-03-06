using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku_Graphic;
using System.Linq;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class GraphNodeTests
    {
        [TestMethod]
        public void T_GraphNode()
        {
            // Arrange
            Cell cell = new Cell();

            // Act
            GraphNode graphNode = new GraphNode(cell);

            // Assert
            Assert.IsTrue(graphNode.ConnectedArcs.Count == 0, "ConnectedArcs isn't empty.");
            Assert.IsTrue(graphNode.Cell == cell, "The cell isn't the right one.");
        }

        [TestMethod]
        public void T_Equals_False_DifferentConnectedArcs()
        {
            // Arrange
            Cell cell = new Cell();
            GraphNode graphNode = new GraphNode(cell);
            GraphNode graphNode2 = new GraphNode(cell);
            graphNode.ConnectedArcs.Add(new GraphArc(graphNode, graphNode2));

            // Act
            bool isEqual = graphNode.Equals(graphNode2);

            // Assert
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void T_Equals_False_DifferentCells()
        {
            // Arrange
            Cell cell = new Cell(0, 1);
            Cell cell2 = new Cell(1, 0);
            GraphNode graphNode = new GraphNode(cell);
            GraphNode graphNode2 = new GraphNode(cell2);

            // Act
            bool isEqual = graphNode.Equals(graphNode2);

            // Assert
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void T_Equals_True()
        {
            // Arrange
            Cell cell = new Cell(0, 0);
            Cell cell2 = new Cell();
            GraphNode graphNode = new GraphNode(cell);
            GraphNode graphNode2 = new GraphNode(cell);
            GraphNode graphNode3 = new GraphNode(cell2);
            graphNode.ConnectedArcs.Add(new GraphArc(graphNode, graphNode3));
            graphNode2.ConnectedArcs.Add(new GraphArc(graphNode, graphNode3));

            // Act
            bool isEqual = graphNode.Equals(graphNode2);

            // Assert
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void T_Equals_WithObject_False_Null()
        {
            // Arrange
            Cell cell = new Cell(0, 0);
            GraphNode graphNode = new GraphNode(cell);

            // Act
            bool isEqual = graphNode.Equals(null);

            // Assert
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void T_Equals_WithObject_False_Type()
        {
            // Arrange
            Cell cell = new Cell(0, 0);
            GraphNode graphNode = new GraphNode(cell);

            // Act
            bool isEqual = graphNode.Equals(cell);

            // Assert
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void T_Equals_WithObject_True_Object()
        {
            // Arrange
            Cell cell = new Cell(0, 0);
            GraphNode graphNode = new GraphNode(cell);
            Object graphNode2 = new GraphNode(cell);

            // Act
            bool isEqual = graphNode.Equals(graphNode2);

            // Assert
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void T_Equals_WithObject_True_SameReference()
        {
            // Arrange
            Cell cell = new Cell(0, 0);
            GraphNode graphNode = new GraphNode(cell);

            // Act
            bool isEqual = graphNode.Equals(graphNode);

            // Assert
            Assert.IsTrue(isEqual);
        }
    }
}
