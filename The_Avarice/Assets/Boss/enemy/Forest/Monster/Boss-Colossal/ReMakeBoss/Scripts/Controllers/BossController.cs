using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform BossTransform;
    public BossStatus status;
    public BossStateMachine stateMachine;
    public BossStage stage;
    public MonsterAniController aniController;
    public MsDetectionRange DetectionRange;
    public BossSkillGroup BossSkillGroup;
    public SpriteRenderer SpriteRenderer;
    public BoxCollider2D Collider2D;
   
}
