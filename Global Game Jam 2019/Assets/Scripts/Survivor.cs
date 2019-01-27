using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Survivor : MonoBehaviour
{
    /// <summary>
    /// This will control aspects of the player that is not movement
    /// </summary>

    public float maxHealth = 100f;
    public float curHealth;
    public Slider healthBar;
    public bool inHome = false;

    private bool hasGasMaskBottom = false;
    private bool hasGasMaskTop = false;
    public bool fullgasMask = false;

    public GameObject player;
    
    

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

        if (inHome == false && fullgasMask == false && curHealth > 0)
        {
            curHealth -= 1f;
        }
        else if(inHome == false && fullgasMask == true && curHealth > 0)
        {
            curHealth -= .5f;
        }
        else if (inHome == true && curHealth < maxHealth)
        {
            curHealth += 1f;
        }

        healthBar.value = curHealth;
    }

    

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.name == "pixel house")
        {
            inHome = true;
            WarpSurvivorHome();
        }

        PickUp pup = coll.GetComponent<PickUp>();

        if (pup == null)
        {
            return;
        }

        if (pup.itemType == PickUp.eType.gasMaskBottom)
        {
            hasGasMaskBottom = true;
            Destroy(coll.gameObject);
        }
        if (pup.itemType == PickUp.eType.gasMaskTop)
        {
            hasGasMaskTop = true;
            Destroy(coll.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.name == "pixel house")
        {
            inHome = false;
        }
    }

    private void WarpSurvivorHome()
    {
        player.transform.position = new Vector3(315, 154, 0);
    }
}
