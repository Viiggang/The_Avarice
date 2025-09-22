using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStage : MonoBehaviour
{
    [Leein.InspectorName("현재 스테이지")]public Stage bossStage;
    private void OnEnable()
    {
        bossStage = Stage.Stage1;
    }
}
