using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float moveDuration = 1f; // Tidsl�ngd f�r varje r�relse
    public float moveSpeed = 5f;    // Hastigheten f�r objektet
    private Camera mainCamera;
    private SpriteRenderer spr;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(MoveContinuously());
        spr = GetComponent<SpriteRenderer>();
        spr.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

    }

    IEnumerator MoveContinuously()
    {
        while (true) // O�ndlig loop f�r att forts�tta r�relsen
        {
            // Generera en slumpm�ssig riktning
            Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

            // Starta klockan f�r r�relsen
            float elapsedTime = 0f;

            while (elapsedTime < moveDuration)
            {
                // Kolla om objektet kommer att vara inom kamerans vy
                Vector3 newPosition = transform.position + (Vector3)randomDirection * moveSpeed * Time.deltaTime;
                if (IsWithinCameraBounds(newPosition))
                {
                    transform.position = newPosition;
                }
                else
                {
                    // Avsluta r�relsen om objektet �r p� v�g att l�mna kamerans vy
                    break;
                }

                elapsedTime += Time.deltaTime;
                yield return null; // V�nta till n�sta frame
            }

            // Efter varje r�relse, v�nta kort innan n�sta
            yield return new WaitForSeconds(0.5f); // Valfri paus innan n�sta r�relse
        }
    }

    bool IsWithinCameraBounds(Vector3 position)
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(position);

        // Kolla om objektet �r inom kamerans vy (mellan 0 och 1 p� X och Y)
        return viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1;
    }
}