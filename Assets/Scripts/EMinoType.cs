using System;
using System.Collections.Generic;
using System.Linq;

public static class EMinoType {
    public enum Type {
        MINO_I = 0,
        MINO_O,
        MINO_S,
        MINO_Z,
        MINO_J,
        MINO_L,
        MINO_T
    }

    public static readonly int TypeCount = Enum.GetValues(typeof(Type)).Length;
    
    public static readonly List<Type> TypeList = Enum.GetValues(typeof(Type)).Cast<Type>().ToList();
}
