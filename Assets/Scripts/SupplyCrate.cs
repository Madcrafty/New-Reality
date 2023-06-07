using UnityEngine;

public class SupplyCrate : MonoBehaviour
{
    public GameObject Contents;
    public GameObject UI;

    private void Awake()
    {
        UI.SetActive(false);
    }
    public void Supply(GameObject player)
    {
        player.GetComponent<Pickup>()._objectHeld = Instantiate(Contents);
    }
}
