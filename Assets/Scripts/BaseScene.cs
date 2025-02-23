using UnityEngine;

public abstract class BaseScene : MonoBehaviour {
    public virtual void Init(){}
    public virtual void OnUpdate(){}
    public virtual void Exit(){}
}
