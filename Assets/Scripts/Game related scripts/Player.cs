using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    // Private variables go with _ to indicate that specificity
    // [SerializeField is jut like [System.Serialize] where a private var becomes visible to the inspector]
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private float _speed = 12.3f;
    private float _speedMultiplier = 2f;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private Spawn_Manager _spawnManager;
    private bool _isTripleShotActive = false;
    private bool _isSpeedPowerupActive = false;
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject _laserTripleShotPrefab;
    [SerializeField]
    private GameObject _shiledArea;
    [SerializeField]
    private int _score;
    [SerializeField]
    private GameObject _engineRight;
    [SerializeField]
    private GameObject _engineLeft;
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _LaserSoundClip;
    private UIManager _uiManager;
    [SerializeField]

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _engineLeft.SetActive(false);
        _engineRight.SetActive(false);
        _audioSource = GetComponent<AudioSource>();
        if (_spawnManager == null)
        {
            Debug.LogError("The spawn manager wasnt found correctly!");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UI manger is null");
        }

        if (_audioSource == null)
        {
            Debug.LogError("The audio source was not found => it is null!");
        }
        else
        {
            _audioSource.clip = _LaserSoundClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerSpeedCalc();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0) && Time.time > _canFire)
        {
            FireLaser();
        }
    }
    void PlayerSpeedCalc()
    {
        //Time.deltaTime == 1 second
        float horizontalInput = Input.GetAxis("Horizontal");
        float getVerticalInput = Input.GetAxis("Vertical");
        Vector3 userPositionOnTheGrid = new Vector3(horizontalInput, getVerticalInput, 0);

        transform.Translate(userPositionOnTheGrid * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.7f, 0), 0);

        if (transform.position.x >= 8.8)
        {
            transform.position = new Vector3(-9.17f, transform.position.y, 0);
        }
        else if (transform.position.x <= -9.17)
        {
            transform.position = new Vector3(8.7f, transform.position.y, 0);
        }
    }
    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_isTripleShotActive)
        {
            Instantiate(_laserTripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laser, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        }
        _audioSource.Play();
    }

    public void Damage()
    {
        if (_isShieldActive)
        {
            _isShieldActive = false;
            _shiledArea.SetActive(false);
            return;
        }
        _lives -= 1;

        if (_lives == 2)
        {
            _engineRight.SetActive(true);
        }
        else if (_lives == 1)
        {
            _engineLeft.SetActive(true);
        }

        _uiManager.UpdateLivesCount(_lives);
        if (_lives < 1)
        {
            _spawnManager.StopSpawning();
            Destroy(this.gameObject, 0.5f);
            _uiManager.DisplayGameOver();
        }
    }
    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(Powerdown());
    }
    IEnumerator Powerdown()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }
    public void SpeedBoostActive()
    {
        _isSpeedPowerupActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerdown());
    }
    IEnumerator SpeedBoostPowerdown()
    {
        while (_isSpeedPowerupActive)
        {
            yield return new WaitForSeconds(5.0f);
            _isSpeedPowerupActive = false;
            _speed /= _speedMultiplier;
        }
    }
    public void ActivateShield()
    {
        _isShieldActive = true;
        _shiledArea.SetActive(true);
    }
    public void AddScorePoints(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
