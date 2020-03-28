using Simple_DNN.Neurons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Simple_DNN
{

  public interface INetwork
  {

  }


  public class Network : INetwork
  {
    public int InputLayerLength { get; }
    public int OutputLayerLength { get; }
    public int LayersNumber { get; }
  

    // Neurons containers created from jagged array
    private List<Object>[] network;

    private int inputLayerLength;

    private int outputLayerLength;
    
    private int layersNumber;


    public Network(int[] layersConfig)
    {

      this.inputLayerLength = layersConfig[0];
      this.outputLayerLength = layersConfig[layersConfig.Length - 1];
      this.layersNumber = layersConfig.Length;

    }

    // iterate over all inputs elements
    public void Inputs(Action<Object, int> iterator)
    {
      ForEach((neuron, network, layerIndex, rowIndex) =>
      {
        bool isNotInputLayer = !(layerIndex == 0);
        if (isNotInputLayer) return;

        iterator(neuron, rowIndex);
      });
    }

    // iterate over all outputs elements
    public T[] Outputs<T>(Func<Object, T> iterator)
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
    public void ForEach(Action<Object, List<Object>[], int, int> iterator)
    {
      for (int layer = 0; layer < this.network.Length; layer++)
      {
        int row = 0;
        this.network[layer].ForEach(neuron => iterator(neuron, this.network, layer, row++));
      }
    }

    public void ForEach(Action<Object> iterator)
    {
      for (int layer = 0; layer < this.network.Length; layer++)
        this.network[layer].ForEach(neuron => iterator(neuron));
    }

  }


}