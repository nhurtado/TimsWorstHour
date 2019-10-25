using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsComponent : MonoBehaviour
{
    public bool UpdateSprite(bool facingRight, float moving)
    {
        if (moving != 0)
        {
            if (facingRight && moving < 0)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                facingRight = !facingRight;
            }
            else if (!facingRight && moving > 0)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                facingRight = !facingRight;
            }
        }
        return facingRight;
    }
}
