using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_DNN.Neuron
{
  interface INeuronFactory
  {
    INeuron Create(string id);
    INeuron Create(string id, float weight, int baias);
  }


  class NeuronFactory : INeuronFactory
  {

    private readonly Random randomNumber;

    public NeuronFactory(Random random) 
    {
      this.randomNumber = random;
    }



    public INeuron Create(string id)
    {
      float weight = (float) this.randomNumber.NextDouble();
      int baias = this.randomNumber.Next(10);

      return new SygmoidNeuron(id, weight, baias);
    }

    public INeuron Create(string id, float weight, int baias)
    {
      return new SygmoidNeuron(id, weight, baias);
    }


  }
}
