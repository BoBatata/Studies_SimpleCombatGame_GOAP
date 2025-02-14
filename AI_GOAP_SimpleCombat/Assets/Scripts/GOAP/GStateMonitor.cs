using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GStateMonitor : MonoBehaviour
{
    public string state;
    public float stateStrenght;
    public float stateDecayRate;
    public WorldStates beliefs;
    public GameObject resourcePrefab;
    public string queueName;
    public string worldState;
    public GAction action;

    bool stateFound = false;
    float initialStrenght;

    private void Awake()
    {
        beliefs = this.GetComponent<GAgent>().beliefs;
        initialStrenght = stateStrenght;
    }

    private void LateUpdate()
    {
        if (action.running)
        {
            stateFound = false;
            stateStrenght = initialStrenght;
        }

        if (!stateFound && beliefs.HasState(state))
        {
            stateFound = true;
        }

        if (stateFound)
        {
            stateStrenght -= stateDecayRate * Time.deltaTime;
            if (stateStrenght <= 0)
            {
                Vector3 location = new Vector3(this.transform.position.x, resourcePrefab.transform.position.y, this.transform.position.z);
                GameObject p = Instantiate(resourcePrefab, location, resourcePrefab.transform.rotation);
                stateFound = false;
                stateStrenght = initialStrenght;
                beliefs.RemoveState(state);
                GWorld.Instance.GetQueue(queueName).AddResource(p);
                GWorld.Instance.GetWorld().ModifyState(worldState, 1);
            }
        }
    }



}