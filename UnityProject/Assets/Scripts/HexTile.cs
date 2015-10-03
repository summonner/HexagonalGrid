using UnityEngine;
using System.Collections;

public class HexTile {
	public readonly CubeCoordinate coordinate;
	public float height;
	public Transform transform;

	public HexTile( int q, int r, Transform meshTransform ) {
		coordinate = new CubeCoordinate( q, r );
		height = 0;
		transform = meshTransform;
	}
}
