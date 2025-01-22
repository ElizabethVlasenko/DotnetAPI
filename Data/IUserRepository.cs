namespace DotnetAPI.Data
{
    public interface IUserRepository
    {
        //calls; not methods
        public bool SaveChanges();
        public void AddEntity<T>(T entityToAdd);
        public void RemoveEntity<T>(T entityToRemove);
    }
}
