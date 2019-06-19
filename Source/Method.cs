namespace LibraProgramming.Serialization.Hessian
{
    public interface IMethod
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class Method<TRequest, TResponse> : IMethod
        where TRequest : class
        where TResponse : class
    {

    }
}