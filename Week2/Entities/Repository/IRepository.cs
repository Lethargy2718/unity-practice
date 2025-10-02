namespace Week2.Entities.Repository
{
    internal interface IRepository<T> where T : class
    {
        int Count { get; }
        T Find(int id);
        void Add(params T[] items);
        void Remove(int id);
        string ToString();
    }
}
