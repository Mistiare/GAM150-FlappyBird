using System.Collections;
using UnityEngine;
using UnityEngine.Events;

static class ExtensionMethods
{
    /// <summary>
    /// Rounds Vector3.
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="decimalPlaces"></param>
    /// <remarks>
    /// Sourced here: https://answers.unity.com/questions/589983/using-mathfround-for-a-vector3.html
    /// </remarks>
    /// <returns></returns>
    public static Vector3 Round(this Vector3 vector3, int decimalPlaces = 2)
    {
        float multiplier = 1;
        for (int i = 0; i < decimalPlaces; i++)
        {
            multiplier *= 10f;
        }
        return new Vector3(
            Mathf.Round(vector3.x * multiplier) / multiplier,
            Mathf.Round(vector3.y * multiplier) / multiplier,
            Mathf.Round(vector3.z * multiplier) / multiplier);
    }

    /// <summary>
    /// Rounds a Vector3 to the nearest integer.
    /// </summary>
    /// <param name="vector3"></param>
    /// <returns>
    /// A rounded vector3 to the nearest integer
    /// </returns>
    public static Vector3 RoundToInt(this Vector3 vector3)
    {
        return new Vector3(
            Mathf.RoundToInt(vector3.x),
            Mathf.RoundToInt(vector3.y),
            Mathf.RoundToInt(vector3.z)
            );
    }
}

[RequireComponent(typeof(Rigidbody))]
public class BossMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float moveSpeed;

    private Vector3 nextPosition;
    public Vector3 NextPosition
    {
        get { return nextPosition; }
        set
        {
            nextPosition = value;
            nextPosition.x = rigidBody.position.x;
        }
    }

    [SerializeField]
    private Vector3 maxRandomPosition;
    [SerializeField]
    private Vector3 minRandomPosition;

    private Rigidbody rigidBody;

    [Header("Timer Settings")]
    [SerializeField]
    private float movementDelay;

    [Header("Event Systems")]
    public UnityEvent onMove;

    private void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
        NextPosition = GetRandomPosition();

        StartCoroutine(MoveBoss(NextPosition));
    }

    /// <summary>
    /// A coroutine that infintie moves the boss and each time it does it
    /// gets a new random position to move to and will fire the attack when it
    /// reaches the destination and is waiting.
    /// </summary>
    /// <param name="newPosition"></param>
    /// <returns></returns>
    private IEnumerator MoveBoss(Vector3 newPosition)
    {
        while (!ComparePositions(transform.position, newPosition))
        {
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.fixedDeltaTime * moveSpeed);
            yield return new WaitForEndOfFrame();
        }

        onMove.Invoke();
        yield return new WaitForSeconds(movementDelay);

        NextPosition = GetRandomPosition();
        yield return MoveBoss(NextPosition);
    }

    private bool ComparePositions(Vector3 firstPosition, Vector3 secondposition)
    {
        firstPosition = firstPosition.RoundToInt();
        secondposition = secondposition.RoundToInt();

        if (Mathf.Approximately(firstPosition.y, secondposition.y)) { return true; }
        return false;
    }

    private Vector2 GetRandomPosition()
    {
        int randomX = Mathf.RoundToInt(Random.Range(minRandomPosition.x, maxRandomPosition.x));
        int randomY = Mathf.RoundToInt(Random.Range(minRandomPosition.y, maxRandomPosition.y));

        return new Vector3(randomX, randomY, 0);
    }
}
