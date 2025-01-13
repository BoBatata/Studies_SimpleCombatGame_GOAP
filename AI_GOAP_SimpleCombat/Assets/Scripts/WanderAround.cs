using UnityEngine;

public class WanderAround : GAction
{
    [SerializeField] private GameObject pointPrefab;
    public override bool PrePerform()
    {
        Vector3 randomPos = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        target = Instantiate(pointPrefab, randomPos, Quaternion.identity);
        return true;
    }

    public override bool PostPerform()
    {
        Destroy(target.gameObject);
        return true;
    }
}
