namespace CommonLogic
{
    public class Singleton<T> where T : class, new()
    {
        public static T Inst
        {
            get { return _inst ??= new T(); }
        }
        private static T _inst;
    }
}