/// <summary>
/// 控制贴图序列帧播放
/// 制作人：小白
/// </summary>
using UnityEngine;
using System.Collections;

public class AtlasManager : MonoBehaviour
{
	//vars for the whole sheet
	public int rowCount = 1;
	public int colCount = 4;

	//vars for animation
	public int rowNumber = 0; //Zero Indexed
	public int colNumber = 0; //Zero Indexed
	public int totalCells = 4;
	public int fps = 10;
	//Maybe this should be a private var
	private Vector2 offset;

	private bool canPlay;
	public bool auto = false;

	public enum PlayMode
	{
		still,
		once,
		loop,
		pingpong,
		random
	}
	public PlayMode playMode;

	void Start()
	{
		if(auto)
		{
			SetAtlas(rowCount ,colCount ,rowNumber ,colNumber);
		}
	}

	void StopAtlas()
	{
		canPlay = false;
		StopCoroutine("SetSpriteAnimation");
	}

	public void SetAtlas(int _rowCount ,int _colCount ,int _rowNumber ,int _colNumber)
	{
		canPlay = true;
		playMode = PlayMode.still;
		rowCount = _rowCount;
		colCount = _colCount;
		rowNumber = _rowNumber;
		colNumber = _colNumber;
		totalCells = _colCount * _rowCount;
		fps = 10;
		StartCoroutine("SetSpriteAnimation");
	}

	public void SetAtlas(PlayMode _mode,int _rowCount , int _colCount ,int _rowNumber ,int _colNumber,int _totalCells,int _fps )
	{
		canPlay = true;
		playMode = _mode;
		rowCount = _rowCount;
		colCount = _colCount;
		rowNumber = _rowNumber;
		colNumber = _colNumber;
		totalCells = _totalCells;
		fps = _fps;

		StartCoroutine("SetSpriteAnimation");
	}

	IEnumerator SetSpriteAnimation()
	{
		int index;
		float sizeX;
		float sizeY;
		Vector2 size;
		int uIndex;
		int vIndex;
		float offsetX;
		float offsetY;
		Vector2 offset;

		switch(playMode)
		{
		case PlayMode.still:
			sizeX = 1.0f / colCount;
			sizeY = 1.0f / rowCount;
			size =  new Vector2(sizeX,sizeY);

			uIndex = rowNumber;
			vIndex = colNumber / colCount;

			offsetX = (uIndex+colNumber) * size.x;
			offsetY = (1.0f - size.y) - (vIndex + rowNumber) * size.y;
			offset = new Vector2(offsetX,offsetY);
			renderer.material.SetTextureOffset ("_MainTex", offset);
			renderer.material.SetTextureScale  ("_MainTex", size);
			break;

		case PlayMode.loop:
			// Calculate index
			while(canPlay)
			{
				index = (int)(Time.time * fps);
				
				// Repeat when exhausting all cells
				index = index % totalCells;
				// Size of every cell
				sizeX = 1.0f / colCount;
				sizeY = 1.0f / rowCount;
				size =  new Vector2(sizeX,sizeY);
				
				// split into horizontal and vertical index
				uIndex = index % colCount;
				vIndex = index / colCount;
				
				// build offset
				// v coordinate is the bottom of the image in opengl so we need to invert.
				offsetX = (uIndex+colNumber) * size.x;
				offsetY = (1.0f - size.y) - (vIndex + rowNumber) * size.y;
				offset = new Vector2(offsetX,offsetY);
				renderer.material.SetTextureOffset ("_MainTex", offset);
				renderer.material.SetTextureScale  ("_MainTex", size);
				yield return null;

			}
			break;
		}
		yield return null;
	}
}