using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Player _playerScript;
    [SerializeField]
    private AudioSource _coinAudio;
    private Collider _collider;
    private Renderer _renderer;
    private Renderer _coinSparkleRender;

    void Start()
    {
        _playerScript = GameObject.Find("Player").GetComponent<Player>();
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();
        _coinSparkleRender = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>();
        if (_playerScript == null)
        {
            Debug.Log("Coin: Player Script is NULL");
        }
        if (_collider == null)
        {
            Debug.Log("Coin: Collider is NULL");
        }
        if (_renderer == null)
        {
            Debug.Log("Coin: Rendere is NULL");
        }
        if (_coinSparkleRender == null)
        {
            Debug.Log("Coin: Coin Sparkle is NULL");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _playerScript.GetCoin();
                _coinAudio.Play();
                _collider.enabled = false;
                _renderer.enabled = false;
                _coinSparkleRender.enabled = false;
                Destroy(this.gameObject, 2.5f);
            }
        }
    }
}
