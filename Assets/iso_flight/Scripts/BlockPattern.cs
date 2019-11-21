using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;

namespace IsoFlight
{
	public class BlockPattern //: ComponentSystem
	{
		static public readonly int[] Ptn1 = {
			1, 1, 1, 1, 1,
			0, 0, 0, 0, 0,
			0, 0, 0, 0, 0,
			1, 1, 1, 1, 1,
		};
		static public readonly int[] Ptn2 = {
			1, 1, 1, 1, 1,
			1, 0, 0, 0, 1,
			1, 0, 0, 0, 1,
			1, 1, 1, 1, 1,
		};
		static public readonly int[] Ptn3 = {
			1, 0, 1, 0, 1,
			1, 0, 1, 0, 1,
			1, 0, 1, 0, 1,
			1, 0, 1, 0, 1,
		};
		static public readonly int[] Ptn4 = {
			0, 1, 1, 1, 1,
			0, 0, 1, 1, 1,
			0, 0, 0, 1, 1,
			0, 0, 0, 0, 1,
		};
		static public readonly int[] Ptn5 = {
			0, 0, 0, 0, 0,
			0, 1, 1, 1, 0,
			0, 1, 1, 1, 0,
			0, 0, 0, 0, 0,
		};
		static public readonly int[] Ptn6 = {
			1, 1, 0, 1, 1,
			1, 1, 0, 1, 1,
			1, 1, 0, 1, 1,
			1, 1, 0, 1, 1,
		};
		static public readonly int[] Ptn7 = {
			1, 1, 0, 0, 1,
			1, 1, 0, 0, 1,
			1, 1, 1, 1, 1,
			1, 1, 1, 1, 1,
		};
		static public readonly int[] Ptn8 = {
			1, 1, 1, 1, 1,
			1, 1, 1, 1, 1,
			1, 0, 0, 0, 1,
			1, 0, 0, 0, 1,
		};
		static public readonly int[] Ptn9 = {
			1, 1, 1, 1, 0,
			1, 1, 1, 0, 0,
			1, 1, 0, 0, 0,
			1, 0, 0, 0, 0,
		};
		static public readonly int[] Ptn10 = {
			1, 1, 1, 1, 1,
			1, 1, 0, 1, 1,
			1, 1, 0, 1, 1,
			1, 1, 1, 1, 1,
		};
		static public readonly int[] Ptn11 = {
			1, 1, 1, 1, 1,
			1, 0, 1, 1, 1,
			1, 1, 1, 0, 1,
			1, 1, 1, 1, 1,
		};
		static public readonly int[] Ptn12 = {
			1, 0, 1, 0, 1,
			0, 1, 0, 1, 0,
			1, 0, 1, 0, 1,
			0, 1, 0, 1, 0,
		};
		static public readonly int[] Ptn13 = {
			1, 1, 1, 1, 1,
			1, 1, 1, 1, 1,
			0, 1, 1, 1, 1,
			1, 1, 1, 1, 1,
		};
		static public readonly int[] Ptn14 = {
			1, 1, 0, 1, 1,
			1, 1, 1, 1, 1,
			1, 1, 1, 1, 1,
			1, 1, 1, 1, 1,
		};
		static public readonly int[] Ptn15 = {
			1, 1, 1, 1, 1,
			1, 1, 1, 1, 0,
			1, 1, 1, 1, 0,
			1, 1, 1, 1, 1,
		};
		static public readonly int[] Ptn16 = {
			1, 1, 1, 0, 1,
			1, 1, 1, 1, 1,
			1, 1, 1, 1, 1,
			1, 0, 1, 1, 1,
		};
		static public readonly int[] Ptn17 = {
			1, 0, 1, 0, 1,
			1, 1, 1, 1, 1,
			1, 1, 1, 1, 1,
			1, 0, 1, 0, 1,
		};
		static public readonly int[] Ptn18 = {
			1, 0, 1, 0, 1,
			1, 1, 0, 1, 1,
			1, 1, 0, 1, 1,
			1, 0, 1, 0, 1,
		};
		static public readonly int[] Ptn19 = {
			1, 1, 1, 0, 1,
			1, 1, 0, 1, 0,
			1, 1, 1, 0, 1,
			1, 1, 1, 1, 0,
		};


		static public readonly int[][] Tbl1 = { Ptn1, Ptn2, Ptn3, Ptn4, Ptn5, Ptn6, Ptn7, Ptn8, Ptn9, Ptn10 };
		static public readonly int[][] Tbl2 = { Ptn10, Ptn11, Ptn12, Ptn13, Ptn14, Ptn15, Ptn16, Ptn17, Ptn18, Ptn19 };

	}
}
