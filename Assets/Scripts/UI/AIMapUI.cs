using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class AIMapUI : UIElement
{
    [SerializeField] float mapScale = 100;

    [Required] [SerializeField] Transform player;
    
    [Required][SerializeField] Transform playerApproximatePosCircle;
    [Required][SerializeField] Transform playerPosition;
    
    [ShowInInspector] PlayerApproximatePos? currentPlayerApproximatePos;

    public void SetPlayerApproximatePos(PlayerApproximatePos playerApproximatePos)
    {
        Show();
        currentPlayerApproximatePos = playerApproximatePos;
        var errorRadius = playerApproximatePos.errorRadius;
        if (errorRadius > mapScale){
            Hide();
            return;
        }
        playerApproximatePosCircle.localScale = Vector3.one * errorRadius / mapScale;
    }

    protected void Update()
    {
        if (!currentPlayerApproximatePos.HasValue){
            return;
        }
        var playerDis = player.position - currentPlayerApproximatePos.Value.pos;
        var playerDisX = playerDis.x * (1/mapScale * GetComponent<RectTransform>().sizeDelta.x);
        var playerDisY = playerDis.z * (1/mapScale * GetComponent<RectTransform>().sizeDelta.y);
        playerDisX = Mathf.Clamp(playerDisX, -GetComponent<RectTransform>().sizeDelta.x / 2, GetComponent<RectTransform>().sizeDelta.x / 2);
        playerDisY = Mathf.Clamp(playerDisY, -GetComponent<RectTransform>().sizeDelta.y / 2, GetComponent<RectTransform>().sizeDelta.y / 2);
        playerPosition.localPosition = new Vector2(playerDisX, playerDisY);
    }
}
