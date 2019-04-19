using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDirectionEnterListener 
{
    void OnScored(int score);
    void OnFailed();
}
