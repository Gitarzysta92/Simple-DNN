using Simple_DNN.Neurons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Simple_DNN
{
  interface Neuron
  {
    float[] Inputs { get; set; }

    float Output { get; set; }

    void Evaluate();

  }

  class NetworkService<T> where T : Neuron
  {

    private Network<T> network;

    private readonly ISygmoidNeuronsService sygmoidNeuronsService;

    public NetworkService(
      ISygmoidNeuronsService sygmoidNeuronsService
    )
    {
      this.sygmoidNeuronsService = sygmoidNeuronsService;
    }


    public void InitializeNetwork(int[] layersConfig)
    {

      this.network = new Network<T>(layersConfig);

      this.network.ForEach((neuron, network, layerIndex, rowIndex) => 
      {
        string id = $"{layerIndex}{rowIndex}";
        neuron = this.sygmoidNeuronsService.InitializeNeuron<T>(id);

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

