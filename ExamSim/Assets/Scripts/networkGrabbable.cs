using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;

public class networkgrabbable : NetworkBehaviour
{
    private NetworkObject netObject;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        netObject = GetComponent<NetworkObject>();
    }
   

    public void requestOwnership()
    {
        requestOwnership_ServerRpc(NetworkManager.Singleton.LocalClient.ClientId);
    }

    [ServerRpc(RequireOwnership = false)]
    private void requestOwnership_ServerRpc(ulong clientID)
    {
        netObject.ChangeOwnership(clientID);
        Debug.Log("Changing ownership");
    }
}
