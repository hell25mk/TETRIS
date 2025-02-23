using UnityEngine;
using static GameManager;

public class MinoController : MonoBehaviour {
    [SerializeField]
    private GameObject minoQueueObject;

    private MinoBoard minoBoard;
    private MinoQueue minoQueue;
    private TetriMino currentMino;
    private TetriMino holdMino;
    private bool isHoldExecute;
    private float minoFallTimer;
    private float minoFallInterval;
    private bool isMinoControll;

    public bool IsMinoControll => isMinoControll;

    public void Start() {
        minoQueue = minoQueueObject.GetComponent<MinoQueue>();
        minoQueue.Initialize();
        currentMino = null;
        holdMino = null;
        isMinoControll = true;
        isHoldExecute = false;
        minoFallTimer = 0.0f;
        minoFallInterval = 1.0f;
    }

    public void Initialize(MinoBoard board) {
        minoBoard = board;

        if(currentMino != null) {
            Destroy(currentMino.gameObject);
            currentMino = null;
        }
        if(holdMino != null) {
            Destroy(holdMino.gameObject);
            holdMino = null;
        }
    }

    public void OnUpdate() {
        if(!isMinoControll) {
            return;
        }

        if(currentMino == null) {
            SetCurrentMino();
        }

        KeyInput();

        if(minoFallTimer < minoFallInterval) {
            minoFallTimer += Time.deltaTime;
            return;
        }

        FreeFall();
        minoFallTimer = 0.0f;
    }

    public void KeyInput() {
        if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            MoveLeft();
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            MoveRight();
        }
        if(Input.GetKey(KeyCode.DownArrow)) {
            minoFallInterval = 0.01f;
        }
        else {
            minoFallInterval = 1.0f;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            while(true) {
                if(!FreeFall()) {
                    return;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.A)) {
            RotateLeft();
        }
        if(Input.GetKeyDown(KeyCode.D)) {
            RotateRight();
        }
        if(Input.GetKeyDown(KeyCode.Space)) {
            Hold();
        }
    }

    public bool FreeFall() {
        currentMino.transform.position += Vector3.down;

        if(!IsMinoMove()) {
            currentMino.transform.position += Vector3.up;
            PlaceMinoOnTheBoard();
            if(!SetCurrentMino()) {
                isMinoControll = false;
            }
            return false;
        }

        return true;
    }

    public void MoveLeft() {
        currentMino.transform.position += Vector3.left;

        if(!IsMinoMove()) {
            MoveRight();
        }
    }

    public void MoveRight() {
        currentMino.transform.position += Vector3.right;

        if(!IsMinoMove()) {
            MoveLeft();
        }
    }

    public void MoveDown() {
        currentMino.transform.position += Vector3.down;

        if(!IsMinoMove()) {
            currentMino.transform.position += Vector3.up;
            PlaceMinoOnTheBoard();
        }
    }

    public void RotateLeft() {
        currentMino.Rotate(TetriMino.RotateDirection.ClockWise);

        if(!IsMinoMove()) {
            RotateRight();
        }
    }

    public void RotateRight() {
        currentMino.Rotate(TetriMino.RotateDirection.CounterClockWise);

        if(!IsMinoMove()) {
            RotateLeft();
        }
    }

    public void Hold() {
        if(isHoldExecute) {
            return;
        }

        if(holdMino == null) {
            holdMino = currentMino;
            SetCurrentMino();
        }
        else {
            TetriMino temp = holdMino;
            holdMino = currentMino;
            currentMino = temp;
            currentMino.transform.position = transform.position;
        }

        holdMino.transform.position = new Vector2(-7.5f, 7.5f); // Žb’è‘Î‰ž
        holdMino.transform.rotation = Quaternion.identity;
        isHoldExecute = true;
    }

    public bool SetCurrentMino() {
        if(minoQueue.Count <= EMinoType.TypeCount) {
            minoQueue.Refill();
        }

        currentMino = minoQueue.Dequeue(transform);
        currentMino.transform.position = transform.position;

        return IsMinoMove();
    }

    private bool IsMinoMove() {
        foreach(Transform block in currentMino.MinoChildren) {
            if(!minoBoard.IsValidPosition(block.position)) {
                return false;
            }

            if(!minoBoard.IsMinoCheck(block.position)) {
                return false;
            }
        }

        return true;
    }

    private void PlaceMinoOnTheBoard() {
        minoBoard.PlaceMino(currentMino.MinoChildren);
        Destroy(currentMino.gameObject);
        currentMino = null;
        isHoldExecute = false;
    }
}
