using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class DebugGui : MonoBehaviour
{
    public FpsCounter fpsCounter;

    public LogStorage logStorage;

    public DebugGuiStyle style;

    private GUIStyle textStyle;

    void Awake()
    {
        textStyle = GUIStyle.none;
        textStyle.wordWrap = false;
        textStyle.normal.textColor = Color.white;
        textStyle.margin = new RectOffset();
        textStyle.padding = new RectOffset();
    }

    void OnGUI()
    {
        int areaCount = 0;

        if (fpsCounter.enabled)
        {
            fpsCounter.Update();
            areaCount += 1;
        }
        if (logStorage.enabled)
        {
            logStorage.Update();
            areaCount += logStorage.list.Count;
        }

        if (0 < areaCount)
        {
            float padding = style.padding;
            int fontSize = style.fontSize;

            // box area
            // 左上に固定(直値は左上に固定するためのもの)
            float width = 300f;
            float height = (areaCount * Mathf.Ceil(fontSize * 1.1f)) + (padding * 2);
            Rect boxArea = new Rect(10f, 10f, width, height);
            GUI.Box(boxArea, "");

            // draw area
            Rect drawArea = new Rect(boxArea.x + padding, boxArea.y + padding, boxArea.width - padding * 2, boxArea.height - padding * 2);

            GUILayout.BeginArea(drawArea);
            {
                textStyle.fontSize = fontSize;

                if (fpsCounter.enabled)
                    GUILayout.Label("FPS:" + fpsCounter.fps.ToString("0.0") + "   Model : Total " + 1 + " Display " + 3, textStyle);

                if (logStorage.enabled)
                    foreach (string str in logStorage.list)
                        GUILayout.Label(str, textStyle);
            }
            GUILayout.EndArea();
        }
    }

    public void Log(string text)
    {
        if (logStorage.enabled == false) return;

        logStorage.Add("I", text);
    }

    public void Log(string format, params object[] paramList)
    {
        if (logStorage.enabled == false) return;

        logStorage.Add("I", format, paramList);
    }

    public void LogError(string text)
    {
        if (logStorage.enabled == false) return;

        logStorage.Add("E", text);
    }

    public void LogError(string format, params object[] paramList)
    {
        if (logStorage.enabled == false) return;

        logStorage.Add("E", format, paramList);
    }

    public void LogWarning(string text)
    {
        if (logStorage.enabled == false) return;

        logStorage.Add("W", text);
    }

    public void LogWarning(string format, params object[] paramList)
    {
        if (logStorage.enabled == false) return;

        logStorage.Add("W", format, paramList);
    }

    #region Inner Class

    [Serializable]
    public class DebugGuiStyle
    {
        public float padding = 4;
        public int fontSize = 13;
    }

    [Serializable]
    public class FpsCounter
    {
        public bool enabled = true;
        public float interval = 3.0f;

        internal float fps = 0.0f;

        private int frameCount = 0;
        private float prevTime = 0.0f;

        public void Update()
        {
            ++frameCount;
            float time = Time.realtimeSinceStartup - prevTime;

            if (interval <= time)
            {
                fps = frameCount / time;
                frameCount = 0;
                prevTime = Time.realtimeSinceStartup;
            }
        }
    }

    [Serializable]
    public class LogStorage
    {
        public bool enabled = true;
        public int max = 5;
        public int lengthLimit = 150;
        public bool alsoUnityLog = true;

        internal List<string> list = new List<string>();

        public void Update()
        {
            if (max < list.Count)
                list.RemoveRange(0, list.Count - max);
        }

        public void Add(string level, string text)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
            sb.Append(" [");
            sb.Append(level);
            sb.Append("] ");
            sb.Append(text.Substring(0, Math.Min(lengthLimit, text.Length)));

            string output = sb.ToString();
            
            // levelごとに色を変更
            if (level == "W") output = string.Format( "<color=yellow>{0}</color>",output);
            if (level == "E") output = string.Format( "<color=red>{0}</color>",output);

            list.Add(output);

            if (alsoUnityLog)
            {
                if (level == "I") Debug.Log(output);
                if (level == "W") Debug.LogWarning(output);
                if (level == "E") Debug.LogError(output);
            }
        }

        public void Add(string level, string format, params object[] paramList)
        {
            string text = string.Format(format, paramList);
            Add(level, text);
        }
    }

    #endregion
}