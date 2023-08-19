using UnityEngine;
using UnityEditor;
using HSMTree;

public class HSMManager
{
    private HSMDrawProperty _HSMDrawPropertyController;
    private HsmNodeAreaDrawController _nodeAreaDrawController;

    public HSMManager()
    {
        Init();
    }

    private void Init()
    {
        HsmDataController.Instance = new HsmDataController();
        HsmDataController.Instance.Init();
        _HSMDrawPropertyController = new HSMDrawProperty();
        _nodeAreaDrawController = new HsmNodeAreaDrawController();
        HSMRunTime.Instance.Init();
    }

    public void OnDestroy()
    {
        HsmDataController.Instance.Destroy();
        _nodeAreaDrawController.OnDestroy();
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
                _nodeAreaDrawController.OnGUI(windowRect);
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
    }

}
