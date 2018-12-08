using UnityEngine;

public class Utils
{
    public enum NodeType
    {
        Ground = 0,
        Water,
        Obstacle
    }

    public struct Node
    {
        public Vector2 gridPosition;
        public Vector3 position;
        public NodeType nodeType;

        public Node(Vector2 gridPosition, Vector3 position, NodeType type)
        {
            this.gridPosition = gridPosition;
            this.position = position;
            this.nodeType = type;
        }
    }
}
