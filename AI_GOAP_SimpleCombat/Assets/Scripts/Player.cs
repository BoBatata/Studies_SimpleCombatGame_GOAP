using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int lifes;

    void Awake()
    {
        GWorld.Instance.GetQueue("players").AddResource(this.gameObject);
    }

    public void TakeDamage()
    {
        --lifes;
        print(lifes);
    }
}
