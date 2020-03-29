using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_DNN.Network
{
  public interface INeuron
  {
    float[] Inputs { get; set; }

    float Output { get; set; }

    void Evaluate();

  }

}
