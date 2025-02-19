using UnityEngine;

public class MinoController : MonoBehaviour {
    private TetriMino currentMino;
    private MinoBoard minoBoard;

    public void SetCurrentMino(TetriMino mino) {
        currentMino = mino;
        currentMino.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public void SetMinoBoard(MinoBoard board) {
        minoBoard = board;
    }

    public void FreeFall() {
        currentMino.transform.position += new Vector3(0.0f, -1.0f);

        if(IsMinoMove()) {
            Debug.Log("");
        }
    }

    public void MoveLeft() {
        currentMino.transform.position -= new Vector3(1.0f, 0.0f);

        if(!IsMinoMove()) {
            MoveRight();
        }
    }

    public void MoveRight() {
        currentMino.transform.position += new Vector3(1.0f, 0.0f);

        if(!IsMinoMove()) {
            MoveLeft();
        }
    }

#if DEBUG
    public void MoveUp() {
        currentMino.transform.position += new Vector3(0.0f, 1.0f);

        if(!IsMinoMove()) {
            MoveDown();
        }
    }

    public void MoveDown() {
        currentMino.transform.position -= new Vector3(0.0f, 1.0f);

        if(!IsMinoMove()) {
            MoveUp();
        }
    }

#endif
    public void RotateLeft() {
        currentMino.transform.RotateAround(currentMino.MinoAxis, new Vector3(0.0f, 0.0f, 1.0f), 90.0f);

        if(!IsMinoMove()) {
            RotateRight();
        }
    }

    public void RotateRight() {
        currentMino.transform.RotateAround(currentMino.MinoAxis, new Vector3(0.0f, 0.0f, 1.0f), -90.0f);

        if(!IsMinoMove()) {
            RotateLeft();
        }
    }

    private bool IsMinoMove() {
        foreach(Transform pos in currentMino.MinoChildren) {
            if(!minoBoard.IsValidPosition(pos.position)) {
                return false;
            }
        }

        return true;
    }
}
