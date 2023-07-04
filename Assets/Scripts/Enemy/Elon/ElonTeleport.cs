using System.Collections;
using UnityEngine;


public class ElonTeleport : MonoBehaviour
{
    [SerializeField]private SpriteRenderer image;
    [SerializeField]private ParticleSystem particleSystem;
    private Collider2D _collider2D; 
    private Transform _transform;
    private float _startScale = 0.14f;
    private float _teleportationTime = 0.6f;
    private float _damageZoneTime=0.3f;
    private float _curTimer=0;

    private void Awake()
    {
        
        _transform = image.transform;
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(TeleportationEffect());

    }

    private IEnumerator TeleportationEffect()
    {        
        image.enabled = true;
        _curTimer = 0;
        while (_curTimer<=_teleportationTime)
        {
            _curTimer += Time.deltaTime;
            _transform.localScale = new Vector3(_curTimer / _teleportationTime * _startScale, _transform.localScale.y, 1);
            yield return null;
        }

        _collider2D.enabled = true;
        image.enabled = false;
        particleSystem.Play();
        yield return new WaitForSeconds(_damageZoneTime);
        _collider2D.enabled = false;

        gameObject.SetActive(false);
    }
}
