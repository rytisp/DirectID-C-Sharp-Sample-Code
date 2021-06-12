using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExampleSetup
{
    public static class Decoder
    {
        public static string EncodeForwardSlashes(string endpoint)
        {
            return endpoint.Replace("/", "\\").Replace(":", "colon");
        }

        public static string DecodeForwardSlashes(string endpoint)
        {
            return endpoint.Replace("\\", "/").Replace("colon", ":");
        }
    }
}