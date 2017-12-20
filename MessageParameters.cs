using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomSocket
{
    [Serializable]
    public class MessageParameters
    {
        private string parameter_1;
        private string parameter_2;
        private int capacity;

        public MessageParameters(string p1, string p2, int cap)
        {
            parameter_1 = p1;
            parameter_2 = p2;
            capacity = cap;
        }

        public String getFirstParameter()
        {
            return parameter_1;
        }

        public String getSecondParameter()
        {
            return parameter_2;
        }

        public int getCapacity()
        {
            return capacity;
        }
    }

}
