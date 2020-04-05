using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_DNN.Network
{

  #region Neural network layer interface

  public interface ILayer
  {
    void ForEach(Action<INeuron, int> iterator);

  }

  #endregion

  #region Neural network layer class

  public class Layer : ILayer
  {
    private int layerId;

    private INeuron[] neurons;


    public Layer(Func<int,INeuron> getNeuron, int numberOfNeurons, int layerId) 
    {

      this.layerId = layerId;
      this.neurons = new INeuron[numberOfNeurons];

      ForEach((neuron, index) => neuron = getNeuron(index));

    }

    public void ForEach(Action<INeuron, int> iterator)
    {
      for (int i = 0; i < this.neurons.Length; i++)
      {
        iterator(this.neurons[i], i);
      }
    }
  }

  #endregion

  #region Neural network layer factory interface

  public interface ILayerFactory
  {
    ILayer Create(int numberOfNeurons, int layerId);
  }

  #endregion

  #region Neural network layer class

  public class LayerFactory : ILayerFactory
  {

    private INeuronFactory neuronFactory;

    public LayerFactory(INeuronFactory neuronFactory)
    {
      this.neuronFactory = neuronFactory;
    }

    public ILayer Create(int numberOfNeurons, int layerId)
    {
      return new Layer(
        (int neuronId) => this.neuronFactory.Create(layerId, neuronId),
        numberOfNeurons,
        layerId
      );
    }

  }

  #endregion


}
