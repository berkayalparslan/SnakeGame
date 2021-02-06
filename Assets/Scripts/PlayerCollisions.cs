using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnTriggerEnterEvent: UnityEvent<Transform>
{

}

public class PlayerCollisions : MonoBehaviour
{
    public OnTriggerEnterEvent OnPlayerEnteredTrigger = new OnTriggerEnterEvent();
    private bool _collided;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (OnPlayerEnteredTrigger != null)
        {
            OnPlayerEnteredTrigger.Invoke(collision.transform);
        }
    }
}
