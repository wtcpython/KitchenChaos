using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = Player.Instance.transform.position;
    }
}
