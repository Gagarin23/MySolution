namespace MyProject
{
    interface ITestReader<T>
        where T : class
    {
        public abstract T GetModelObject(string address);
    }
}