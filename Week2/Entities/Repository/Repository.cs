namespace Week2.Entities.Repository
{
    internal abstract class Repository<T> : IRepository<T> where T : class
    {
        public readonly List<T> Items = [];

        public int Count => Items.Count;

        public virtual T Find(int id)
        {
            var item = Items.FirstOrDefault(item => GetId(item) == id);
            return item is null ? throw new KeyNotFoundException($"No {typeof(T).Name} found with Id {id}") : item;
        }

        public void Add(params T[] items) => Items.AddRange(items);

        public bool Remove(int id)
        {
            int removedCount = Items.RemoveAll(item => GetId(item) == id);
            return removedCount > 0;
        }

        protected abstract int GetId(T item);

        public override string ToString() => string.Join("\n", Items);
    }
}
