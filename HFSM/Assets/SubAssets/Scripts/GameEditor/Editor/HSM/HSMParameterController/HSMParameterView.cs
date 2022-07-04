using System.Collections.Generic;
using UnityEngine;
using GraphicTree;
using UnityEditor;
using System;

namespace HSMTree
{
    public class HSMParameterView
    {
        private Vector2 scrollPos = Vector2.zero;
        private HsmDrawDataParameter hsmDrawDataParameter = new HsmDrawDataParameter();
        private HsmDrawDataAddParameter hsmDataAddParameter = new HsmDrawDataAddParameter();
        public void Draw(List<NodeParameter> parameterList)
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("全部变量");
                GUILayout.Space(50);
                if (GUILayout.Button("导入变量"))
                {
                    HsmConfigFileImportParameter.ImportParameter();
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical("box", GUILayout.ExpandWidth(true));
            {
                EditorGUILayout.LabelField("条件参数");
                scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandHeight(true));
                {
                    DrawAllParameter(parameterList);
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();

            GUILayout.Space(10);
            EditorGUILayout.BeginVertical("box");
            {
                DrawAddParameter();
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawAllParameter(List<NodeParameter> parameterList)
        {
            GUI.backgroundColor = new Color(0.85f, 0.85f, 0.85f, 1f);
            for (int i = 0; i < parameterList.Count; ++i)
            {
                NodeParameter hsmParameter = parameterList[i];

                EditorGUILayout.BeginVertical("box");
                {
                    hsmDrawDataParameter.SetDelCallBack(DelParameter);
                    hsmDrawDataParameter.Draw(hsmParameter);
                }
                EditorGUILayout.EndVertical();
            }
            GUI.backgroundColor = Color.white;
        }

        private void DelParameter(NodeParameter parameter)
        {
            DataTreeParameterHandle dataTreeParameterHandle = new DataTreeParameterHandle();
            dataTreeParameterHandle.DataTreeDelParameter(parameter);
        }

        private NodeParameter newAddParameter = new NodeParameter();
        private void DrawAddParameter()
        {
            if (null == newAddParameter)
            {
                newAddParameter = new NodeParameter();
            }

            EditorGUILayout.BeginVertical("box");
            {
                hsmDataAddParameter.Draw(newAddParameter);
            }
            EditorGUILayout.EndVertical();
            GUILayout.Space(5);

            if (GUILayout.Button("添加条件"))
            {
                DataTreeParameterHandle dataTreeParameterHandle = new DataTreeParameterHandle();
                dataTreeParameterHandle.DataTreeAddParameter(newAddParameter);
            }
        }

    }
}
