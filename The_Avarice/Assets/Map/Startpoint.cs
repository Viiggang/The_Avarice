using UnityEngine;

public class Startpoint : MonoBehaviour
{
    private void Awake()
    {
        if (this.gameObject != null)
        {
            PlayerMgr.instance.Startpos = this.gameObject;
            PlayerMgr.instance.Spawnplayer();
        }
    }
}
