using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAblity
{
   public string Name { get; set; }
   public float MaxHp { get; set; }
   public float Hp { get; set; }
}

public class ClossalAblity : IAblity
{
    [SerializeField] public string Name { get; set; } = "Clossal";
    [SerializeField] public float MaxHp { get; set; } = 100000f;
    [SerializeField] public float Hp { get; set; }

    public ClossalAblity()
    {
        Hp = MaxHp;

    }
}
