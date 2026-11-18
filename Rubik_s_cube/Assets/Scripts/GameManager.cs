using UnityEngine;

public enum CubeType
{
    CUBE2x2,
    CUBE3x3
}

public class GameManager : MonoBehaviour
{
    static public CubeType cubeType = CubeType.CUBE2x2;
}
