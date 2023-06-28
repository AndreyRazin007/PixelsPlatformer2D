using System.Collections;
using UnityEngine;

public class ManageGame : MonoBehaviour
{
    private Vector2 _checkPointPosition;
    private Rigidbody2D _playerRigidbody;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _checkPointPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    public void UpdateCheckPoint(Vector2 position)
    {
        _checkPointPosition = position;
    }

    private void Die()
    {
        StartCoroutine(Respawn(0.1f));
    }

    IEnumerator Respawn(float duration)
    {
        _playerRigidbody.simulated = false;
        _playerRigidbody.velocity = new Vector2(0, 0);

        transform.localScale = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(duration);

        transform.position = _checkPointPosition;
        transform.localScale = new Vector3(1, 1, 1);

        _playerRigidbody.simulated = true;
    }
}
