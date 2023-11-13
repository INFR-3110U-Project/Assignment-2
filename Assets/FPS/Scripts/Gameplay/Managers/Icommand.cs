
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;


namespace MyNamespace
{
    public interface ICommand
    {
        void Execute();
        void Undo(); // Optional, for implementing undo functionality
    }

}

