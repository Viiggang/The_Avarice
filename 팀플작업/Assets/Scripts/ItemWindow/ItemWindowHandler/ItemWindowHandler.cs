using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ItemWindow
{
    public class ItemWindowHandler : MonoBehaviour
    {
        public static ItemWindowHandler Instance;
        public List<Item> items = new List<Item>();
        public bool WeaponFlag = false;

        private void Awake()
        {
            // ΩÃ±€≈Ê ¿ŒΩ∫≈œΩ∫ º≥¡§
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            Instance = this;
        }
        private void Start()
        {
            var ItemAll = GetComponentsInChildren<Item>();
            foreach (var Item in ItemAll)
            {
                items.Add(Item);
            }

        }

    }
}