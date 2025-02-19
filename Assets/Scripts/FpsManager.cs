using UnityEngine;

public class FpsManager : MonoBehaviour {
    private int fps = 60;

    public void Awake() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = fps;
    }
}
