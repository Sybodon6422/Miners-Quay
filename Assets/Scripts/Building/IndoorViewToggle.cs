using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndoorViewToggle : MonoBehaviour
{
    [SerializeField] private GameObject indoorView;
    [SerializeField] private GameObject outdoorView;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            indoorView.SetActive(true);
            outdoorView.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            indoorView.SetActive(false);
            outdoorView.SetActive(true);
        }
    }
}
