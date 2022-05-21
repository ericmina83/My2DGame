using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool attacking = false;

    public Damage damage = new Damage();

    private int characterLayer;

    // Start is called before the first frame update
    void Awake()
    {
        characterLayer = LayerMask.NameToLayer("Character");

        damage.damageAmount = 30;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (attacking)
            if (other.gameObject.layer == characterLayer)
            {
                other.GetComponent<Character>().GetHit(damage);
                attacking = false;
            }
    }
}
