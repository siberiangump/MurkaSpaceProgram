using UnityEngine;
using System.Collections;

[CreateAssetMenu (fileName = "ItemsDictionaryData", menuName = "OfferConfig/ScriptableObject/ItemsDictionaryData", order = 1231527)]
public class ItemsDictionaryData : ScriptableObject
{
    
    [SerializeField] ItemData[] Items;

    public GameObject GetGameObject(string id)
    {
        GameObject gmo = null;
        for (int i = 0; i < Items.Length; i++)
        {
            if(Items[i].Id == id)
                return Items[i].Item;
        }
        return gmo;
    }

    public GameObject GetGameObject(int id)
    {
        return GetGameObject(id.ToString());
    }

    [System.Serializable]
    public class ItemData
    {
        public string Id;
        public GameObject Item;
    }
}

