using UnityEngine;

public class GameManager : MonoBehaviour {
    public enum GameMode {
        Title = 0,
        InGame,
        GameOver
    }

    [SerializeField]
    private GameObject minoQueueObject;
    [SerializeField]
    private GameObject minoBoardObject;
    [SerializeField]
    private GameObject minoControllerObject;

    private MinoBoard minoBoard;
    private MinoController minoController;

    private GameMode gameMode;
    private float gameTimer;

    public void Start() {
        minoBoard = minoBoardObject.GetComponent<MinoBoard>();
        minoController = minoControllerObject.GetComponent<MinoController>();

        gameMode = GameMode.Title;
        gameTimer = 0.0f;
    }

    public void Update() {
        gameTimer += Time.deltaTime;

        switch(gameMode) {
            case GameMode.Title:
                UpdateTitle();
                return;
            case GameMode.InGame:
                UpdateInGame();
                return;
            case GameMode.GameOver:
                UpdateGameOver();
                return;
        }
    }

    public void UpdateTitle() {
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            gameMode = GameMode.InGame;
            InitializeInGame();
        }
    }

    public void InitializeInGame() {
        minoBoard.Initialize();
        minoController.Initialize(minoBoard);
    }

    public void UpdateInGame() {
        minoController.OnUpdate();

        if(!minoController.IsMinoControll) {
            gameMode = GameMode.GameOver;
        }
    }

    public void UpdateGameOver() {
        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            gameMode = GameMode.Title;
        }
    }

#if DEBUG
    private void OnGUI() {
        string modeText = "";

        switch(gameMode) {
            case GameMode.Title:
                modeText = "Title";
                break;
            case GameMode.InGame:
                modeText = "InGame";
                break;
            case GameMode.GameOver:
                modeText = "GameOver";
                break;
        }

        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        style.normal.textColor = Color.white;

        GUI.Label(new Rect(10, 10, 500, 1000), modeText, style);
    }
#endif
}
