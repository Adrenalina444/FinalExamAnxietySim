using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;

public class clientNetworkTransform : NetworkTransform
{
    protected override bool OnIsServerAuthoritative()
    {
        //return base.OnIsServerAuthoritative();

        return false;
    }
}
