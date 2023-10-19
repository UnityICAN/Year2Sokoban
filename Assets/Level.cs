using UnityEngine;

[CreateAssetMenu]
public class Level : ScriptableObject {
    [SerializeField] [TextArea(10, 10)] private string content;
    public string Content => content;
}