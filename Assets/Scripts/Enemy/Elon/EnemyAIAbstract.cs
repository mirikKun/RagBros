using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAIAbstract : MonoBehaviour
{
    public abstract IEnumerator Attack();
}
