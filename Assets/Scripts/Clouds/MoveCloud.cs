using UnityEngine;

public class MoveCloud : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Translate(new Vector3(-0.25f, 0, 0));
        transform.localScale = new Vector3(transform.localScale.x - 0.0005f, transform.localScale.y - 0.0005f, transform.localScale.z - 0.0005f);
    }
}
