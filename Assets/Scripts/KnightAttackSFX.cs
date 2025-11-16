using Metroidvania.InputSystem;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class KnightAttackSFX : MonoBehaviour
{
    [Header("Som de ataque da espada")]
    [SerializeField] private AudioClip swordClip;
    [SerializeField] [Range(0f, 1f)] private float volume = 1f;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        
        InputReader.instance.AttackEvent += OnAttack;
    }

    private void OnDisable()
    {
        if (InputReader.instance != null)
            InputReader.instance.AttackEvent -= OnAttack;
    }

    private void OnAttack()
    {
        if (_audioSource != null && swordClip != null)
        {
            _audioSource.PlayOneShot(swordClip, volume);
        }
    }
}
