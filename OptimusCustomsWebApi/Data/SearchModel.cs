using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimusCustomsWebApi.Data
{
    public class SearchModel
    {
        private Dictionary<string, object> parameters;
        public SearchModel()
        {
            parameters = new Dictionary<string, object>();
        }

        public Dictionary<string, object> Parameters
        {
            get
            {
                return parameters;
            }
            set
            {
                parameters = value;
            }
        }


    }
}
