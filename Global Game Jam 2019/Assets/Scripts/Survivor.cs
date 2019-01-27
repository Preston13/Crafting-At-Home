using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Survivor : MonoBehaviour
{
    /// <summary>
    /// This will control aspects of the player that is not movement
    /// </summary>

    public float maxHealth = 100f;
    public float curHealth;
    public Slider healthBar;
    public bool inHome = true;
    public Camera mainCam;
    public Camera houseCam;
    public TopDownCam cameraScript;

    private bool hasGasMaskBottom = false;
    private bool hasGasMaskTop = false;
    public bool fullgasMask = false;
    public bool hasMeds = false;
    public bool hasFood = false;
    public bool hasConsole = false;

    public AudioClip doorClose;
    public AudioClip doorOpen;
    public AudioClip pickupSound;
    public AudioClip gameOverSound;
    public AudioClip VictorySound;

    public AudioClip getMaskSound;

    private AudioSource audioSource;

    public GameObject player;
    
    

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        houseCam.enabled = true;
        mainCam.enabled = false;
        audioSource = GetComponent<AudioSource>();
        
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

        if(curHealth <= 0)
        {
            SceneManager.LoadScene("Death");
        }
        
        if(hasMeds && hasFood && hasConsole && inHome)
        {
            SceneManager.LoadScene("Win");
        }
    }

    private void PlayPickupNoise()
    {
        audioSource.PlayOneShot(pickupSound, 1);
    }

    private void PlayDoorClose()
    {
        audioSource.PlayOneShot(doorClose, 1);
    }

    private void PlayDoorOpen()
    {
        audioSource.PlayOneShot(doorOpen, 1);
    }

    private void PlayGotMask()
    {
        audioSource.PlayOneShot(getMaskSound, 1);
    }


    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.name == "pixel house")
        {
            PlayDoorOpen();
            WarpSurvivorHome();
        }

        if (coll.name == "HouseExit")
        {
            PlayDoorClose();
            WarpSurvivorOut();
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
            if (hasGasMaskBottom && hasGasMaskTop) {
                PlayGotMask();
            } else {
                PlayPickupNoise();
            }
        }
        if (pup.itemType == PickUp.eType.gasMaskTop)
        {
            PlayPickupNoise();
            hasGasMaskTop = true;
            Destroy(coll.gameObject);
        if (hasGasMaskBottom && hasGasMaskTop) {
                PlayGotMask();
            } else {
                PlayPickupNoise();
            }
        }
        if (pup.itemType == PickUp.eType.food)
        {
            hasFood = true;
            Destroy(coll.gameObject);
        }
        if (pup.itemType == PickUp.eType.meds)
        {
            hasMeds = true;
            Destroy(coll.gameObject);
        }
        if (pup.itemType == PickUp.eType.console)
        {
            hasConsole = true;
            Destroy(coll.gameObject);
        }
    }

    private void WarpSurvivorHome()
    {
        houseCam.enabled = true;
        mainCam.enabled = false;
        inHome = true;
        player.transform.position = new Vector3(314, 150, 0);
    }

    private void WarpSurvivorOut()
    {
        inHome = false;
        player.transform.position = new Vector3(55, 5.2f, 0);
        Invoke("EnableCamera", 1);
    }

    void EnableCamera()
    {
        mainCam.enabled = true;
        houseCam.enabled = false;
    }
}
