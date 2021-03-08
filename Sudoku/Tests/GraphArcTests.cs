using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku_Graphic;

namespace Tests
{
    [TestClass]
    public class GraphArcTests
    {
        [TestMethod]
        public void T_GraphArc()
        {
            // Arrange
            GraphNode graphNodeBgn = new GraphNode(new Cell());
            GraphNode graphNodeEnd = new GraphNode(new Cell());

            // Act
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);
            PrivateObject po = new PrivateObject(graphArc);

            // Assert
            Assert.AreEqual((GraphNode)po.GetField("node1"), graphNodeBgn, "Wrong first node.");
            Assert.AreEqual((GraphNode)po.GetField("node2"), graphNodeEnd, "Wrong second node.");
        }

        [TestMethod]
        public void T_IsConsistant_True_Node1Dot()
        {
            // Arrange
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            cell.Value = '.';
            cell2.Value = '1';
            GraphNode graphNodeBgn = new GraphNode(cell);
            GraphNode graphNodeEnd = new GraphNode(cell2);
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);

            // Act
            bool isConsistant = graphArc.IsConsistant();

            // Assert
            Assert.IsTrue(isConsistant);
        }

        [TestMethod]
        public void T_IsConsistant_True_Node2Dot()
        {
            // Arrange
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            cell.Value = 'A';
            cell2.Value = '.';
            GraphNode graphNodeBgn = new GraphNode(cell);
            GraphNode graphNodeEnd = new GraphNode(cell2);
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);

            // Act
            bool isConsistant = graphArc.IsConsistant();

            // Assert
            Assert.IsTrue(isConsistant);
        }

        [TestMethod]
        public void T_IsConsistant_True_DifferentValue()
        {
            // Arrange
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            cell.Value = '2';
            cell2.Value = '1';
            GraphNode graphNodeBgn = new GraphNode(cell);
            GraphNode graphNodeEnd = new GraphNode(cell2);
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);

            // Act
            bool isConsistant = graphArc.IsConsistant();

            // Assert
            Assert.IsTrue(isConsistant);
        }

        [TestMethod]
        public void T_IsConsistant_False()
        {
            // Arrange
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            cell.Value = '1';
            cell2.Value = '1';
            GraphNode graphNodeBgn = new GraphNode(cell);
            GraphNode graphNodeEnd = new GraphNode(cell2);
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);

            // Act
            bool isConsistant = graphArc.IsConsistant();

            // Assert
            Assert.IsFalse(isConsistant);
        }

        [TestMethod]
        public void T_GetFirstNode()
        {
            // Arrange
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            cell.Value = '1';
            cell2.Value = '1';
            GraphNode graphNodeBgn = new GraphNode(cell);
            GraphNode graphNodeEnd = new GraphNode(cell2);
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);

            // Act
            GraphNode getFirstNode = graphArc.GetFirstNode();

            // Assert
            Assert.AreEqual(getFirstNode, graphNodeBgn);
        }

        [TestMethod]
        public void T_IsCellIn_False()
        {
            // Arrange
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            Cell cell3 = new Cell();
            cell.Value = '1';
            cell2.Value = 'A';
            cell3.Value = '8';
            GraphNode graphNodeBgn = new GraphNode(cell);
            GraphNode graphNodeEnd = new GraphNode(cell2);
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);

            // Act
            bool isCellIn = graphArc.IsCellIn(cell3);

            // Assert
            Assert.IsFalse(isCellIn);
        }

        [TestMethod]
        public void T_IsCellIn_True_Node1()
        {
            // Arrange
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            cell.Value = '1';
            cell2.Value = 'A';
            GraphNode graphNodeBgn = new GraphNode(cell);
            GraphNode graphNodeEnd = new GraphNode(cell2);
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);

            // Act
            bool isCellIn = graphArc.IsCellIn(cell);

            // Assert
            Assert.IsTrue(isCellIn);
        }

        [TestMethod]
        public void T_IsCellIn_True_Node2()
        {
            // Arrange
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            cell.Value = '1';
            cell2.Value = 'A';
            GraphNode graphNodeBgn = new GraphNode(cell);
            GraphNode graphNodeEnd = new GraphNode(cell2);
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);

            // Act
            bool isCellIn = graphArc.IsCellIn(cell2);

            // Assert
            Assert.IsTrue(isCellIn);
        }

        [TestMethod]
        public void T_IsDuplicata_True()
        {
            // Arrange
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            cell.Value = '1';
            cell2.Value = 'A';
            GraphNode graphNodeBgn = new GraphNode(cell);
            GraphNode graphNodeEnd = new GraphNode(cell2);
            GraphNode graphNodeBgn1 = new GraphNode(cell);
            GraphNode graphNodeEnd1 = new GraphNode(cell2);
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);
            GraphArc graphArc2 = new GraphArc(graphNodeBgn1, graphNodeEnd1);

            // Act
            bool isDuplicata = graphArc.IsDuplicata(graphArc2);

            // Assert
            Assert.IsTrue(isDuplicata);
        }

        [TestMethod]
        public void T_IsDuplicata_True_Reverse()
        {
            // Arrange
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            cell.Value = '1';
            cell2.Value = 'A';
            GraphNode graphNodeBgn = new GraphNode(cell);
            GraphNode graphNodeEnd = new GraphNode(cell2);
            GraphNode graphNodeBgn1 = new GraphNode(cell);
            GraphNode graphNodeEnd1 = new GraphNode(cell2);
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);
            GraphArc graphArc2 = new GraphArc(graphNodeEnd1, graphNodeBgn1);

            // Act
            bool isDuplicata = graphArc.IsDuplicata(graphArc2);

            // Assert
            Assert.IsTrue(isDuplicata);
        }

        [TestMethod]
        public void T_IsDuplicata_False_Node1()
        {
            // Arrange
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            Cell cell3 = new Cell();
            cell.Value = '1';
            cell2.Value = 'A';
            cell3.Value = '8';
            GraphNode graphNodeBgn = new GraphNode(cell);
            GraphNode graphNodeEnd = new GraphNode(cell2);
            GraphNode graphNodeBgn1 = new GraphNode(cell3);
            GraphNode graphNodeEnd1 = new GraphNode(cell2);
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);
            GraphArc graphArc2 = new GraphArc(graphNodeBgn1, graphNodeEnd1);

            // Act
            bool isDuplicata = graphArc.IsDuplicata(graphArc2);

            // Assert
            Assert.IsFalse(isDuplicata);
        }

        [TestMethod]
        public void T_IsDuplicata_False_Node2()
        {
            // Arrange
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            Cell cell3 = new Cell();
            cell.Value = '1';
            cell2.Value = 'A';
            cell3.Value = '8';
            GraphNode graphNodeBgn = new GraphNode(cell);
            GraphNode graphNodeEnd = new GraphNode(cell3);
            GraphNode graphNodeBgn1 = new GraphNode(cell);
            GraphNode graphNodeEnd1 = new GraphNode(cell2);
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);
            GraphArc graphArc2 = new GraphArc(graphNodeBgn1, graphNodeEnd1);

            // Act
            bool isDuplicata = graphArc.IsDuplicata(graphArc2);

            // Assert
            Assert.IsFalse(isDuplicata);
        }

        [TestMethod]
        public void T_GetOtherCellValue_Node1()
        {
            // Arrange
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            cell.Value = '1';
            cell2.Value = 'A';
            GraphNode graphNodeBgn = new GraphNode(cell);
            GraphNode graphNodeEnd = new GraphNode(cell2);
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);

            // Act
            char value = graphArc.GetOtherCellValue(cell2);

            // Assert
            Assert.AreEqual(value, '1');
        }

        [TestMethod]
        public void T_GetOtherCellValue_Node2()
        {
            // Arrange
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            cell.Value = '1';
            cell2.Value = 'A';
            GraphNode graphNodeBgn = new GraphNode(cell);
            GraphNode graphNodeEnd = new GraphNode(cell2);
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);

            // Act
            char value = graphArc.GetOtherCellValue(cell);

            // Assert
            Assert.AreEqual(value, 'A');
        }

        [TestMethod]
        public void T_GetOtherCellValue_InexistantCell()
        {
            // Arrange
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            Cell cell3 = new Cell();
            cell.Value = '1';
            cell2.Value = 'A';
            GraphNode graphNodeBgn = new GraphNode(cell);
            GraphNode graphNodeEnd = new GraphNode(cell2);
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);

            // Act
            char value = graphArc.GetOtherCellValue(cell3);

            // Assert
            Assert.AreEqual(value, ' ');
        }

        [TestMethod]
        public void T_GetOtherNode_Node1()
        {
            // Arrange
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            cell.Value = '1';
            cell2.Value = 'A';
            GraphNode graphNodeBgn = new GraphNode(cell);
            GraphNode graphNodeEnd = new GraphNode(cell2);
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);

            // Act
            GraphNode otherNode = graphArc.GetOtherNode(graphNodeEnd);

            // Assert
            Assert.AreEqual(otherNode, graphNodeBgn);
        }

        [TestMethod]
        public void T_GetOtherNode_Node2()
        {
            // Arrange
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            cell.Value = '1';
            cell2.Value = 'A';
            GraphNode graphNodeBgn = new GraphNode(cell);
            GraphNode graphNodeEnd = new GraphNode(cell2);
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);

            // Act
            GraphNode otherNode = graphArc.GetOtherNode(graphNodeBgn);

            // Assert
            Assert.AreEqual(otherNode, graphNodeEnd);
        }

        [TestMethod]
        public void T_GetOtherNode_InexistantCell()
        {
            // Arrange
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            Cell cell3 = new Cell();
            cell.Value = '1';
            cell2.Value = 'A';
            GraphNode graphNodeBgn = new GraphNode(cell);
            GraphNode graphNodeEnd = new GraphNode(cell2);
            GraphNode graphNode = new GraphNode(cell3);
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);

            // Act
            GraphNode otherNode = graphArc.GetOtherNode(graphNode);

            // Assert
            Assert.IsNull(otherNode);
        }

        [TestMethod]
        public void T_Equals_True()
        {
            // Arrange
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            cell.Value = '1';
            cell2.Value = 'A';
            GraphNode graphNodeBgn = new GraphNode(cell);
            GraphNode graphNodeEnd = new GraphNode(cell2);
            GraphNode graphNodeBgn2 = new GraphNode(cell);
            GraphNode graphNodeEnd2 = new GraphNode(cell2);
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);
            GraphArc graphArc2 = new GraphArc(graphNodeBgn2, graphNodeEnd2);

            // Act
            bool isEqual = graphArc.Equals(graphArc2);

            // Assert
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void T_Equals_False_Node1()
        {
            // Arrange
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            Cell cell3 = new Cell();
            cell.Value = '1';
            cell2.Value = 'A';
            cell3.Value = '8';
            GraphNode graphNodeBgn = new GraphNode(cell);
            GraphNode graphNodeEnd = new GraphNode(cell2);
            GraphNode graphNodeBgn2 = new GraphNode(cell3);
            GraphNode graphNodeEnd2 = new GraphNode(cell2);
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);
            GraphArc graphArc2 = new GraphArc(graphNodeBgn2, graphNodeEnd2);

            // Act
            bool isEqual = graphArc.Equals(graphArc2);

            // Assert
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void T_Equals_False_Node2()
        {
            // Arrange
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            Cell cell3 = new Cell();
            cell.Value = '1';
            cell2.Value = 'A';
            cell3.Value = '8';
            GraphNode graphNodeBgn = new GraphNode(cell);
            GraphNode graphNodeEnd = new GraphNode(cell3);
            GraphNode graphNodeBgn2 = new GraphNode(cell);
            GraphNode graphNodeEnd2 = new GraphNode(cell2);
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);
            GraphArc graphArc2 = new GraphArc(graphNodeBgn2, graphNodeEnd2);

            // Act
            bool isEqual = graphArc.Equals(graphArc2);

            // Assert
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void T_Equals_WithObject_False_Null()
        {
            // Arrange
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            cell.Value = '1';
            cell2.Value = 'A';
            GraphNode graphNodeBgn = new GraphNode(cell);
            GraphNode graphNodeEnd = new GraphNode(cell2);
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);

            // Act
            bool isEqual = graphArc.Equals(null);

            // Assert
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void T_Equals_WithObject_True_SameReference()
        {
            // Arrange
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            cell.Value = '1';
            cell2.Value = 'A';
            GraphNode graphNodeBgn = new GraphNode(cell);
            GraphNode graphNodeEnd = new GraphNode(cell2);
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);

            // Act
            bool isEqual = graphArc.Equals((Object)graphArc);

            // Assert
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void T_Equals_WithObject_False_DifferentTypes()
        {
            // Arrange
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            cell.Value = '1';
            cell2.Value = 'A';
            GraphNode graphNodeBgn = new GraphNode(cell);
            GraphNode graphNodeEnd = new GraphNode(cell2);
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);

            // Act
            bool isEqual = graphArc.Equals(graphNodeBgn);

            // Assert
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void T_Equals_WithObject_True_DifferentReference()
        {
            // Arrange
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            cell.Value = '1';
            cell2.Value = 'A';
            GraphNode graphNodeBgn = new GraphNode(cell);
            GraphNode graphNodeEnd = new GraphNode(cell2);
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);
            Object graphArc2 = new GraphArc(graphNodeBgn, graphNodeEnd);

            // Act
            bool isEqual = graphArc.Equals(graphArc2);

            // Assert
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void T_Equals_GetReverseArc()
        {
            // Arrange
            Cell cell = new Cell();
            Cell cell2 = new Cell();
            cell.Value = '1';
            cell2.Value = 'A';
            GraphNode graphNodeBgn = new GraphNode(cell);
            GraphNode graphNodeEnd = new GraphNode(cell2);
            GraphArc graphArc = new GraphArc(graphNodeBgn, graphNodeEnd);
            GraphArc graphArc2 = new GraphArc(graphNodeEnd, graphNodeBgn);

            // Act
            GraphArc graphArcReverse = graphArc.GetReverseArc();

            // Assert
            Assert.AreEqual(graphArcReverse, graphArc2);
        }
    }
}
