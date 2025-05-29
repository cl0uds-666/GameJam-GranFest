using UnityEngine;

public static class PlayerUtils
{
    public static int GetPlayerIndex(GameObject player)
    {
        if (player == null) return -1;

        string[] parts = player.name.Split('_');
        if (parts.Length > 1 && int.TryParse(parts[1], out int index))
        {
            return index;
        }

        return -1;
    }
}
