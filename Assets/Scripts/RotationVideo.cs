using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationVideo : MonoBehaviour
{
    public float rotationSpeed = 5f; // настрой скорость поворота

    void Update()
    {
        if (Camera.main == null) return;

        // Направление до камеры
        Vector3 direction = Camera.main.transform.position - transform.position;

        // Желаемый поворот
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // Убираем наклон (обнуляем Z)
        Vector3 euler = targetRotation.eulerAngles;
        euler.z = 0f;
        targetRotation = Quaternion.Euler(euler);

        // Плавно поворачиваем к целевому повороту
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }


}
