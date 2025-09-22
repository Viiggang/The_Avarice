using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class BossSkillGroup : MonoBehaviour
{
    public Transform Scale;
    //페이즈1 사용스킬
    public List<BossSkill> bossSkills1=new List<BossSkill>();

    //페이즈2 사용스킬
    public List<BossSkill> bossSkills2 = new List<BossSkill>();
    
    //보스 스테이지 확인용
    public BossStage CurrentStage;

    public Vector3 localPos;

    #region 테스트
    public BossAttackCollision data;
    public BossSkill CurrentSkill;
    public SpriteRenderer flip;
    public BoxCollider2D box;
    public Transform Transform;
    int RandomValue = 0;
    public Collider2D colluders;
    public LayerMask p;
    #endregion
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(Transform.position.x + (CurrentSkill.CollisionData.offset.x * Transform.lossyScale.x),
                                        Transform.position.y + (CurrentSkill.CollisionData.offset.y * Transform.lossyScale.y),
                                        0f),

                            new Vector3((Transform.lossyScale.x * CurrentSkill.CollisionData.Size.x),
                                        (Transform.lossyScale.y * CurrentSkill.CollisionData.Size.y),
            0f));
    }
    private void Awake()
    {
        //스킬  전부 활성화
        foreach (var item in bossSkills1)
        {
            item.coolTime.Available = true;
        }
        foreach (var item in bossSkills2)
        {
            item.coolTime.Available = true;
        }
    }
    public BossSkill GetRandomSkill()
    {
        // 현재 페이즈의 스킬 리스트 선택
        List<BossSkill> currentSkills = CurrentStage.bossStage == Stage.Stage1 ? bossSkills1 : bossSkills2;

        // 쿨타임 가능한 스킬만 필터링
        List<BossSkill> availableSkills = currentSkills.Where(skill => skill.GetAvailable()).ToList();

        //직전에 사용한 스킬은 다시 선택
        if(CurrentSkill !=null)
        {
            availableSkills = availableSkills
           .Where(skill => skill != CurrentSkill)
           .ToList();
        }
        // 사용 가능한 스킬이 없다면 null 반환
        if (availableSkills.Count == 0) return null;

        // 랜덤 선택
        int randomIndex = Random.Range(0, availableSkills.Count);
        CurrentSkill = availableSkills[randomIndex];

        return CurrentSkill;
    }

    public void OnCoolTime()
    {
        StartCoroutine(CoolTimeRoutine(CurrentSkill));
    }

    private IEnumerator CoolTimeRoutine(BossSkill skill)
    {
        skill.coolTime.Available = false;
        yield return new WaitForSeconds(skill.coolTime.Time); 
        skill.coolTime.Available = true;
    }



}
