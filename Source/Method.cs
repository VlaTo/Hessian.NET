namespace LibraProgramming.Serialization.Hessian
{
    public enum MethodType
    {
        ClientStreaming,
        ServerStreaming,
        DuplexStreaming
    }

    public interface IMethod
    {
        MethodType MethodType
        {
            get;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class Method<TRequest, TResponse> : IMethod
        where TRequest : class
        where TResponse : class
    {
        public string Name
        {
            get;
        }

        public MethodType MethodType
        {
            get;
        }

        public Method(MethodType methodType, string name)
        {
            MethodType = methodType;
            Name = name;
        }
    }
}