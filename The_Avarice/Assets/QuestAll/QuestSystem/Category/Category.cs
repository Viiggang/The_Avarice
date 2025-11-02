using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(menuName = "category", fileName = "category_")]
    public class Category : ScriptableObject, IEquatable<Category>
    {
        [SerializeField] private string codeName; 
        [SerializeField] private string displayName; 

        public string CodeName => codeName;
        public string DisplayName => displayName;

        public override bool Equals(object other) => Equals(other as Category);


        public bool Equals(Category other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(other, this))
                return true;
            if (GetType() != other.GetType())
                return false;

            return codeName == other.CodeName;
        }


        public static bool operator == (Category lhs, string rhs)
        {
            if (lhs is null)
                return ReferenceEquals(rhs, null);
            return lhs.CodeName == rhs || lhs.DisplayName == rhs;
        }

        public static bool operator !=(Category lhs, string rhs) => !(lhs.codeName == rhs);

    }


}
