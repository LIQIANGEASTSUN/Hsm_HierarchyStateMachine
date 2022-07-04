using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphicTree;
using UnityEditor;

namespace HSMTree
{
    public class HsmDrawNodeParameter : HsmDrawParameterBase
    {

        public override void Draw(NodeParameter hSMParameter)
        {
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.BeginHorizontal();
                {
                    GUI.enabled = false;
                    DrawIndex(hSMParameter);
                    DrawParameterType(hSMParameter);
                    DrawParameterName(hSMParameter);
                    GUI.enabled = true;

                    GUI.enabled = DelEnableHandle();
                    DrawDelBtn(hSMParameter);
                    GUI.enabled = true;
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                {
                    GUI.enabled = ParameterValueEnableHandle();
                    DrawParameterSelect(hSMParameter);
                    DrawCompare(hSMParameter);
                    DrawParameterValue(hSMParameter);
                    GUI.enabled = true;
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawIndex(NodeParameter hSMParameter)
        {
            hSMParameter.index = EditorGUILayout.IntField(hSMParameter.index, GUILayout.Width(30));
        }

        protected void DrawParameterSelect(NodeParameter hSMParameter)
        {
            List<NodeParameter> parameterList = HsmDataController.Instance.HSMTreeData.parameterList;
            string[] parameterArr = new string[parameterList.Count];
            int index = -1;
            for (int i = 0; i < parameterList.Count; ++i)
            {
                NodeParameter p = parameterList[i];
                parameterArr[i] = p.CNName; //p.ParameterName;
                if (hSMParameter.parameterName.CompareTo(p.parameterName) == 0)
                {
                    index = i;
                }
            }

            int result = EditorGUILayout.Popup(index, parameterArr, GUILayout.ExpandWidth(true));
            if (result != index)
            {
                hSMParameter.parameterName = parameterList[result].parameterName; //parameterArr[result];
            }
        }

        private bool DelEnableHandle()
        {
            HSMPlayType playType = HsmDataController.Instance.PlayType;
            if (playType == HSMPlayType.PLAY || playType == HSMPlayType.PAUSE)
            {
                return false;
            }
            return true;
        }

        private bool ParameterValueEnableHandle()
        {
            HSMPlayType playType = HsmDataController.Instance.PlayType;
            if (playType == HSMPlayType.PLAY || playType == HSMPlayType.PAUSE)
            {
                return false;
            }
            return true;
        }

    }
}

