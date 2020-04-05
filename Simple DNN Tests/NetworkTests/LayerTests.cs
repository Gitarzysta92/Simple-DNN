using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Simple_DNN.Network;


   #region Additional test attributes
    //
    // You can use the following additional attributes as you write your tests:
    //
    // Use ClassInitialize to run code before running the first test in the class
    // [ClassInitialize()]
    // public static void MyClassInitialize(TestContext testContext) { }
    //
    // Use ClassCleanup to run code after all tests in a class have run
    // [ClassCleanup()]
    // public static void MyClassCleanup() { }
    //
    // Use TestInitialize to run code before running each test 
    // [TestInitialize()]
    // public void MyTestInitialize() { }
    //
    // Use TestCleanup to run code after each test has run
    // [TestCleanup()]
    // public void MyTestCleanup() { }
    //
    #endregion

namespace Simple_DNN_Tests.NetworkTests
{

  [TestClass]
  public class LayerTests
  {
    private INeuronFactory neuronFactory;
    private INeuron neuron;

    public LayerTests()
    {
      this.neuron = new Mock<INeuron>().Object;

      var neuronFactoryMock = new Mock<INeuronFactory>();
      neuronFactoryMock.Setup(factory => factory.Create(It.IsAny<int>(), It.IsAny<int>())).Returns(this.neuron);
      this.neuronFactory = neuronFactoryMock.Object;
    }

 
    #region Test for correct initialization
    [TestMethod]
    public void Layer__ForGivenNumberOfNeurons_ShouldHaveThatManyNeurons()
    {
      // Arrange
      int layerId = 0;
      int numberOfNeurons = 10;
      Func<int, INeuron> neuronCreate = (int neuronId) => new Mock<INeuron>().Object;
      ILayer layer = new Layer(neuronCreate, numberOfNeurons, layerId);
      PrivateObject privateLayer = new PrivateObject(layer);

      // Act
      int layerNeurons = ((INeuron[]) privateLayer.GetFieldOrProperty("neurons")).Length;

      // Assert
      Assert.AreEqual(numberOfNeurons, layerNeurons);
    }
    #endregion

    #region Test for ForEach number of iterations
    [TestMethod]
    public void Layer__ForGivenNumberOfNeurons_ShouldIterateThatManyTimes()
    {
      // Arrange
      int layerId = 0;
      int numberOfNeurons = 10;
      Func<int, INeuron> neuronCreate = (int neuronId) => this.neuronFactory.Create(layerId, neuronId);
      ILayer layer = new Layer(neuronCreate, numberOfNeurons, layerId);
      int iterations = 0;

      // Act
      layer.ForEach((neuron, index) => iterations++);

      // Assert
      Assert.AreEqual(numberOfNeurons, iterations);
    }
    #endregion

  }
}
