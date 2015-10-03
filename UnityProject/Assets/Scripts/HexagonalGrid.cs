using UnityEngine;
using System.Collections;

public class HexagonalGrid : MonoBehaviour {
	public int width;
	public int height;

	public GameObject hexMesh;

	private HexTile[,] map;

	void Start () {
		map = new HexTile[width, height];
		for ( int i=0; i < width; ++i ) {
			for ( int j=0; j < height; ++j ) {
				var q = j - Mathf.FloorToInt( i * 0.5f );
				var r = i;
				var trans = Spawn( new CubeCoordinate( q, r ) );
				map[ i, j ] = new HexTile( q, r, trans );
			}
		}
	}

	private Transform Spawn( CubeCoordinate hexPosition ) {
		var mesh = Instantiate<GameObject>( hexMesh );
		mesh.name = hexPosition.ToString();
		var trans = mesh.transform;
		trans.parent = transform;
		trans.localPosition = hexPosition.ToWorldPosition();
		trans.localRotation = Quaternion.identity;
		trans.localScale = Vector3.one;
		return trans;
	}

	void OnGUI() {
		var ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		var distance = -ray.origin.y / ray.direction.y;
		var hit = ray.GetPoint( distance );
		var text = string.Format( "{0} -> {1}", hit, CubeCoordinate.World2Hex( hit ).ToString() );
		GUILayout.Label( text );
	}
}
