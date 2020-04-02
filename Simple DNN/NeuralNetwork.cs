using Simple_DNN.Neuron;
using Simple_DNN.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_DNN
{
  class NeuralNetwork
  {
    static void Main(string[] args)
    {
      var rand = new Random();
      var sygmoidNeuronFactory = new SygmoidNeuronFactory(rand);
      var sygmoidNeuronService = new SygmoidNeuronService(sygmoidNeuronFactory);

      var layerFactory = new LayerFactory(sygmoidNeuronFactory);
      var networkFactory = new NetworkFactory(layerFactory);
      var networkService = new NetworkService(networkFactory);

      var layers = new[] {10, 10, 10};


      networkService.InitializeNetwork(layers);


      float[] inputData = { };

      var result = networkService.Evaluate(inputData);


      Console.WriteLine("Begin deep net IO demo");
      Console.ReadLine();
    }
  }
}
