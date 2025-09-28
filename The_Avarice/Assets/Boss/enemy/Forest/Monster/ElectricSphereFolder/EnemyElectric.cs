using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

namespace ElectricSphere
{

    public class EnemyElectric : MonoBehaviour
    {
        public float moveSpeed;
        public MonsterAniController Electric;
        public const float chaseTime = 4f;
        public float Ypos = 1f;
        public IEnemyChase EnemyChase;
        public string attack;
        
        public Transform pos;
        public BossStage bosspage;
        public Transform target;


        [SerializeField] private LayerMask PlayerLayer;
        [SerializeField] private Vector2 Offset;
        [SerializeField] private Vector2 Size;
        private float Angle = 0f;
        private void OnEnable()
        {
            var Collider2D=Physics2D.OverlapBox(Offset, Size, Angle, PlayerLayer);
            if (!Collider2D) { Debug.Log("전기구체 플레이어 못찾음"); return; }
            target = Collider2D.gameObject.transform;
        }
        public bool OnChase;
        
        void Start()
        {
           
            EnemyChase = new ElectChase();
            OnChase = true;
            pos.position = target.position + new Vector3(0f, Ypos, 0f);
            StartCoroutine(ChaseLoop());
        }

        private IEnumerator ChaseLoop()
        {
            while (OnChase)
            {
                float elapsed = 0f;
                while (elapsed < chaseTime)//chaseTime 는 const float 4f
                {
                   
                    EnemyChase.Chase(pos, target, moveSpeed);
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
