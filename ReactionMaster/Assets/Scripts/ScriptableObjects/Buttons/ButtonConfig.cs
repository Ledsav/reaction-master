using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ButtonConfig", menuName = "Config/ButtonConfig")]
    public class ButtonConfig : ScriptableObject
    {
        public Sprite buttonImage; // The image for the button
        public string buttonTag; // The tag for the button
        public Color buttonColor; // The color for the button
        public int buttonPoints; // The points for the button
    }
}