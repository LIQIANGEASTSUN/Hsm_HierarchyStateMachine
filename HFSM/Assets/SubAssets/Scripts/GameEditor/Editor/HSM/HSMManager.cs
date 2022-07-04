using UnityEngine;
using UnityEditor;
using HSMTree;

public class HSMManager
{
    private HSMDrawProperty _HSMDrawPropertyController;
    private HsmNodeDrawController _nodeDrawController;

    public HSMManager()
    {
        Init();
    }

    private void Init()
    {
        HsmDataController.Instance = new HsmDataController();
        HsmDataController.Instance.Init();
        _HSMDrawPropertyController = new HSMDrawProperty();
        _nodeDrawController = new HsmNodeDrawController();
        HSMRunTime.Instance.Init();
    }

    public void OnDestroy()
    {
        HsmDataController.Instance.Destroy();
        _nodeDrawController.OnDestroy();
        _HSMDrawPropertyController.OnDestroy();
        HSMRunTime.Instance.OnDestroy();

        AssetDatabase.Refresh();
        UnityEngine.Caching.ClearCache();
    }

    public void Update()
    {
        HSMRunTime.Instance.Update();
    }

    public void OnGUI(Rect windowRect)
    {
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.BeginVertical("box", GUILayout.Width(350), GUILayout.ExpandHeight(true));
            {
                _HSMDrawPropertyController.OnGUI();
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            {
                _nodeDrawController.OnGUI(windowRect);
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
    }

}
