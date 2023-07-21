using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndoorViewToggle : MonoBehaviour
{
    [SerializeField] private GameObject[] indoorView;
    [SerializeField] private GameObject[] outdoorView;
    private void SwapView(bool indoor)
    {
        foreach (GameObject obj in indoorView)
        {
            obj.SetActive(indoor);
        }
        foreach (GameObject obj in outdoorView)
        {
            obj.SetActive(!indoor);
        }
    }
}
