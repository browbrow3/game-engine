using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Input
{
    /// <summary>
    /// Enum representing different types of inputs that into the game engine.
    /// </summary>
    public enum InputType
    {
        Invalid = -1,
        MoveLeft,
        MoveRight,
        MoveUp,
        MoveDown,
        Interact,
        Attack,
        Pause,
        Quit,
    }
}
