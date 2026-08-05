using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

// TODO: How Items are storing data should be updated to be able to store non-rectangle shapes, have item descriptions, etc...
public class ItemData : ScriptableObject
{
    public int width = 1;
    public int height = 1;

    public Sprite itemIcon;
}
