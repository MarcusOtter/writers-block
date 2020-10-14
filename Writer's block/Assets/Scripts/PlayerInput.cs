using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    internal static event EventHandler<KeyType> OnKeyDown;

    [Header("Active writer profile")]
    [SerializeField] internal WriterProfile ActiveWriter;

    private void Update()
    {
        GetPlayerInput();
    }

    private void GetPlayerInput()
    {
        // Return if no new keys were pressed down.
        if (!Input.anyKeyDown) { return; }

        if (Input.GetMouseButtonDown(0) || 
            Input.GetMouseButtonDown(1) || 
            Input.GetMouseButtonDown(2))
        {
            OnKeyDown?.Invoke(this, KeyType.MouseClick);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            OnKeyDown?.Invoke(this, KeyType.Backspace);
            return;
        }

        OnKeyDown?.Invoke(this, KeyType.KeyboardKey);
    }
}
