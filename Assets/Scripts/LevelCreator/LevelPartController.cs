using UnityEngine;
using Random = UnityEngine.Random;

public class LevelPartController : MonoBehaviour
{
    /// <summary>
    /// Pre-made parts of the level
    /// </summary>
    [SerializeField] private LevelPart[] levelParts;

    [SerializeField] private Transform player;
    [SerializeField] private Transform startPoint;

    /// <summary>
    /// The distance at which new parts appear
    /// </summary>
    [SerializeField] private float partCreatingDistance;

    /// <summary>
    /// the distance at which previous parts are removed
    /// </summary>
    [SerializeField] private float partDeletingDistance=15;

    private Vector3 _nextPartPosition;

    private void Start()
    {
        _nextPartPosition = startPoint.position;
        LoadParts();
    }

    private void Update()
    {
        RemoveParts();
        LoadParts();
    }

    /// <summary>
    /// Place part of the level in front
    /// </summary>
    private void LoadParts()
    {
        if ((_nextPartPosition - player.position).x < partCreatingDistance)
        {
            LevelPart part = levelParts[Random.Range(0, levelParts.Length)];
            LevelPart newPart = Instantiate(part, transform);
            newPart.PlaceLevelPart(_nextPartPosition);
            _nextPartPosition = newPart.GetEndOfLevelPart().position;
        }
    }

    /// <summary>
    /// Removes parts of the level behind the character
    /// </summary>
    private void RemoveParts()
    {
        if (transform.childCount > 1)
        {
            Vector3 diff = player.transform.position - transform.GetChild(1).position;
            if (diff.x > partDeletingDistance)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
        }
    }
}