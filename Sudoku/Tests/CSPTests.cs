using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku_Graphic;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class CSPTests
    {
        [TestMethod]
        public void T_CSP()
        {
            // Arrange

            // Act
            CSP csp = new CSP();

            // Assert
            Assert.IsTrue(csp.Dimensions.GridSizeX == 9, "GridSizeX isn't 9.");
            Assert.IsTrue(csp.Dimensions.GridSizeY == 9, "GridSizeY isn't 9.");
            Assert.IsTrue(csp.Dimensions.SquareSizeX == 3, "SquareSizeX isn't 3.");
            Assert.IsTrue(csp.Dimensions.SquareSizeY == 3, "SquareSizeY isn't 3.");
            Assert.IsTrue(csp.Nodes.Count == 0, "Nodes is not empty.");
        }

        [TestMethod]
        public void T_GenerateArcs_SameLine()
        {
            // Arrange
            CSP csp = new CSP();
            GraphNode gn0 = new GraphNode(new Cell(0, 0));
            GraphNode gn1 = new GraphNode(new Cell(0, 2));
            GraphNode gn2 = new GraphNode(new Cell(0, 9));
            csp.Nodes.Add(gn0);
            csp.Nodes.Add(gn1);
            csp.Nodes.Add(gn2);

            GraphArc ga01 = new GraphArc(gn0, gn1);
            GraphArc ga02 = new GraphArc(gn0, gn2);
            GraphArc ga10 = new GraphArc(gn1, gn0);
            GraphArc ga12 = new GraphArc(gn1, gn2);
            GraphArc ga20 = new GraphArc(gn2, gn0);
            GraphArc ga21 = new GraphArc(gn2, gn1);

            // Act
            csp.GenerateArcs();

            // Assert
            CollectionAssert.AreEqual(
                gn0.ConnectedArcs,
                new List<GraphArc>(new GraphArc[] { ga01, ga02 })
            );
            CollectionAssert.AreEqual(
                gn1.ConnectedArcs,
                new List<GraphArc>(new GraphArc[] { ga10, ga12 })
            );
            CollectionAssert.AreEqual(
                gn2.ConnectedArcs,
                new List<GraphArc>(new GraphArc[] { ga20, ga21 })
            );
        }

        [TestMethod]
        public void T_GenerateArcs_SameColumn()
        {
            // Arrange
            CSP csp = new CSP();
            GraphNode gn0 = new GraphNode(new Cell(0, 9));
            GraphNode gn1 = new GraphNode(new Cell(3, 9));
            GraphNode gn2 = new GraphNode(new Cell(9, 9));
            csp.Nodes.Add(gn0);
            csp.Nodes.Add(gn1);
            csp.Nodes.Add(gn2);

            GraphArc ga01 = new GraphArc(gn0, gn1);
            GraphArc ga02 = new GraphArc(gn0, gn2);
            GraphArc ga10 = new GraphArc(gn1, gn0);
            GraphArc ga12 = new GraphArc(gn1, gn2);
            GraphArc ga20 = new GraphArc(gn2, gn0);
            GraphArc ga21 = new GraphArc(gn2, gn1);

            // Act
            csp.GenerateArcs();

            // Assert
            CollectionAssert.AreEqual(
                gn0.ConnectedArcs,
                new List<GraphArc>(new GraphArc[] { ga01, ga02 })
            );
            CollectionAssert.AreEqual(
                gn1.ConnectedArcs,
                new List<GraphArc>(new GraphArc[] { ga10, ga12 })
            );
            CollectionAssert.AreEqual(
                gn2.ConnectedArcs,
                new List<GraphArc>(new GraphArc[] { ga20, ga21 })
            );
        }

        [TestMethod]
        public void T_GenerateArcs_SameSquare()
        {
            // Arrange
            CSP csp = new CSP();
            GraphNode gn0 = new GraphNode(new Cell(0, 2));
            GraphNode gn1 = new GraphNode(new Cell(1, 1));
            GraphNode gn2 = new GraphNode(new Cell(2, 0));
            csp.Nodes.Add(gn0);
            csp.Nodes.Add(gn1);
            csp.Nodes.Add(gn2);

            GraphArc ga01 = new GraphArc(gn0, gn1);
            GraphArc ga02 = new GraphArc(gn0, gn2);
            GraphArc ga10 = new GraphArc(gn1, gn0);
            GraphArc ga12 = new GraphArc(gn1, gn2);
            GraphArc ga20 = new GraphArc(gn2, gn0);
            GraphArc ga21 = new GraphArc(gn2, gn1);

            // Act
            csp.GenerateArcs();

            // Assert
            CollectionAssert.AreEqual(
                gn0.ConnectedArcs,
                new List<GraphArc>(new GraphArc[] { ga01, ga02 })
            );
            CollectionAssert.AreEqual(
                gn1.ConnectedArcs,
                new List<GraphArc>(new GraphArc[] { ga10, ga12 })
            );
            CollectionAssert.AreEqual(
                gn2.ConnectedArcs,
                new List<GraphArc>(new GraphArc[] { ga20, ga21 })
            );
        }

        [TestMethod]
        public void T_IsConsistant_WithParameter_False_SameLine()
        {
            // Arrange
            CSP csp = new CSP();
            PrivateObject obj = new PrivateObject(csp);

            GraphNode gn0 = new GraphNode(new Cell(9, 6));
            gn0.Cell.Value = '2';
            GraphNode gn1 = new GraphNode(new Cell(9, 2));
            gn1.Cell.Value = '2';
            GraphNode gn2 = new GraphNode(new Cell(9, 0));
            gn2.Cell.Value = '3';
            csp.Nodes.Add(gn0);
            csp.Nodes.Add(gn1);
            csp.Nodes.Add(gn2);

            GraphArc ga01 = new GraphArc(gn0, gn1);
            GraphArc ga02 = new GraphArc(gn0, gn2);
            gn0.ConnectedArcs = new List<GraphArc> { ga01, ga02 };
            GraphArc ga10 = new GraphArc(gn1, gn0);
            GraphArc ga12 = new GraphArc(gn1, gn2);
            gn1.ConnectedArcs = new List<GraphArc> { ga10, ga12 };
            GraphArc ga20 = new GraphArc(gn2, gn0);
            GraphArc ga21 = new GraphArc(gn2, gn1);
            gn2.ConnectedArcs = new List<GraphArc> { ga20, ga21 };

            // Act
            var isConsistant = obj.Invoke("IsConsistant", gn0);

            // Assert
            Assert.IsInstanceOfType(isConsistant, typeof(bool), "Wrong type.");
            Assert.IsFalse((bool)isConsistant, "Wrong value.");
        }

        [TestMethod]
        public void T_IsConsistant_WithParameter_False_SameColumn()
        {
            // Arrange
            CSP csp = new CSP();
            PrivateObject obj = new PrivateObject(csp);

            GraphNode gn0 = new GraphNode(new Cell(0, 0));
            gn0.Cell.Value = '9';
            GraphNode gn1 = new GraphNode(new Cell(3, 0));
            gn1.Cell.Value = '9';
            GraphNode gn2 = new GraphNode(new Cell(8, 0));
            gn2.Cell.Value = '1';
            csp.Nodes.Add(gn0);
            csp.Nodes.Add(gn1);
            csp.Nodes.Add(gn2);

            GraphArc ga01 = new GraphArc(gn0, gn1);
            GraphArc ga02 = new GraphArc(gn0, gn2);
            gn0.ConnectedArcs = new List<GraphArc> { ga01, ga02 };
            GraphArc ga10 = new GraphArc(gn1, gn0);
            GraphArc ga12 = new GraphArc(gn1, gn2);
            gn1.ConnectedArcs = new List<GraphArc> { ga10, ga12 };
            GraphArc ga20 = new GraphArc(gn2, gn0);
            GraphArc ga21 = new GraphArc(gn2, gn1);
            gn2.ConnectedArcs = new List<GraphArc> { ga20, ga21 };

            // Act
            var isConsistant = obj.Invoke("IsConsistant", gn0);

            // Assert
            Assert.IsInstanceOfType(isConsistant, typeof(bool), "Wrong type.");
            Assert.IsFalse((bool)isConsistant, "Wrong value.");
        }

        [TestMethod]
        public void T_IsConsistant_WithParameter_False_SameSquare()
        {
            // Arrange
            CSP csp = new CSP();
            PrivateObject obj = new PrivateObject(csp);

            GraphNode gn0 = new GraphNode(new Cell(0, 2));
            gn0.Cell.Value = '2';
            GraphNode gn1 = new GraphNode(new Cell(1, 1));
            gn1.Cell.Value = '2';
            GraphNode gn2 = new GraphNode(new Cell(2, 0));
            gn2.Cell.Value = '3';
            csp.Nodes.Add(gn0);
            csp.Nodes.Add(gn1);
            csp.Nodes.Add(gn2);

            GraphArc ga01 = new GraphArc(gn0, gn1);
            GraphArc ga02 = new GraphArc(gn0, gn2);
            gn0.ConnectedArcs = new List<GraphArc> { ga01, ga02 };
            GraphArc ga10 = new GraphArc(gn1, gn0);
            GraphArc ga12 = new GraphArc(gn1, gn2);
            gn1.ConnectedArcs = new List<GraphArc> { ga10, ga12 };
            GraphArc ga20 = new GraphArc(gn2, gn0);
            GraphArc ga21 = new GraphArc(gn2, gn1);
            gn2.ConnectedArcs = new List<GraphArc> { ga20, ga21 };

            // Act
            var isConsistant = obj.Invoke("IsConsistant", gn0);

            // Assert
            Assert.IsInstanceOfType(isConsistant, typeof(bool), "Wrong type.");
            Assert.IsFalse((bool)isConsistant, "Wrong value.");
        }

        [TestMethod]
        public void T_IsConsistant_WithParameter_True_SameLine()
        {
            // Arrange
            CSP csp = new CSP();
            PrivateObject obj = new PrivateObject(csp);

            GraphNode gn0 = new GraphNode(new Cell(9, 6));
            gn0.Cell.Value = '9';
            GraphNode gn1 = new GraphNode(new Cell(9, 2));
            gn1.Cell.Value = '2';
            GraphNode gn2 = new GraphNode(new Cell(9, 0));
            gn2.Cell.Value = '3';
            csp.Nodes.Add(gn0);
            csp.Nodes.Add(gn1);
            csp.Nodes.Add(gn2);

            GraphArc ga01 = new GraphArc(gn0, gn1);
            GraphArc ga02 = new GraphArc(gn0, gn2);
            gn0.ConnectedArcs = new List<GraphArc> { ga01, ga02 };
            GraphArc ga10 = new GraphArc(gn1, gn0);
            GraphArc ga12 = new GraphArc(gn1, gn2);
            gn1.ConnectedArcs = new List<GraphArc> { ga10, ga12 };
            GraphArc ga20 = new GraphArc(gn2, gn0);
            GraphArc ga21 = new GraphArc(gn2, gn1);
            gn2.ConnectedArcs = new List<GraphArc> { ga20, ga21 };

            // Act
            var isConsistant = obj.Invoke("IsConsistant", gn0);

            // Assert
            Assert.IsInstanceOfType(isConsistant, typeof(bool), "Wrong type.");
            Assert.IsTrue((bool)isConsistant, "Wrong value.");
        }

        [TestMethod]
        public void T_IsConsistant_WithParameter_True_SameColumn()
        {
            // Arrange
            CSP csp = new CSP();
            PrivateObject obj = new PrivateObject(csp);

            GraphNode gn0 = new GraphNode(new Cell(0, 0));
            gn0.Cell.Value = '9';
            GraphNode gn1 = new GraphNode(new Cell(3, 0));
            gn1.Cell.Value = '5';
            GraphNode gn2 = new GraphNode(new Cell(8, 0));
            gn2.Cell.Value = '1';
            csp.Nodes.Add(gn0);
            csp.Nodes.Add(gn1);
            csp.Nodes.Add(gn2);

            GraphArc ga01 = new GraphArc(gn0, gn1);
            GraphArc ga02 = new GraphArc(gn0, gn2);
            gn0.ConnectedArcs = new List<GraphArc> { ga01, ga02 };
            GraphArc ga10 = new GraphArc(gn1, gn0);
            GraphArc ga12 = new GraphArc(gn1, gn2);
            gn1.ConnectedArcs = new List<GraphArc> { ga10, ga12 };
            GraphArc ga20 = new GraphArc(gn2, gn0);
            GraphArc ga21 = new GraphArc(gn2, gn1);
            gn2.ConnectedArcs = new List<GraphArc> { ga20, ga21 };

            // Act
            var isConsistant = obj.Invoke("IsConsistant", gn0);

            // Assert
            Assert.IsInstanceOfType(isConsistant, typeof(bool), "Wrong type.");
            Assert.IsTrue((bool)isConsistant, "Wrong value.");
        }

        [TestMethod]
        public void T_IsConsistant_WithParameter_True_SameSquare()
        {
            // Arrange
            CSP csp = new CSP();
            PrivateObject obj = new PrivateObject(csp);

            GraphNode gn0 = new GraphNode(new Cell(0, 2));
            gn0.Cell.Value = '9';
            GraphNode gn1 = new GraphNode(new Cell(1, 1));
            gn1.Cell.Value = '2';
            GraphNode gn2 = new GraphNode(new Cell(2, 0));
            gn2.Cell.Value = '3';
            csp.Nodes.Add(gn0);
            csp.Nodes.Add(gn1);
            csp.Nodes.Add(gn2);

            GraphArc ga01 = new GraphArc(gn0, gn1);
            GraphArc ga02 = new GraphArc(gn0, gn2);
            gn0.ConnectedArcs = new List<GraphArc> { ga01, ga02 };
            GraphArc ga10 = new GraphArc(gn1, gn0);
            GraphArc ga12 = new GraphArc(gn1, gn2);
            gn1.ConnectedArcs = new List<GraphArc> { ga10, ga12 };
            GraphArc ga20 = new GraphArc(gn2, gn0);
            GraphArc ga21 = new GraphArc(gn2, gn1);
            gn2.ConnectedArcs = new List<GraphArc> { ga20, ga21 };

            // Act
            var isConsistant = obj.Invoke("IsConsistant", gn0);

            // Assert
            Assert.IsInstanceOfType(isConsistant, typeof(bool), "Wrong type.");
            Assert.IsTrue((bool)isConsistant, "Wrong value.");
        }


        [TestMethod]
        public void T_IsConsistant_False_SameLine()
        {
            // Arrange
            CSP csp = new CSP();
            PrivateObject obj = new PrivateObject(csp);

            GraphNode gn0 = new GraphNode(new Cell(9, 6));
            gn0.Cell.Value = '2';
            GraphNode gn1 = new GraphNode(new Cell(9, 2));
            gn1.Cell.Value = '2';
            GraphNode gn2 = new GraphNode(new Cell(9, 0));
            gn2.Cell.Value = '3';
            csp.Nodes.Add(gn0);
            csp.Nodes.Add(gn1);
            csp.Nodes.Add(gn2);

            GraphArc ga01 = new GraphArc(gn0, gn1);
            GraphArc ga02 = new GraphArc(gn0, gn2);
            gn0.ConnectedArcs = new List<GraphArc> { ga01, ga02 };
            GraphArc ga10 = new GraphArc(gn1, gn0);
            GraphArc ga12 = new GraphArc(gn1, gn2);
            gn1.ConnectedArcs = new List<GraphArc> { ga10, ga12 };
            GraphArc ga20 = new GraphArc(gn2, gn0);
            GraphArc ga21 = new GraphArc(gn2, gn1);
            gn2.ConnectedArcs = new List<GraphArc> { ga20, ga21 };

            // Act
            var isConsistant = obj.Invoke("IsConsistant");

            // Assert
            Assert.IsInstanceOfType(isConsistant, typeof(bool), "Wrong type.");
            Assert.IsFalse((bool)isConsistant, "Wrong value.");
        }

        [TestMethod]
        public void T_IsConsistant_False_SameColumn()
        {
            // Arrange
            CSP csp = new CSP();
            PrivateObject obj = new PrivateObject(csp);

            GraphNode gn0 = new GraphNode(new Cell(0, 0));
            gn0.Cell.Value = '9';
            GraphNode gn1 = new GraphNode(new Cell(3, 0));
            gn1.Cell.Value = '9';
            GraphNode gn2 = new GraphNode(new Cell(8, 0));
            gn2.Cell.Value = '1';
            csp.Nodes.Add(gn0);
            csp.Nodes.Add(gn1);
            csp.Nodes.Add(gn2);

            GraphArc ga01 = new GraphArc(gn0, gn1);
            GraphArc ga02 = new GraphArc(gn0, gn2);
            gn0.ConnectedArcs = new List<GraphArc> { ga01, ga02 };
            GraphArc ga10 = new GraphArc(gn1, gn0);
            GraphArc ga12 = new GraphArc(gn1, gn2);
            gn1.ConnectedArcs = new List<GraphArc> { ga10, ga12 };
            GraphArc ga20 = new GraphArc(gn2, gn0);
            GraphArc ga21 = new GraphArc(gn2, gn1);
            gn2.ConnectedArcs = new List<GraphArc> { ga20, ga21 };

            // Act
            var isConsistant = obj.Invoke("IsConsistant");

            // Assert
            Assert.IsInstanceOfType(isConsistant, typeof(bool), "Wrong type.");
            Assert.IsFalse((bool)isConsistant, "Wrong value.");
        }

        [TestMethod]
        public void T_IsConsistant_False_SameSquare()
        {
            // Arrange
            CSP csp = new CSP();
            PrivateObject obj = new PrivateObject(csp);

            GraphNode gn0 = new GraphNode(new Cell(0, 2));
            gn0.Cell.Value = '2';
            GraphNode gn1 = new GraphNode(new Cell(1, 1));
            gn1.Cell.Value = '2';
            GraphNode gn2 = new GraphNode(new Cell(2, 0));
            gn2.Cell.Value = '3';
            csp.Nodes.Add(gn0);
            csp.Nodes.Add(gn1);
            csp.Nodes.Add(gn2);

            GraphArc ga01 = new GraphArc(gn0, gn1);
            GraphArc ga02 = new GraphArc(gn0, gn2);
            gn0.ConnectedArcs = new List<GraphArc> { ga01, ga02 };
            GraphArc ga10 = new GraphArc(gn1, gn0);
            GraphArc ga12 = new GraphArc(gn1, gn2);
            gn1.ConnectedArcs = new List<GraphArc> { ga10, ga12 };
            GraphArc ga20 = new GraphArc(gn2, gn0);
            GraphArc ga21 = new GraphArc(gn2, gn1);
            gn2.ConnectedArcs = new List<GraphArc> { ga20, ga21 };

            // Act
            var isConsistant = obj.Invoke("IsConsistant");

            // Assert
            Assert.IsInstanceOfType(isConsistant, typeof(bool), "Wrong type.");
            Assert.IsFalse((bool)isConsistant, "Wrong value.");
        }

        [TestMethod]
        public void T_IsConsistant_True_SameLine()
        {
            // Arrange
            CSP csp = new CSP();
            PrivateObject obj = new PrivateObject(csp);

            GraphNode gn0 = new GraphNode(new Cell(9, 6));
            gn0.Cell.Value = '9';
            GraphNode gn1 = new GraphNode(new Cell(9, 2));
            gn1.Cell.Value = '2';
            GraphNode gn2 = new GraphNode(new Cell(9, 0));
            gn2.Cell.Value = '3';
            csp.Nodes.Add(gn0);
            csp.Nodes.Add(gn1);
            csp.Nodes.Add(gn2);

            GraphArc ga01 = new GraphArc(gn0, gn1);
            GraphArc ga02 = new GraphArc(gn0, gn2);
            gn0.ConnectedArcs = new List<GraphArc> { ga01, ga02 };
            GraphArc ga10 = new GraphArc(gn1, gn0);
            GraphArc ga12 = new GraphArc(gn1, gn2);
            gn1.ConnectedArcs = new List<GraphArc> { ga10, ga12 };
            GraphArc ga20 = new GraphArc(gn2, gn0);
            GraphArc ga21 = new GraphArc(gn2, gn1);
            gn2.ConnectedArcs = new List<GraphArc> { ga20, ga21 };

            // Act
            var isConsistant = obj.Invoke("IsConsistant");

            // Assert
            Assert.IsInstanceOfType(isConsistant, typeof(bool), "Wrong type.");
            Assert.IsTrue((bool)isConsistant, "Wrong value.");
        }

        [TestMethod]
        public void T_IsConsistant_True_SameColumn()
        {
            // Arrange
            CSP csp = new CSP();
            PrivateObject obj = new PrivateObject(csp);

            GraphNode gn0 = new GraphNode(new Cell(0, 0));
            gn0.Cell.Value = '9';
            GraphNode gn1 = new GraphNode(new Cell(3, 0));
            gn1.Cell.Value = '5';
            GraphNode gn2 = new GraphNode(new Cell(8, 0));
            gn2.Cell.Value = '1';
            csp.Nodes.Add(gn0);
            csp.Nodes.Add(gn1);
            csp.Nodes.Add(gn2);

            GraphArc ga01 = new GraphArc(gn0, gn1);
            GraphArc ga02 = new GraphArc(gn0, gn2);
            gn0.ConnectedArcs = new List<GraphArc> { ga01, ga02 };
            GraphArc ga10 = new GraphArc(gn1, gn0);
            GraphArc ga12 = new GraphArc(gn1, gn2);
            gn1.ConnectedArcs = new List<GraphArc> { ga10, ga12 };
            GraphArc ga20 = new GraphArc(gn2, gn0);
            GraphArc ga21 = new GraphArc(gn2, gn1);
            gn2.ConnectedArcs = new List<GraphArc> { ga20, ga21 };

            // Act
            var isConsistant = obj.Invoke("IsConsistant");

            // Assert
            Assert.IsInstanceOfType(isConsistant, typeof(bool), "Wrong type.");
            Assert.IsTrue((bool)isConsistant, "Wrong value.");
        }

        [TestMethod]
        public void T_IsConsistant_True_SameSquare()
        {
            // Arrange
            CSP csp = new CSP();
            PrivateObject obj = new PrivateObject(csp);

            GraphNode gn0 = new GraphNode(new Cell(0, 2));
            gn0.Cell.Value = '9';
            GraphNode gn1 = new GraphNode(new Cell(1, 1));
            gn1.Cell.Value = '2';
            GraphNode gn2 = new GraphNode(new Cell(2, 0));
            gn2.Cell.Value = '3';
            csp.Nodes.Add(gn0);
            csp.Nodes.Add(gn1);
            csp.Nodes.Add(gn2);

            GraphArc ga01 = new GraphArc(gn0, gn1);
            GraphArc ga02 = new GraphArc(gn0, gn2);
            gn0.ConnectedArcs = new List<GraphArc> { ga01, ga02 };
            GraphArc ga10 = new GraphArc(gn1, gn0);
            GraphArc ga12 = new GraphArc(gn1, gn2);
            gn1.ConnectedArcs = new List<GraphArc> { ga10, ga12 };
            GraphArc ga20 = new GraphArc(gn2, gn0);
            GraphArc ga21 = new GraphArc(gn2, gn1);
            gn2.ConnectedArcs = new List<GraphArc> { ga20, ga21 };

            // Act
            var isConsistant = obj.Invoke("IsConsistant");

            // Assert
            Assert.IsInstanceOfType(isConsistant, typeof(bool), "Wrong type.");
            Assert.IsTrue((bool)isConsistant, "Wrong value.");
        }

        [TestMethod]
        public void T_IsComplete_True()
        {
            // Arrange
            CSP csp = new CSP();
            PrivateObject obj = new PrivateObject(csp);

            GraphNode gn0 = new GraphNode(new Cell(0, 9));
            gn0.Cell.Value = '9';
            GraphNode gn1 = new GraphNode(new Cell(3, 3));
            gn1.Cell.Value = '2';
            GraphNode gn2 = new GraphNode(new Cell(9, 0));
            gn2.Cell.Value = '3';
            csp.Nodes.Add(gn0);
            csp.Nodes.Add(gn1);
            csp.Nodes.Add(gn2);

            // Act
            var isConsistant = obj.Invoke("IsComplete");

            // Assert
            Assert.IsInstanceOfType(isConsistant, typeof(bool), "Wrong type.");
            Assert.IsTrue((bool)isConsistant, "Wrong value.");
        }

        [TestMethod]
        public void T_IsComplete_False()
        {
            // Arrange
            CSP csp = new CSP();
            PrivateObject obj = new PrivateObject(csp);

            GraphNode gn0 = new GraphNode(new Cell(0, 9));
            gn0.Cell.Value = '9';
            GraphNode gn1 = new GraphNode(new Cell(3, 3));
            gn1.Cell.Value = '.';
            GraphNode gn2 = new GraphNode(new Cell(9, 0));
            gn2.Cell.Value = '3';
            csp.Nodes.Add(gn0);
            csp.Nodes.Add(gn1);
            csp.Nodes.Add(gn2);

            // Act
            var isConsistant = obj.Invoke("IsComplete");

            // Assert
            Assert.IsInstanceOfType(isConsistant, typeof(bool), "Wrong type.");
            Assert.IsFalse((bool)isConsistant, "Wrong value.");
        }

        [TestMethod]
        public void T_RecursiveBacktracking_9x9()
        {
            // Arrange
            CSP csp = new CSP();
            PrivateObject obj = new PrivateObject(csp);

            GraphNode[,] sudoku = new GraphNode[,] {
                {
                    new GraphNode(new Cell(0, 0)),
                    new GraphNode(new Cell(0, 1)),
                    new GraphNode(new Cell(0, 2)),
                    new GraphNode(new Cell(0, 3)),
                    new GraphNode(new Cell(0, 4)),
                    new GraphNode(new Cell(0, 5)),
                    new GraphNode(new Cell(0, 6)),
                    new GraphNode(new Cell(0, 7)),
                    new GraphNode(new Cell(0, 8)),
                },
                {
                    new GraphNode(new Cell(1, 0)),
                    new GraphNode(new Cell(1, 1)),
                    new GraphNode(new Cell(1, 2)),
                    new GraphNode(new Cell(1, 3)),
                    new GraphNode(new Cell(1, 4)),
                    new GraphNode(new Cell(1, 5)),
                    new GraphNode(new Cell(1, 6)),
                    new GraphNode(new Cell(1, 7)),
                    new GraphNode(new Cell(1, 8)),
                },
                {
                    new GraphNode(new Cell(2, 0)),
                    new GraphNode(new Cell(2, 1)),
                    new GraphNode(new Cell(2, 2)),
                    new GraphNode(new Cell(2, 3)),
                    new GraphNode(new Cell(2, 4)),
                    new GraphNode(new Cell(2, 5)),
                    new GraphNode(new Cell(2, 6)),
                    new GraphNode(new Cell(2, 7)),
                    new GraphNode(new Cell(2, 8)),
                },
                {
                    new GraphNode(new Cell(3, 0)),
                    new GraphNode(new Cell(3, 1)),
                    new GraphNode(new Cell(3, 2)),
                    new GraphNode(new Cell(3, 3)),
                    new GraphNode(new Cell(3, 4)),
                    new GraphNode(new Cell(3, 5)),
                    new GraphNode(new Cell(3, 6)),
                    new GraphNode(new Cell(3, 7)),
                    new GraphNode(new Cell(3, 8)),
                },
                {
                    new GraphNode(new Cell(4, 0)),
                    new GraphNode(new Cell(4, 1)),
                    new GraphNode(new Cell(4, 2)),
                    new GraphNode(new Cell(4, 3)),
                    new GraphNode(new Cell(4, 4)),
                    new GraphNode(new Cell(4, 5)),
                    new GraphNode(new Cell(4, 6)),
                    new GraphNode(new Cell(4, 7)),
                    new GraphNode(new Cell(4, 8)),
                },
                {
                    new GraphNode(new Cell(5, 0)),
                    new GraphNode(new Cell(5, 1)),
                    new GraphNode(new Cell(5, 2)),
                    new GraphNode(new Cell(5, 3)),
                    new GraphNode(new Cell(5, 4)),
                    new GraphNode(new Cell(5, 5)),
                    new GraphNode(new Cell(5, 6)),
                    new GraphNode(new Cell(5, 7)),
                    new GraphNode(new Cell(5, 8)),
                },
                {
                    new GraphNode(new Cell(6, 0)),
                    new GraphNode(new Cell(6, 1)),
                    new GraphNode(new Cell(6, 2)),
                    new GraphNode(new Cell(6, 3)),
                    new GraphNode(new Cell(6, 4)),
                    new GraphNode(new Cell(6, 5)),
                    new GraphNode(new Cell(6, 6)),
                    new GraphNode(new Cell(6, 7)),
                    new GraphNode(new Cell(6, 8)),
                },
                {
                    new GraphNode(new Cell(7, 0)),
                    new GraphNode(new Cell(7, 1)),
                    new GraphNode(new Cell(7, 2)),
                    new GraphNode(new Cell(7, 3)),
                    new GraphNode(new Cell(7, 4)),
                    new GraphNode(new Cell(7, 5)),
                    new GraphNode(new Cell(7, 6)),
                    new GraphNode(new Cell(7, 7)),
                    new GraphNode(new Cell(7, 8)),
                },
                {
                    new GraphNode(new Cell(8, 0)),
                    new GraphNode(new Cell(8, 1)),
                    new GraphNode(new Cell(8, 2)),
                    new GraphNode(new Cell(8, 3)),
                    new GraphNode(new Cell(8, 4)),
                    new GraphNode(new Cell(8, 5)),
                    new GraphNode(new Cell(8, 6)),
                    new GraphNode(new Cell(8, 7)),
                    new GraphNode(new Cell(8, 8)),
                }
            };
            sudoku[0, 0].Cell.Value = '.';
            sudoku[1, 0].Cell.Value = '.';
            sudoku[2, 0].Cell.Value = '.';
            sudoku[3, 0].Cell.Value = '1';
            sudoku[4, 0].Cell.Value = '.';
            sudoku[5, 0].Cell.Value = '.';
            sudoku[6, 0].Cell.Value = '.';
            sudoku[7, 0].Cell.Value = '.';
            sudoku[8, 0].Cell.Value = '5';
            sudoku[0, 1].Cell.Value = '.';
            sudoku[1, 1].Cell.Value = '.';
            sudoku[2, 1].Cell.Value = '.';
            sudoku[3, 1].Cell.Value = '8';
            sudoku[4, 1].Cell.Value = '.';
            sudoku[5, 1].Cell.Value = '3';
            sudoku[6, 1].Cell.Value = '.';
            sudoku[7, 1].Cell.Value = '.';
            sudoku[8, 1].Cell.Value = '.';
            sudoku[0, 2].Cell.Value = '.';
            sudoku[1, 2].Cell.Value = '.';
            sudoku[2, 2].Cell.Value = '.';
            sudoku[3, 2].Cell.Value = '.';
            sudoku[4, 2].Cell.Value = '.';
            sudoku[5, 2].Cell.Value = '.';
            sudoku[6, 2].Cell.Value = '6';
            sudoku[7, 2].Cell.Value = '2';
            sudoku[8, 2].Cell.Value = '.';
            sudoku[0, 3].Cell.Value = '.';
            sudoku[1, 3].Cell.Value = '.';
            sudoku[2, 3].Cell.Value = '4';
            sudoku[3, 3].Cell.Value = '.';
            sudoku[4, 3].Cell.Value = '.';
            sudoku[5, 3].Cell.Value = '.';
            sudoku[6, 3].Cell.Value = '.';
            sudoku[7, 3].Cell.Value = '.';
            sudoku[8, 3].Cell.Value = '.';
            sudoku[0, 4].Cell.Value = '.';
            sudoku[1, 4].Cell.Value = '.';
            sudoku[2, 4].Cell.Value = '.';
            sudoku[3, 4].Cell.Value = '.';
            sudoku[4, 4].Cell.Value = '6';
            sudoku[5, 4].Cell.Value = '1';
            sudoku[6, 4].Cell.Value = '.';
            sudoku[7, 4].Cell.Value = '.';
            sudoku[8, 4].Cell.Value = '3';
            sudoku[0, 5].Cell.Value = '.';
            sudoku[1, 5].Cell.Value = '.';
            sudoku[2, 5].Cell.Value = '9';
            sudoku[3, 5].Cell.Value = '.';
            sudoku[4, 5].Cell.Value = '.';
            sudoku[5, 5].Cell.Value = '.';
            sudoku[6, 5].Cell.Value = '.';
            sudoku[7, 5].Cell.Value = '7';
            sudoku[8, 5].Cell.Value = '.';
            sudoku[0, 6].Cell.Value = '3';
            sudoku[1, 6].Cell.Value = '.';
            sudoku[2, 6].Cell.Value = '.';
            sudoku[3, 6].Cell.Value = '.';
            sudoku[4, 6].Cell.Value = '.';
            sudoku[5, 6].Cell.Value = '.';
            sudoku[6, 6].Cell.Value = '.';
            sudoku[7, 6].Cell.Value = '.';
            sudoku[8, 6].Cell.Value = '.';
            sudoku[0, 7].Cell.Value = '1';
            sudoku[1, 7].Cell.Value = '.';
            sudoku[2, 7].Cell.Value = '.';
            sudoku[3, 7].Cell.Value = '.';
            sudoku[4, 7].Cell.Value = '.';
            sudoku[5, 7].Cell.Value = '.';
            sudoku[6, 7].Cell.Value = '8';
            sudoku[7, 7].Cell.Value = '.';
            sudoku[8, 7].Cell.Value = '.';
            sudoku[0, 8].Cell.Value = '.';
            sudoku[1, 8].Cell.Value = '.';
            sudoku[2, 8].Cell.Value = '.';
            sudoku[3, 8].Cell.Value = '.';
            sudoku[4, 8].Cell.Value = '7';
            sudoku[5, 8].Cell.Value = '.';
            sudoku[6, 8].Cell.Value = '.';
            sudoku[7, 8].Cell.Value = '9';
            sudoku[8, 8].Cell.Value = '.';
            sudoku[0, 0].Cell.ZoneNumber = 0;
            sudoku[1, 0].Cell.ZoneNumber = 0;
            sudoku[2, 0].Cell.ZoneNumber = 0;
            sudoku[3, 0].Cell.ZoneNumber = 1;
            sudoku[4, 0].Cell.ZoneNumber = 1;
            sudoku[5, 0].Cell.ZoneNumber = 1;
            sudoku[6, 0].Cell.ZoneNumber = 2;
            sudoku[7, 0].Cell.ZoneNumber = 2;
            sudoku[8, 0].Cell.ZoneNumber = 2;
            sudoku[0, 1].Cell.ZoneNumber = 0;
            sudoku[1, 1].Cell.ZoneNumber = 0;
            sudoku[2, 1].Cell.ZoneNumber = 0;
            sudoku[3, 1].Cell.ZoneNumber = 1;
            sudoku[4, 1].Cell.ZoneNumber = 1;
            sudoku[5, 1].Cell.ZoneNumber = 1;
            sudoku[6, 1].Cell.ZoneNumber = 2;
            sudoku[7, 1].Cell.ZoneNumber = 2;
            sudoku[8, 1].Cell.ZoneNumber = 2;
            sudoku[0, 2].Cell.ZoneNumber = 0;
            sudoku[1, 2].Cell.ZoneNumber = 0;
            sudoku[2, 2].Cell.ZoneNumber = 0;
            sudoku[3, 2].Cell.ZoneNumber = 1;
            sudoku[4, 2].Cell.ZoneNumber = 1;
            sudoku[5, 2].Cell.ZoneNumber = 1;
            sudoku[6, 2].Cell.ZoneNumber = 2;
            sudoku[7, 2].Cell.ZoneNumber = 2;
            sudoku[8, 2].Cell.ZoneNumber = 2;
            sudoku[0, 3].Cell.ZoneNumber = 3;
            sudoku[1, 3].Cell.ZoneNumber = 3;
            sudoku[2, 3].Cell.ZoneNumber = 3;
            sudoku[3, 3].Cell.ZoneNumber = 4;
            sudoku[4, 3].Cell.ZoneNumber = 4;
            sudoku[5, 3].Cell.ZoneNumber = 4;
            sudoku[6, 3].Cell.ZoneNumber = 5;
            sudoku[7, 3].Cell.ZoneNumber = 5;
            sudoku[8, 3].Cell.ZoneNumber = 5;
            sudoku[0, 4].Cell.ZoneNumber = 3;
            sudoku[1, 4].Cell.ZoneNumber = 3;
            sudoku[2, 4].Cell.ZoneNumber = 3;
            sudoku[3, 4].Cell.ZoneNumber = 4;
            sudoku[4, 4].Cell.ZoneNumber = 4;
            sudoku[5, 4].Cell.ZoneNumber = 4;
            sudoku[6, 4].Cell.ZoneNumber = 5;
            sudoku[7, 4].Cell.ZoneNumber = 5;
            sudoku[8, 4].Cell.ZoneNumber = 5;
            sudoku[0, 5].Cell.ZoneNumber = 3;
            sudoku[1, 5].Cell.ZoneNumber = 3;
            sudoku[2, 5].Cell.ZoneNumber = 3;
            sudoku[3, 5].Cell.ZoneNumber = 4;
            sudoku[4, 5].Cell.ZoneNumber = 4;
            sudoku[5, 5].Cell.ZoneNumber = 4;
            sudoku[6, 5].Cell.ZoneNumber = 5;
            sudoku[7, 5].Cell.ZoneNumber = 5;
            sudoku[8, 5].Cell.ZoneNumber = 5;
            sudoku[0, 6].Cell.ZoneNumber = 6;
            sudoku[1, 6].Cell.ZoneNumber = 6;
            sudoku[2, 6].Cell.ZoneNumber = 6;
            sudoku[3, 6].Cell.ZoneNumber = 7;
            sudoku[4, 6].Cell.ZoneNumber = 7;
            sudoku[5, 6].Cell.ZoneNumber = 7;
            sudoku[6, 6].Cell.ZoneNumber = 8;
            sudoku[7, 6].Cell.ZoneNumber = 8;
            sudoku[8, 6].Cell.ZoneNumber = 8;
            sudoku[0, 7].Cell.ZoneNumber = 6;
            sudoku[1, 7].Cell.ZoneNumber = 6;
            sudoku[2, 7].Cell.ZoneNumber = 6;
            sudoku[3, 7].Cell.ZoneNumber = 7;
            sudoku[4, 7].Cell.ZoneNumber = 7;
            sudoku[5, 7].Cell.ZoneNumber = 7;
            sudoku[6, 7].Cell.ZoneNumber = 8;
            sudoku[7, 7].Cell.ZoneNumber = 8;
            sudoku[8, 7].Cell.ZoneNumber = 8;
            sudoku[0, 8].Cell.ZoneNumber = 6;
            sudoku[1, 8].Cell.ZoneNumber = 6;
            sudoku[2, 8].Cell.ZoneNumber = 6;
            sudoku[3, 8].Cell.ZoneNumber = 7;
            sudoku[4, 8].Cell.ZoneNumber = 7;
            sudoku[5, 8].Cell.ZoneNumber = 7;
            sudoku[6, 8].Cell.ZoneNumber = 8;
            sudoku[7, 8].Cell.ZoneNumber = 8;
            sudoku[8, 8].Cell.ZoneNumber = 8;
            csp.Nodes.Add(sudoku[0, 0]);
            csp.Nodes.Add(sudoku[3, 0]);
            csp.Nodes.Add(sudoku[4, 0]);
            csp.Nodes.Add(sudoku[5, 0]);
            csp.Nodes.Add(sudoku[6, 0]);
            csp.Nodes.Add(sudoku[7, 0]);
            csp.Nodes.Add(sudoku[8, 0]);
            csp.Nodes.Add(sudoku[0, 1]);
            csp.Nodes.Add(sudoku[1, 1]);
            csp.Nodes.Add(sudoku[2, 1]);
            csp.Nodes.Add(sudoku[3, 1]);
            csp.Nodes.Add(sudoku[4, 1]);
            csp.Nodes.Add(sudoku[5, 1]);
            csp.Nodes.Add(sudoku[6, 1]);
            csp.Nodes.Add(sudoku[7, 1]);
            csp.Nodes.Add(sudoku[8, 1]);
            csp.Nodes.Add(sudoku[0, 2]);
            csp.Nodes.Add(sudoku[1, 2]);
            csp.Nodes.Add(sudoku[2, 2]);
            csp.Nodes.Add(sudoku[3, 2]);
            csp.Nodes.Add(sudoku[4, 2]);
            csp.Nodes.Add(sudoku[5, 2]);
            csp.Nodes.Add(sudoku[6, 2]);
            csp.Nodes.Add(sudoku[7, 2]);
            csp.Nodes.Add(sudoku[8, 2]);
            csp.Nodes.Add(sudoku[0, 3]);
            csp.Nodes.Add(sudoku[1, 3]);
            csp.Nodes.Add(sudoku[2, 3]);
            csp.Nodes.Add(sudoku[3, 3]);
            csp.Nodes.Add(sudoku[4, 3]);
            csp.Nodes.Add(sudoku[5, 3]);
            csp.Nodes.Add(sudoku[6, 3]);
            csp.Nodes.Add(sudoku[7, 3]);
            csp.Nodes.Add(sudoku[8, 3]);
            csp.Nodes.Add(sudoku[0, 4]);
            csp.Nodes.Add(sudoku[1, 4]);
            csp.Nodes.Add(sudoku[2, 4]);
            csp.Nodes.Add(sudoku[3, 4]);
            csp.Nodes.Add(sudoku[4, 4]);
            csp.Nodes.Add(sudoku[5, 4]);
            csp.Nodes.Add(sudoku[6, 4]);
            csp.Nodes.Add(sudoku[7, 4]);
            csp.Nodes.Add(sudoku[8, 4]);
            csp.Nodes.Add(sudoku[0, 5]);
            csp.Nodes.Add(sudoku[1, 5]);
            csp.Nodes.Add(sudoku[2, 5]);
            csp.Nodes.Add(sudoku[3, 5]);
            csp.Nodes.Add(sudoku[4, 5]);
            csp.Nodes.Add(sudoku[5, 5]);
            csp.Nodes.Add(sudoku[6, 5]);
            csp.Nodes.Add(sudoku[7, 5]);
            csp.Nodes.Add(sudoku[8, 5]);
            csp.Nodes.Add(sudoku[0, 6]);
            csp.Nodes.Add(sudoku[1, 6]);
            csp.Nodes.Add(sudoku[2, 6]);
            csp.Nodes.Add(sudoku[3, 6]);
            csp.Nodes.Add(sudoku[4, 6]);
            csp.Nodes.Add(sudoku[5, 6]);
            csp.Nodes.Add(sudoku[6, 6]);
            csp.Nodes.Add(sudoku[7, 6]);
            csp.Nodes.Add(sudoku[8, 6]);
            csp.Nodes.Add(sudoku[0, 7]);
            csp.Nodes.Add(sudoku[1, 7]);
            csp.Nodes.Add(sudoku[2, 7]);
            csp.Nodes.Add(sudoku[3, 7]);
            csp.Nodes.Add(sudoku[4, 7]);
            csp.Nodes.Add(sudoku[5, 7]);
            csp.Nodes.Add(sudoku[6, 7]);
            csp.Nodes.Add(sudoku[7, 7]);
            csp.Nodes.Add(sudoku[8, 7]);
            csp.Nodes.Add(sudoku[0, 8]);
            csp.Nodes.Add(sudoku[1, 8]);
            csp.Nodes.Add(sudoku[2, 8]);
            csp.Nodes.Add(sudoku[3, 8]);
            csp.Nodes.Add(sudoku[4, 8]);
            csp.Nodes.Add(sudoku[5, 8]);
            csp.Nodes.Add(sudoku[6, 8]);
            csp.Nodes.Add(sudoku[7, 8]);
            csp.Nodes.Add(sudoku[8, 8]);
            csp.Nodes.Add(sudoku[1, 0]);
            csp.Nodes.Add(sudoku[2, 0]);
            csp.GenerateArcs();

            List<GraphNode> expected = new List<GraphNode>();
            sudoku[0, 0].Cell.Value = '9';
            sudoku[1, 0].Cell.Value = '4';
            sudoku[2, 0].Cell.Value = '7';
            sudoku[3, 0].Cell.Value = '1';
            sudoku[4, 0].Cell.Value = '2';
            sudoku[5, 0].Cell.Value = '6';
            sudoku[6, 0].Cell.Value = '3';
            sudoku[7, 0].Cell.Value = '8';
            sudoku[8, 0].Cell.Value = '5';
            sudoku[0, 1].Cell.Value = '2';
            sudoku[1, 1].Cell.Value = '6';
            sudoku[2, 1].Cell.Value = '1';
            sudoku[3, 1].Cell.Value = '8';
            sudoku[4, 1].Cell.Value = '5';
            sudoku[5, 1].Cell.Value = '3';
            sudoku[6, 1].Cell.Value = '7';
            sudoku[7, 1].Cell.Value = '4';
            sudoku[8, 1].Cell.Value = '9';
            sudoku[0, 2].Cell.Value = '8';
            sudoku[1, 2].Cell.Value = '5';
            sudoku[2, 2].Cell.Value = '3';
            sudoku[3, 2].Cell.Value = '7';
            sudoku[4, 2].Cell.Value = '4';
            sudoku[5, 2].Cell.Value = '9';
            sudoku[6, 2].Cell.Value = '6';
            sudoku[7, 2].Cell.Value = '2';
            sudoku[8, 2].Cell.Value = '1';
            sudoku[0, 3].Cell.Value = '5';
            sudoku[1, 3].Cell.Value = '3';
            sudoku[2, 3].Cell.Value = '4';
            sudoku[3, 3].Cell.Value = '2';
            sudoku[4, 3].Cell.Value = '8';
            sudoku[5, 3].Cell.Value = '7';
            sudoku[6, 3].Cell.Value = '9';
            sudoku[7, 3].Cell.Value = '1';
            sudoku[8, 3].Cell.Value = '6';
            sudoku[0, 4].Cell.Value = '7';
            sudoku[1, 4].Cell.Value = '2';
            sudoku[2, 4].Cell.Value = '8';
            sudoku[3, 4].Cell.Value = '9';
            sudoku[4, 4].Cell.Value = '6';
            sudoku[5, 4].Cell.Value = '1';
            sudoku[6, 4].Cell.Value = '4';
            sudoku[7, 4].Cell.Value = '5';
            sudoku[8, 4].Cell.Value = '3';
            sudoku[0, 5].Cell.Value = '6';
            sudoku[1, 5].Cell.Value = '1';
            sudoku[2, 5].Cell.Value = '9';
            sudoku[3, 5].Cell.Value = '5';
            sudoku[4, 5].Cell.Value = '3';
            sudoku[5, 5].Cell.Value = '4';
            sudoku[6, 5].Cell.Value = '2';
            sudoku[7, 5].Cell.Value = '7';
            sudoku[8, 5].Cell.Value = '8';
            sudoku[0, 6].Cell.Value = '3';
            sudoku[1, 6].Cell.Value = '9';
            sudoku[2, 6].Cell.Value = '2';
            sudoku[3, 6].Cell.Value = '4';
            sudoku[4, 6].Cell.Value = '1';
            sudoku[5, 6].Cell.Value = '8';
            sudoku[6, 6].Cell.Value = '5';
            sudoku[7, 6].Cell.Value = '6';
            sudoku[8, 6].Cell.Value = '7';
            sudoku[0, 7].Cell.Value = '1';
            sudoku[1, 7].Cell.Value = '7';
            sudoku[2, 7].Cell.Value = '5';
            sudoku[3, 7].Cell.Value = '6';
            sudoku[4, 7].Cell.Value = '9';
            sudoku[5, 7].Cell.Value = '2';
            sudoku[6, 7].Cell.Value = '8';
            sudoku[7, 7].Cell.Value = '3';
            sudoku[8, 7].Cell.Value = '4';
            sudoku[0, 8].Cell.Value = '4';
            sudoku[1, 8].Cell.Value = '8';
            sudoku[2, 8].Cell.Value = '6';
            sudoku[3, 8].Cell.Value = '3';
            sudoku[4, 8].Cell.Value = '7';
            sudoku[5, 8].Cell.Value = '5';
            sudoku[6, 8].Cell.Value = '1';
            sudoku[7, 8].Cell.Value = '9';
            sudoku[8, 8].Cell.Value = '2';
            expected.Add(sudoku[0, 0]);
            expected.Add(sudoku[3, 0]);
            expected.Add(sudoku[4, 0]);
            expected.Add(sudoku[5, 0]);
            expected.Add(sudoku[6, 0]);
            expected.Add(sudoku[7, 0]);
            expected.Add(sudoku[8, 0]);
            expected.Add(sudoku[0, 1]);
            expected.Add(sudoku[1, 1]);
            expected.Add(sudoku[2, 1]);
            expected.Add(sudoku[3, 1]);
            expected.Add(sudoku[4, 1]);
            expected.Add(sudoku[5, 1]);
            expected.Add(sudoku[6, 1]);
            expected.Add(sudoku[7, 1]);
            expected.Add(sudoku[8, 1]);
            expected.Add(sudoku[0, 2]);
            expected.Add(sudoku[1, 2]);
            expected.Add(sudoku[2, 2]);
            expected.Add(sudoku[3, 2]);
            expected.Add(sudoku[4, 2]);
            expected.Add(sudoku[5, 2]);
            expected.Add(sudoku[6, 2]);
            expected.Add(sudoku[7, 2]);
            expected.Add(sudoku[8, 2]);
            expected.Add(sudoku[0, 3]);
            expected.Add(sudoku[1, 3]);
            expected.Add(sudoku[2, 3]);
            expected.Add(sudoku[3, 3]);
            expected.Add(sudoku[4, 3]);
            expected.Add(sudoku[5, 3]);
            expected.Add(sudoku[6, 3]);
            expected.Add(sudoku[7, 3]);
            expected.Add(sudoku[8, 3]);
            expected.Add(sudoku[0, 4]);
            expected.Add(sudoku[1, 4]);
            expected.Add(sudoku[2, 4]);
            expected.Add(sudoku[3, 4]);
            expected.Add(sudoku[4, 4]);
            expected.Add(sudoku[5, 4]);
            expected.Add(sudoku[6, 4]);
            expected.Add(sudoku[7, 4]);
            expected.Add(sudoku[8, 4]);
            expected.Add(sudoku[0, 5]);
            expected.Add(sudoku[1, 5]);
            expected.Add(sudoku[2, 5]);
            expected.Add(sudoku[3, 5]);
            expected.Add(sudoku[4, 5]);
            expected.Add(sudoku[5, 5]);
            expected.Add(sudoku[6, 5]);
            expected.Add(sudoku[7, 5]);
            expected.Add(sudoku[8, 5]);
            expected.Add(sudoku[0, 6]);
            expected.Add(sudoku[1, 6]);
            expected.Add(sudoku[2, 6]);
            expected.Add(sudoku[3, 6]);
            expected.Add(sudoku[4, 6]);
            expected.Add(sudoku[5, 6]);
            expected.Add(sudoku[6, 6]);
            expected.Add(sudoku[7, 6]);
            expected.Add(sudoku[8, 6]);
            expected.Add(sudoku[0, 7]);
            expected.Add(sudoku[1, 7]);
            expected.Add(sudoku[2, 7]);
            expected.Add(sudoku[3, 7]);
            expected.Add(sudoku[4, 7]);
            expected.Add(sudoku[5, 7]);
            expected.Add(sudoku[6, 7]);
            expected.Add(sudoku[7, 7]);
            expected.Add(sudoku[8, 7]);
            expected.Add(sudoku[0, 8]);
            expected.Add(sudoku[1, 8]);
            expected.Add(sudoku[2, 8]);
            expected.Add(sudoku[3, 8]);
            expected.Add(sudoku[4, 8]);
            expected.Add(sudoku[5, 8]);
            expected.Add(sudoku[6, 8]);
            expected.Add(sudoku[7, 8]);
            expected.Add(sudoku[8, 8]);
            expected.Add(sudoku[1, 0]);
            expected.Add(sudoku[2, 0]);

            // Act
            var isResolved = csp.BacktrackingSearch();

            // Assert
            Assert.IsTrue(isResolved, "Isn't resolved.");
            CollectionAssert.AreEqual(csp.Nodes, expected, "Wrong values.");
        }

        [TestMethod]
        public void T_RecursiveBacktracking_9x9_2()
        {
            // Arrange
            CSP csp = new CSP();
            PrivateObject obj = new PrivateObject(csp);

            GraphNode[,] sudoku = new GraphNode[,] {
                {
                    new GraphNode(new Cell(0, 0)),
                    new GraphNode(new Cell(0, 1)),
                    new GraphNode(new Cell(0, 2)),
                    new GraphNode(new Cell(0, 3)),
                    new GraphNode(new Cell(0, 4)),
                    new GraphNode(new Cell(0, 5)),
                    new GraphNode(new Cell(0, 6)),
                    new GraphNode(new Cell(0, 7)),
                    new GraphNode(new Cell(0, 8)),
                },
                {
                    new GraphNode(new Cell(1, 0)),
                    new GraphNode(new Cell(1, 1)),
                    new GraphNode(new Cell(1, 2)),
                    new GraphNode(new Cell(1, 3)),
                    new GraphNode(new Cell(1, 4)),
                    new GraphNode(new Cell(1, 5)),
                    new GraphNode(new Cell(1, 6)),
                    new GraphNode(new Cell(1, 7)),
                    new GraphNode(new Cell(1, 8)),
                },
                {
                    new GraphNode(new Cell(2, 0)),
                    new GraphNode(new Cell(2, 1)),
                    new GraphNode(new Cell(2, 2)),
                    new GraphNode(new Cell(2, 3)),
                    new GraphNode(new Cell(2, 4)),
                    new GraphNode(new Cell(2, 5)),
                    new GraphNode(new Cell(2, 6)),
                    new GraphNode(new Cell(2, 7)),
                    new GraphNode(new Cell(2, 8)),
                },
                {
                    new GraphNode(new Cell(3, 0)),
                    new GraphNode(new Cell(3, 1)),
                    new GraphNode(new Cell(3, 2)),
                    new GraphNode(new Cell(3, 3)),
                    new GraphNode(new Cell(3, 4)),
                    new GraphNode(new Cell(3, 5)),
                    new GraphNode(new Cell(3, 6)),
                    new GraphNode(new Cell(3, 7)),
                    new GraphNode(new Cell(3, 8)),
                },
                {
                    new GraphNode(new Cell(4, 0)),
                    new GraphNode(new Cell(4, 1)),
                    new GraphNode(new Cell(4, 2)),
                    new GraphNode(new Cell(4, 3)),
                    new GraphNode(new Cell(4, 4)),
                    new GraphNode(new Cell(4, 5)),
                    new GraphNode(new Cell(4, 6)),
                    new GraphNode(new Cell(4, 7)),
                    new GraphNode(new Cell(4, 8)),
                },
                {
                    new GraphNode(new Cell(5, 0)),
                    new GraphNode(new Cell(5, 1)),
                    new GraphNode(new Cell(5, 2)),
                    new GraphNode(new Cell(5, 3)),
                    new GraphNode(new Cell(5, 4)),
                    new GraphNode(new Cell(5, 5)),
                    new GraphNode(new Cell(5, 6)),
                    new GraphNode(new Cell(5, 7)),
                    new GraphNode(new Cell(5, 8)),
                },
                {
                    new GraphNode(new Cell(6, 0)),
                    new GraphNode(new Cell(6, 1)),
                    new GraphNode(new Cell(6, 2)),
                    new GraphNode(new Cell(6, 3)),
                    new GraphNode(new Cell(6, 4)),
                    new GraphNode(new Cell(6, 5)),
                    new GraphNode(new Cell(6, 6)),
                    new GraphNode(new Cell(6, 7)),
                    new GraphNode(new Cell(6, 8)),
                },
                {
                    new GraphNode(new Cell(7, 0)),
                    new GraphNode(new Cell(7, 1)),
                    new GraphNode(new Cell(7, 2)),
                    new GraphNode(new Cell(7, 3)),
                    new GraphNode(new Cell(7, 4)),
                    new GraphNode(new Cell(7, 5)),
                    new GraphNode(new Cell(7, 6)),
                    new GraphNode(new Cell(7, 7)),
                    new GraphNode(new Cell(7, 8)),
                },
                {
                    new GraphNode(new Cell(8, 0)),
                    new GraphNode(new Cell(8, 1)),
                    new GraphNode(new Cell(8, 2)),
                    new GraphNode(new Cell(8, 3)),
                    new GraphNode(new Cell(8, 4)),
                    new GraphNode(new Cell(8, 5)),
                    new GraphNode(new Cell(8, 6)),
                    new GraphNode(new Cell(8, 7)),
                    new GraphNode(new Cell(8, 8)),
                }
            };
            sudoku[0, 0].Cell.Value = '.';
            sudoku[1, 0].Cell.Value = '.';
            sudoku[2, 0].Cell.Value = '.';
            sudoku[3, 0].Cell.Value = '.';
            sudoku[4, 0].Cell.Value = '.';
            sudoku[5, 0].Cell.Value = '.';
            sudoku[6, 0].Cell.Value = '.';
            sudoku[7, 0].Cell.Value = '4';
            sudoku[8, 0].Cell.Value = '7';
            sudoku[0, 1].Cell.Value = '.';
            sudoku[1, 1].Cell.Value = '.';
            sudoku[2, 1].Cell.Value = '.';
            sudoku[3, 1].Cell.Value = '.';
            sudoku[4, 1].Cell.Value = '.';
            sudoku[5, 1].Cell.Value = '3';
            sudoku[6, 1].Cell.Value = '.';
            sudoku[7, 1].Cell.Value = '.';
            sudoku[8, 1].Cell.Value = '8';
            sudoku[0, 2].Cell.Value = '.';
            sudoku[1, 2].Cell.Value = '9';
            sudoku[2, 2].Cell.Value = '.';
            sudoku[3, 2].Cell.Value = '.';
            sudoku[4, 2].Cell.Value = '.';
            sudoku[5, 2].Cell.Value = '6';
            sudoku[6, 2].Cell.Value = '.';
            sudoku[7, 2].Cell.Value = '.';
            sudoku[8, 2].Cell.Value = '.';
            sudoku[0, 3].Cell.Value = '.';
            sudoku[1, 3].Cell.Value = '6';
            sudoku[2, 3].Cell.Value = '4';
            sudoku[3, 3].Cell.Value = '.';
            sudoku[4, 3].Cell.Value = '8';
            sudoku[5, 3].Cell.Value = '.';
            sudoku[6, 3].Cell.Value = '.';
            sudoku[7, 3].Cell.Value = '5';
            sudoku[8, 3].Cell.Value = '.';
            sudoku[0, 4].Cell.Value = '.';
            sudoku[1, 4].Cell.Value = '.';
            sudoku[2, 4].Cell.Value = '5';
            sudoku[3, 4].Cell.Value = '.';
            sudoku[4, 4].Cell.Value = '.';
            sudoku[5, 4].Cell.Value = '.';
            sudoku[6, 4].Cell.Value = '7';
            sudoku[7, 4].Cell.Value = '9';
            sudoku[8, 4].Cell.Value = '.';
            sudoku[0, 5].Cell.Value = '.';
            sudoku[1, 5].Cell.Value = '.';
            sudoku[2, 5].Cell.Value = '.';
            sudoku[3, 5].Cell.Value = '.';
            sudoku[4, 5].Cell.Value = '6';
            sudoku[5, 5].Cell.Value = '2';
            sudoku[6, 5].Cell.Value = '.';
            sudoku[7, 5].Cell.Value = '.';
            sudoku[8, 5].Cell.Value = '.';
            sudoku[0, 6].Cell.Value = '1';
            sudoku[1, 6].Cell.Value = '.';
            sudoku[2, 6].Cell.Value = '.';
            sudoku[3, 6].Cell.Value = '8';
            sudoku[4, 6].Cell.Value = '.';
            sudoku[5, 6].Cell.Value = '.';
            sudoku[6, 6].Cell.Value = '.';
            sudoku[7, 6].Cell.Value = '.';
            sudoku[8, 6].Cell.Value = '.';
            sudoku[0, 7].Cell.Value = '4';
            sudoku[1, 7].Cell.Value = '.';
            sudoku[2, 7].Cell.Value = '2';
            sudoku[3, 7].Cell.Value = '1';
            sudoku[4, 7].Cell.Value = '.';
            sudoku[5, 7].Cell.Value = '.';
            sudoku[6, 7].Cell.Value = '.';
            sudoku[7, 7].Cell.Value = '.';
            sudoku[8, 7].Cell.Value = '.';
            sudoku[0, 8].Cell.Value = '.';
            sudoku[1, 8].Cell.Value = '.';
            sudoku[2, 8].Cell.Value = '.';
            sudoku[3, 8].Cell.Value = '.';
            sudoku[4, 8].Cell.Value = '.';
            sudoku[5, 8].Cell.Value = '.';
            sudoku[6, 8].Cell.Value = '5';
            sudoku[7, 8].Cell.Value = '.';
            sudoku[8, 8].Cell.Value = '4';
            sudoku[0, 0].Cell.ZoneNumber = 0;
            sudoku[1, 0].Cell.ZoneNumber = 0;
            sudoku[2, 0].Cell.ZoneNumber = 0;
            sudoku[3, 0].Cell.ZoneNumber = 1;
            sudoku[4, 0].Cell.ZoneNumber = 1;
            sudoku[5, 0].Cell.ZoneNumber = 1;
            sudoku[6, 0].Cell.ZoneNumber = 2;
            sudoku[7, 0].Cell.ZoneNumber = 2;
            sudoku[8, 0].Cell.ZoneNumber = 2;
            sudoku[0, 1].Cell.ZoneNumber = 0;
            sudoku[1, 1].Cell.ZoneNumber = 0;
            sudoku[2, 1].Cell.ZoneNumber = 0;
            sudoku[3, 1].Cell.ZoneNumber = 1;
            sudoku[4, 1].Cell.ZoneNumber = 1;
            sudoku[5, 1].Cell.ZoneNumber = 1;
            sudoku[6, 1].Cell.ZoneNumber = 2;
            sudoku[7, 1].Cell.ZoneNumber = 2;
            sudoku[8, 1].Cell.ZoneNumber = 2;
            sudoku[0, 2].Cell.ZoneNumber = 0;
            sudoku[1, 2].Cell.ZoneNumber = 0;
            sudoku[2, 2].Cell.ZoneNumber = 0;
            sudoku[3, 2].Cell.ZoneNumber = 1;
            sudoku[4, 2].Cell.ZoneNumber = 1;
            sudoku[5, 2].Cell.ZoneNumber = 1;
            sudoku[6, 2].Cell.ZoneNumber = 2;
            sudoku[7, 2].Cell.ZoneNumber = 2;
            sudoku[8, 2].Cell.ZoneNumber = 2;
            sudoku[0, 3].Cell.ZoneNumber = 3;
            sudoku[1, 3].Cell.ZoneNumber = 3;
            sudoku[2, 3].Cell.ZoneNumber = 3;
            sudoku[3, 3].Cell.ZoneNumber = 4;
            sudoku[4, 3].Cell.ZoneNumber = 4;
            sudoku[5, 3].Cell.ZoneNumber = 4;
            sudoku[6, 3].Cell.ZoneNumber = 5;
            sudoku[7, 3].Cell.ZoneNumber = 5;
            sudoku[8, 3].Cell.ZoneNumber = 5;
            sudoku[0, 4].Cell.ZoneNumber = 3;
            sudoku[1, 4].Cell.ZoneNumber = 3;
            sudoku[2, 4].Cell.ZoneNumber = 3;
            sudoku[3, 4].Cell.ZoneNumber = 4;
            sudoku[4, 4].Cell.ZoneNumber = 4;
            sudoku[5, 4].Cell.ZoneNumber = 4;
            sudoku[6, 4].Cell.ZoneNumber = 5;
            sudoku[7, 4].Cell.ZoneNumber = 5;
            sudoku[8, 4].Cell.ZoneNumber = 5;
            sudoku[0, 5].Cell.ZoneNumber = 3;
            sudoku[1, 5].Cell.ZoneNumber = 3;
            sudoku[2, 5].Cell.ZoneNumber = 3;
            sudoku[3, 5].Cell.ZoneNumber = 4;
            sudoku[4, 5].Cell.ZoneNumber = 4;
            sudoku[5, 5].Cell.ZoneNumber = 4;
            sudoku[6, 5].Cell.ZoneNumber = 5;
            sudoku[7, 5].Cell.ZoneNumber = 5;
            sudoku[8, 5].Cell.ZoneNumber = 5;
            sudoku[0, 6].Cell.ZoneNumber = 6;
            sudoku[1, 6].Cell.ZoneNumber = 6;
            sudoku[2, 6].Cell.ZoneNumber = 6;
            sudoku[3, 6].Cell.ZoneNumber = 7;
            sudoku[4, 6].Cell.ZoneNumber = 7;
            sudoku[5, 6].Cell.ZoneNumber = 7;
            sudoku[6, 6].Cell.ZoneNumber = 8;
            sudoku[7, 6].Cell.ZoneNumber = 8;
            sudoku[8, 6].Cell.ZoneNumber = 8;
            sudoku[0, 7].Cell.ZoneNumber = 6;
            sudoku[1, 7].Cell.ZoneNumber = 6;
            sudoku[2, 7].Cell.ZoneNumber = 6;
            sudoku[3, 7].Cell.ZoneNumber = 7;
            sudoku[4, 7].Cell.ZoneNumber = 7;
            sudoku[5, 7].Cell.ZoneNumber = 7;
            sudoku[6, 7].Cell.ZoneNumber = 8;
            sudoku[7, 7].Cell.ZoneNumber = 8;
            sudoku[8, 7].Cell.ZoneNumber = 8;
            sudoku[0, 8].Cell.ZoneNumber = 6;
            sudoku[1, 8].Cell.ZoneNumber = 6;
            sudoku[2, 8].Cell.ZoneNumber = 6;
            sudoku[3, 8].Cell.ZoneNumber = 7;
            sudoku[4, 8].Cell.ZoneNumber = 7;
            sudoku[5, 8].Cell.ZoneNumber = 7;
            sudoku[6, 8].Cell.ZoneNumber = 8;
            sudoku[7, 8].Cell.ZoneNumber = 8;
            sudoku[8, 8].Cell.ZoneNumber = 8;
            csp.Nodes.Add(sudoku[0, 0]);
            csp.Nodes.Add(sudoku[3, 0]);
            csp.Nodes.Add(sudoku[4, 0]);
            csp.Nodes.Add(sudoku[5, 0]);
            csp.Nodes.Add(sudoku[6, 0]);
            csp.Nodes.Add(sudoku[7, 0]);
            csp.Nodes.Add(sudoku[8, 0]);
            csp.Nodes.Add(sudoku[0, 1]);
            csp.Nodes.Add(sudoku[1, 1]);
            csp.Nodes.Add(sudoku[2, 1]);
            csp.Nodes.Add(sudoku[3, 1]);
            csp.Nodes.Add(sudoku[4, 1]);
            csp.Nodes.Add(sudoku[5, 1]);
            csp.Nodes.Add(sudoku[6, 1]);
            csp.Nodes.Add(sudoku[7, 1]);
            csp.Nodes.Add(sudoku[8, 1]);
            csp.Nodes.Add(sudoku[0, 2]);
            csp.Nodes.Add(sudoku[1, 2]);
            csp.Nodes.Add(sudoku[2, 2]);
            csp.Nodes.Add(sudoku[3, 2]);
            csp.Nodes.Add(sudoku[4, 2]);
            csp.Nodes.Add(sudoku[5, 2]);
            csp.Nodes.Add(sudoku[6, 2]);
            csp.Nodes.Add(sudoku[7, 2]);
            csp.Nodes.Add(sudoku[8, 2]);
            csp.Nodes.Add(sudoku[0, 3]);
            csp.Nodes.Add(sudoku[1, 3]);
            csp.Nodes.Add(sudoku[2, 3]);
            csp.Nodes.Add(sudoku[3, 3]);
            csp.Nodes.Add(sudoku[4, 3]);
            csp.Nodes.Add(sudoku[5, 3]);
            csp.Nodes.Add(sudoku[6, 3]);
            csp.Nodes.Add(sudoku[7, 3]);
            csp.Nodes.Add(sudoku[8, 3]);
            csp.Nodes.Add(sudoku[0, 4]);
            csp.Nodes.Add(sudoku[1, 4]);
            csp.Nodes.Add(sudoku[2, 4]);
            csp.Nodes.Add(sudoku[3, 4]);
            csp.Nodes.Add(sudoku[4, 4]);
            csp.Nodes.Add(sudoku[5, 4]);
            csp.Nodes.Add(sudoku[6, 4]);
            csp.Nodes.Add(sudoku[7, 4]);
            csp.Nodes.Add(sudoku[8, 4]);
            csp.Nodes.Add(sudoku[0, 5]);
            csp.Nodes.Add(sudoku[1, 5]);
            csp.Nodes.Add(sudoku[2, 5]);
            csp.Nodes.Add(sudoku[3, 5]);
            csp.Nodes.Add(sudoku[4, 5]);
            csp.Nodes.Add(sudoku[5, 5]);
            csp.Nodes.Add(sudoku[6, 5]);
            csp.Nodes.Add(sudoku[7, 5]);
            csp.Nodes.Add(sudoku[8, 5]);
            csp.Nodes.Add(sudoku[0, 6]);
            csp.Nodes.Add(sudoku[1, 6]);
            csp.Nodes.Add(sudoku[2, 6]);
            csp.Nodes.Add(sudoku[3, 6]);
            csp.Nodes.Add(sudoku[4, 6]);
            csp.Nodes.Add(sudoku[5, 6]);
            csp.Nodes.Add(sudoku[6, 6]);
            csp.Nodes.Add(sudoku[7, 6]);
            csp.Nodes.Add(sudoku[8, 6]);
            csp.Nodes.Add(sudoku[0, 7]);
            csp.Nodes.Add(sudoku[1, 7]);
            csp.Nodes.Add(sudoku[2, 7]);
            csp.Nodes.Add(sudoku[3, 7]);
            csp.Nodes.Add(sudoku[4, 7]);
            csp.Nodes.Add(sudoku[5, 7]);
            csp.Nodes.Add(sudoku[6, 7]);
            csp.Nodes.Add(sudoku[7, 7]);
            csp.Nodes.Add(sudoku[8, 7]);
            csp.Nodes.Add(sudoku[0, 8]);
            csp.Nodes.Add(sudoku[1, 8]);
            csp.Nodes.Add(sudoku[2, 8]);
            csp.Nodes.Add(sudoku[3, 8]);
            csp.Nodes.Add(sudoku[4, 8]);
            csp.Nodes.Add(sudoku[5, 8]);
            csp.Nodes.Add(sudoku[6, 8]);
            csp.Nodes.Add(sudoku[7, 8]);
            csp.Nodes.Add(sudoku[8, 8]);
            csp.Nodes.Add(sudoku[1, 0]);
            csp.Nodes.Add(sudoku[2, 0]);
            csp.GenerateArcs();

            List<GraphNode> expected = new List<GraphNode>();
            sudoku[0, 0].Cell.Value = '6';
            sudoku[1, 0].Cell.Value = '3';
            sudoku[2, 0].Cell.Value = '1';
            sudoku[3, 0].Cell.Value = '2';
            sudoku[4, 0].Cell.Value = '5';
            sudoku[5, 0].Cell.Value = '8';
            sudoku[6, 0].Cell.Value = '9';
            sudoku[7, 0].Cell.Value = '4';
            sudoku[8, 0].Cell.Value = '7';
            sudoku[0, 1].Cell.Value = '5';
            sudoku[1, 1].Cell.Value = '4';
            sudoku[2, 1].Cell.Value = '7';
            sudoku[3, 1].Cell.Value = '9';
            sudoku[4, 1].Cell.Value = '1';
            sudoku[5, 1].Cell.Value = '3';
            sudoku[6, 1].Cell.Value = '6';
            sudoku[7, 1].Cell.Value = '2';
            sudoku[8, 1].Cell.Value = '8';
            sudoku[0, 2].Cell.Value = '2';
            sudoku[1, 2].Cell.Value = '9';
            sudoku[2, 2].Cell.Value = '8';
            sudoku[3, 2].Cell.Value = '4';
            sudoku[4, 2].Cell.Value = '7';
            sudoku[5, 2].Cell.Value = '6';
            sudoku[6, 2].Cell.Value = '1';
            sudoku[7, 2].Cell.Value = '3';
            sudoku[8, 2].Cell.Value = '5';
            sudoku[0, 3].Cell.Value = '3';
            sudoku[1, 3].Cell.Value = '6';
            sudoku[2, 3].Cell.Value = '4';
            sudoku[3, 3].Cell.Value = '7';
            sudoku[4, 3].Cell.Value = '8';
            sudoku[5, 3].Cell.Value = '9';
            sudoku[6, 3].Cell.Value = '2';
            sudoku[7, 3].Cell.Value = '5';
            sudoku[8, 3].Cell.Value = '1';
            sudoku[0, 4].Cell.Value = '8';
            sudoku[1, 4].Cell.Value = '2';
            sudoku[2, 4].Cell.Value = '5';
            sudoku[3, 4].Cell.Value = '3';
            sudoku[4, 4].Cell.Value = '4';
            sudoku[5, 4].Cell.Value = '1';
            sudoku[6, 4].Cell.Value = '7';
            sudoku[7, 4].Cell.Value = '9';
            sudoku[8, 4].Cell.Value = '6';
            sudoku[0, 5].Cell.Value = '7';
            sudoku[1, 5].Cell.Value = '1';
            sudoku[2, 5].Cell.Value = '9';
            sudoku[3, 5].Cell.Value = '5';
            sudoku[4, 5].Cell.Value = '6';
            sudoku[5, 5].Cell.Value = '2';
            sudoku[6, 5].Cell.Value = '4';
            sudoku[7, 5].Cell.Value = '8';
            sudoku[8, 5].Cell.Value = '3';
            sudoku[0, 6].Cell.Value = '1';
            sudoku[1, 6].Cell.Value = '5';
            sudoku[2, 6].Cell.Value = '6';
            sudoku[3, 6].Cell.Value = '8';
            sudoku[4, 6].Cell.Value = '9';
            sudoku[5, 6].Cell.Value = '4';
            sudoku[6, 6].Cell.Value = '3';
            sudoku[7, 6].Cell.Value = '7';
            sudoku[8, 6].Cell.Value = '2';
            sudoku[0, 7].Cell.Value = '4';
            sudoku[1, 7].Cell.Value = '7';
            sudoku[2, 7].Cell.Value = '2';
            sudoku[3, 7].Cell.Value = '1';
            sudoku[4, 7].Cell.Value = '3';
            sudoku[5, 7].Cell.Value = '5';
            sudoku[6, 7].Cell.Value = '8';
            sudoku[7, 7].Cell.Value = '6';
            sudoku[8, 7].Cell.Value = '9';
            sudoku[0, 8].Cell.Value = '9';
            sudoku[1, 8].Cell.Value = '8';
            sudoku[2, 8].Cell.Value = '3';
            sudoku[3, 8].Cell.Value = '6';
            sudoku[4, 8].Cell.Value = '2';
            sudoku[5, 8].Cell.Value = '7';
            sudoku[6, 8].Cell.Value = '5';
            sudoku[7, 8].Cell.Value = '1';
            sudoku[8, 8].Cell.Value = '4';
            expected.Add(sudoku[0, 0]);
            expected.Add(sudoku[3, 0]);
            expected.Add(sudoku[4, 0]);
            expected.Add(sudoku[5, 0]);
            expected.Add(sudoku[6, 0]);
            expected.Add(sudoku[7, 0]);
            expected.Add(sudoku[8, 0]);
            expected.Add(sudoku[0, 1]);
            expected.Add(sudoku[1, 1]);
            expected.Add(sudoku[2, 1]);
            expected.Add(sudoku[3, 1]);
            expected.Add(sudoku[4, 1]);
            expected.Add(sudoku[5, 1]);
            expected.Add(sudoku[6, 1]);
            expected.Add(sudoku[7, 1]);
            expected.Add(sudoku[8, 1]);
            expected.Add(sudoku[0, 2]);
            expected.Add(sudoku[1, 2]);
            expected.Add(sudoku[2, 2]);
            expected.Add(sudoku[3, 2]);
            expected.Add(sudoku[4, 2]);
            expected.Add(sudoku[5, 2]);
            expected.Add(sudoku[6, 2]);
            expected.Add(sudoku[7, 2]);
            expected.Add(sudoku[8, 2]);
            expected.Add(sudoku[0, 3]);
            expected.Add(sudoku[1, 3]);
            expected.Add(sudoku[2, 3]);
            expected.Add(sudoku[3, 3]);
            expected.Add(sudoku[4, 3]);
            expected.Add(sudoku[5, 3]);
            expected.Add(sudoku[6, 3]);
            expected.Add(sudoku[7, 3]);
            expected.Add(sudoku[8, 3]);
            expected.Add(sudoku[0, 4]);
            expected.Add(sudoku[1, 4]);
            expected.Add(sudoku[2, 4]);
            expected.Add(sudoku[3, 4]);
            expected.Add(sudoku[4, 4]);
            expected.Add(sudoku[5, 4]);
            expected.Add(sudoku[6, 4]);
            expected.Add(sudoku[7, 4]);
            expected.Add(sudoku[8, 4]);
            expected.Add(sudoku[0, 5]);
            expected.Add(sudoku[1, 5]);
            expected.Add(sudoku[2, 5]);
            expected.Add(sudoku[3, 5]);
            expected.Add(sudoku[4, 5]);
            expected.Add(sudoku[5, 5]);
            expected.Add(sudoku[6, 5]);
            expected.Add(sudoku[7, 5]);
            expected.Add(sudoku[8, 5]);
            expected.Add(sudoku[0, 6]);
            expected.Add(sudoku[1, 6]);
            expected.Add(sudoku[2, 6]);
            expected.Add(sudoku[3, 6]);
            expected.Add(sudoku[4, 6]);
            expected.Add(sudoku[5, 6]);
            expected.Add(sudoku[6, 6]);
            expected.Add(sudoku[7, 6]);
            expected.Add(sudoku[8, 6]);
            expected.Add(sudoku[0, 7]);
            expected.Add(sudoku[1, 7]);
            expected.Add(sudoku[2, 7]);
            expected.Add(sudoku[3, 7]);
            expected.Add(sudoku[4, 7]);
            expected.Add(sudoku[5, 7]);
            expected.Add(sudoku[6, 7]);
            expected.Add(sudoku[7, 7]);
            expected.Add(sudoku[8, 7]);
            expected.Add(sudoku[0, 8]);
            expected.Add(sudoku[1, 8]);
            expected.Add(sudoku[2, 8]);
            expected.Add(sudoku[3, 8]);
            expected.Add(sudoku[4, 8]);
            expected.Add(sudoku[5, 8]);
            expected.Add(sudoku[6, 8]);
            expected.Add(sudoku[7, 8]);
            expected.Add(sudoku[8, 8]);
            expected.Add(sudoku[1, 0]);
            expected.Add(sudoku[2, 0]);

            // Act
            var isResolved = csp.BacktrackingSearch();

            // Assert
            Assert.IsTrue(isResolved, "Isn't resolved.");
            CollectionAssert.AreEqual(csp.Nodes, expected, "Wrong values.");
        }

        [TestMethod]
        public void T_RecursiveBacktracking_5x5_Easy()
        {
            // Arrange
            CSP csp = new CSP();
            PrivateObject obj = new PrivateObject(csp);

            GraphNode[,] sudoku = new GraphNode[,] {
                {
                    new GraphNode(new Cell(0, 0)),
                    new GraphNode(new Cell(0, 1)),
                    new GraphNode(new Cell(0, 2)),
                    new GraphNode(new Cell(0, 3)),
                },
                {
                    new GraphNode(new Cell(1, 0)),
                    new GraphNode(new Cell(1, 1)),
                    new GraphNode(new Cell(1, 2)),
                    new GraphNode(new Cell(1, 3)),
                },
                {
                    new GraphNode(new Cell(2, 0)),
                    new GraphNode(new Cell(2, 1)),
                    new GraphNode(new Cell(2, 2)),
                    new GraphNode(new Cell(2, 3)),
                },
                {
                    new GraphNode(new Cell(3, 0)),
                    new GraphNode(new Cell(3, 1)),
                    new GraphNode(new Cell(3, 2)),
                    new GraphNode(new Cell(3, 3)),
                }
            };
            sudoku[0, 0].Cell.Value = '1';
            sudoku[1, 0].Cell.Value = '.';
            sudoku[2, 0].Cell.Value = '3';
            sudoku[3, 0].Cell.Value = '4';
            sudoku[0, 1].Cell.Value = '.';
            sudoku[1, 1].Cell.Value = '3';
            sudoku[2, 1].Cell.Value = '2';
            sudoku[3, 1].Cell.Value = '1';
            sudoku[0, 2].Cell.Value = '2';
            sudoku[1, 2].Cell.Value = '1';
            sudoku[2, 2].Cell.Value = '4';
            sudoku[3, 2].Cell.Value = '.';
            sudoku[0, 3].Cell.Value = '3';
            sudoku[1, 3].Cell.Value = '4';
            sudoku[2, 3].Cell.Value = '.';
            sudoku[3, 3].Cell.Value = '2';
            sudoku[0, 0].Cell.ZoneNumber = 0;
            sudoku[1, 0].Cell.ZoneNumber = 0;
            sudoku[2, 0].Cell.ZoneNumber = 0;
            sudoku[3, 0].Cell.ZoneNumber = 0;
            sudoku[0, 1].Cell.ZoneNumber = 1;
            sudoku[1, 1].Cell.ZoneNumber = 1;
            sudoku[2, 1].Cell.ZoneNumber = 1;
            sudoku[3, 1].Cell.ZoneNumber = 1;
            sudoku[0, 2].Cell.ZoneNumber = 2;
            sudoku[1, 2].Cell.ZoneNumber = 2;
            sudoku[2, 2].Cell.ZoneNumber = 2;
            sudoku[3, 2].Cell.ZoneNumber = 2;
            sudoku[0, 3].Cell.ZoneNumber = 3;
            sudoku[1, 3].Cell.ZoneNumber = 3;
            sudoku[2, 3].Cell.ZoneNumber = 3;
            sudoku[3, 3].Cell.ZoneNumber = 3;
            csp.Nodes.Add(sudoku[0, 0]);
            csp.Nodes.Add(sudoku[1, 0]);
            csp.Nodes.Add(sudoku[2, 0]);
            csp.Nodes.Add(sudoku[3, 0]);
            csp.Nodes.Add(sudoku[0, 1]);
            csp.Nodes.Add(sudoku[1, 1]);
            csp.Nodes.Add(sudoku[2, 1]);
            csp.Nodes.Add(sudoku[3, 1]);
            csp.Nodes.Add(sudoku[0, 2]);
            csp.Nodes.Add(sudoku[1, 2]);
            csp.Nodes.Add(sudoku[2, 2]);
            csp.Nodes.Add(sudoku[3, 2]);
            csp.Nodes.Add(sudoku[0, 3]);
            csp.Nodes.Add(sudoku[1, 3]);
            csp.Nodes.Add(sudoku[2, 3]);
            csp.Nodes.Add(sudoku[3, 3]);
            csp.GenerateArcs();

            List<GraphNode> expected = new List<GraphNode>();
            sudoku[0, 0].Cell.Value = '1';
            sudoku[1, 0].Cell.Value = '2';
            sudoku[2, 0].Cell.Value = '3';
            sudoku[3, 0].Cell.Value = '4';
            sudoku[0, 1].Cell.Value = '4';
            sudoku[1, 1].Cell.Value = '3';
            sudoku[2, 1].Cell.Value = '2';
            sudoku[3, 1].Cell.Value = '1';
            sudoku[0, 2].Cell.Value = '2';
            sudoku[1, 2].Cell.Value = '1';
            sudoku[2, 2].Cell.Value = '4';
            sudoku[3, 2].Cell.Value = '3';
            sudoku[0, 3].Cell.Value = '3';
            sudoku[1, 3].Cell.Value = '4';
            sudoku[2, 3].Cell.Value = '1';
            sudoku[3, 3].Cell.Value = '2';
            expected.Add(sudoku[0, 0]);
            expected.Add(sudoku[1, 0]);
            expected.Add(sudoku[2, 0]);
            expected.Add(sudoku[3, 0]);
            expected.Add(sudoku[0, 1]);
            expected.Add(sudoku[1, 1]);
            expected.Add(sudoku[2, 1]);
            expected.Add(sudoku[3, 1]);
            expected.Add(sudoku[0, 2]);
            expected.Add(sudoku[1, 2]);
            expected.Add(sudoku[2, 2]);
            expected.Add(sudoku[3, 2]);
            expected.Add(sudoku[0, 3]);
            expected.Add(sudoku[1, 3]);
            expected.Add(sudoku[2, 3]);
            expected.Add(sudoku[3, 3]);

            // Act
            var isResolved = csp.BacktrackingSearch();

            // Assert
            Assert.IsTrue(isResolved, "Isn't resolved.");
            CollectionAssert.AreEqual(csp.Nodes, expected, "Wrong values.");
        }

        [TestMethod]
        public void T_RecursiveBacktracking_9x9_Unsolvable()
        {
            // Arrange
            CSP csp = new CSP();
            PrivateObject obj = new PrivateObject(csp);

            GraphNode[,] sudoku = new GraphNode[,] {
                {
                    new GraphNode(new Cell(0, 0)),
                    new GraphNode(new Cell(0, 1)),
                    new GraphNode(new Cell(0, 2)),
                    new GraphNode(new Cell(0, 3)),
                    new GraphNode(new Cell(0, 4)),
                    new GraphNode(new Cell(0, 5)),
                    new GraphNode(new Cell(0, 6)),
                    new GraphNode(new Cell(0, 7)),
                    new GraphNode(new Cell(0, 8)),
                },
                {
                    new GraphNode(new Cell(1, 0)),
                    new GraphNode(new Cell(1, 1)),
                    new GraphNode(new Cell(1, 2)),
                    new GraphNode(new Cell(1, 3)),
                    new GraphNode(new Cell(1, 4)),
                    new GraphNode(new Cell(1, 5)),
                    new GraphNode(new Cell(1, 6)),
                    new GraphNode(new Cell(1, 7)),
                    new GraphNode(new Cell(1, 8)),
                },
                {
                    new GraphNode(new Cell(2, 0)),
                    new GraphNode(new Cell(2, 1)),
                    new GraphNode(new Cell(2, 2)),
                    new GraphNode(new Cell(2, 3)),
                    new GraphNode(new Cell(2, 4)),
                    new GraphNode(new Cell(2, 5)),
                    new GraphNode(new Cell(2, 6)),
                    new GraphNode(new Cell(2, 7)),
                    new GraphNode(new Cell(2, 8)),
                },
                {
                    new GraphNode(new Cell(3, 0)),
                    new GraphNode(new Cell(3, 1)),
                    new GraphNode(new Cell(3, 2)),
                    new GraphNode(new Cell(3, 3)),
                    new GraphNode(new Cell(3, 4)),
                    new GraphNode(new Cell(3, 5)),
                    new GraphNode(new Cell(3, 6)),
                    new GraphNode(new Cell(3, 7)),
                    new GraphNode(new Cell(3, 8)),
                },
                {
                    new GraphNode(new Cell(4, 0)),
                    new GraphNode(new Cell(4, 1)),
                    new GraphNode(new Cell(4, 2)),
                    new GraphNode(new Cell(4, 3)),
                    new GraphNode(new Cell(4, 4)),
                    new GraphNode(new Cell(4, 5)),
                    new GraphNode(new Cell(4, 6)),
                    new GraphNode(new Cell(4, 7)),
                    new GraphNode(new Cell(4, 8)),
                },
                {
                    new GraphNode(new Cell(5, 0)),
                    new GraphNode(new Cell(5, 1)),
                    new GraphNode(new Cell(5, 2)),
                    new GraphNode(new Cell(5, 3)),
                    new GraphNode(new Cell(5, 4)),
                    new GraphNode(new Cell(5, 5)),
                    new GraphNode(new Cell(5, 6)),
                    new GraphNode(new Cell(5, 7)),
                    new GraphNode(new Cell(5, 8)),
                },
                {
                    new GraphNode(new Cell(6, 0)),
                    new GraphNode(new Cell(6, 1)),
                    new GraphNode(new Cell(6, 2)),
                    new GraphNode(new Cell(6, 3)),
                    new GraphNode(new Cell(6, 4)),
                    new GraphNode(new Cell(6, 5)),
                    new GraphNode(new Cell(6, 6)),
                    new GraphNode(new Cell(6, 7)),
                    new GraphNode(new Cell(6, 8)),
                },
                {
                    new GraphNode(new Cell(7, 0)),
                    new GraphNode(new Cell(7, 1)),
                    new GraphNode(new Cell(7, 2)),
                    new GraphNode(new Cell(7, 3)),
                    new GraphNode(new Cell(7, 4)),
                    new GraphNode(new Cell(7, 5)),
                    new GraphNode(new Cell(7, 6)),
                    new GraphNode(new Cell(7, 7)),
                    new GraphNode(new Cell(7, 8)),
                },
                {
                    new GraphNode(new Cell(8, 0)),
                    new GraphNode(new Cell(8, 1)),
                    new GraphNode(new Cell(8, 2)),
                    new GraphNode(new Cell(8, 3)),
                    new GraphNode(new Cell(8, 4)),
                    new GraphNode(new Cell(8, 5)),
                    new GraphNode(new Cell(8, 6)),
                    new GraphNode(new Cell(8, 7)),
                    new GraphNode(new Cell(8, 8)),
                }
            };
            sudoku[0, 0].Cell.Value = '1';
            sudoku[1, 0].Cell.Value = '2';
            sudoku[2, 0].Cell.Value = '3';
            sudoku[3, 0].Cell.Value = '.';
            sudoku[4, 0].Cell.Value = '.';
            sudoku[5, 0].Cell.Value = '.';
            sudoku[6, 0].Cell.Value = '7';
            sudoku[7, 0].Cell.Value = '4';
            sudoku[8, 0].Cell.Value = '5';
            sudoku[0, 1].Cell.Value = '4';
            sudoku[1, 1].Cell.Value = '5';
            sudoku[2, 1].Cell.Value = '6';
            sudoku[3, 1].Cell.Value = '.';
            sudoku[4, 1].Cell.Value = '.';
            sudoku[5, 1].Cell.Value = '.';
            sudoku[6, 1].Cell.Value = '8';
            sudoku[7, 1].Cell.Value = '2';
            sudoku[8, 1].Cell.Value = '3';
            sudoku[0, 2].Cell.Value = '7';
            sudoku[1, 2].Cell.Value = '8';
            sudoku[2, 2].Cell.Value = '9';
            sudoku[3, 2].Cell.Value = '.';
            sudoku[4, 2].Cell.Value = '.';
            sudoku[5, 2].Cell.Value = '.';
            sudoku[6, 2].Cell.Value = '6';
            sudoku[7, 2].Cell.Value = '1';
            sudoku[8, 2].Cell.Value = '.';
            sudoku[0, 3].Cell.Value = '.';
            sudoku[1, 3].Cell.Value = '.';
            sudoku[2, 3].Cell.Value = '.';
            sudoku[3, 3].Cell.Value = '.';
            sudoku[4, 3].Cell.Value = '.';
            sudoku[5, 3].Cell.Value = '.';
            sudoku[6, 3].Cell.Value = '.';
            sudoku[7, 3].Cell.Value = '.';
            sudoku[8, 3].Cell.Value = '.';
            sudoku[0, 4].Cell.Value = '.';
            sudoku[1, 4].Cell.Value = '.';
            sudoku[2, 4].Cell.Value = '.';
            sudoku[3, 4].Cell.Value = '.';
            sudoku[4, 4].Cell.Value = '.';
            sudoku[5, 4].Cell.Value = '.';
            sudoku[6, 4].Cell.Value = '.';
            sudoku[7, 4].Cell.Value = '.';
            sudoku[8, 4].Cell.Value = '.';
            sudoku[0, 5].Cell.Value = '.';
            sudoku[1, 5].Cell.Value = '.';
            sudoku[2, 5].Cell.Value = '.';
            sudoku[3, 5].Cell.Value = '.';
            sudoku[4, 5].Cell.Value = '.';
            sudoku[5, 5].Cell.Value = '.';
            sudoku[6, 5].Cell.Value = '.';
            sudoku[7, 5].Cell.Value = '.';
            sudoku[8, 5].Cell.Value = '.';
            sudoku[0, 6].Cell.Value = '.';
            sudoku[1, 6].Cell.Value = '.';
            sudoku[2, 6].Cell.Value = '.';
            sudoku[3, 6].Cell.Value = '.';
            sudoku[4, 6].Cell.Value = '.';
            sudoku[5, 6].Cell.Value = '.';
            sudoku[6, 6].Cell.Value = '.';
            sudoku[7, 6].Cell.Value = '.';
            sudoku[8, 6].Cell.Value = '.';
            sudoku[0, 7].Cell.Value = '.';
            sudoku[1, 7].Cell.Value = '.';
            sudoku[2, 7].Cell.Value = '.';
            sudoku[3, 7].Cell.Value = '.';
            sudoku[4, 7].Cell.Value = '.';
            sudoku[5, 7].Cell.Value = '.';
            sudoku[6, 7].Cell.Value = '.';
            sudoku[7, 7].Cell.Value = '.';
            sudoku[8, 7].Cell.Value = '.';
            sudoku[0, 8].Cell.Value = '.';
            sudoku[1, 8].Cell.Value = '.';
            sudoku[2, 8].Cell.Value = '.';
            sudoku[3, 8].Cell.Value = '.';
            sudoku[4, 8].Cell.Value = '.';
            sudoku[5, 8].Cell.Value = '.';
            sudoku[6, 8].Cell.Value = '.';
            sudoku[7, 8].Cell.Value = '.';
            sudoku[8, 8].Cell.Value = '.';
            sudoku[0, 0].Cell.ZoneNumber = 0;
            sudoku[1, 0].Cell.ZoneNumber = 0;
            sudoku[2, 0].Cell.ZoneNumber = 0;
            sudoku[3, 0].Cell.ZoneNumber = 1;
            sudoku[4, 0].Cell.ZoneNumber = 1;
            sudoku[5, 0].Cell.ZoneNumber = 1;
            sudoku[6, 0].Cell.ZoneNumber = 2;
            sudoku[7, 0].Cell.ZoneNumber = 2;
            sudoku[8, 0].Cell.ZoneNumber = 2;
            sudoku[0, 1].Cell.ZoneNumber = 0;
            sudoku[1, 1].Cell.ZoneNumber = 0;
            sudoku[2, 1].Cell.ZoneNumber = 0;
            sudoku[3, 1].Cell.ZoneNumber = 1;
            sudoku[4, 1].Cell.ZoneNumber = 1;
            sudoku[5, 1].Cell.ZoneNumber = 1;
            sudoku[6, 1].Cell.ZoneNumber = 2;
            sudoku[7, 1].Cell.ZoneNumber = 2;
            sudoku[8, 1].Cell.ZoneNumber = 2;
            sudoku[0, 2].Cell.ZoneNumber = 0;
            sudoku[1, 2].Cell.ZoneNumber = 0;
            sudoku[2, 2].Cell.ZoneNumber = 0;
            sudoku[3, 2].Cell.ZoneNumber = 1;
            sudoku[4, 2].Cell.ZoneNumber = 1;
            sudoku[5, 2].Cell.ZoneNumber = 1;
            sudoku[6, 2].Cell.ZoneNumber = 2;
            sudoku[7, 2].Cell.ZoneNumber = 2;
            sudoku[8, 2].Cell.ZoneNumber = 2;
            sudoku[0, 3].Cell.ZoneNumber = 3;
            sudoku[1, 3].Cell.ZoneNumber = 3;
            sudoku[2, 3].Cell.ZoneNumber = 3;
            sudoku[3, 3].Cell.ZoneNumber = 4;
            sudoku[4, 3].Cell.ZoneNumber = 4;
            sudoku[5, 3].Cell.ZoneNumber = 4;
            sudoku[6, 3].Cell.ZoneNumber = 5;
            sudoku[7, 3].Cell.ZoneNumber = 5;
            sudoku[8, 3].Cell.ZoneNumber = 5;
            sudoku[0, 4].Cell.ZoneNumber = 3;
            sudoku[1, 4].Cell.ZoneNumber = 3;
            sudoku[2, 4].Cell.ZoneNumber = 3;
            sudoku[3, 4].Cell.ZoneNumber = 4;
            sudoku[4, 4].Cell.ZoneNumber = 4;
            sudoku[5, 4].Cell.ZoneNumber = 4;
            sudoku[6, 4].Cell.ZoneNumber = 5;
            sudoku[7, 4].Cell.ZoneNumber = 5;
            sudoku[8, 4].Cell.ZoneNumber = 5;
            sudoku[0, 5].Cell.ZoneNumber = 3;
            sudoku[1, 5].Cell.ZoneNumber = 3;
            sudoku[2, 5].Cell.ZoneNumber = 3;
            sudoku[3, 5].Cell.ZoneNumber = 4;
            sudoku[4, 5].Cell.ZoneNumber = 4;
            sudoku[5, 5].Cell.ZoneNumber = 4;
            sudoku[6, 5].Cell.ZoneNumber = 5;
            sudoku[7, 5].Cell.ZoneNumber = 5;
            sudoku[8, 5].Cell.ZoneNumber = 5;
            sudoku[0, 6].Cell.ZoneNumber = 6;
            sudoku[1, 6].Cell.ZoneNumber = 6;
            sudoku[2, 6].Cell.ZoneNumber = 6;
            sudoku[3, 6].Cell.ZoneNumber = 7;
            sudoku[4, 6].Cell.ZoneNumber = 7;
            sudoku[5, 6].Cell.ZoneNumber = 7;
            sudoku[6, 6].Cell.ZoneNumber = 8;
            sudoku[7, 6].Cell.ZoneNumber = 8;
            sudoku[8, 6].Cell.ZoneNumber = 8;
            sudoku[0, 7].Cell.ZoneNumber = 6;
            sudoku[1, 7].Cell.ZoneNumber = 6;
            sudoku[2, 7].Cell.ZoneNumber = 6;
            sudoku[3, 7].Cell.ZoneNumber = 7;
            sudoku[4, 7].Cell.ZoneNumber = 7;
            sudoku[5, 7].Cell.ZoneNumber = 7;
            sudoku[6, 7].Cell.ZoneNumber = 8;
            sudoku[7, 7].Cell.ZoneNumber = 8;
            sudoku[8, 7].Cell.ZoneNumber = 8;
            sudoku[0, 8].Cell.ZoneNumber = 6;
            sudoku[1, 8].Cell.ZoneNumber = 6;
            sudoku[2, 8].Cell.ZoneNumber = 6;
            sudoku[3, 8].Cell.ZoneNumber = 7;
            sudoku[4, 8].Cell.ZoneNumber = 7;
            sudoku[5, 8].Cell.ZoneNumber = 7;
            sudoku[6, 8].Cell.ZoneNumber = 8;
            sudoku[7, 8].Cell.ZoneNumber = 8;
            sudoku[8, 8].Cell.ZoneNumber = 8;
            csp.Nodes.Add(sudoku[0, 0]);
            csp.Nodes.Add(sudoku[3, 0]);
            csp.Nodes.Add(sudoku[4, 0]);
            csp.Nodes.Add(sudoku[5, 0]);
            csp.Nodes.Add(sudoku[6, 0]);
            csp.Nodes.Add(sudoku[7, 0]);
            csp.Nodes.Add(sudoku[8, 0]);
            csp.Nodes.Add(sudoku[0, 1]);
            csp.Nodes.Add(sudoku[1, 1]);
            csp.Nodes.Add(sudoku[2, 1]);
            csp.Nodes.Add(sudoku[3, 1]);
            csp.Nodes.Add(sudoku[4, 1]);
            csp.Nodes.Add(sudoku[5, 1]);
            csp.Nodes.Add(sudoku[6, 1]);
            csp.Nodes.Add(sudoku[7, 1]);
            csp.Nodes.Add(sudoku[8, 1]);
            csp.Nodes.Add(sudoku[0, 2]);
            csp.Nodes.Add(sudoku[1, 2]);
            csp.Nodes.Add(sudoku[2, 2]);
            csp.Nodes.Add(sudoku[3, 2]);
            csp.Nodes.Add(sudoku[4, 2]);
            csp.Nodes.Add(sudoku[5, 2]);
            csp.Nodes.Add(sudoku[6, 2]);
            csp.Nodes.Add(sudoku[7, 2]);
            csp.Nodes.Add(sudoku[8, 2]);
            csp.Nodes.Add(sudoku[0, 3]);
            csp.Nodes.Add(sudoku[1, 3]);
            csp.Nodes.Add(sudoku[2, 3]);
            csp.Nodes.Add(sudoku[3, 3]);
            csp.Nodes.Add(sudoku[4, 3]);
            csp.Nodes.Add(sudoku[5, 3]);
            csp.Nodes.Add(sudoku[6, 3]);
            csp.Nodes.Add(sudoku[7, 3]);
            csp.Nodes.Add(sudoku[8, 3]);
            csp.Nodes.Add(sudoku[0, 4]);
            csp.Nodes.Add(sudoku[1, 4]);
            csp.Nodes.Add(sudoku[2, 4]);
            csp.Nodes.Add(sudoku[3, 4]);
            csp.Nodes.Add(sudoku[4, 4]);
            csp.Nodes.Add(sudoku[5, 4]);
            csp.Nodes.Add(sudoku[6, 4]);
            csp.Nodes.Add(sudoku[7, 4]);
            csp.Nodes.Add(sudoku[8, 4]);
            csp.Nodes.Add(sudoku[0, 5]);
            csp.Nodes.Add(sudoku[1, 5]);
            csp.Nodes.Add(sudoku[2, 5]);
            csp.Nodes.Add(sudoku[3, 5]);
            csp.Nodes.Add(sudoku[4, 5]);
            csp.Nodes.Add(sudoku[5, 5]);
            csp.Nodes.Add(sudoku[6, 5]);
            csp.Nodes.Add(sudoku[7, 5]);
            csp.Nodes.Add(sudoku[8, 5]);
            csp.Nodes.Add(sudoku[0, 6]);
            csp.Nodes.Add(sudoku[1, 6]);
            csp.Nodes.Add(sudoku[2, 6]);
            csp.Nodes.Add(sudoku[3, 6]);
            csp.Nodes.Add(sudoku[4, 6]);
            csp.Nodes.Add(sudoku[5, 6]);
            csp.Nodes.Add(sudoku[6, 6]);
            csp.Nodes.Add(sudoku[7, 6]);
            csp.Nodes.Add(sudoku[8, 6]);
            csp.Nodes.Add(sudoku[0, 7]);
            csp.Nodes.Add(sudoku[1, 7]);
            csp.Nodes.Add(sudoku[2, 7]);
            csp.Nodes.Add(sudoku[3, 7]);
            csp.Nodes.Add(sudoku[4, 7]);
            csp.Nodes.Add(sudoku[5, 7]);
            csp.Nodes.Add(sudoku[6, 7]);
            csp.Nodes.Add(sudoku[7, 7]);
            csp.Nodes.Add(sudoku[8, 7]);
            csp.Nodes.Add(sudoku[0, 8]);
            csp.Nodes.Add(sudoku[1, 8]);
            csp.Nodes.Add(sudoku[2, 8]);
            csp.Nodes.Add(sudoku[3, 8]);
            csp.Nodes.Add(sudoku[4, 8]);
            csp.Nodes.Add(sudoku[5, 8]);
            csp.Nodes.Add(sudoku[6, 8]);
            csp.Nodes.Add(sudoku[7, 8]);
            csp.Nodes.Add(sudoku[8, 8]);
            csp.Nodes.Add(sudoku[1, 0]);
            csp.Nodes.Add(sudoku[2, 0]);
            csp.GenerateArcs();

             // Act
             var isResolved = csp.BacktrackingSearch();

            // Assert
            Assert.IsFalse(isResolved);
        }

        /*
        [TestMethod]
        public void T_RecursiveBacktracking_20x20()
        {
            // Arrange
            string fileContent = System.IO.File.ReadAllText("..\\..\\..\\..\\Sudokus\\20x20\\true_sudoku.ss");
            Form1 form = new Form1();
            PrivateObject obj = new PrivateObject(form);
            var loaded = obj.Invoke("DecodeGrid_Regular", fileContent);
            CSP csp = (CSP)obj.GetProperty("Csp");

            // Act
            var isResolved = csp.BacktrackingSearch();

            // Assert
            Assert.IsInstanceOfType(loaded, typeof(bool), "Wrong type returned by Form1.DecodeGrid_Regular().");
            Assert.IsTrue((bool)loaded, "Grid cannot be loaded from file.");

            Assert.IsTrue(isResolved, "Sudoku cannot be solved");
        }

        [TestMethod]
        public void T_RecursiveBacktracking_25x25()
        {
            // Arrange
            string fileContent = System.IO.File.ReadAllText("..\\..\\..\\..\\Sudokus\\25x25\\25x25_n3181.ss");
            Form1 form = new Form1();
            PrivateObject obj = new PrivateObject(form);
            var loaded = obj.Invoke("DecodeGrid_Regular", fileContent);
            CSP csp = (CSP)obj.GetProperty("Csp");

            string fileContentExpected = System.IO.File.ReadAllText("..\\..\\..\\..\\Sudokus\\25x25\\25x25_n3181_expected.ss");
            Form1 formExpected = new Form1();
            PrivateObject objExpected = new PrivateObject(form);
            var loadedExpected = obj.Invoke("DecodeGrid_Regular", fileContent);
            CSP cspExpected = (CSP)obj.GetProperty("Csp");

            // Act
            var isResolved = csp.BacktrackingSearch();

            // Assert
            Assert.IsInstanceOfType(loaded, typeof(bool), "Wrong type returned by Form1.DecodeGrid_Regular().");
            Assert.IsTrue((bool)loaded, "Grid cannot be loaded from file.");

            Assert.IsTrue(isResolved, "Sudoku cannot be solved");
            CollectionAssert.AreEqual(csp.Nodes, cspExpected.Nodes, "Wrong values");
        }*/
    }
}
