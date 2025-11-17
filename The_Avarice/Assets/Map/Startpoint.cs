using UnityEngine;

public class Startpoint : OnScriptLoaded
{
    private void Awake()
    {
        StartCoroutine(OnScriptLoaded.WaitUntilActive<CameraManager>(() => {
            if (this.gameObject != null)
            {
                PlayerMgr.instance.Startpos = this.gameObject;
                PlayerMgr.instance.Spawnplayer();
            }
        }
        ));
    }
}
