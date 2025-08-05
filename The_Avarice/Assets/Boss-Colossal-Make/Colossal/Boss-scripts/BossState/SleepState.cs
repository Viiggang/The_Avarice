using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class SleepState : IBossState
{
   
    public void Enter(BossController boss)
    {
        Debug.Log("SleepState Enter ½ÇÇà");

        InitBossCollider(boss);
        InitBossPos(boss);
    }

    public void Execute(BossController boss)
    {
        
    }

    public void Exit(BossController boss)
    {
        
    }


    private void InitBossCollider(BossController boss)
    {
        float Size_x = 0.6960176f;
        float Size_y = 0.04474938f;

        float xoffset = -0.06514633f;
        float yoffset = -0.3680703f;

        Vector2 m_size = new Vector2(Size_x, Size_y);
        Vector2 m_offset = new Vector2(xoffset, yoffset);

        boss.Collider.size = m_size;
        boss.Collider.offset = m_offset;
    }
    private void InitBossPos(BossController boss)
    {
        float posx = -0.516f;
        float posy = -0.1792696f;

        Vector2 mpos = new Vector2(posx, posy);


        boss.Boss_Pos.position = mpos;
    }
}
