using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

namespace ElectricSphere
{

    public class EnemyElectric : MonoBehaviour, ITarget
    {
        public float moveSpeed;
        public MonsterAniController Electric;
        public const float chaseTime = 4f;
        public float Ypos = 1f;
        public IEnemyChase EnemyChase;
        public string attack;
        [SerializeField] private Transform Target;
        public Transform pos;
        public BossStage bosspage;
        public Transform target
        {
            get => Target;
            set => Target = value;
        }

      
        public bool OnChase;

        void Start()
        {
            if (Target == null)
            {
                Debug.LogError("Target이 할당되지 않았습니다!", this);
                return;
            }
            EnemyChase = new ElectChase();
            OnChase = true;
            pos.position = Target.position + new Vector3(0f, Ypos, 0f);
            StartCoroutine(ChaseLoop());
        }

        private IEnumerator ChaseLoop()
        {
            while (OnChase)
            {
                float elapsed = 0f;
                while (elapsed < chaseTime)//chaseTime 는 const float 4f
                {
                   
                    EnemyChase.Chase(pos,Target,moveSpeed);
                    elapsed += Time.deltaTime;
                    yield return null;
                }

                //공격
                if (bosspage.bossStage== Stage.Stage1)
                {
                    attack = "attack1";
                }
                else if(bosspage.bossStage == Stage.Stage2)
                {
                    attack = "attack2";
                }
                yield return new WaitForSeconds(1f);
                Electric.Play(attack);
                 
            }

            Debug.Log("추적 종료");
        }
        public void StopChase()
        {
            OnChase = false;
        }
    }
}
