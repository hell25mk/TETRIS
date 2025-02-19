using UnityEngine;

public class MinoBoard : MonoBehaviour {
    private const float UnitSize = 1.0f;
    private const float HalfUnitSize = UnitSize / 2.0f;
    private const int BoardWidth = 10;
    private const int BoardHeight = 40;

    private Transform[,] boardGrid;
    private Vector2Int boardButtomLeftPosition;
    private Vector2Int boardTopRightPosition;

    public void Start() {
        boardGrid = new Transform[BoardWidth, BoardHeight];
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        float halfBoardWidth = BoardWidth / 2.0f;
        float halfBoardHeight = BoardHeight / 2.0f;
        boardButtomLeftPosition = new Vector2Int(
            Mathf.RoundToInt(transform.position.x - halfBoardWidth),
            Mathf.RoundToInt(transform.position.y - halfBoardHeight)
        );
        boardTopRightPosition = new Vector2Int(
            Mathf.RoundToInt(transform.position.x + halfBoardWidth),
            Mathf.RoundToInt(transform.position.y + halfBoardHeight)
        );
    }

    public bool IsValidPosition(Vector2 pos) {
        int left = Mathf.RoundToInt(pos.x - HalfUnitSize);
        int right = Mathf.RoundToInt(pos.x + HalfUnitSize);
        int buttom = Mathf.RoundToInt(pos.y - HalfUnitSize);

        return left >= boardButtomLeftPosition.x && right <= boardTopRightPosition.x && buttom >= boardButtomLeftPosition.y;
    }
}
