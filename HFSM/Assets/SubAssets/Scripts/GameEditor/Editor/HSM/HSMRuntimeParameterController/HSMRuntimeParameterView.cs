using System.Collections.Generic;
using UnityEngine;
using GraphicTree;
using UnityEditor;

namespace HSMTree
{
    public class HSMRuntimeParameterView
    {
        private Vector2 scrollPos = Vector2.zero;
        private HsmDrawDataParameter hsmDataParameter = new HsmDrawDataParameter();
        public void Draw(List<NodeParameter> parameterList)
        {
            EditorGUILayout.LabelField("运行时变量");

            EditorGUILayout.BeginVertical("box", GUILayout.ExpandWidth(true));
            {
                EditorGUILayout.LabelField("条件参数");
                scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandHeight(true));
                {
                    GUI.backgroundColor = new Color(0.85f, 0.85f, 0.85f, 1f);
                    for (int i = 0; i < parameterList.Count; ++i)
                    {
                        NodeParameter hSMParameter = parameterList[i];
                        EditorGUILayout.BeginVertical("box");
                        {
                            hsmDataParameter.Draw(hSMParameter);
                        }
                        EditorGUILayout.EndVertical();
                    }
                    GUI.backgroundColor = Color.white;
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();
        }

    }
}
