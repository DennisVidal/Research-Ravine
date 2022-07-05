﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Icaros.Desktop.Localization {
    public class VersionText : MonoBehaviour {

        void Start() {
            GetComponent<Text>().text = "v" + Application.version;
        }
    }
}