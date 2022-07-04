using UnityEngine;
using UnityEditor;

namespace HSMTree
{

    public class HSMFileHandleController
    {

        public HSMFileHandleController()
        {
        }

        public void OnDestroy()
        {

        }

        public void OnGUI()
        {
            EditorGUILayout.BeginVertical("box");
            {
                DrawBtn();
                DrawFileName();
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawBtn()
        {
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("选择文件"))
                {
                    HsmConfigFileLoad.LoadFile();
                }

                if (GUILayout.Button("保存"))
                {
                    HsmConfigFileSave.CreateSaveFile();
                    AssetDatabase.Refresh();
                }

                if (GUILayout.Button("删除"))
                {
                    HsmConfigFileDelete.DeleteFile();
                    AssetDatabase.Refresh();
                }

                if (GUILayout.Button("批量更新"))
                {
                    HsmConfigFileUpdate.UpdateAllFile();
                    AssetDatabase.Refresh();
                }

                if (GUILayout.Button("批量合并"))
                {
                    HsmConfigFileMerge.BeatchMergeAllFile();
                    AssetDatabase.Refresh();
                }
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);
        }

        private void DrawFileName()
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("文件名", GUILayout.Width(80));
                HsmDataController.Instance.FileName = EditorGUILayout.TextField(HsmDataController.Instance.FileName);
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);
        }
    }
}
