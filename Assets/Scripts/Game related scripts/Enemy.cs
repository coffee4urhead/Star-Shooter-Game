using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private Player _player;
    private Animator _animator;
    private AudioSource _audioSource;
    private bool _hasEntered = true;
    private float _collisionTime = 4.0f;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        if (_player == null)
        {
            Debug.LogError("The player is not found correctly!");
        }

        _animator = gameObject.GetComponent<Animator>();

        if (_animator == null)
        {
            Debug.LogError("The animator is not found correctly!");
        }
    }
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        float currentPositionOnXAxis = Random.Range(-10f, 10f);

        if (transform.position.y <= -3.7f)
        {
            transform.position = new Vector3(Mathf.Clamp(currentPositionOnXAxis, -8.6f, 8.7f), 9, 0);
        }

        if (_collisionTime > Time.time)
        {
            _collisionTime = Time.time + 3.0f;
            _hasEntered = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _hasEntered)
        {
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.5f);
            Player playerCollidet = other.transform.GetComponent<Player>();

            if (playerCollidet != null)
            {
                playerCollidet.Damage();
            }
            _hasEntered = false;
        }

        if (other.tag == "Laser" && _hasEntered)
        {
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.5f);
            if (_player != null)
            {
                _player.AddScorePoints(Random.Range(5, 10));
            }
            Destroy(other.gameObject);
            _hasEntered = false;
        }
    }
}
