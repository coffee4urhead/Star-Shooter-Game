using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed;
    private Animator _animComponent;
    private Spawn_Manager _spawny;
    [SerializeField]
    private GameObject _explosionPrefab;
    private bool _isCollisionAcceptable = true;
    private float _nextCollisionTime = 3f;

    void Start()
    {
        _animComponent = GetComponent<Animator>();
        _spawny = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * _rotateSpeed);

        if (Time.time > _nextCollisionTime)
        {
            _nextCollisionTime += Time.time + 3.0f;
            _isCollisionAcceptable = true;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser" && _isCollisionAcceptable)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _animComponent.SetTrigger("OnExplosion");
            Destroy(collision.gameObject);
            _spawny.StartSpawning();
            Destroy(gameObject, 2.8f);
            _isCollisionAcceptable = false;
        }
    }
}
