using System.Collections.Generic;
using System.IO;
using UnityEngine;



#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ScreenPocket
{

#if UNITY_EDITOR
    public class TextureUtility
    {

        [MenuItem("Assets/ScreenPocket/Texture/Create")]
        static void Create()
        {
            OpenCreateTextureWindow((w, h, col) =>
            {
                var icon = EditorGUIUtility.FindTexture("Texture2D Icon");
                ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, new DoCreateTexture() { width = w, height = h, color = col }, "New Texture.png", icon, null);
            });
        }

        static void OpenCreateTextureWindow(System.Action<int, int, Color> callback)
        {
            var w = EditorWindow.GetWindow<CreateTextureInformationWindow>(true, "テクスチャ作成");
            w.callback = callback;
            w.position = new Rect(150f, 150f, 280f, 100f);
        }

        public static void Fill(Texture2D texture, Color color)
        {
            for (int y = 0; y < texture.height; ++y)
            {
                for (int x = 0; x < texture.width; ++x)
                {
                    texture.SetPixel(x, y, color);
                }
            }
            texture.Apply();
        }
    }

    class DoCreateTexture : UnityEditor.ProjectWindowCallback.EndNameEditAction
    {
        public int width;
        public int height;
        public Color color;
        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            var texture = new Texture2D(width, height);
            texture.name = Path.GetFileNameWithoutExtension(pathName);
            TextureUtility.Fill(texture, color);
            byte[] bytes = texture.EncodeToPNG();
            DestroyImmediate(texture);
            File.WriteAllBytes(pathName, bytes);
            AssetDatabase.Refresh();
        }
    }


    public class CreateTextureInformationWindow : EditorWindow
    {
        int w = 0;
        int h = 0;

        TextureImporterNPOTScale npotW = TextureImporterNPOTScale.None;
        TextureImporterNPOTScale npotH = TextureImporterNPOTScale.None;

        Color color = Color.white;

        public System.Action<int, int, Color> callback = null;
        void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            w = EditorGUILayout.IntField(w);
            EditorGUILayout.LabelField("x", GUILayout.Width(12f));
            h = EditorGUILayout.IntField(h);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUI.enabled = !(Mathf.IsPowerOfTwo(w));
            npotW = (TextureImporterNPOTScale)EditorGUILayout.EnumPopup(npotW);
            GUI.enabled = !(Mathf.IsPowerOfTwo(h));
            npotH = (TextureImporterNPOTScale)EditorGUILayout.EnumPopup(npotH);
            EditorGUILayout.EndHorizontal();

            int finalW = w;
            int finalH = h;

            switch (npotW)
            {
                case TextureImporterNPOTScale.ToNearest:
                    finalW = Mathf.ClosestPowerOfTwo(finalW);
                    break;
                case TextureImporterNPOTScale.ToLarger:
                    finalW = Mathf.NextPowerOfTwo(finalW);
                    break;
                case TextureImporterNPOTScale.ToSmaller:
                    finalW = Mathf.NextPowerOfTwo(finalW) / 2;
                    break;
            }

            switch (npotH)
            {
                case TextureImporterNPOTScale.ToNearest:
                    finalH = Mathf.ClosestPowerOfTwo(finalH);
                    break;
                case TextureImporterNPOTScale.ToLarger:
                    finalH = Mathf.NextPowerOfTwo(finalH);
                    break;
                case TextureImporterNPOTScale.ToSmaller:
                    finalH = Mathf.NextPowerOfTwo(finalH) / 2;
                    break;
            }

            color = EditorGUILayout.ColorField(color);
            var isValidSize = !(finalW == 0 || finalH == 0);
            string msg = "サイズが不正なため、テクスチャを作成できません！";
            if (isValidSize)
            {
                msg = finalW.ToString() + "x" + finalH.ToString() + " のテクスチャを作成します";
            }
            EditorGUILayout.LabelField(msg);
            GUI.enabled = isValidSize;
            if (GUILayout.Button("Create"))
            {
                if (callback != null)
                {
                    callback(w, h, color);
                }
                Close();
            }
            GUI.enabled = true;
        }
    }


#endif
}