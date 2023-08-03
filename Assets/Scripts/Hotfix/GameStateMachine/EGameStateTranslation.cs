namespace Game
{
    public enum EGameStateTranslation
    {
        Init2Loading = 0, // Game Init -> Loading
        Any2Loading = 1, // Game Any -> Loading
        Loading2Login = 2, // Game Loading -> Login
        Loading2Outside = 3, // Game Loading -> Outside
        Loading2Inside = 4, // Game Loading -> Inside
    }
}