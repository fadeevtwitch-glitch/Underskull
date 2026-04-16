using UnityEngine;
using System.Collections.Generic;

public class ChunkSystem : MonoBehaviour
{
    [SerializeField] private int chunkSize = 100;
    [SerializeField] private int loadDistance = 2;
    
    private Dictionary<Vector2Int, GameObject> loadedChunks = new Dictionary<Vector2Int, GameObject>();
    private Transform playerTransform;
    
    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerManager>()?.transform;
    }
    
    private void Update()
    {
        if (playerTransform == null) return;
        
        UpdateChunks();
    }
    
    private void UpdateChunks()
    {
        Vector2Int playerChunk = GetChunkCoordinates(playerTransform.position);
        
        // Load chunks around player
        for (int x = -loadDistance; x <= loadDistance; x++)
        {
            for (int z = -loadDistance; z <= loadDistance; z++)
            {
                Vector2Int chunkPos = playerChunk + new Vector2Int(x, z);
                
                if (!loadedChunks.ContainsKey(chunkPos))
                {
                    LoadChunk(chunkPos);
                }
            }
        }
        
        // Unload distant chunks
        List<Vector2Int> chunksToUnload = new List<Vector2Int>();
        foreach (var chunk in loadedChunks.Keys)
        {
            if (Vector2Int.Distance(chunk, playerChunk) > loadDistance + 1)
            {
                chunksToUnload.Add(chunk);
            }
        }
        
        foreach (var chunk in chunksToUnload)
        {
            UnloadChunk(chunk);
        }
    }
    
    private Vector2Int GetChunkCoordinates(Vector3 position)
    {
        return new Vector2Int(
            Mathf.FloorToInt(position.x / chunkSize),
            Mathf.FloorToInt(position.z / chunkSize)
        );
    }
    
    private void LoadChunk(Vector2Int chunkPos)
    {
        GameObject chunk = new GameObject($"Chunk_{chunkPos.x}_{chunkPos.y}");
        chunk.transform.parent = transform;
        chunk.transform.position = new Vector3(chunkPos.x * chunkSize, 0, chunkPos.y * chunkSize);
        
        loadedChunks[chunkPos] = chunk;
    }
    
    private void UnloadChunk(Vector2Int chunkPos)
    {
        if (loadedChunks.TryGetValue(chunkPos, out GameObject chunk))
        {
            Destroy(chunk);
            loadedChunks.Remove(chunkPos);
        }
    }
}