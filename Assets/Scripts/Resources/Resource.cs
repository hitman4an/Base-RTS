using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool CanCollect { get; private set; } = true;

    public void setCanCollect(bool canCollect)
    {
        this.CanCollect = canCollect;
    }
}
