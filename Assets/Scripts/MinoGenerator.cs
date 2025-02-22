using UnityEngine;

public class MinoGenerator : MonoBehaviour {
    [SerializeField]
    private TetriMino[] minoPrefabs;

    public TetriMino GenerateMino(EMinoType.Type type, Transform parent) {
        TetriMino mino = Instantiate(minoPrefabs[(int)type], parent.position, Quaternion.identity);
        mino.transform.SetParent(parent);

        return mino;
    }
}
