using UnityEngine;

public class TetriMino : MonoBehaviour {
    [SerializeField]
    private Transform[] minoChildren;
    [SerializeField]
    private Transform minoAxis;

    [SerializeField]
    private MinoType minoType {
        get; set;
    }

    public Transform[] MinoChildren {
        get {
            return minoChildren;
        }
    }

    public Vector3 MinoAxis {
        get {
            return minoAxis.position;
        }
    }
}
