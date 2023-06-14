using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region singleton
    private static GameManager _instance;
    public static GameManager I { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    #endregion

    public GameObject itemWorldFAB;
    [SerializeField] private Transform itemHolder;
    public void SpawnItem(int itemID, Vector2 position)
    {
        var go = Instantiate(itemWorldFAB,position,Quaternion.identity);
        go.transform.parent = itemHolder;
    }

    public void SpawnItem(ItemSO item, int amount, Vector2 position)
    {
        for (int i = 0; i < amount; i++)
        {
            var go = Instantiate(itemWorldFAB,position,Quaternion.identity);
            go.transform.parent = itemHolder;
            go.GetComponent<WorldItem>().thisItem = item;
        }
    }
}
