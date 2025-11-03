namespace HelpersAndExtensions.SaveSystem
{
    public interface IBind<TData> where TData : ISavable
    {
        string Id { get; set; }
        void Bind(TData data);
    }
}