using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinoGenerator : MonoBehaviour {
    [SerializeField]
    private TetriMino[] minoPrefabs;

    private List<TetriMino> minos;
    private List<EMinoType.Type> minoTypes;

    public void Start() {
        minos = new List<TetriMino>();
        //minoTypes = Enum.GetValues(typeof(MinoType)).Cast<MinoType>().ToList();
        minoTypes = EMinoType.TypeList;
    }

    public List<TetriMino> GenerateMinoSet(Transform parent) {
        FisherYastesShuffle();

        foreach(MinoType type in minoTypes){
            minos.Add(GenerateMino(parent, type));
        }

        return minos;
    }

    private void FisherYastesShuffle() {
        for(int i = minoTypes.Count - 1; i > 0; --i) {
            int j = UnityEngine.Random.Range(0, i + 1);

            (minoTypes[j], minoTypes[i]) = (minoTypes[i], minoTypes[j]);
        }
    }

    private TetriMino GenerateMino(Transform parent, MinoType type) {
        TetriMino mino = Instantiate(minoPrefabs[(int)type], parent.position, Quaternion.identity);
        mino.transform.SetParent(parent);

        return mino;
    }
}
