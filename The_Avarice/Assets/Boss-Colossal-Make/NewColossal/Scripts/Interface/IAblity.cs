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
    public string Name { get; set; } = "Clossal";
    public float MaxHp { get; set; } = 100000f;
    public float Hp { get; set; }

    public ClossalAblity()
    {
        Hp = MaxHp;

    }
}
