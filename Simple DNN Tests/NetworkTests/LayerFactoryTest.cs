using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple_DNN.Network;
using Moq;

namespace Simple_DNN_Tests.NetworkTests
{

  [TestClass]
  public class LayerFactoryTest
  {
    private INeuronFactory neuronFactory;
    private INeuron neuron;

    public LayerFactoryTest()
    {
     
      this.neuron = new Mock<INeuron>().Object;
      var neuronFactoryMock = new Mock<INeuronFactory>();
      neuronFactoryMock.Setup(factory => factory.Create(It.IsAny<int>(), It.IsAny<int>())).Returns(this.neuron);
      this.neuronFactory = neuronFactoryMock.Object;
    }

    #region Test for right return value
    [TestMethod]
    public void LayerFactory__ForCreateMethodCall_ShouldReturnLayerInstance()
    {
      // Arrange
      int numberOfNeurons = 10;
      int layerId = 0;
      ILayerFactory layerFactory = new LayerFactory(this.neuronFactory);

      // Act
      var newLayer = layerFactory.Create(numberOfNeurons, layerId);

      // Assert
      Assert.AreEqual(typeof(Layer), newLayer.GetType());
    }
    #endregion
  }
}
