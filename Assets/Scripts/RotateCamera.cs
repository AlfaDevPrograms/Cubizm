using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public Transform target; // Цель, вокруг которой будет вращаться камера
    public float distance; // Расстояние от камеры до цели
    public float height; // Высота камеры
    public float speed; // Скорость вращения

    private float currentAngle; // Текущий угол вращения

    private void Update()
    {
        if (target != null)
        {
            // Получаем угол вращения
            currentAngle += speed * Time.deltaTime;

            // Устанавливаем позицию камеры
            Vector3 offset = new(0, height, -distance);
            Quaternion rotation = Quaternion.Euler(0, currentAngle, 0); // Поворот вокруг Y
            transform.position = target.position + rotation * offset; // Устанавливаем позицию камеры
            transform.LookAt(target.position + Vector3.up * 0.5f); // Камера смотрит на цель немного выше
        }
    }
}