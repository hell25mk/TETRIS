using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinoBoard : MonoBehaviour {
    private const float UnitSize = 1.0f;
    private const float HalfUnitSize = UnitSize / 2.0f;
    private const int BoardWidth = 10;
    private const int BoardHeight = 40;

    private List<List<Transform>> boardGrid;
    private Coordinates coordinates;

    public void Start() {
        float halfWidth = BoardWidth / 2.0f;
        float halfHeight = BoardHeight / 2.0f;
        coordinates = new Coordinates(
            new Vector2(transform.position.x - halfWidth, transform.position.y + halfHeight),
            new Vector2(transform.position.x + halfWidth, transform.position.y + halfHeight),
            new Vector2(transform.position.x - halfWidth, transform.position.y - halfHeight),
            new Vector2(transform.position.x + halfWidth, transform.position.y - halfHeight)
        );

        boardGrid = new List<List<Transform>>();

        for(int y = 0; y < BoardHeight; y++) {
            boardGrid.Add(CreateEmptyLine());
        }
    }

    public void Initialize() {
        for(int y = 0; y < BoardHeight; y++) {
            for(int x = 0; x < BoardWidth; x++) {
                if(boardGrid[y][x] == null) {
                    continue;
                }

                Destroy(boardGrid[y][x].gameObject);
                boardGrid[y][x] = null;
            }
        }
    }

    public bool IsValidPosition(Vector2 pos) {
        // IsMinoCheckだけで十分そう(配列外参照だけ注意)
        int left = Mathf.RoundToInt(pos.x - HalfUnitSize);
        int right = Mathf.RoundToInt(pos.x + HalfUnitSize);
        int buttom = Mathf.RoundToInt(pos.y - HalfUnitSize);

        return left >= coordinates.ButtomLeft.x
            && right <= coordinates.ButtomRight.x
            && buttom >= coordinates.ButtomLeft.y;
    }

    public bool IsMinoCheck(Vector2 pos) {
        Vector2Int grid = PositionToGrid(pos);

        return boardGrid[grid.y][grid.x] == null;
    }

    public Vector2Int PositionToGrid(Vector2 pos) {
        Vector2Int grid = new Vector2Int(
            Mathf.FloorToInt((pos.x - coordinates.ButtomLeft.x) / UnitSize),
            (BoardHeight - 1) - Mathf.FloorToInt((pos.y - coordinates.ButtomLeft.y) / UnitSize)
        );

        return grid;
    }

    public void PlaceMino(Transform[] blocks) {
        List<int> rows = new List<int>();

        foreach(Transform block in blocks) {
            block.SetParent(transform);
            Vector2Int grid = PositionToGrid(block.position);
            boardGrid[grid.y][grid.x] = block;

            if(!rows.Contains(grid.y)) {
                rows.Add(grid.y);
            }
        }

        rows.Sort();

        ClearLines(rows);
    }

    private List<Transform> CreateEmptyLine() {
        List<Transform> line = new List<Transform>();

        for(int x = 0; x < BoardWidth; x++) {
            line.Add(null);
        }

        return line;
    }

    private int ClearLines(List<int> rows) {
        int clearLineCount = 0;

        foreach(int y in rows) {
            if(!IsLineFilled(y)) {
                continue;
            }

            for(int x = 0; x < BoardWidth; x++) {
                Destroy(boardGrid[y][x].gameObject);
            }

            boardGrid.RemoveAt(y);
            LineFall(y);
            boardGrid.Insert(0, CreateEmptyLine());

            clearLineCount++;
        }

        return clearLineCount;
    }

    private bool IsLineFilled(int y) {
        for(int x = 0; x < BoardWidth; x++) {
            if(boardGrid[y][x] == null) {
                return false;
            }
        }

        return true;
    }

    private void LineFall(int row) {
        for(int y = row - 1; y > 0; --y) {
            foreach(Transform block in boardGrid[y]) {
                if(block == null) {
                    continue;
                }

                block.position += Vector3.down;
            }
        }
    }

#if DEBUG
    private void OnGUI() {
        if(boardGrid == null || boardGrid.Count == 0) {
            return;
        }

        string board = "";

        for(int y = 0; y < BoardHeight; y++) {
            foreach(Transform x in boardGrid[y]) {
                board += x != null ? "■" : "□";
            }

            board += "\n";
        }

        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        style.normal.textColor = Color.white;

        GUI.Label(new Rect(10, 120, 500, 1000), board, style);
    }
#endif
}
