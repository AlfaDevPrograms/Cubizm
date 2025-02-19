using UnityEngine;

public class YandexGameAPI : MonoBehaviour
{
    // ����� ����������, ����� ���� ���������
    public void OnGameReady()
    {
        Debug.Log("Game is ready!");
        CallYandexAPI("onGameReady");
    }

    // ����� ����������, ����� ���������� ������� �������\
    public void OnGameStart()
    {
        Debug.Log("Game started!");
        CallYandexAPI("onGameStart");
    }

    // ����� ����������, ����� ���� �������� �� �����
    public void OnGamePause()
    {
        Debug.Log("Game paused!");
        CallYandexAPI("onGamePause");
    }

    // ����� ����������, ����� ���� �����������
    public void OnGameOver()
    {
        Debug.Log("Game over!");
        CallYandexAPI("onGameEnd");
    }

    private void CallYandexAPI(string methodName)
    {
#pragma warning disable CS0618 // ��� ��� ���� �������
        Application.ExternalCall($"yagames.{methodName}");
#pragma warning restore CS0618 // ��� ��� ���� �������
    }
}