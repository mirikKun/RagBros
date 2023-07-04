using UnityEngine;

public class LevelPart : MonoBehaviour
{
    [SerializeField] private Transform startOfLevelPart;
    [SerializeField] private Transform endOfLevelPart;

    /// <summary>
    /// Placing new part at the end of previous one
    /// </summary>
    /// <param name="lastEndOfLevelPart">Previous part end transform</param>
    public void PlaceLevelPart(Vector3 lastEndOfLevelPart)
    {
        var curTransform = transform;
        curTransform.position = lastEndOfLevelPart + (curTransform.position - startOfLevelPart.position);
    }

    public Transform GetEndOfLevelPart()
    {
        return endOfLevelPart;
    }
}