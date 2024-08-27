using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue")]
public class Dialogue : ScriptableObject
{
    public string speakerText;
    public string sentenceText;

    public Texture leftCharacterImage;
    public bool isLeftSpeaking;

    public Texture rightCharacterImage;
    public bool isRightSpeaking;
}
