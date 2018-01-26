using UnityEngine;

public class InputConfig
{
    private static PlayerInput[] _config = {
        new PlayerInput {
            left = KeyCode.A,
            right = KeyCode.D,
            up = KeyCode.W,
            down = KeyCode.S,
            a = KeyCode.E,
            b = KeyCode.LeftShift,
            c = KeyCode.Q
        },
        new PlayerInput {
            left = KeyCode.L,
            right = KeyCode.Quote,
            up = KeyCode.P,
            down = KeyCode.Semicolon,
            a = KeyCode.O,
            b = KeyCode.RightShift,
            c = KeyCode.LeftBracket
        },
        new PlayerInput {
            left = KeyCode.G,
            right = KeyCode.J,
            up = KeyCode.Y,
            down = KeyCode.H,
            a = KeyCode.U,
            b = KeyCode.Comma,
            c = KeyCode.C
        },
        new PlayerInput {
            left = KeyCode.Keypad4,
            right = KeyCode.Keypad6,
            up = KeyCode.Keypad8,
            down = KeyCode.Keypad5,
            a = KeyCode.Keypad7,
            b = KeyCode.KeypadEnter,
            c = KeyCode.Keypad9
        }
    };
    public static PlayerInput GetPlayerInput(int index)
    {
        return _config[index];
    }
}