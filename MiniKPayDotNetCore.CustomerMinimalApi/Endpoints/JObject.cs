
namespace MiniKPayDotNetCore.MiniKPay.Api.Endpoints
{
    internal class JObject
    {
        public bool ContainsKey { get; internal set; }

        internal static JObject Parse(object value)
        {
            throw new NotImplementedException();
        }

        internal static JObject Parse(object serializeObject)
        {
            throw new NotImplementedException();
        }

        internal bool ContainsKey(string v)
        {
            throw new NotImplementedException();
        }
    }
}