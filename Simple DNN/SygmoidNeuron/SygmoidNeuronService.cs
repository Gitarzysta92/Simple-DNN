﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_DNN.Neurons
{
  interface ISygmoidNeuronsService
  {
    SygmoidNeuron InitializeNeuron<T>(string id);
  }

  class SygmoidNeuronService : ISygmoidNeuronsService
  {

    private readonly ISygmoidNeuronFactory sygmoidNeuronFactory;

    public SygmoidNeuronService(
      ISygmoidNeuronFactory sygmoidNeuronsFactory
    ) {
      this.sygmoidNeuronFactory = sygmoidNeuronsFactory;
    }
   

    public SygmoidNeuron InitializeNeuron<T>(string id)
    {
      return this.sygmoidNeuronFactory.Create(id);
    }


  }
}
