using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomSocket
{
    [Serializable]
    public class Wrapper
    {
        private String parameter;
        private object ob;

        public Wrapper(String function, object objectToSend)
        {
            this.parameter = function;
            this.ob = objectToSend;
        }

        public object getObject()
        {
            return ob;
        }
        
        public String getFunction()
        {
            return parameter;
        }
    }
}
