using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BowAssemblyTracker : MonoBehaviour
{
    [Header("Bow Parts")]
    public Transform middlePart;    // The active, interactable middle part (base)
    public Transform upperPart;     // The upper part of the bow
    public Transform lowerPart;     // The lower part of the bow

    [Header("Attachment Settings")]
    public float attachDistanceUpper = 0.5f;  // Distance threshold for the upper part
    public float attachDistanceLower = 0.5f;  // Distance threshold for the lower part
    public float attachDistanceString = 0.5f; // Distance threshold for the string part

    [Header("Part Attachment Events")]
    public UnityEvent onUpperPartAttached;
    public UnityEvent onUpperPartInRange;
    public UnityEvent onUpperPartOutRange;
    [Space]
    public UnityEvent onLowerPartAttached;
    public UnityEvent onLowerPartInRange;
    public UnityEvent onLowerPartOutRange;

    // Internal flags to ensure each part is attached only once.
    private bool upperAttached = false;
    private bool lowerAttached = false;
    private bool stringAttached = false;
    private bool bowAssembled = false;

    // Internal flags to ensure each part is in range .
    private bool upperInRange = false;
    private bool lowerInRange = false;
    private bool stringInRange = false;


    private void Start()
    {
        upperAttached = false;
        lowerAttached = false;
        stringAttached = false;
        bowAssembled = false;

        upperInRange = false;
        lowerInRange = false;
        stringInRange = false;
    }


    void Update()
    {
        if (bowAssembled)
            return; // No further checking needed once the bow is fully assembled

        // Check the upper part
        if (!upperAttached && upperPart != null)
        {
            if (Vector3.Distance(middlePart.position, upperPart.position) <= attachDistanceUpper)
            {
                upperInRange = true;
                onUpperPartInRange?.Invoke();
            }
            else
            {
                upperInRange = false;
                onUpperPartOutRange?.Invoke();
            }

        }

        // Check the lower part
        if (!lowerAttached && lowerPart != null)
        {
            if (Vector3.Distance(middlePart.position, lowerPart.position) <= attachDistanceLower)
            {
                lowerInRange = true;
                onLowerPartInRange?.Invoke();
            }
            else
            {
                lowerInRange = false;
                onLowerPartOutRange?.Invoke();
            }

        }

        // Check if all parts are attached
        if (upperAttached && lowerAttached && stringAttached)
        {
            AssembleBow();
        }
    }


    // Method called when the upper part attaches
    public void AttachUpperPart()
    {
        if (upperInRange)
        {
            upperAttached = true;
            Debug.Log("Upper part attached.");
            onUpperPartAttached?.Invoke();
        }
    }

    // Method called when the lower part attaches
    public void AttachLowerPart()
    {
        if (lowerInRange)
        {
            lowerAttached = true;
            Debug.Log("Lower part attached.");
            onLowerPartAttached?.Invoke();
        }
    }

    // Method called when the string attaches
    public void AttachStringPart()
    {
        if (stringInRange)
        {
            stringAttached = true;
            Debug.Log("String attached.");
        }
    }

    // Method called when all parts are attached
    private void AssembleBow()
    {
        if (!bowAssembled)
        {
            bowAssembled = true;
            Debug.Log("Bow fully assembled.");
        }
    }
}
