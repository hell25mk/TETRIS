using System.Collections.Generic;
using UnityEngine;

public class MinoQueue : MonoBehaviour {
    private MinoGenerator minoGenerator;
    private List<EMinoType.Type> minoTypes;
    private Queue<EMinoType.Type> minoQueue;

    public int Count => minoQueue.Count;

    public void Start() {
        minoGenerator = GetComponent<MinoGenerator>();
        minoTypes = EMinoType.TypeList;
    }

    public void Initialize() {
        minoQueue = new Queue<EMinoType.Type>();
    }

    private void FisherYastesShuffle() {
        for(int i = EMinoType.TypeCount - 1; i > 0; --i) {
            int j = Random.Range(0, i + 1);

            (minoTypes[j], minoTypes[i]) = (minoTypes[i], minoTypes[j]);
        }
    }

    public void Refill() {
        FisherYastesShuffle();

        foreach(EMinoType.Type type in minoTypes){
            minoQueue.Enqueue(type);
        }
    }

    public TetriMino Dequeue(Transform parent) {
        return minoGenerator.GenerateMino(minoQueue.Dequeue(), parent);
    }
}
