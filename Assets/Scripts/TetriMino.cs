using UnityEngine;

public class TetriMino : MonoBehaviour {
    public enum RotateDirection {
        ClockWise,
        CounterClockWise
    }

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

    public void Rotate(RotateDirection direction) {
        float angle = (direction == RotateDirection.ClockWise) ? 90.0f : -90.0f;
        transform.RotateAround(minoAxis.position, new Vector3(0.0f, 0.0f, 1.0f), angle);

        ChildrenRotateReset();
    }

    private void ChildrenRotateReset() {
        foreach(Transform mino in minoChildren) {
            mino.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
    }
}
