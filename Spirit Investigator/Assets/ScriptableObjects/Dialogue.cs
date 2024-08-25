using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue")]
public class Dialogue : ScriptableObject
{
    public string dialogueText;
    public Sprite leftCharacterImage;
    public Sprite rightCharacterImage;
    public bool isRightSpeaker;
}
