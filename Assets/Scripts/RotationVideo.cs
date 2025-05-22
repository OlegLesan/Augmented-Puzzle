using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationVideo : MonoBehaviour
{
    public float rotationSpeed = 5f; // ������� �������� ��������

    void Update()
    {
        if (Camera.main == null) return;

        // ����������� �� ������
        Vector3 direction = Camera.main.transform.position - transform.position;

        // �������� �������
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // ������� ������ (�������� Z)
        Vector3 euler = targetRotation.eulerAngles;
        euler.z = 0f;
        targetRotation = Quaternion.Euler(euler);

        // ������ ������������ � �������� ��������
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }


}
