using System;
using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

[Obsolete]
class AIObjectUI : SerializedMonoBehaviour
{
    [GetComponent] [SerializeField] Image image;
}