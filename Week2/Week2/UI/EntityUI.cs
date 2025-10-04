using Week2.Entities.Repository;
using Week2.Utils;

namespace Week2.UI
{
    internal abstract class EntityUI<T> where T : class
    {
        protected abstract string EntityName { get; }
        protected abstract Repository<T> EntityCollection { get; }

        public void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== {EntityName + 's'} ===");
                Console.WriteLine("1. Add");
                Console.WriteLine("2. Edit");
                Console.WriteLine("3. Delete");
                Console.WriteLine("4. View");
                Console.WriteLine("0. Back");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddEntity();
                        break;
                    case "2":
                        EditEntity();
                        break;
                    case "3":
                        DeleteEntity();
                        break;
                    case "4":
                        ViewEntities();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press Enter...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        protected abstract void AddEntity();
        protected abstract void EditEntity();

        protected void DeleteEntity()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"== Delete {EntityName} ==");

                if (EntityCollection.Count == 0)
                {
                    Console.WriteLine($"No {EntityName.ToLower()} found.");
                    InputHelper.WaitForBack();
                    return;
                }

                Console.WriteLine(EntityCollection);
                Console.WriteLine();

                int entityId = InputHelper.GetInt($"Enter the id of the {EntityName.ToLower()} you wish to delete (-1 to cancel)");

                if (entityId == -1)
                {
                    return;
                }
                else if (EntityCollection.Remove(entityId))
                {
                    Console.WriteLine($"Successfully deleted {EntityName.ToLower()} with id {entityId}. Press enter...");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine($"{EntityName} with id {entityId} not found. Press enter...");
                    Console.ReadLine();
                }
            }
        }

        protected void ViewEntities()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== {EntityName + 's'} ===");

                if (EntityCollection.Count == 0)
                {
                    Console.WriteLine($"No {EntityName.ToLower()} found.");
                }
                else
                {
                    Console.WriteLine(EntityCollection);
                }

                InputHelper.WaitForBack();
                return;
            }
        }
    }
}