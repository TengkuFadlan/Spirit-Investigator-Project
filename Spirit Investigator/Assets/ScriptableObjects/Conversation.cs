using UnityEngine;

[CreateAssetMenu(fileName = "New Conversation", menuName = "Dialogue/Conversation")]
public class Conversation : ScriptableObject
{
    public string eventName;
    public Dialogue[] dialogues;
}
