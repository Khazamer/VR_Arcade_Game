using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeMenu : MonoBehaviour
{
    /*
    Dictionary<keytype, objectType> dictionaryName;

    Dictionary <string, string> fruits = new Dictionary<string, string>();

    fruits.Add("apple","macintosh");
    */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void upgradeStat(string upgradeName) {
        // try reflection here

        /*
        public int myVar = 42;

        void OnGUI () {
            if (GUILayout.Button("Output Variable Value")) {
                int newVar = (int)this.GetType().GetField("myVar").GetValue(this);
                Debug.Log("Variable value: " + newVar);
            }
        }
        */

        // int newVar = (int)this.GetType().GetField("myVar").GetValue(this);

        /*
        public string valueName = "value";
        public float value = 0;
        public Component source;
        public void Update ()
        {
            source.GetType().GetField(valueName).SetValue(source,value);
        }
        */

        // curr idea
        // int.GetType().GetField(thing).SetValue()

        if (globals.weaponStats.ContainsKey(upgradeName)) {
            if (globals.weaponStats[upgradeName] < 4) {
                globals.weaponStats[upgradeName] ++;
            }
        }
    }
}
