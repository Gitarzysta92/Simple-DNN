using Simple_DNN.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_DNN.Neuron
{

  #region Sygmoid neuron factory interface
  interface ISygmoidNeuronFactory : INeuronFactory { }
  #endregion

  #region Sygmoid neuron factory class
  class SygmoidNeuronFactory : ISygmoidNeuronFactory
  {

    private readonly Random randomNumber;

    public SygmoidNeuronFactory(Random random)
    {
      this.randomNumber = random;
    }



    public INeuron Create(int layerId, int neuronId)
    {
      float weight = (float)this.randomNumber.NextDouble();
      int baias = this.randomNumber.Next(10);

      string id = $"{layerId}{neuronId}";

      return new SygmoidNeuron(id, weight, baias);
    }

    public INeuron Create(string id, float weight, int baias)
    {
      return new SygmoidNeuron(id, weight, baias);
    }
  }
  #endregion

  #region Sygmoid neuron interface
  interface ISygmoidNeuron : INeuron { };
  #endregion

  #region Sygmoid neuron class
  // The name comes from math sygmoid function that provides smooth activation point for neuron.
  // It is very similar to Rosneblatt perceptron but instead of giving boolean output
  // it returns any value between 0 and 1.
  public class SygmoidNeuron : ISygmoidNeuron
  {
    public float[] Inputs { get; set; }

    public float Output { get; set; }

    // Neuron identifier
    private string id;

    // Importance of the SygmoidNeuron
    private float weight;

    // Threshold value that decides of neuron activation 
    private int baias;

    // Neuron output value
    private float value;

    public SygmoidNeuron(string id, float weight, int baias)
    {
      this.id = id;
      this.weight = weight;
      this.baias = baias;
    }


    public void Evaluate()
    {

    }

  }



  // neuronValue = Sigmoid function( weight * someValue + baias)
  // baias = -threshold
  #endregion

}
