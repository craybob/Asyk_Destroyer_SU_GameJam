using System;
using Unity.VisualScripting;
using UnityEngine;


public enum ThrowStateModes: byte
{
    verticalDirMode = 0, 
    horizontalDirMode = 1,
    farDirMode = 2,
    throwMode = 3
}
public class MovementHandler : MonoBehaviour
{
    [SerializeField] ThrowStateModes throwState = ThrowStateModes.horizontalDirMode;

    private float minDirValue = 0f;
    private float maxDirValue = 30f;

    [SerializeField] private float time = 0f;
    [SerializeField] private Vector3 ThrowDirection = new Vector3(0,0,0);
    public float forcePush;

    private Rigidbody body;
    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeThrowMode();
        }
        
    }

    private void FixedUpdate()
    {

        ThrowDirection = GetDirection();

        if (throwState == ThrowStateModes.throwMode)
        {
            body.AddForce(ThrowDirection * forcePush, ForceMode.Impulse);
            ChangeThrowMode();
        }
    }

    private Vector3 GetDirection()
    {
        
        time += 0.5f * Time.deltaTime;
        if (time > 1.0f)
        {
            float temp = maxDirValue;
            maxDirValue = minDirValue;
            minDirValue = temp;
            time = 0.0f;
        }
        float horizontalDirection = (throwState == ThrowStateModes.horizontalDirMode) ? GetZDirection() : ThrowDirection.x;
        float verticalDirection = (throwState == ThrowStateModes.verticalDirMode) ? GetYDirection() : ThrowDirection.y;
        float farDirection = (throwState == ThrowStateModes.farDirMode) ? GetZDirection() : ThrowDirection.z;

        return new Vector3(horizontalDirection, verticalDirection, farDirection);
    }

    private float GetYDirection()
    {
        minDirValue = 0f;
        maxDirValue = 7f;
        float verticalDirection = Mathf.Lerp(minDirValue, maxDirValue, time);
        return verticalDirection;
    }

    private float GetXDirection()
    {
        minDirValue = -15f;
        maxDirValue = 15f;
        float horizontalDirection = Mathf.Lerp(minDirValue, maxDirValue, time);
        return horizontalDirection;
    }

    private float GetZDirection()
    {
        minDirValue = 0f;
        maxDirValue = 15f;
        float farDirection = Mathf.Lerp(minDirValue, maxDirValue, time);
        return farDirection;
    }

    private void ChangeThrowMode()
    {
        Debug.Log("ChangemodeWork!");
        throwState += 1;
        ThrowModeReload();
    }

    private void ThrowModeReload()
    {
        if ((byte)throwState > 3)
        {
            throwState = 0;
        }
    }
}
