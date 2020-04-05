using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple_DNN.Network;
using Moq;

namespace Simple_DNN_Tests.NetworkTests
{
  [TestClass]
  public class NetworkTests
  {
    private Mock<ILayerFactory> layerFactoryMock;
    private ILayerFactory layerFactory;

    private Mock<INeuronFactory> neuronFactoryMock;
    private INeuronFactory neuronFactory;

    [TestInitialize]
    public void TestInitialize()
    {
      // Prepare Layer factory
      this.layerFactoryMock = new Mock<ILayerFactory>();
      this.layerFactoryMock.Setup(layer => layer.Create(It.IsAny<int>(), It.IsAny<int>())).Returns(new Mock<ILayer>().Object);

      this.layerFactory = this.layerFactoryMock.Object;

      // Prepare Neuron factory
      this.neuronFactoryMock = new Mock<INeuronFactory>();
      this.neuronFactoryMock.Setup(neuron => neuron.Create(It.IsAny<int>(), It.IsAny<int>())).Returns(new Mock<INeuron>().Object);

      this.neuronFactory = this.neuronFactoryMock.Object;
    }

    [TestCleanup]
    public void TestClean() { }

    #region Test for network inputs
    [TestMethod]
    public void Network__ForThreeLayersWithTenNeuronsEach_ShouldHaveTenInputs()
    {
      // Arrange
      int[] layers = { 10 };
      INetwork network = new Network(this.layerFactory);

      // Act
      network.Initialize(layers);

      // Assert
      Assert.AreEqual(layers[0], network.InputLayerLength);
    }
    #endregion

    #region Test for network outputs
    [TestMethod]
    public void Network__ForThreeLayersWithTenNeuronsEach_ShouldHaveTenOutputs()
    {
      // Arrange
      int[] layers = { 10 };
      INetwork network = new Network(this.layerFactory);

      // Act
      network.Initialize(layers);

      // Assert
      Assert.AreEqual(layers[layers.Length - 1], network.InputLayerLength);
    }
    #endregion

    #region Test for empty config
    [TestMethod]
    public void Network__ForZeroLayers_ShouldHaveZeroInputs()
    {
      // Arrange
      int[] layers = { };
      INetwork network = new Network(this.layerFactory);

      // Act
      network.Initialize(layers);

      // Assert
      Assert.AreEqual(0, network.InputLayerLength);
    }
    #endregion

    #region Test for number of layers
    [TestMethod]
    public void Network__ForGivenThreeLayers_ShouldThreeLayers()
    {
      // Arrange
      int[] layers = { 1, 1, 1 };
      INetwork network = new Network(this.layerFactory);

      // Act
      network.Initialize(layers);

      // Assert
      Assert.AreEqual(layers.Length, network.LayersNumber);
    }
    #endregion

    #region Test for input layer iterations
    [TestMethod]
    public void Network__ForGivenInputLayerLength_ShouldIterateEqualTimes()
    {
      // Arrange
      int[] layers = { 10 };
      int layerId = 0;
      int iterations = 0;

      var layerMock = this.GetLayerMockWithForEach(layerId, layers[layerId]);
      this.layerFactoryMock.Setup(layer => layer.Create(It.IsAny<int>(), It.IsAny<int>())).Returns(layerMock.Object);

      INetwork network = new Network(this.layerFactory);
      network.Initialize(layers);

      // Act
      network.Inputs((neuron, index) => iterations++);

      // Assert
      Assert.AreEqual(layers[0], iterations);
    }
    #endregion

    #region Test for output layer iterations
    [TestMethod]
    public void Network__ForGivenOutputLayerLength_ShouldIterateEqualTimes()
    {
      // Arrange
      int[] layers = { 10 };
      int layerId = layers.Length - 1;
      int iterations = 0;

      var layerMock = this.GetLayerMockWithForEach(layerId, layers[layerId]);
      this.layerFactoryMock.Setup(layer => layer.Create(It.IsAny<int>(), It.IsAny<int>())).Returns(layerMock.Object);

      INetwork network = new Network(this.layerFactory);
      network.Initialize(layers);

      // Act
      network.Outputs<int>((neuron) => iterations++);

      // Assert
      Assert.AreEqual(layers[layers.Length - 1], iterations);
    }
    #endregion

    #region Test for iterations over all neurons
    [TestMethod]
    public void Network__ForGivenLayersSetup_ShouldIterateOverAllNeurons()
    { 
      // Arrange
      int[] layers = { 10, 10, 10 };
      int numberOfNeurons = layers.Sum();
      int iterations = 0;

      this.layerFactoryMock.Setup(layer => layer.Create(It.IsAny<int>(), It.IsAny<int>()))
        .Returns((int neuronsNumber, int layerId) => this.GetLayerMockWithForEach(layerId, layers[layerId]).Object);

      INetwork network = new Network(this.layerFactory);
      network.Initialize(layers);

      // Act
      network.ForEach((neuron) => iterations++);

      // Assert
      Assert.AreEqual(numberOfNeurons, iterations);
    }
    #endregion

    #region Test for ForEach method prev layer accessibility
    [TestMethod]
    public void Network__WhileIteratingOverLayer_ShouldProvideReferenceToPreviousLayer()
    {
      // Arrange
      int[] layers = { 10, 10 };

      int firstLayerId = 0;
      ILayer firstLayer = null;
      ILayer firstLayerReference = null;

      this.layerFactoryMock.Setup(layer => layer.Create(It.IsAny<int>(), It.IsAny<int>()))
        .Returns((int neuronsNumber, int layerId) =>
        {
          var layer = this.GetLayerMockWithForEach(layerId, layers[layerId]).Object;
          if (layerId == firstLayerId) firstLayer = layer;
          return layer;
        });

      INetwork network = new Network(this.layerFactory);
      network.Initialize(layers);

      // Act
      network.ForEach((neuron, layer, layerId, neuronId) => 
      {
        if (layerId == firstLayerId + 1) firstLayerReference = layer[firstLayerId];
      });


      // Assert
      Assert.AreSame(firstLayer, firstLayerReference);
    }
    #endregion

    #region Test for 

    #endregion

    #region Helpers

    // Create layer with method that returns new neurons
    private Mock<ILayer> GetLayerMockWithForEach(int layerId, int numberOfNeurons)
    {
      Mock<ILayer> layerMock = new Mock<ILayer>();
      layerMock.Setup(layer => layer.ForEach(It.IsAny<Action<INeuron, int>>()))
        .Callback((Action<INeuron, int> iterator) => {
          foreach (int i in Enumerable.Range(layerId, numberOfNeurons))
            iterator(this.neuronFactory.Create(layerId, i), i);
        });
      return layerMock;
    }


    #endregion

  }
}
