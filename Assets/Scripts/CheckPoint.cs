using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private ManageGame _manageGame;
    public Transform _respawnPoint;

    private SpriteRenderer _spriteRenderer;
    public Sprite _passive;
    public Sprite _active;
    private Collider2D _collider;

    private AudioManager _audio;

    private void Awake()
    {
        _manageGame = GameObject.FindGameObjectWithTag("Player").GetComponent<ManageGame>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        _spriteRenderer.sprite = _passive;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        _audio.PlaySFX(_audio._checkPoint);
        _manageGame.UpdateCheckPoint(_respawnPoint.position);
        _spriteRenderer.sprite = _active;
        _collider.enabled = false;
    }
}
