using System.Collections;
using UnityEngine;

public class PortalTeleported : MonoBehaviour
{
    public Transform _destination;
    private GameObject _player;
    private Animation _animation;
    private Rigidbody2D _playerRigidbody;

    private AudioManager _audio;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _animation = _player.GetComponent<Animation>();
        _playerRigidbody = _player.GetComponent<Rigidbody2D>();
        _audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (Vector2.Distance(_player.transform.position, transform.position) > 0.3f)
            {
                StartCoroutine(Portal());
            }
        }
    }

    IEnumerator Portal()
    {
        _audio.PlaySFX(_audio._inPortal);
        _playerRigidbody.simulated = false;
        _animation.Play("Portal");
        StartCoroutine(MoveToPortal());
        yield return new WaitForSeconds(0.5f);
        _player.transform.position = _destination.position;
        _playerRigidbody.velocity = Vector2.zero;
        
        _animation.Play("Out");
        _audio.PlaySFX(_audio._outPortal);
        yield return new WaitForSeconds(0.5f);
        _playerRigidbody.simulated = true;
    }

    IEnumerator MoveToPortal()
    {
        float timer = 0f;

        while (timer < 0.5f)
        {
            _player.transform.position = Vector2.MoveTowards(_player.transform.position, transform.position, 3 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
    }
}
