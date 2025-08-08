using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Colossal
{
    public class Foot : ICollider
    {
        public BoxCollider2D Collider { get; set; }

        public Foot(BoxCollider2D Collider)
        {
            this.Collider = Collider;
        }
       
    }
    public class DetectionZone : ICollider
    {
        public BoxCollider2D Collider { get; set; }
        public DetectionZone(BoxCollider2D Collider)
        {
            this.Collider= Collider;
        }
        
    }
    public class HitBox : ICollider
    {
        public BoxCollider2D Collider { get; set; }
        public HitBox(BoxCollider2D Collider)
        {
            this.Collider = Collider;
        }
       
    }


}


