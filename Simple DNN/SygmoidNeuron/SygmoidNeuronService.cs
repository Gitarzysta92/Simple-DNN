using Simple_DNN.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_DNN.Neuron
{
  interface ISygmoidNeuronService
  {
 
  }

  class SygmoidNeuronService : ISygmoidNeuronService
  {

    private readonly INeuronFactory neuronFactory;

    public SygmoidNeuronService(
      INeuronFactory neuronsFactory
    ) {
      this.neuronFactory = neuronsFactory;
    }
   



  }
}
