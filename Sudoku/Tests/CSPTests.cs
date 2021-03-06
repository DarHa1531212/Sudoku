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
    }
}
