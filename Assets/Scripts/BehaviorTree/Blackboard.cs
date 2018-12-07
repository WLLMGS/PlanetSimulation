using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Blackboard
    {
        public Blackboard()
        {
            _strings = new Dictionary<string, string>();
            _floats = new Dictionary<string, float>();
            _gameobjects = new Dictionary<string, GameObject>();
            _vec3s = new Dictionary<string, Vector3>();
            _bools = new Dictionary<string, bool>();
        }
        public void SetString(string name, string value)
        {
            _strings[name] = value;
        }
        public void SetFloat(string name, float value)
        {
            _floats.Add(name, value);
        }
        public void SetGameObject(string name, GameObject obj)
        {
            _gameobjects[name] = obj;
        }
        public void SetVector3(string name, Vector3 value)
        {
            _vec3s[name] = value;
        }
        public void SetBool(string name, bool value)
        {
            _bools[name] = value;
        }


        public string GetString(string name)
        {
            string s;
            _strings.TryGetValue(name, out s);
            return s;

        }
        public float GetFloat(string name)
        {
            float f;
            _floats.TryGetValue(name, out f);
            return f;
        }
        public GameObject GetGameObject(string name)
        {
            GameObject g;
            _gameobjects.TryGetValue(name, out g);
            return g;
        }

        public Vector3 GetVector3(string name)
        {
            Vector3 vec;
            _vec3s.TryGetValue(name, out vec);
            return vec;
        }

        public bool GetBool(string name)
        {
            bool b;
            _bools.TryGetValue(name, out b);
            return b;
        }

        Dictionary<string, string> _strings;
        Dictionary<string, float> _floats;
        Dictionary<string, GameObject> _gameobjects;
        Dictionary<string, Vector3> _vec3s;
        Dictionary<string, bool> _bools;
    }
}
