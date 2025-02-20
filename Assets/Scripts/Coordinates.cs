using UnityEngine;

public class Coordinates {
    private Vector2 topLeft;
    private Vector2 topRight;
    private Vector2 buttomLeft;
    private Vector2 buttomRight;

    public Coordinates(Vector2 tLeft = default, Vector2 tRight = default, Vector2 bLeft = default, Vector2 bRight = default) {
        topLeft = tLeft != default ? tLeft : Vector2.zero;
        topRight = tRight != default ? tRight : Vector2.zero;
        buttomLeft = bLeft != default ? bLeft : Vector2.zero;
        buttomRight = bRight != default ? bRight : Vector2.zero;
    }

    public Vector2 TopLeft {
        get => topLeft;
        set => topLeft = value;
    }

    public Vector2 TopRight {
        get => topRight;
        set => topRight = value;
    }

    public Vector2 ButtomLeft {
        get => buttomLeft;
        set => buttomLeft = value;
    }

    public Vector2 ButtomRight {
        get => buttomRight;
        set => buttomRight = value;
    }
}
