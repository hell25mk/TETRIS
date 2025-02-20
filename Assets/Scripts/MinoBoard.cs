using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinoBoard : MonoBehaviour {
    private const float UnitSize = 1.0f;
    private const float HalfUnitSize = UnitSize / 2.0f;
    private const int BoardWidth = 10;
    private const int BoardHeight = 40;

    private List<List<Transform>> lBoardGrid;
    private Transform[,] boardGrid;
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
    }

    public void BoardInitialize() {
        lBoardGrid = new List<List<Transform>>();
        for(int y = 0; y < BoardHeight; y++) {
            lBoardGrid.Add(CreateEmptyLine());
        }

        boardGrid = new Transform[BoardWidth, BoardHeight];
    }

    public bool IsValidPosition(Vector2 pos) {
        int left = Mathf.RoundToInt(pos.x - HalfUnitSize);
        int right = Mathf.RoundToInt(pos.x + HalfUnitSize);
        int buttom = Mathf.RoundToInt(pos.y - HalfUnitSize);

        return left >= coordinates.ButtomLeft.x && right <= coordinates.ButtomRight.x && buttom > coordinates.ButtomLeft.y;
    }

    public bool IsMinoCheck(Vector2 pos) {
        Vector2Int grid = PositionToGrid(pos);

        return lBoardGrid[grid.y][grid.x] = null;
        //return boardGrid[grid.x, grid.y] == null;
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
            Vector2Int grid = PositionToGrid(block.position);
            lBoardGrid[grid.y][grid.x] = block;
            //boardGrid[grid.x, grid.y] = block;

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
                Destroy(lBoardGrid[y][x].gameObject);
                //Destroy(boardGrid[x, y].gameObject);
                //boardGrid[x, y] = null;
            }

            lBoardGrid.RemoveAt(y);
            lBoardGrid.Insert(0, CreateEmptyLine());

            clearLineCount++;
        }

        return clearLineCount;
    }

    private bool IsLineFilled(int y) {
        for(int x = 0; x < BoardWidth; x++) {
            if(lBoardGrid[y][x] == null) {
            //if(boardGrid[x, y] == null) {
                return false;
            }
        }

        return true;
    }
}
