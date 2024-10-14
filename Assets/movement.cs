using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float moveDuration = 1f; // Tidslängd för varje rörelse
    public float moveSpeed = 5f;    // Hastigheten för objektet
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
        while (true) // Oändlig loop för att fortsätta rörelsen
        {
            // Generera en slumpmässig riktning
            Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

            // Starta klockan för rörelsen
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
                    // Avsluta rörelsen om objektet är på väg att lämna kamerans vy
                    break;
                }

                elapsedTime += Time.deltaTime;
                yield return null; // Vänta till nästa frame
            }

            // Efter varje rörelse, vänta kort innan nästa
            yield return new WaitForSeconds(0.5f); // Valfri paus innan nästa rörelse
        }
    }

    bool IsWithinCameraBounds(Vector3 position)
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(position);

        // Kolla om objektet är inom kamerans vy (mellan 0 och 1 på X och Y)
        return viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1;
    }
}