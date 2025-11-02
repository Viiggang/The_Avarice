using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QuestSystem
{
    public class QuestLibrary : MonoBehaviour
    {/*
      Resources 폴더에 있는 퀘스트 전부 가져와서 사용 할까말까
      순서를 얘로 1순위한다음에 그다음 하면 어떨까 말까좋을까 말까 
       
      */
        public List<Quest> quests = new List<Quest>();

        public Dictionary<string, Quest> QuestDic=new Dictionary<string, Quest>();
        private const string Path = "몰라 일단 저장해둠";
        private void Awake()
        {
            Quest[] questArray = Resources.LoadAll<Quest>(Path);
         
            QuestDic = questArray.ToDictionary(x => x.CodeName, x=>x);

        }

        public Quest QuestFind(string CodeName)
        {

            return QuestDic[CodeName];
        }
    }
}


