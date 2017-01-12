namespace LibraProgramming.Hessian
{
    public interface IObjectSerializer
    {
        void Serialize(HessianOutputWriter writer, object graph);

        object Deserialize(HessianInputReader reader);
    }
}