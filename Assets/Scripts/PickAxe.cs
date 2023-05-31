using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAxe : MonoBehaviour
{
    private Animation animationC;
    private BoxCollider2D pickCollider;

    bool canSwing = true;
    float coolDownTime = 0;

    private void Start()
    {
        animationC = GetComponentInParent<Animation>();
        pickCollider = GetComponent<BoxCollider2D>();

        pickCollider.enabled = false;
    }

    public void Swing()
    {
        if(!canSwing){return;}
        pickCollider.enabled = true;
        animationC.Play();
        coolDownTime = .7f;
        canSwing = false;
        
    }
    void FixedUpdate()
    {
        coolDownTime -= Time.deltaTime;
        if(coolDownTime <= 0)
        {
            canSwing = true;
            pickCollider.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.GetComponent<IDamagable>() != null)
        {
            col.GetComponent<IDamagable>()?.TakeDamage(4);
            EffectManager.Instance.PlayEffect(col.transform.position,0,1,Color.grey);
        }
    }
}
