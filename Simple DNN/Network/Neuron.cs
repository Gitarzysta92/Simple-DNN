using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_DNN.Network
{
  public interface INeuron
  {
    float[] Inputs { get; set; }

    float Output { get; set; }

    void Evaluate();

  }

  public class Neuron : INeuron
  {
    public int Id { get; set; }


    public float[] Inputs { get; set; }

    public float Output { get; set; }

    public Neuron(int id)
    {
      Id = id;
    }
    public void Evaluate() { }
  }




  public interface INeuronFactory
  {
    INeuron Create(int layerId, int neuronId);
  }

  public class NeuronFactory : INeuronFactory
  {
    public NeuronFactory() { }

    public INeuron Create(int layerid, int neuronId)
    {

      return new Neuron(neuronId);
    }
  }

}
