using Simple_DNN.Neurons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Simple_DNN
{
  class NetworkService
  {

    private INetwork network;

    private readonly ISygmoidNeuronsService sygmoidNeuronsService;

    public NetworkService(
      ISygmoidNeuronsService sygmoidNeuronsService
    )
    {
      this.sygmoidNeuronsService = sygmoidNeuronsService;
    }


    public void InitializeNetwork(int[] layersConfig, neuron)
    {

      this.network = new Network(layersConfig);

      this.network.ForEach((neuron, network, layerIndex, rowIndex) => 
      {
        string id = $"{layerIndex}{rowIndex}";
        neuron = this.sygmoidNeuronsService.InitializeNeuron(id);

        var prevLayer = network[layerIndex - 1];
        if (prevLayer != null)
          neuron.Inputs = prevLayer.Select(n => n.Output).ToArray();

      });
    }



    public void setInputData(float[] inputData)
    {
      this.neuralNetwork.Inputs((neuron, index) => neuron.Inputs = new float[] { inputData[index] });
    }



    public float[] Evaluate(float[] inputData)
    {
      
     int numberOfLayers = this.neuralNetwork.LayersNumber;
     int inputLayerLength = this.neuralNetwork.InputLayerLength;

      if (inputData.Length != inputLayerLength)
        throw new Exception();

      

      this.neuralNetwork.ForEach(neuron => neuron.Evaluate());

      return this.neuralNetwork.Outputs<float>(neuron => neuron.Output);
    }

  }
}

