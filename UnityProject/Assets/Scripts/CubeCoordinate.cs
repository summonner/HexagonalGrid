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

	[System.Diagnostics.Conditional( "UNITY_EDITOR" )]
	private static void AssertArgument( float x, float y, float z ) {
		const float threshold = 0.001f;
		if ( x + y + z > threshold ) {
			throw new System.ArgumentException( "Invalid argument : " + x + " + " + y + " + " + z + " = " + (x + y + z) );
		}
	}

	private CubeCoordinate( int x, int y, int z ) {
		AssertArgument( x, y, z );
		this.x = x;
		this.y = y;
		this.z = z;
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

	public Vector3 ToWorldPosition() {
		return Hex2World( this );
	}

	public override string ToString() {
		return string.Format( "QR({0}, {1})", q, r );
	}

	public string ToString3() {
		return string.Format( "CubeCoord({0}, {1}, {2})", x, y, z );
	}

	private const float HEX_SIZE = 1f;
	private const float GAP = 0.1f;
	private static readonly float verticalSpacing = (HEX_SIZE * 1.5f + GAP) * -1;
	private static readonly float horizontalSpacing = HEX_SIZE * Mathf.Sqrt( 3 ) + GAP;
	public static Vector3 Hex2World( CubeCoordinate hex ) {
		Vector3 position;
		position.y = 0;
		position.x = horizontalSpacing * (hex.x + 0.5f * hex.z);
		position.z = verticalSpacing * hex.z;
		return position;
	}

	private static readonly float inverseVerticalSpacing = 1 / verticalSpacing;
	private static readonly float inverseHorizontalSpacing = 1 / horizontalSpacing;
	public static CubeCoordinate World2Hex( Vector3 world ) {
		var r = world.z * inverseVerticalSpacing;
		var q = world.x * inverseHorizontalSpacing - 0.5f * r;
		return CubeRound( q, r );
	}
	
	private static CubeCoordinate CubeRound( float q, float r ) {
		return CubeRound( q, -(q + r), r );
	}

	private static CubeCoordinate CubeRound( float x, float y, float z ) {
		AssertArgument( x, y, z );
		var rx = Mathf.RoundToInt( x );
		var ry = Mathf.RoundToInt( y );
		var rz = Mathf.RoundToInt( z );

		var dx = Mathf.Abs( rx - x );
		var dy = Mathf.Abs( ry - y );
		var dz = Mathf.Abs( rz - z );
		var largeDiff = Mathf.Max( dx, dy, dz );

		if ( largeDiff == dx ) {
			rx = -(ry + rz);
		}
		else if ( largeDiff == dy ) {
			ry = -(rx + rz);
		}
		else {	// largeDiff == dz
			rz = -(rx + ry);
		}

		return new CubeCoordinate( rx, ry, rz );
	}

	public int Distance( CubeCoordinate subject ) {
		return Distance( this, subject );
	}

	public static int Distance( CubeCoordinate left, CubeCoordinate right ) {
		var x = Mathf.Abs( left.x - right.x );
		var y = Mathf.Abs( left.y - right.y );
		var z = Mathf.Abs( left.z - right.z );
		return Mathf.Max( x, y, z );
	}

	public static CubeCoordinate operator+ ( CubeCoordinate left, CubeCoordinate right ) {
		var x = left.x + right.x;
		var y = left.y + right.y;
		var z = left.z + right.z;
		return new CubeCoordinate( x, y, z );
	}

	public static CubeCoordinate operator- ( CubeCoordinate left, CubeCoordinate right ) {
		var x = left.x - right.x;
		var y = left.y - right.y;
		var z = left.z - right.z;
		return new CubeCoordinate( x, y, z );
	}

	public static CubeCoordinate operator* ( CubeCoordinate left, int scala ) {
		var x = left.x * scala;
		var y = left.y * scala;
		var z = left.z * scala;
		return new CubeCoordinate( x, y, z );
	}

	public static CubeCoordinate operator* ( int scala, CubeCoordinate right ) {
		return right * scala;
	}

	public static CubeCoordinate operator* ( CubeCoordinate left, float scala ) {
		var x = left.x * scala;
		var y = left.y * scala;
		var z = left.z * scala;
		return CubeRound( x, y, z );
	}

	public static CubeCoordinate operator* ( float scala, CubeCoordinate right ) {
		return right * scala;
	}

	public static CubeCoordinate operator/ ( CubeCoordinate left, float scala ) {
		return left * (1 / scala);
	}

	public static CubeCoordinate operator/ ( float scala, CubeCoordinate right ) {
		return right * (1 / scala);
	}
}
