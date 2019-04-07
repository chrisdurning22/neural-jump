using System;
using System.Collections.Generic;
using System.Configuration;

namespace neat
{
	public class ActivationFunction {
        
        public static float sigmoid(float output) {
            //return (float)Math.Tanh(output);
            return 1.0f / (1.0f + (float)Math.Exp(-output));
        }
	}

}

