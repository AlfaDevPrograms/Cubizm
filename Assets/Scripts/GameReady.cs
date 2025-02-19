using UnityEngine;

public class GameReady : MonoBehaviour
{
    private void Start()
    {
        Application.ExternalCall("OnGameReady");
        Application.ExternalEval("OnGameReady");
    }
}