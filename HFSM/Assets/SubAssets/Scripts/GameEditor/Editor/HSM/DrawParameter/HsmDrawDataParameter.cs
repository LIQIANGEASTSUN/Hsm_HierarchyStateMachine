using System.Collections;
using System.Collections.Generic;
using GraphicTree;
using UnityEngine;
using UnityEditor;

namespace HSMTree
{
    public class HsmDrawDataParameter : HsmDrawParameterBase
    {
        public override void Draw(NodeParameter hSMParameter)
        {
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.BeginHorizontal();
                {
                    GUI.enabled = false;
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
                    GUI.enabled = false;
                    DrawParameterCnName(hSMParameter);
                    GUI.enabled = true;

                    GUI.enabled = ParameterValueEnableHandle();
                    DrawParameterValue(hSMParameter);
                    GUI.enabled = true;
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
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
