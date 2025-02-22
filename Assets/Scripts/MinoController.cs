using UnityEngine;

public class MinoController : MonoBehaviour {
    [SerializeField]
    private GameObject minoQueueObject;

    private MinoQueue minoQueue;
    private TetriMino currentMino;
    private MinoBoard minoBoard;

    public bool HasCurrentMino {
        get => currentMino != null;
    }

    public void Start() {
        minoQueue = minoQueueObject.GetComponent<MinoQueue>();
        currentMino = null;
    }

    public void Initialize(MinoBoard board) {
        minoBoard = board;
        minoQueue.Initialize();

        if(currentMino != null) {
            Destroy(currentMino.gameObject);
            currentMino = null;
        }
    }

    public void FreeFall() {
        currentMino.transform.position += Vector3.down;

        if(!IsMinoMove()) {
            currentMino.transform.position += Vector3.up;
            PlaceMinoOnTheBoard();
        }
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

    public bool SetCurrentMino() {
        if(minoQueue.Count <= EMinoType.TypeCount) {
            minoQueue.Refill();
        }
        MyDebug.Logger.Log(minoQueue.Count);

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
    }
}
