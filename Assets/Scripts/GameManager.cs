using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private GameObject minoQueueObject;
    [SerializeField]
    private GameObject minoBoardObject;

    private MinoBoard minoBoard;
    private MinoQueue minoQueue;
    private MinoController minoController;

    public void Start() {
        minoQueue = minoQueueObject.GetComponent<MinoQueue>();
        minoBoard = minoBoardObject.GetComponent<MinoBoard>();
        minoController = GetComponent<MinoController>();
        minoController.SetMinoBoard(minoBoard);
    }

    public void Update() {
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            minoQueue.Refill();
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            TetriMino mino = minoQueue.Dequeue();
            mino.transform.SetParent(minoBoardObject.transform);
            minoController.SetCurrentMino(mino);
        }

        PlayerInput();
    }

    private void PlayerInput() {
        if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            minoController.MoveLeft();
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            minoController.MoveRight();
        }

        if(Input.GetKeyDown(KeyCode.A)) {
            minoController.RotateLeft();
        }
        if(Input.GetKeyDown(KeyCode.D)) {
            minoController.RotateRight();
        }
    }
}
