using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;

namespace IsoFlight
{
	public class BlockPattern //: ComponentSystem
	{
		static public readonly int[] Ptn1 = {
			0, 1, 0, 1, 0,
			0, 0, 0, 0, 0,
			0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, };

		//static public readonly int[] Ptn1 = { 0, 1, 0, 1, 0,  0, 1, 0, 1, 0,  0, 1, 0, 1, 0,  0, 1, 0, 1, 0, };
		static public readonly int[] Ptn2 = { 0, 0, 0, 0, 0,  1, 1, 0, 1, 1,  1, 1, 0, 1, 1,  0, 1, 1, 1, 0, };

		static public readonly int[][] Tbl = { Ptn2, Ptn2 };

		/*
		public readonly int[,] Tbl = {
			{ 0, 1, 0, 1, 0 },
			{ 1, 1, 1, 1, 1 }
		};*/
#if false
		protected override void OnUpdate()
		{
			/*
			int[] Ptn1 = { 0, 1, 0, 1, 0 };
			int[] Ptn2 = { 1, 1, 1, 1, 1 };

			int[][] Tbl = { Ptn1, Ptn2 };
			*/
#if false
			Entities.ForEach( ( ref BlkShadowInfo shadow, ref WorldPosInfo info, ref Translation trans, ref NonUniformScale scl ) => {
				if( !shadow.IsActive )
					return;
				if( shadow.Initialized )
					return;

				shadow.Initialized = true;
				scl.Value.x = 1f;



			} );
#endif
		}
#endif
	}
}
