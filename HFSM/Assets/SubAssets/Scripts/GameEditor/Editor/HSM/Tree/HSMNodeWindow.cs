using UnityEngine;
using UnityEditor;
using System;

namespace HSMTree
{
    public class HSMNodeWindowOpen
    {
        [MenuItem("Window/CreateHSM")]
        public static void OpenWindow()
        {
            CloseWindow();
            HSMNodeWindow.ShowWindow();
        }

        public static void CloseWindow()
        {
            HSMNodeWindow.CloseWindow();
        }
    }

    public delegate void DrawWindowCallBack(Action callBack);
    public class HSMNodeWindow : EditorWindow
    {
        public static HSMNodeWindow window;
        private static Rect windowsPosition = new Rect(10, 30, 1236, 864);
        public static DrawWindowCallBack _drawWindowCallBack;

        private HSMManager _hsmManager;

        public static void ShowWindow()
        {
            window = EditorWindow.GetWindow<HSMNodeWindow>();
            window.position = windowsPosition;
            window.autoRepaintOnSceneChange = true;
            window.Show();
        }

        public static void CloseWindow()
        {
            if (null != window && window)
            {
                window.Close();
            }
        }

        private void OnEnable()
        {
            _hsmManager = new HSMManager();
            EditorApplication.update += OnFrame;
            _drawWindowCallBack += DrawWindow;
        }

        private void OnDisable()
        {
            _hsmManager.OnDestroy();
            EditorApplication.update -= OnFrame;
            _drawWindowCallBack -= DrawWindow;
        }

        private void OnFrame()
        {
            _hsmManager.Update();
        }

        private void OnGUI()
        {
            if (null == window)
            {
                return;
            }

            _hsmManager.OnGUI(window.position);
            Repaint();
        }

        private void DrawWindow(Action callBack)
        {
            // 开始绘制节点 
            // 注意：必须在  BeginWindows(); 和 EndWindows(); 之间 调用 GUI.Window 才能显示
            BeginWindows();
            {
                if (null != callBack)
                {
                    callBack();
                }
            }
            EndWindows();
        }

        public void ShowNotification(string meg)
        {
            ShowNotification(new GUIContent(meg));
            //RemoveNotification();
        }
    }
}
