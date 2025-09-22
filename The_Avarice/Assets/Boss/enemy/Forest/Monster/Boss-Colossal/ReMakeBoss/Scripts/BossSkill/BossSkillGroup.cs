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
    //private void OnDrawGizmos()
    //{
    //    //if (data == null || box == null || Scale == null) return;

    //    Gizmos.color = Color.red;

    //    // 1. ������ ��� (flipX ���)
    //    //Vector3 offset = new Vector3(
    //    //    flip != null && flip.flipX ? -data.offset.x : data.offset.x,
    //    //    data.offset.y,
    //    //    0f
    //    //);
    //    Gizmos.DrawWireCube(new Vector2(2.33f, -0.65f), new Vector2(3.21f,-1.95f));
    //  // 5. ����� �׸��� (2D �浹 ���� �ð�ȭ)
    //  //Gizmos.DrawWireCube(Transform.position+(new Vector3(flip.flipX? 
    //  //      -data.offset.x * Mathf.Abs(Transform.lossyScale.x):data.offset.x*Mathf.Abs(Transform.lossyScale.x),
    //  //       data.offset.y * Mathf.Abs(Transform.lossyScale.y),0f))
    //  //      , new Vector3(data.Size.x*transform.lossyScale.x, data.Size.y * transform.lossyScale.y,0f));

   
    //  //  var dat = Physics2D.OverlapBox(data.offset, data.Size, 0f, p);
    //  //  colluders = dat;
    //}

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
