using System.Collections;
using System.Collections.Generic;
using GraphicTree;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

namespace HSMTree
{
    public class HsmDrawDataAddParameter : HsmDrawParameterBase
    {
        public override void Draw(NodeParameter hSMParameter)
        {
            EditorGUILayout.BeginVertical();
            {
                DrawParameterType(hSMParameter);
                EditorGUILayout.BeginHorizontal();
                {
                    DrawParameterName(hSMParameter);
                    DrawParameterValue(hSMParameter);
                }
                EditorGUILayout.EndHorizontal();
                DrawParameterCnName(hSMParameter);
            }
            EditorGUILayout.EndVertical();
        }

        protected override void DrawParameterName(NodeParameter hSMParameter)
        {
            string oldName = hSMParameter.parameterName;
            hSMParameter.parameterName = EditorGUILayout.TextField("英文:", hSMParameter.parameterName);
            if (oldName.CompareTo(hSMParameter.parameterName) != 0)
            {
                bool isNumOrAlp = IsNumOrAlp(hSMParameter.parameterName);
                if (!isNumOrAlp)
                {
                    string msg = string.Format("参数名只能包含:数字、字母、下划线，且数字不能放在第一个字符位置");
                    HSMNodeWindow.window.ShowNotification(msg);
                    hSMParameter.parameterName = oldName;
                }
            }
        }

        protected override void DrawParameterCnName(NodeParameter hSMParameter)
        {
            hSMParameter.CNName = EditorGUILayout.TextField("中文", hSMParameter.CNName);
        }

        private bool IsNumOrAlp(string str)
        {
            string pattern = @"^[a-zA-Z_][A-Za-z0-9_]*$";
            Match match = Regex.Match(str, pattern);
            return match.Success;
        }


    }

}
