using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator shake(float mag, float time)
    {
        float elapsed = 0;
        Vector3 originalPos = transform.localPosition;

        while(elapsed < time)
        {
            float x = Random.Range(-1f, 1f) * mag;
            float y = Random.Range(-1f, 1f) * mag;

            transform.localPosition += new Vector3(x, y, 0f);

            elapsed += 0.01f;
        }

        transform.localPosition = originalPos;
        yield return null;
    }
}
