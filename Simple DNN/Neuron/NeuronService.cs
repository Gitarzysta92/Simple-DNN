using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_DNN.Neuron
{
  interface INeuronService
  {
    INeuron InitializeNeuron(string id);
  }

  class NeuronService : INeuronService
  {

    private readonly INeuronFactory neuronFactory;

    public NeuronService(
      INeuronFactory neuronsFactory
    ) {
      this.neuronFactory = neuronsFactory;
    }
   

    public INeuron InitializeNeuron(string id)
    {
      return this.neuronFactory.Create(id);
    }


  }
}
