using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple_DNN.Network;
using Moq;

namespace Simple_DNN_Tests.NetworkTests
{
  [TestClass]
  public class NeuronFactoryTest
  {

    private INeuron neuron;

    public NeuronFactoryTest()
    {
      this.neuron = new Mock<INeuron>().Object;
    }


    #region Test for right return value
    [TestMethod]
    public void NeuronFactory__ForCreateMethodCall_ShouldReturnNeuronInstance()
    {
      // Arrange
      int layerId = 0;
      int neuronId = 1;
      INeuronFactory layerFactory = new NeuronFactory();

      // Act
      var newNeuron = layerFactory.Create(layerId, neuronId);

      // Assert
      Assert.AreEqual(typeof(Neuron), newNeuron.GetType());
    }
    #endregion
  }
}
