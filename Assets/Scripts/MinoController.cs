using UnityEngine;

public class MinoController : MonoBehaviour {
    [SerializeField]
    private GameObject minoQueueObject;

    private MinoQueue minoQueue;
    private TetriMino currentMino;
    private MinoBoard minoBoard;
    public bool MinoControll {
        get; private set;
    }

    public void Start() {
        minoQueue = minoQueueObject.GetComponent<MinoQueue>();
    }

    public void Initialize(MinoBoard board) {
        MinoControll = false;
        minoBoard = board;
    }

    public void SetCurrentMino() {
        if(minoQueue.Count < 7) {
            minoQueue.Refill();
        }

        currentMino = minoQueue.Dequeue();
        currentMino.transform.SetParent(transform);
        currentMino.transform.position = transform.position;
        MinoControll = true;
    }

    public void FreeFall() {
        currentMino.transform.position += Vector3.down;

        if(!IsMinoMove()) {
            MinoControll = false;
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
    }
}
