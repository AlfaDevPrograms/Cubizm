using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public Transform target; // ����, ������ ������� ����� ��������� ������
    public float distance; // ���������� �� ������ �� ����
    public float height; // ������ ������
    public float speed; // �������� ��������

    private float currentAngle; // ������� ���� ��������

    private void Update()
    {
        if (target != null)
        {
            // �������� ���� ��������
            currentAngle += speed * Time.deltaTime;

            // ������������� ������� ������
            Vector3 offset = new(0, height, -distance);
            Quaternion rotation = Quaternion.Euler(0, currentAngle, 0); // ������� ������ Y
            transform.position = target.position + rotation * offset; // ������������� ������� ������
            transform.LookAt(target.position + Vector3.up * 0.5f); // ������ ������� �� ���� ������� ����
        }
    }
}