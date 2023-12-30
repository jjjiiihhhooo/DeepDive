using UnityEngine;
using UnityEngine.SceneManagement;

public class Dummy_Move : MonoBehaviour
{
    public Scene scene;

    public float speed;

    private void Awake()
    {
        scene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        if (!Manager.Instance.isStart) return;

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed * Time.deltaTime);
    }
}
