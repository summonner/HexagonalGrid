using UnityEngine;
using System.Collections;

public class HexagonalGrid : MonoBehaviour {
	public Vector3 size = Vector3.one;
	public GameObject hexMesh;


	void Start () {
/*		Vector3 iter = Vector3.zero;
		for ( iter.x = -size.x; iter.x <= size.x; iter.x += 1f ) {
			if ( iter.x <= 0 ) {
				iter.y = size.y;
				do {
					iter.z = -(iter.x + iter.y);
					Spawn( iter );
					iter.y -= 1f;
				} while ( iter.z < size.z );
			}
			else {
				iter.z = -size.z;
				do {
					iter.y = -(iter.x + iter.z);
					Spawn( iter );
					iter.z += 1f;
				} while ( iter.y > -size.y );
			}
		}*/
		for ( int i=(int)-size.x; i < size.x; ++i ) {
			for ( int j=(int)-size.y; j < size.y; ++j ) {
				Spawn( new CubeCoordinate( i, j ) );
			}
		}
	}

	private void Spawn( CubeCoordinate hexPosition ) {
		var mesh = Instantiate<GameObject>( hexMesh );
		mesh.name = hexPosition.ToString();
		mesh.transform.localPosition = hexPosition.ToPosition();
	}
}
