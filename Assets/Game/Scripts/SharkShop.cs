using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player playerScript = other.GetComponent<Player>();
                if (playerScript.HasCoin())
                {
                    playerScript.RemoveCoin();
                    playerScript.EquipWeapon();
                    _audioSource.Play();
                }
            }
        }
    }
}
