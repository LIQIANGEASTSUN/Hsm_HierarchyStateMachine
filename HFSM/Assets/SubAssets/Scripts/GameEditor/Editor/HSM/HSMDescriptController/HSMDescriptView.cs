using UnityEngine;
using UnityEditor;

namespace HSMTree
{
    public class HSMDescriptView
    {

        public void Draw(HsmConfigTreeData data)
        {
            Rect rect = GUILayoutUtility.GetRect(0f, 0, GUILayout.ExpandWidth(true));
            EditorGUILayout.BeginVertical("box");
            {
                data.descript = EditorGUILayout.TextArea(data.descript, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            }
            EditorGUILayout.EndVertical();
        }

    }
}