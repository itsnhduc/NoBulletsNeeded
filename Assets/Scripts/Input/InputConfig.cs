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
        }
    };
    public static PlayerInput GetPlayerInput(int index)
    {
        return _config[index];
    }
}