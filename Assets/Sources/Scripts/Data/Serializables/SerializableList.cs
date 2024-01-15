using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Serializables
{
    [Serializable]
    public class SerializableList<T>
    {
        [SerializeField] private List<T> _value;

        public SerializableList(List<T> value)
        {
            _value = value;
        }

        public List<T> Value => _value;
    }
}