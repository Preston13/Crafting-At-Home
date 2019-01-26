using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survivor : MonoBehaviour
{
    /// <summary>
    /// This will control aspects of the player that is not movement
    /// </summary>

    public float maxHealth = 100f;
    public float curHealth;

    private bool hasGasMaskBottom = false;
    private bool hasGasMaskTop = false;
    private bool fullgasMask = false;
    public bool inHome = false;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasGasMaskBottom && hasGasMaskTop && inHome)
        {
            fullgasMask = true;
        }

        if (inHome == false && fullgasMask == false)
        {
            curHealth -= 1f;
        }
        else if(inHome == false && fullgasMask == true)
        {
            curHealth -= .5f;
        }
        else if (inHome == true && curHealth < maxHealth)
        {
            curHealth += 1f;
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.name == "pixel house")
        {
            inHome = true;
        }

        PickUp pup = coll.GetComponent<PickUp>();

        if(pup == null)
        {
            return;
        }

        if(pup.itemType == PickUp.eType.gasMaskBottom)
        {
            hasGasMaskBottom = true;
            Destroy(coll.gameObject);
        }
        if(pup.itemType == PickUp.eType.gasMaskTop)
        {
            hasGasMaskTop = true;
            Destroy(coll.gameObject);
        }
        

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "pixel house")
        {
            inHome = false;
        }
    }
}
