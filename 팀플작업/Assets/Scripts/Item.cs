using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class Item : MonoBehaviour
{
    public ItemEnum itemenum;
    public equipmentEnum equipmentEnum;
    public Image itemImage;
    void init_item()
    {
        itemenum = ItemEnum.equipment;
        equipmentEnum = equipmentEnum.equipment_Weapon;
        itemImage= GetComponentsInChildren<Image>()
              .FirstOrDefault(img => img.gameObject != this.gameObject);
    }

    public void ItemUse()
    {
        Debug.Log("ItemUse ");
        if (itemenum == ItemEnum.NULL)//아이템이 없으면 
        {
            Debug.Log("아이템 없음");
            return;
        }
        
        if(itemenum == ItemEnum.equipment) //장비
        {
            Debug.Log("ItemEnum.equipment ");

        }
        else //소비
        {
            Debug.Log("Consumption_Heal ");
        }
     

    }
    // Start is called before the first frame update
    void Start()
    {
        init_item();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
