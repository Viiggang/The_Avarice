using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class BossSkillGroup : MonoBehaviour
{
    public Transform Scale;
    //������1 ��뽺ų
    public List<BossSkill> bossSkills1=new List<BossSkill>();

    //������2 ��뽺ų
    public List<BossSkill> bossSkills2 = new List<BossSkill>();
    
    //���� �������� Ȯ�ο�
    public BossStage CurrentStage;

    public Vector3 localPos;

    #region �׽�Ʈ
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
        //��ų  ���� Ȱ��ȭ
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
        // ���� �������� ��ų ����Ʈ ����
        List<BossSkill> currentSkills = CurrentStage.bossStage == Stage.Stage1 ? bossSkills1 : bossSkills2;

        // ��Ÿ�� ������ ��ų�� ���͸�
        List<BossSkill> availableSkills = currentSkills.Where(skill => skill.GetAvailable()).ToList();

        //������ ����� ��ų�� �ٽ� ����
        if(CurrentSkill !=null)
        {
            availableSkills = availableSkills
           .Where(skill => skill != CurrentSkill)
           .ToList();
        }
        // ��� ������ ��ų�� ���ٸ� null ��ȯ
        if (availableSkills.Count == 0) return null;

        // ���� ����
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
