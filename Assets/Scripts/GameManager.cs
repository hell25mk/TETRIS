using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private GameObject minoQueueObject;
    [SerializeField]
    private GameObject minoBoardObject;
    [SerializeField]
    private GameObject minoControllerObject;

    private MinoBoard minoBoard;
    private MinoQueue minoQueue;
    private MinoController minoController;

    private float gameTimer;
    private float minoFallTimer;
    private float minoFallInterval;

    public void Start() {
        minoQueue = minoQueueObject.GetComponent<MinoQueue>();
        minoBoard = minoBoardObject.GetComponent<MinoBoard>();
        minoBoard.BoardInitialize();
        minoController = minoControllerObject.GetComponent<MinoController>();
        minoController.Initialize(minoBoard);

        gameTimer = 0.0f;
        minoFallTimer = 0.0f;
        minoFallInterval = 1.0f;
    }

    public void Update() {
        UpdateInGame();
    }

    public void UpdateInGame() {
        if(!minoController.MinoControll) {
            minoController.SetCurrentMino();
            return;
        }

        PlayerInput();

        gameTimer += Time.deltaTime;

        if(minoFallTimer < minoFallInterval) {
            minoFallTimer += Time.deltaTime;
            return;
        }

        minoFallTimer = 0.0f;
        minoController.FreeFall();
    }

    private void PlayerInput() {
        if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            minoController.MoveLeft();
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            minoController.MoveRight();
        }
        // デバッグ用
        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            minoController.MoveUp();
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)) {
            minoController.MoveDown();
        }

        if(Input.GetKeyDown(KeyCode.A)) {
            minoController.RotateLeft();
        }
        if(Input.GetKeyDown(KeyCode.D)) {
            minoController.RotateRight();
        }
    }
}
