using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple_DNN.Network;
using Simple_DNN.Neuron;
using Moq;

namespace Simple_DNN_Tests
{
  [TestClass]
  public class NetworkTests
  {
    [TestMethod]
    public void Network__ForGivenNumberOfLayersAndElements_ShouldIterateOverAllElements()
    {
      int[] layers = { 10, 10, 10 };
      int excpectedIterations = layers.ToList().Sum();
      int iterations = 0;


      Network<INeuron> network = new Network<INeuron>(layers);

      network.ForEach(neuron => iterations++);

      Assert.AreEqual(excpectedIterations, iterations);
    }


    //public void Network__ForGivenNumberOfLayersAndElements_ShouldIterateOverInputLayerElementsOnly()
    //{

    //}
  }
}
