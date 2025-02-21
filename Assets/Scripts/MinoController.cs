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

    public void MoveUp() {
        currentMino.transform.position += Vector3.up;

        if(!IsMinoMove()) {
            MoveDown();
        }
    }

    public void MoveDown() {
        currentMino.transform.position += Vector3.down;

        if(!IsMinoMove()) {
            MoveUp();
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

    public void SetCurrentMino() {
        MyDebug.Logger.Log(minoQueue.Count);
        if(minoQueue.Count < EMinoType.TypeCount) {
            minoQueue.Refill();
        }

        currentMino = minoQueue.Dequeue();
        currentMino.transform.SetParent(transform);
        currentMino.transform.position = transform.position;
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
