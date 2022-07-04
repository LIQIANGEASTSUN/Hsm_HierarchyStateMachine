using UnityEngine;
using UnityEditor;

namespace HSMTree
{
    public enum OptionEnum
    {
        /// <summary>
        /// 详细描述
        /// </summary>
        [EnumAttirbute("详细描述")]
        Descript = 0,

        /// <summary>
        /// 节点面板
        /// </summary>
        [EnumAttirbute("节点面板")]
        NodeInspector = 1,

        /// <summary>
        /// 连线面板
        /// </summary>
        [EnumAttirbute("节点连线面板")]
        TransitionInspector = 2,

        /// <summary>
        /// 参数列表
        /// </summary>
        [EnumAttirbute("参数列表")]
        Parameter = 3,
    }

    public class HSMPropertyOption
    {
        private OptionEnum option = OptionEnum.NodeInspector;

        public OptionEnum OnGUI()
        {
            int index = EnumNames.GetEnumIndex<OptionEnum>(option);
            string[] optionArr = EnumNames.GetEnumNames<OptionEnum>();
            index = GUILayout.Toolbar(index, optionArr, EditorStyles.toolbarButton);
            option = EnumNames.GetEnum<OptionEnum>(index);
            return option;
        }

        public void OnDestroy()
        {

        }
    }
}
