namespace Investigacion.Web.Test.Services
{
    public interface IDao
    {
        int Count { get; }

        void Increment();
    }

    public class Dao : IDao
    {
        public int Count { get; set; }

        public void Increment()
        {
            Count++;
        }
    }
}