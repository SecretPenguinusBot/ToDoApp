
namespace backend.Data.Repositories
{
    public interface IRepositoryActionResult{}
    public interface IOkResult : IRepositoryActionResult{}
    public interface IOkResultWithData : IOkResult 
    { 
        public object Data {get; set; }
    }
    public interface IOkResultWithData<T> : IOkResultWithData
    {
        public new T Data { get; set;}
    }

    public interface INotFoundResult : IRepositoryActionResult {}
    public interface IBadRequestResult : IRepositoryActionResult {}
    public interface IServerErrorResult : IRepositoryActionResult 
    { 
        public string Error {get;set;}
    }

    public class OkResultWithData<T> : IOkResultWithData<T>
    {
        public T Data { get; set; }
        object IOkResultWithData.Data { 
            get => Data; 
            set{
                if(value is not T)
                {
                    throw new System.ArgumentException($"Required {typeof(T)} as value type");
                }
                Data = (T)value;
            }
        }
    }
    public class ServerErrorResult : IServerErrorResult
    {
        public string Error { get; set; }
    } 
    public sealed class OkResult : IOkResult{
        private OkResult(){}
        private static OkResult _instance = new();
        public static OkResult Instance => _instance;
    }
    public sealed class NotFoundResult : INotFoundResult{
        private NotFoundResult(){}
        private static NotFoundResult _instance = new();
        public static NotFoundResult Instance => _instance;
    }
}