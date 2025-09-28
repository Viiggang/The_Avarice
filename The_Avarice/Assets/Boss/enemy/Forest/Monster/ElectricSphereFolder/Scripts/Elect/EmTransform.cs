using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElectricSphere
{
    public class EmTransform : MonoBehaviour
    {
        public Transform myTransform;
        public Transform playertransform;
        public Transform ElectricPos;
        public Vector3 pos;
        public Vector3 Attackpos;
        public bool chase = false;
        public bool flag = false;
        private void OnDrawGizmos()
        {
            if (!chase) return;
            myTransform.position= playertransform.position+pos;
            ElectricPos.position= myTransform.transform.position+ Attackpos;
        }
        private void OnEnable()
        {
            int playerLayer = LayerMask.NameToLayer("Player");
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (var obj in allObjects)
            {
                if (obj.layer == playerLayer)
                {
                    playertransform = obj.transform;
                    Debug.Log("플레이어 발견: " + obj.name);
                }
            }
        }
        private void Start()
        {
            Invoke("OnEnable",2f);
        }
      

 
      
    }
}


