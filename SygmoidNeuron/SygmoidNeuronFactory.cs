using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_DNN.Neurons
{
  interface ISygmoidNeuronFactory
  {
    SygmoidNeuron Create(string id);
    SygmoidNeuron Create(string id, float weight, int baias);
  }


  class SygmoidNeuronFactory : ISygmoidNeuronFactory
  {

    private readonly Random randomNumber;

    public SygmoidNeuronFactory(Random random) 
    {
      this.randomNumber = random;
    }



    public SygmoidNeuron Create(string id)
    {
      float weight = (float) this.randomNumber.NextDouble();
      int baias = this.randomNumber.Next(10);

      return new SygmoidNeuron(id, weight, baias);
    }

    public SygmoidNeuron Create(string id, float weight, int baias)
    {
      return new SygmoidNeuron(id, weight, baias);
    }


  }
}
