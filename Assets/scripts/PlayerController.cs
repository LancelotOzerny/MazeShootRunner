using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MazeGenerator _mazeGenerator = null;

    private void Start()
    {
        _mazeGenerator = GameObject.FindGameObjectWithTag("MazeGenerator").GetComponent<MazeGenerator>();
    }

    private void Update()
    {
        TestControl();
    }

    private void TestControl()
    {
        if (_mazeGenerator != null)
        {
            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                if (_mazeGenerator.RoadElements.Contains(new Vector2Int((int)transform.position.x + 1, (int)transform.position.y)))
                    transform.position = new Vector2(transform.position.x + 1, transform.position.y);
            }

            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                if (_mazeGenerator.RoadElements.Contains(new Vector2Int((int)transform.position.x - 1, (int)transform.position.y)))
                    transform.position = new Vector2(transform.position.x - 1, transform.position.y);
            }

            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                if (_mazeGenerator.RoadElements.Contains(new Vector2Int((int)transform.position.x, (int)transform.position.y + 1)))
                    transform.position = new Vector2(transform.position.x, transform.position.y + 1);
            }

            if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                if (_mazeGenerator.RoadElements.Contains(new Vector2Int((int)transform.position.x, (int)transform.position.y - 1)))
                    transform.position = new Vector2(transform.position.x, transform.position.y - 1);
            }
        }
    }
}
