using UnityEngine;
using System.Collections;

public interface Entity {

    GameObject gameObject { get; }

    Transform headTransform { get; }

    Transform feetTransform { get; }

    Vector3 getTargetLocation();

    string getEnemyTag();
}