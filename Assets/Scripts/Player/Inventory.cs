using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IDataPersistance
{
    
    #region Singleton

    public static Inventory instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of inventory!");
            return;
        }
        instance = this;
    }

    #endregion

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip collectingClip;

    public List<Item> items;

    public int space = 20;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    private void Start()
    {
        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enough space!");
                return false;
            }

            items.Add(item);
            audioSource.PlayOneShot(collectingClip);
            if (onItemChangedCallBack != null)
            {
                onItemChangedCallBack.Invoke();
            }
        }

        return true;
    }
    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }

    public void LoadData(GameData data)
    {
        //this.items = data.items;
        //if (onItemChangedCallBack != null)
        //{
        //    onItemChangedCallBack.Invoke();
        //}

        for (int i = 0; i < data.items.Count; i++)
        {
            this.items.Add((Item)data.items[i]);
        }



    }

    public void SaveData(ref GameData data)
    {
        data.items = this.items;
    }
}
