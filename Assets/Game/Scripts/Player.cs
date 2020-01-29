using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _characterController;
    [SerializeField]
    private float _speed = 3.5f;
    private float _gravtiy = 9.8f;
    [SerializeField]
    private GameObject _muzzleFlash;
    [SerializeField]
    private GameObject _hitMarkerPrefab;
    [SerializeField]
    private AudioSource _weaponAudio;
    private int currentAmmo;
    private int maxAmmo = 50;
    private bool isReloading = false;
    private UIManager _uiManager;
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.Log("Player: UI Manager is NULL");
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetMouseButton(0) && currentAmmo > 0)
        {
            Shoot();
            _uiManager.UpdateAmmo(currentAmmo);
        } 
        else 
        {
            _muzzleFlash.SetActive(false);
            _weaponAudio.Stop();
        }
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            isReloading = true;
            StartCoroutine(ReloadRoutine());
        }
        CalculateMovement();

    }

    void Shoot()
    {
        currentAmmo--;
            _muzzleFlash.SetActive(true);
            if (!_weaponAudio.isPlaying)
            {
                _weaponAudio.Play();
            }
            Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity))
            {
                Debug.Log("Hit: " + hitInfo.transform.name);
                GameObject hitMarker = (GameObject)Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(hitMarker, 1f);
            }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * _speed;
        velocity.y -= _gravtiy;
        velocity = transform.transform.TransformDirection(velocity);
        _characterController.Move(velocity * Time.deltaTime);
    }

    IEnumerator ReloadRoutine() 
    {
        yield return new WaitForSeconds(1.5f);
        currentAmmo = maxAmmo;
        _uiManager.UpdateAmmo(currentAmmo);
        isReloading= false;
    }
}
