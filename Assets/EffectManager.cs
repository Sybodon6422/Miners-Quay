using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private static EffectManager _instance;

    public static EffectManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public GameObject effectFAB;

    public void PlayEffect(Vector2 location, float angle, int effectID, Color effectColorOverRide)
    {
        var go = Instantiate(effectFAB,location,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
