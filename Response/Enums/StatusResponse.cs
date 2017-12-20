using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteApi.Response.Enums
{
    public class StatusResponse
    {
        private string _value;

        public static StatusResponse SUCCESS { get { return new StatusResponse("Sucess"); } }
        public static StatusResponse ERROR { get { return new StatusResponse("Error"); } }


        public StatusResponse(string value)
        {
            this.Value = value;
        }

        public string Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }
    }
}