using UnityEngine;

public static class InputContoller{
    public static bool InputLeftArrow() {
        return Input.GetKey(KeyCode.LeftArrow);
    }

    public static bool InputRightArrow() {
        return Input.GetKey(KeyCode.RightArrow);
    }

    public static bool InputUpArrow() {
        return Input.GetKey(KeyCode.UpArrow);
    }

    public static bool InputDownArrow() {
        return Input.GetKey(KeyCode.DownArrow);
    }
}
