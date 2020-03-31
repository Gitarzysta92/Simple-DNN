using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_DNN.Network
{ 
  public interface ILayer
  {

  
  }


  public class Layer : ILayer
  {
    private int layerid;

    private INeuron[] neurons;


    public Layer(Func<int,INeuron> getNeuron, int numberOfNeurons, int layerId) 
    {

      this.layerid = layerId;
      this.neurons = new INeuron[numberOfNeurons];

      for (int i = 0; i < this.neurons.Length; i++)
      {
        this.neurons[i] = getNeuron(i);
      }
    }
  }


  public interface ILayerFactory
  {
    ILayer Create(int numberOfNeurons);
  }


  public class LayerFactory
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
}
