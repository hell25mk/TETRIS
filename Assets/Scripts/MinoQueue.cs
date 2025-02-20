using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MinoQueue : MonoBehaviour {
    private MinoGenerator minoGenerator;
    private Queue<TetriMino> minoQueue;

    public int Count => minoQueue.Count;

    public void Start() {
        minoGenerator = GetComponent<MinoGenerator>();
        minoQueue = new Queue<TetriMino>();
    }

    public void Refill() {
        foreach(TetriMino mino in minoGenerator.GenerateMinoSet(transform)) {
            minoQueue.Enqueue(mino);
        }
    }

    public TetriMino Dequeue() {
        return minoQueue.Dequeue();
    }
}
