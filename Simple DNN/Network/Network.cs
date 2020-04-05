﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Simple_DNN.Network
{

  #region Network factory interface
  public interface INetworkFactory
  {
    INetwork Create();
  }
  #endregion

  #region Network factory
  public class NetworkFactory : INetworkFactory
  {

    private ILayerFactory layerFactory;

    public NetworkFactory(ILayerFactory layerFactory)
    {
      this.layerFactory = layerFactory;
    }

    public INetwork Create()
    {
      return new Network(this.layerFactory);
    }
  }
  #endregion

  #region Network interface
  public interface INetwork
  {
    int InputLayerLength { get; }
    int OutputLayerLength { get; }
    int LayersNumber { get; }


    void Initialize(int[] layersConfig);

    void Inputs(Action<INeuron, int> iterator);

    T[] Outputs<T>(Func<INeuron, T> iterator);

    void ForEach(Action<INeuron, ILayer[], int, int> iterator);

    void ForEach(Action<INeuron> iterator);
  }
  #endregion

  #region Network class
  public class Network : INetwork
  {
    public int InputLayerLength => this.inputLayerLength;
    public int OutputLayerLength => this.outputLayerLength;
    public int LayersNumber => this.layersNumber;


    // Neurons containers created from jagged array
    private ILayer[] network;

    private int inputLayerLength;

    private int outputLayerLength;

    private int layersNumber;


    private ILayerFactory layerFactory;


    public Network(ILayerFactory layerFactory)
    {
      this.layerFactory = layerFactory;

    }

    public void Initialize(int[] layersConfig)
    {
      if (layersConfig.Length == 0) return;

      this.inputLayerLength = layersConfig[0];
      this.outputLayerLength = layersConfig[layersConfig.Length - 1];
      this.layersNumber = layersConfig.Length;


      this.network = new ILayer[this.layersNumber];

      for (int layer = 0; layer < this.network.Length; layer++)
      {
        this.network[layer] = this.layerFactory.Create(layersConfig[layer], layer);
      }
    }


    // iterate over all inputs elements
    public void Inputs(Action<INeuron, int> iterator)
    {
      ForEach((neuron, network, layerIndex, rowIndex) =>
      {
        bool isNotInputLayer = !(layerIndex == 0);
        if (isNotInputLayer) return;

        iterator(neuron, rowIndex);
      });
    }

    // iterate over all outputs elements
    public T[] Outputs<T>(Func<INeuron, T> iterator)
    {
      T[] outputs = new T[this.outputLayerLength];

      ForEach((neuron, network, layerIndex, rowIndex) =>
      {
        bool isNotInputLayer = !(layerIndex == 0);
        if (isNotInputLayer) return;

        outputs[layerIndex] = (iterator(neuron));
      });

      return outputs;
    }

    // iterate over all neurons in the network
    public void ForEach(Action<INeuron, ILayer[], int, int> iterator)
    {
      for (int layer = 0; layer < this.network.Length; layer++)
      {
        int row = 0;
        this.network[layer].ForEach((neuron, index) => iterator(neuron, this.network, layer, row++));
      }
    }

    public void ForEach(Action<INeuron> iterator)
    {
      for (int layer = 0; layer < this.network.Length; layer++)
        this.network[layer].ForEach((neuron, index) => iterator(neuron));
    }

  }
  #endregion

}