using UnityEngine;
using System.Collections;

public struct CubeCoordinate {
	public int x { get; private set; }
	public int y { get; private set; }
	public int z { get; private set; }

	public int q {
		get {
			return x;
		}
	}

	public int r {
		get {
			return z;
		}
	}

	public CubeCoordinate( int q, int r ) {
		x = q;
		z = r;
		y = (x + z) * -1;
	}

	public int xy {
		set {
			x += value;
			y -= value;
		}
	}

	public int yz {
		set {
			y += value;
			z -= value;
		}
	}

	public int zx {
		set {
			z += value;
			x -= value;
		}
	}

	public Vector3 ToPosition() {
		return Hex2Position( this );
	}

	public override string ToString() {
		return string.Format( "CubeCoord({0}, {1}, {2})", x, y, z );
	}

	private const float hexSize = 1f;
	private static readonly float verticalSpacing = hexSize * 1.5f + 0.1f;
	private static readonly float horizontalSpacing = hexSize * Mathf.Sqrt( 3 ) + 0.1f;
	public static Vector3 Hex2Position( CubeCoordinate hex ) {
		Vector3 position;
		position.y = 0;
		position.x = horizontalSpacing * (hex.x + 0.5f * hex.z);
		position.z = verticalSpacing * hex.z;
		return position;
	}
}
