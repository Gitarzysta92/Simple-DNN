using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple_DNN.Network;
using Moq;

namespace Simple_DNN_Tests.NetworkTests
{
  [TestClass]
  public class NetworkFactoryTest
  {

    private Mock<ILayerFactory> layerFactoryMock;
    private ILayerFactory layerFactory;

    public NetworkFactoryTest()
    {
      this.layerFactoryMock = new Mock<ILayerFactory>();
      this.layerFactoryMock.Setup(factory => factory.Create(It.IsAny<int>(), It.IsAny<int>())).Returns(new Mock<ILayer>().Object);
      this.layerFactory = this.layerFactoryMock.Object;
    }

    #region Test for right return value
    [TestMethod]
    public void NetworkFactory__ForCreateMethodCall_ShouldReturnNetworkInstance()
    {
      // Arrange
      INetworkFactory networkFactory = new NetworkFactory(this.layerFactory);

      // Act
      INetwork newNetwork = networkFactory.Create();

      // Assert
      Assert.AreEqual(typeof(Network), newNetwork.GetType());
    }
    #endregion
  }
}
