using Simple_DNN.Neuron;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Simple_DNN.Network
{
 

  class NetworkService
  {

    private Network<INeuron> network;

    private readonly INeuronService neuronService;

    public NetworkService(
      INeuronService neuronsService
    )
    {
      this.neuronService = neuronsService;
    }


    public void InitializeNetwork(int[] layersConfig)
    {

      this.network = new Network<INeuron>(layersConfig);

      this.network.ForEach((neuron, network, layerIndex, rowIndex) => 
      {
        string id = $"{layerIndex}{rowIndex}";
        neuron = this.neuronService.InitializeNeuron(id);

        var prevLayer = network[layerIndex - 1];
        if (prevLayer != null)
          neuron.Inputs = prevLayer.Select(n => n.Output).ToArray();

      });
    }



    public void setInputData(float[] inputData)
    {
      this.network.Inputs((neuron, index) => neuron.Inputs = new float[] { inputData[index] });
    }



    public float[] Evaluate(float[] inputData)
    {
      
     int numberOfLayers = this.network.LayersNumber;
     int inputLayerLength = this.network.InputLayerLength;

      if (inputData.Length != inputLayerLength)
        throw new Exception();

      

      this.network.ForEach(neuron => neuron.Evaluate());

      return this.network.Outputs<float>(neuron => neuron.Output);
    }

  }
}

