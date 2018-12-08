using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MainManager : Singleton<MainManager>
{
    protected MainManager()
    {
        m_unsortedNodes = new List<Utils.Node>();
        m_Layers = new Tilemap[3];
        m_Rows = 0;
        m_Columns = 0;
    }

    [SerializeField] private Tilemap[] m_Layers;
    [SerializeField] private Utils.Node[,] m_nodeGraph;
    [SerializeField] private List<Utils.Node> m_unsortedNodes;
    private int m_Rows;
    private int m_Columns;

    public Utils.Node[,] NodeGraph
    {
        get
        {
            return m_nodeGraph;
        }
    }

    private void Awake()
    {
        GetMapLayers();
        CreateNodeFromTilemap();
        PrintNodeGraph();
    }
	
	// Update is called once per frame
	private void Update ()
	{
	}

    private void GetMapLayers()
    {
        for(int i = 0; i < m_Layers.Length; ++i)
        {
            GameObject map = GameObject.FindGameObjectWithTag("Layer_" + i);
            if(map != null)
            {
                m_Layers[i] = map.GetComponent<Tilemap>();
            }
            else
            {
                Debug.Log("Can't find Layer_" + i);
            }
        }
    }

    private void CreateNodeFromTilemap()
    {
        int startX = m_Layers[0].cellBounds.xMin;
        int endX = m_Layers[0].cellBounds.xMax;
        int startY = m_Layers[0].cellBounds.yMin;
        int endY = m_Layers[0].cellBounds.yMax;

        m_Rows = m_Layers[0].size.y;
        m_Columns = m_Layers[0].size.x;
        m_nodeGraph = new Utils.Node[m_Rows, m_Columns];

        for (int x = startX; x < endX; ++x)
        {
            for (int y = startY; y < endY; ++y)
            {
                TileBase tile = m_Layers[0].GetTile(new Vector3Int(x, y, 0));
                Vector3 worldPos = m_Layers[0].CellToWorld(new Vector3Int(x, y, 0));
                Utils.Node node = new Utils.Node(new Vector2(x, y), worldPos, Utils.NodeType.Ground);
                if (tile == null)
                {
                    node.nodeType = Utils.NodeType.Obstacle;
                }
                else
                {
                    var obstacle = m_Layers[1].GetTile(new Vector3Int(x, y, 0));
                    if (obstacle != null)
                    {
                        node.nodeType = Utils.NodeType.Obstacle;
                    }
                }

                m_unsortedNodes.Add(node);
            }
        }

        int index = 0;
        for (int i = 0; i < m_Rows; ++i)
        {
            for (int j = 0; j < m_Columns; ++j)
            {
                m_nodeGraph[i, j] = m_unsortedNodes[index];
                index++;
            }
        }
    }

    private void PrintNodeGraph()
    {
        for (int i = 0; i < m_Rows; ++i)
        {
            for (int j = 0; j < m_Columns; ++j)
            {
                Debug.Log("node[" + i + ", " + j + "].gridPos = " + m_nodeGraph[i, j].gridPosition);
                Debug.Log("node[" + i + ", " + j + "].worldPos = " + m_nodeGraph[i, j].position);
                Debug.Log("node[" + i + ", " + j + "].type = " + m_nodeGraph[i, j].nodeType);
            }
        }
    }
}
