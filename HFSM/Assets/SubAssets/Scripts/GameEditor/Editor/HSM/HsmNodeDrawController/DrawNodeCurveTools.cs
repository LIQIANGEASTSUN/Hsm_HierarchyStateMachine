using GraphicTree;
using UnityEditor;
using UnityEngine;

namespace HSMTree
{
    public class DrawNodeCurveTools
    {
        // 绘制线
        public static void DrawNodeCurve(RectT start, RectT end, Color color)
        {
            Vector3 startPos = Vector3.zero;
            Vector3 endPos = Vector3.zero;
            Vector3 middle = (startPos + endPos) * 0.5f;
            Handles.color = Color.white;
            CalculateTranstion(start, end, ref startPos, ref endPos);
            DrawArrow(startPos, endPos, color);
        }

        private static void DrawArrow(Vector2 from, Vector2 to, Color color)
        {
            Handles.BeginGUI();
            Handles.color = color;
            Handles.DrawAAPolyLine(3, from, to);
            Vector2 v0 = from - to;
            v0 *= 10 / v0.magnitude;
            Vector2 v1 = new Vector2(v0.x * 0.866f - v0.y * 0.5f, v0.x * 0.5f + v0.y * 0.866f);
            Vector2 v2 = new Vector2(v0.x * 0.866f + v0.y * 0.5f, v0.x * -0.5f + v0.y * 0.866f);
            Vector2 middle = (from + to) * 0.5f;
            Handles.DrawAAPolyLine(5, middle + v1, middle, middle + v2);
            Handles.EndGUI();
        }

        public static void CalculateTranstion(RectT start, RectT end, ref Vector3 startPoint, ref Vector3 endPoint)
        {
            Vector3 startCenter = Vector3.zero;
            Vector3 endCenter = Vector3.zero;
            CalculateTransitionPoint(start, end, ref startCenter, ref endCenter);

            Vector3 axis = Vector3.Cross((endCenter - startCenter), new Vector3(0, 0, 1)).normalized;
            startPoint = startCenter + 10 * axis;
            endPoint = endCenter + 10 * axis;

            GUI.Box(new Rect(startPoint, Vector2.one * 10), "1");
            GUI.Box(new Rect(endPoint, Vector2.one * 10), "1");
        }

        private const float coefficient = 0.5f;
        private static void CalculateTransitionPoint(RectT start, RectT end, ref Vector3 startCenter, ref Vector3 endCenter)
        {
            startCenter = new Vector3(start.x + start.width * coefficient, start.y + start.height * coefficient);
            endCenter = new Vector3(end.x + end.width * coefficient, end.y + end.height * coefficient);
        }
    }
}
