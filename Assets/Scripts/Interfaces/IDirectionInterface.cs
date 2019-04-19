using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneDirection.Game { 
    public interface IDirectionListener 
    {
       void OnDirectionChanged(Vector2 newDirection);
    }
}
