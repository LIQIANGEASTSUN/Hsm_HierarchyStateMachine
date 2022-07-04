using UnityEditor;
using UnityEngine;

namespace HSMTree
{
    public class HsmConfigFileSelect
    {

        public static string SelectFile()
        {
            string path = HsmDataController.Instance.FilePath;
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            GUILayout.Space(8);

            string filePath = EditorUtility.OpenFilePanel("选择技能ID文件", path, "txt");
            return filePath;
        }
    }
}

