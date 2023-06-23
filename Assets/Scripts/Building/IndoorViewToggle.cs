using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndoorViewToggle : MonoBehaviour
{
    [SerializeField] private GameObject[] indoorView;
    [SerializeField] private GameObject[] outdoorView;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            SwapView(true);
            other.gameObject.GetComponent<CharacterController>().PlayerEnter();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            SwapView(false);
            other.gameObject.GetComponent<CharacterController>().PlayerExit();
        }
    }

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
