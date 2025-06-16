using UnityEngine;

public class MoveCloud : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Translate(new Vector3(-0.25f, 0, 0));
    }
}
