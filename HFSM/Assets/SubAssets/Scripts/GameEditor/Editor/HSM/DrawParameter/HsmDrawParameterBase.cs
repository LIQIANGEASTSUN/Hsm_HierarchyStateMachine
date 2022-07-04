using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphicTree;
using UnityEditor;
using System;

namespace HSMTree
{
    public abstract class HsmDrawParameterBase
    {
        protected Action<NodeParameter> _delCallBack;

        public abstract void Draw(NodeParameter hSMParameter);

        protected void DrawParameterType(NodeParameter hSMParameter)
        {
            string[] parameterNameArr = EnumNames.GetEnumNames<ParameterType>();
            int index = EnumNames.GetEnumIndex<ParameterType>((ParameterType)(hSMParameter.parameterType));
            index = EditorGUILayout.Popup(index, parameterNameArr, GUILayout.ExpandWidth(true));
            hSMParameter.parameterType = (int)EnumNames.GetEnum<ParameterType>(index);
        }

        public void SetDelCallBack(Action<NodeParameter> callBack)
        {
            _delCallBack = callBack;
        }

        protected void DrawDelBtn(NodeParameter hSMParameter)
        {
            if (GUILayout.Button("删除", GUILayout.Width(45)))
            {
                _delCallBack?.Invoke(hSMParameter);
            }
        }

        protected virtual void DrawParameterName(NodeParameter hSMParameter)
        {
            hSMParameter.parameterName = EditorGUILayout.TextField(hSMParameter.parameterName);
        }

        protected virtual void DrawParameterCnName(NodeParameter hSMParameter)
        {
            hSMParameter.CNName = EditorGUILayout.TextField(hSMParameter.CNName);
        }

        protected virtual void DrawParameterValue(NodeParameter hSMParameter)
        {
            if (hSMParameter.parameterType == (int)ParameterType.Int)
            {
                hSMParameter.intValue = EditorGUILayout.IntField(hSMParameter.intValue, GUILayout.Width(50));
            }

            if (hSMParameter.parameterType == (int)ParameterType.Long)
            {
                hSMParameter.longValue = EditorGUILayout.LongField(hSMParameter.longValue, GUILayout.Width(50));
            }

            if (hSMParameter.parameterType == (int)ParameterType.Float)
            {
                hSMParameter.floatValue = EditorGUILayout.FloatField(hSMParameter.floatValue, GUILayout.Width(50));
            }

            if (hSMParameter.parameterType == (int)ParameterType.Bool)
            {
                hSMParameter.boolValue = EditorGUILayout.Toggle(hSMParameter.boolValue, GUILayout.Width(50));
            }

            if (hSMParameter.parameterType == (int)ParameterType.String)
            {
                hSMParameter.stringValue = EditorGUILayout.TextField(hSMParameter.stringValue, GUILayout.Width(50));
            }
        }

        protected virtual void DrawCompare(NodeParameter hSMParameter)
        {
            ParameterCompare[] compareEnumArr = new ParameterCompare[] { };
            if (hSMParameter.parameterType == (int)ParameterType.Int)
            {
                compareEnumArr = new ParameterCompare[] { ParameterCompare.GREATER, ParameterCompare.GREATER_EQUALS, ParameterCompare.LESS_EQUAL, ParameterCompare.LESS, ParameterCompare.EQUALS, ParameterCompare.NOT_EQUAL };
            }
            if (hSMParameter.parameterType == (int)ParameterType.Float)
            {
                compareEnumArr = new ParameterCompare[] { ParameterCompare.GREATER, ParameterCompare.LESS };
            }
            if (hSMParameter.parameterType == (int)ParameterType.Bool)
            {
                compareEnumArr = new ParameterCompare[] { ParameterCompare.EQUALS, ParameterCompare.NOT_EQUAL };
            }

            string[] compareArr = new string[compareEnumArr.Length];
            int compareIndex = (int)compareEnumArr[0];
            for (int i = 0; i < compareEnumArr.Length; ++i)
            {
                int index = EnumNames.GetEnumIndex<ParameterCompare>(compareEnumArr[i]);
                compareArr[i] = EnumNames.GetEnumName<ParameterCompare>(index);
                if ((ParameterCompare)hSMParameter.compare == compareEnumArr[i])
                {
                    compareIndex = i;
                }
            }

            //GUI.enabled = (hSMParameter.parameterType != (int)ParameterType.Bool);
            compareIndex = EditorGUILayout.Popup(compareIndex, compareArr, GUILayout.Width(75));
            hSMParameter.compare = (int)(compareEnumArr[compareIndex]);
            //GUI.enabled = true;
        }

    }

}
