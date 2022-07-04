using UnityEngine;
using UnityEditor;

namespace HSMTree
{
    public class HSMPlayView
    {
        private int option = 2;
        private readonly string[] optionArr = new string[] { "播放", "暂停", "停止" };
        public HSMPlayView()
        {

        }

        public void Draw()
        {
            EditorGUILayout.BeginHorizontal("box");
            {
                int index = option;
                option = GUILayout.Toolbar(option, optionArr, EditorStyles.toolbarButton);
                if (index != option)
                {
                    HsmDataController.hSMRuntimePlay((HSMPlayType)option);
                }

            }
            EditorGUILayout.EndHorizontal();
        }
    }
}