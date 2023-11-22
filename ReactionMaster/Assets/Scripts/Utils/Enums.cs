using System;

namespace Utils
{
    [Serializable]
    public enum ButtonType
    {
        Good,
        Bad
    }
    
    public enum GameState
    {
        Menu,
        PlayMode,
        GameOver
    }

}