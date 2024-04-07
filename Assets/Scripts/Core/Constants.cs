namespace Game
{
    public static class Constants
    {
        public const string AsteroidsConfigPath = "AsteroidsConfig";

        public const string ParentAsteroids = "ParentAsteroids";
        public const string AsteroidTag = "Asteroid";

        public const string AnimIdDied = "Died";

        public const string GameBtnID = "GameBtnID";
        public const string ResetBtnID = "ResetBtnID";
        public const string QuitBtnID = "QuitBtnID";
        
        public const string AsteroidCollisionCounterID = "AsteroidCollisionCounter";

        public const string ShowKey = "Show";
        public const string HideKey = "Hide";

        public const string ActivePopupID = "ActivePopup";

        public enum AsteroidsParentType
        {
            None,
            Active,
            Inactive
        }

        public enum PopupsID
        {
            None,
            MainMenu
        }
    }
}