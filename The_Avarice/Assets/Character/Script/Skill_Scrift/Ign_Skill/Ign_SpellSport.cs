using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ign_SpellSport : MonoBehaviour
{
    public GameObject Fire;
    public GameObject Thunder;
    public GameObject Ice;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        ResetChildEffects();
        ActivateChildByType();
    }

    private void ResetChildEffects()
    {
        if (Fire != null) 
            Fire.SetActive(false);

        if (Thunder != null) 
            Thunder.SetActive(false);

        if (Ice != null) 
            Ice.SetActive(false);
    }

    private void ActivateChildByType()
    {
        switch (PlayerMgr.instance.ElementType)
        {
            case Element_Type.Fire:
                if (Fire != null) Fire.SetActive(true);
                break;
            case Element_Type.Thunder:
                if (Thunder != null) Thunder.SetActive(true);
                break;
            case Element_Type.Ice:
                if (Ice != null) Ice.SetActive(true);
                break;
        }
    }



}
