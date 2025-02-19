using UnityEngine;

public class MinoBoard : MonoBehaviour {
    private const float UnitSize = 1.0f;
    private const float HalfUnitSize = UnitSize / 2.0f;
    private const int BoardWidth = 10;
    private const int BoardHeight = 40;

    private Transform[,] boardGrid;
    private ObjectRange objectRange;

    public void Start() {
        boardGrid = new Transform[BoardWidth, BoardHeight];
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        objectRange = new ObjectRange();
        float halfBoardWidth = BoardWidth / 2.0f;
        float halfBoardHeight = BoardHeight / 2.0f;
        objectRange.minRange = new Vector2(transform.position.x - halfBoardWidth, transform.position.y - halfBoardHeight);
        objectRange.maxRange = new Vector2(transform.position.x + halfBoardWidth, transform.position.y + halfBoardHeight);
    }

    public bool IsValidPosition(Vector3 pos) {
        Debug.Log("pos=" + pos);
        Debug.Log("max=" + objectRange.maxRange);
        return pos.x - HalfUnitSize >= objectRange.minRange.x && pos.x + HalfUnitSize <= objectRange.maxRange.x
            && pos.y - HalfUnitSize >= objectRange.minRange.y && pos.y + HalfUnitSize <= objectRange.maxRange.y;
    }
}
