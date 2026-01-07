using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ign_SpellSport : MonoBehaviour
{
    [Header("- Normal_SKill")]
    public GameObject Fire;
    public GameObject Thunder;
    public GameObject Ice;
    [Space, Header("- Enhance_SKill")]
    public GameObject Fire2;
    public GameObject Thunder2;
    public GameObject Ice2;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        ResetChildEffects();
        if (PlayerMgr.instance.Ign_OnPassive)
            ActivateChildByType();
        else
            ActivateChilByType_Onpassive();
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

    private void ActivateChilByType_Onpassive()
    {
        switch (PlayerMgr.instance.ElementType)
        {
            case Element_Type.Fire:
                if (Fire != null) Fire2.SetActive(true);
                break;
            case Element_Type.Thunder:
                if (Thunder != null) Thunder2.SetActive(true);
                break;
            case Element_Type.Ice:
                if (Ice != null) Ice2.SetActive(true);
                break;
        }
    }



}
