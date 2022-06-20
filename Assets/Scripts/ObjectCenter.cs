using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCenter : MonoBehaviour
{
    static private ObjectCenter _isntance;

    public ObjectCenter instance
    {
        get
        {
            if (_isntance == null)
            {
                _isntance = new ObjectCenter();
            }
            return _isntance;
        }
    }
}
