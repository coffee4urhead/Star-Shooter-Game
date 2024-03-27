using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speedOfPowerup = 3f;
    [SerializeField]
    private int _powerupId;
    [SerializeField]
    private AudioClip _audioSrc;

    void Update()
    {
        transform.Translate(Time.deltaTime * _speedOfPowerup * Vector3.down);
        // float positionOnThex = transform.position.x;
        float positionOnthey = transform.position.y;

        if (positionOnthey < -3.7f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D otherGameObject)
    {
        if (otherGameObject.tag == "Player")
        {
            Player player = otherGameObject.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_audioSrc, transform.position);

            if (player != null)
            {
                if (_powerupId == 0)
                {
                    player.TripleShotActive();
                }
                else if (_powerupId == 1)
                {
                    player.SpeedBoostActive();
                }
                else if (_powerupId == 2)
                {
                    player.ActivateShield();
                }
            }
            Destroy(this.gameObject);
        }
    }
}
