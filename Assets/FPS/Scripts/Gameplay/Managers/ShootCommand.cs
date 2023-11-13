using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;
using MyNamespace;

public class ShootCommand : ICommand
{

    private Camera playerCamera;
    private float originalFOV;
    private float newFOV = 70.0f; // Adjust the value to your desired FOV.

    public ShootCommand(Camera camera)
    {
        playerCamera = camera;
        originalFOV = playerCamera.fieldOfView;
    }

    public void Execute()
    {
        playerCamera.fieldOfView = newFOV;
    }
    public void Undo()
    {
        playerCamera.fieldOfView = originalFOV;
    }
}