using System.Diagnostics;

namespace MyDebug {
    public static class Logger {
        [Conditional("UNITY_EDITOR")]
        public static void Log(object o) {
            //Debug.Log("<color=red>aiueo</color>");
            UnityEngine.Debug.Log(o);
        }
    }
}
