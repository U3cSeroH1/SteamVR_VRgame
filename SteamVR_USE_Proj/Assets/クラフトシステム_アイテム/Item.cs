using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



#if UNITY_EDITOR
using UnityEditor;
#endif


[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class Item : ScriptableObject
{

    public enum KindOfItem
    {
        Weapon,
        UseableItem,
        StorageItem,

    }


    //　アイテムの種類
    [SerializeField]
    private KindOfItem kindOfItem;
    //　アイテムのアイコン
    [SerializeField]
    private GameObject gameObject;
    //　アイテムの名前
    [SerializeField]
    private string itemName;
    //　アイテムの情報
    [SerializeField]
    private string information;

    //アイテムのレシピ
    //[HideInInspector]
    public List<Item> Recipe = new List<Item>();


    public KindOfItem GetKindOfItem()
    {
        return kindOfItem;
    }

    public GameObject GetgameObject()
    {
        return gameObject;
    }

    public string GetItemName()
    {
        return itemName;
    }

    public string GetInformation()
    {
        return information;
    }




#if UNITY_EDITOR
    //-------------------------------------------------------------------------
    [UnityEditor.CustomEditor(typeof(Item))]
    public class UIElementEditor : UnityEditor.Editor
    {
        //-------------------------------------------------
        // Custom Inspector GUI allows us to click from within the UI
        //-------------------------------------------------
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Item item = (Item)target;


        }
    }
#endif

}