using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    public GameObject objectToSpawn; // Objektet som ska spawnas
    public Camera mainCamera;        // Referens till huvudkameran
    public int numberOfObjects = 10; // Antal objekt att spawna

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            SpawnObjectAtRandomPosition();
        }
    }

    void SpawnObjectAtRandomPosition()
    {
        // Generera en slumpmässig position inom kamerans vy (X och Y mellan 0 och 1)
        Vector3 randomViewportPosition = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0);

        // Konvertera viewport-positionen till världens position
        Vector3 worldPosition = mainCamera.ViewportToWorldPoint(randomViewportPosition);

        // Sätt Z-positionen till 0 så att objektet syns i 2D
        worldPosition.z = 0f;

        // Spawna objektet på den slumpmässiga positionen
        Instantiate(objectToSpawn, worldPosition, Quaternion.identity);
    }
}
