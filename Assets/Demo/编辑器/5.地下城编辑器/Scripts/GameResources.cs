using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.DungeonEditor
{
    public class GameResources : MonoBehaviour
    {
        private static GameResources instance;
        public static GameResources Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<GameResources>("GameResources");
                }
                return instance;
            }
        }

        //所有的房间类型
        public RoomNodeTypeListSO roomNodeTypeList;
    }
}