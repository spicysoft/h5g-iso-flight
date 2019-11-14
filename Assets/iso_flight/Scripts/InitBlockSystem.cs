using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
using Unity.Tiny.Debugging;

namespace IsoFlight
{
	[UpdateAfter( typeof( BlockGenerateSystem ) )]
	public class InitBlockSystem : ComponentSystem
	{
		Random _random;
		private int cnt = 0;
		//private int BlkCounter = 0;

		protected override void OnUpdate()
		{
			Entities.ForEach( ( ref InitBlockInfo info ) => {
				if( !info.Initialized ) {
					info.Initialized = true;
					int seed = World.TinyEnvironment().frameNum;
					_random.InitState( (uint)seed );
				}
			} );

			Entities.ForEach( ( ref BlockInfo block, ref WorldPosInfo info, /*ref Translation trans,*/ ref NonUniformScale scl ) => {
				if( !block.IsActive )
					return;
				if( block.Initialized )
					return;

				block.Initialized = true;
				block.ShadowChecked = false;
				block.Timer = 0;
				//block.ShadowId = 0;
				//block.SerialId = ++BlkCounter;
				//Debug.LogFormatAlways( "id {0}", block.SerialId);
				
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
				info.Wpos.y = (iy + 1) * BlockSystem.UnitY + BlockSystem.UnitY * 0.5f;
				info.Wpos.z = (iz + 2) * BlockSystem.UnitZ;

				block.CellPos.x = (int)(info.Wpos.x / BlockSystem.UnitX);
				block.CellPos.y = (int)(info.Wpos.y / BlockSystem.UnitY);
				block.CellPos.z = (int)(info.Wpos.z / BlockSystem.UnitZ);

				cnt++;


				// shadow.
				BlkShadowInfo shadow = EntityManager.GetComponentData<BlkShadowInfo>( block.shadowEntity );
				shadow.Initialized = true;
				EntityManager.SetComponentData( block.shadowEntity, shadow );
				WorldPosInfo blkpos = EntityManager.GetComponentData<WorldPosInfo>( block.shadowEntity );
				blkpos.Wpos.x = info.Wpos.x;
				blkpos.Wpos.y = 0;
				blkpos.Wpos.z = info.Wpos.z;
				EntityManager.SetComponentData( block.shadowEntity, blkpos );

				// shadow2.
				BlkShadowInfo shadow2 = EntityManager.GetComponentData<BlkShadowInfo>( block.shadowEntity2 );
				shadow2.Initialized = true;
				EntityManager.SetComponentData( block.shadowEntity2, shadow2 );
				WorldPosInfo blkpos2 = EntityManager.GetComponentData<WorldPosInfo>( block.shadowEntity2 );
				blkpos2.Wpos.x = -150;
				blkpos2.Wpos.y = info.Wpos.y/* + BlockSystem.UnitY * 0.5f*/;
				blkpos2.Wpos.z = info.Wpos.z;
				EntityManager.SetComponentData( block.shadowEntity2, blkpos2 );

			} );

		}
	}
}
