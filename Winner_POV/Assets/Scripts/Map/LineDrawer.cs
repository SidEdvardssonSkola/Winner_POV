using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField] private GameObject lineObject;
    [SerializeField] private float lineSpacing = 1f;
    [SerializeField] int maxDots = 25;
    public void DrawLine(Vector2 pos1, Vector2 pos2)
    {
        transform.position = pos1;
        transform.eulerAngles = new Vector3(0, 0, Vector2.Angle(Vector2.up, pos2 - pos1));

        float distance = Mathf.Sqrt((pos2.x - pos1.x) * (pos2.x - pos1.x) + (pos2.y - pos1.y) * (pos2.y - pos1.y));
        float elapsedDistance = 0f;

        while (distance > elapsedDistance || maxDots < 1)
        {
            transform.Translate(0, lineSpacing, 0);
            elapsedDistance += lineSpacing;
            Instantiate(lineObject, transform.position, transform.rotation);

            maxDots--;
        }
    }
}
