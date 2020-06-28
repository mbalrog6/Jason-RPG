using System.Collections;
using JasonRPG;
using JasonRPG.Inventory;
using UnityEngine;

public class UIBinding : MonoBehaviour
{
    IEnumerator Start()
    {
        var player = FindObjectOfType<Player>();
        while (player == null)
        {
            yield return null;
            player = FindObjectOfType<Player>();
        }

        GetComponent<UIInventoryPanel>().Bind(player.GetComponent<Inventory>() );
    }
}
