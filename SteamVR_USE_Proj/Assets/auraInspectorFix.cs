using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

#endif

public class auraInspectorFix : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }








#if UNITY_EDITOR
    /**
     * Inspector拡張クラス
     */
    [CustomEditor(typeof(auraInspectorFix))]               //!< 拡張するときのお決まりとして書いてね
    public class auraInspectorFixEditor : Editor           //!< Editorを継承するよ！
    {
        //bool folding = false;

        public override void OnInspectorGUI()
        {
            // target は処理コードのインスタンスだよ！ 処理コードの型でキャストして使ってね！
            auraInspectorFix aIF = target as auraInspectorFix;

            /* -- カスタム表示 -- */

            // -- 体力 --
            EditorGUILayout.LabelField("a");
            EditorGUILayout.LabelField("a");
            EditorGUILayout.LabelField("a");
            EditorGUILayout.LabelField("a");
            EditorGUILayout.LabelField("a");
            EditorGUILayout.LabelField("a");
            EditorGUILayout.LabelField("a");
            EditorGUILayout.LabelField("a");
            EditorGUILayout.LabelField("a");
            EditorGUILayout.LabelField("a");

        }
    }
#endif

}
