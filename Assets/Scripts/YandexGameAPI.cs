using UnityEngine;

public class YandexGameAPI : MonoBehaviour
{
    // Метод вызывается, когда игра загружена
    public void OnGameReady()
    {
        Debug.Log("Game is ready!");
        CallYandexAPI("onGameReady");
    }

    // Метод вызывается, когда начинается игровой процесс\
    public void OnGameStart()
    {
        Debug.Log("Game started!");
        CallYandexAPI("onGameStart");
    }

    // Метод вызывается, когда игра ставится на паузу
    public void OnGamePause()
    {
        Debug.Log("Game paused!");
        CallYandexAPI("onGamePause");
    }

    // Метод вызывается, когда игра завершается
    public void OnGameOver()
    {
        Debug.Log("Game over!");
        CallYandexAPI("onGameEnd");
    }

    private void CallYandexAPI(string methodName)
    {
#pragma warning disable CS0618 // Тип или член устарел
        Application.ExternalCall($"yagames.{methodName}");
#pragma warning restore CS0618 // Тип или член устарел
    }
}