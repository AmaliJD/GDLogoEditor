using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaskOffset : MonoBehaviour
{
    public TextMeshPro text;

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(0, -1935 + (35 * (text.fontSize / 196.75f)), 0);
    }
}
