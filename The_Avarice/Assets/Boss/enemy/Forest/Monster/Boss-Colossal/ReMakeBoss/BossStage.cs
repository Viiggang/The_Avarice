using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStage : MonoBehaviour
{
    [Leein.InspectorName("���� ��������")]public Stage bossStage;
    private void OnEnable()
    {
        bossStage = Stage.Stage1;
    }
}
