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
    private float minoFallTimer;
    private float minoFallInterval;

    public void Start() {
        minoBoard = minoBoardObject.GetComponent<MinoBoard>();
        minoController = minoControllerObject.GetComponent<MinoController>();

        gameMode = GameMode.Title;
        gameTimer = 0.0f;
    }

    public void Update() {
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
        minoFallTimer = 0.0f;
        minoFallInterval = 1.0f;
    }

    public void UpdateInGame() {
        gameTimer += Time.deltaTime;

        if(!minoController.HasCurrentMino) {
            // 次のミノが出てきた時点でミノが被っていたらゲームオーバー
            if(!minoController.SetCurrentMino()) {
                gameMode = GameMode.GameOver;
            }
            return;
        }

        PlayerInput();

        if(minoFallTimer < minoFallInterval) {
            minoFallTimer += Time.deltaTime;
            return;
        }

        minoFallTimer = 0.0f;
        minoController.FreeFall();
    }
    public void UpdateGameOver() {
        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            gameMode = GameMode.Title;
        }
    }


    private void PlayerInput() {
        if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            minoController.MoveLeft();
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            minoController.MoveRight();
        }
        if(Input.GetKey(KeyCode.DownArrow)) {
            minoFallInterval = 0.01f;
        }
        else {
            minoFallInterval = 1.0f;
        }

        if(Input.GetKeyDown(KeyCode.A)) {
            minoController.RotateLeft();
        }
        if(Input.GetKeyDown(KeyCode.D)) {
            minoController.RotateRight();
        }
    }
}
