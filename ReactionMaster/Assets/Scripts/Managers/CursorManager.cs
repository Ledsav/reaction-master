using UnityEngine;

namespace Managers
{
    public class CursorManager : MonoBehaviour
    {
        [SerializeField] private Texture2D cursorTex;
        [SerializeField] private Vector2 cursorHotspot;

        private void Awake()
        {
            Cursor.SetCursor(cursorTex, cursorHotspot, CursorMode.ForceSoftware);
        }
    }
}