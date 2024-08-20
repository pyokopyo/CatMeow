using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    DebugGui  debugGui;

    void Start()
    {
        debugGui = FindObjectOfType<DebugGui>();
    }

	void Update( ) {
		if ( Input.GetKeyDown( KeyCode.I ) ) debugGui.Log( "Logだよ" );
		if ( Input.GetKeyDown( KeyCode.W ) ) debugGui.LogWarning( "Warningだよ" );
		if ( Input.GetKeyDown( KeyCode.E ) ) debugGui.LogError( "Errorだよ" );
	}
}
