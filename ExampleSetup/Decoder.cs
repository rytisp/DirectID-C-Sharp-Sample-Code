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