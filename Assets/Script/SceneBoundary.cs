using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBoundary : MonoBehaviour
{
    public enum Type { TOP, BOTTOM, LEFT, RIGHT}
    [SerializeField] private Type boundaryType;

    public Type BoundaryType { get => boundaryType; }
}
