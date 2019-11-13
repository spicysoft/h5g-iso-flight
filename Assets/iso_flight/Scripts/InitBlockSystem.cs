using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;

namespace IsoFlight
{
	[UpdateAfter( typeof( BlockGenerateSystem ) )]
	public class InitBlockSystem : ComponentSystem
	{
		Random _random;
		private int cnt = 0;

		protected override void OnUpdate()
		{
			Entities.ForEach( ( ref InitBlockInfo info ) => {
				if( !info.Initialized ) {
					info.Initialized = true;
					int seed = World.TinyEnvironment().frameNum;
					_random.InitState( (uint)seed );
				}
			} );

			Entities.ForEach( ( ref BlockInfo block, ref WorldPosInfo info, ref Translation trans, ref NonUniformScale scl ) => {
				if( !block.IsActive )
					return;
				if( block.Initialized )
					return;

				block.Initialized = true;
				block.Timer = 0;
				block.Vx = 0;
				block.Vy = 0;
				scl.Value.x = 1f;


				int cntxy = cnt % 20;
				int ix = cntxy % 5;
				int iy = cntxy / 5;
				int iz = cnt / 20;

#if false
				if( (ix == 2 && iy == 2) || (ix == 2 && iy == 1) ) {
					cnt++;
					cntxy = cnt % 20;
					ix = cntxy % 5;
					iy = cntxy / 5;
					iz = cnt / 20;
				}
#endif

				info.Wpos.x = (ix - 1) * BlockSystem.UnitX;
				info.Wpos.y = (iy + 1) * BlockSystem.UnitY;
				info.Wpos.z = (iz + 2) * BlockSystem.UnitZ;

				cnt++;

			} );

		}
	}
}
