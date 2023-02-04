using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ImageElement
{
    public Sprite[] sprites;
}

[Serializable]
public class StringImageElementListDictionary : SerializableDictionary<string,
ImageElement>
{ }