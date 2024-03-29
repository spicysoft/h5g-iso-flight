using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
using Unity.Tiny.Debugging;

namespace IsoFlight
{
	public class BlockSystem : ComponentSystem
	{
		// セルサイス.
		public const float UnitX = 60f;
		public const float UnitY = 50f;
		public const float UnitZ = 60f;

		protected override void OnUpdate()
		{
			bool IsPause = false;
			Entities.ForEach( ( ref GameMngr mngr ) => {
				IsPause = mngr.IsPause;
			} );
			if( IsPause )
				return;

			float dt = World.TinyEnvironment().frameDeltaTime;
			float scrollSpd = 0;

			Entities.ForEach( ( ref GameMngr mngr ) => {
				scrollSpd = mngr.ScrollSpd;
			} );

			Entities.ForEach( ( ref BlockInfo block, ref WorldPosInfo info ) => {
				if( !block.IsActive )
					return;
				if( !block.Initialized )
					return;

				block.Timer += dt;
				info.Wpos.z -= scrollSpd * dt;

				// スクロールアウト.
				if( info.Wpos.z < -10f * UnitZ ) {
					// 消す.
					block.IsActive = false;
					return;
				}

				block.CellPos.x = (int)(info.Wpos.x / BlockSystem.UnitX);
				block.CellPos.y = (int)(info.Wpos.y / BlockSystem.UnitY);
				block.CellPos.z = (int)(info.Wpos.z / BlockSystem.UnitZ);

				if( block.Timer > 0.1f ) {
					if( !block.ShadowChecked ) {
						block.ShadowChecked = true;
						int retvatl = shadowCheck( block.CellPos );

						NonUniformScale scl = EntityManager.GetComponentData<NonUniformScale>( block.shadowEntity );
						if( (retvatl & 1) != 0 ) {
							scl.Value.x = 0;
						}
						if( (retvatl & 1) == 0 ) {
							scl.Value.x = 1;
						}
						EntityManager.SetComponentData( block.shadowEntity, scl );

						NonUniformScale scl2 = EntityManager.GetComponentData<NonUniformScale>( block.shadowEntity2 );
						if( (retvatl & (1 << 1)) != 0 ) {
							scl2.Value.x = 0;
						}
						if( (retvatl & (1 << 1)) == 0 ) {
							scl2.Value.x = 1;
						}
						EntityManager.SetComponentData( block.shadowEntity2, scl2 );

					}
				}

			} );
		}

		int shadowCheck( int3 cell )
		{
			int retval = 0;
			Entities.ForEach( ( ref BlockInfo block ) => {
				if( block.CellPos.z == cell.z ) {
					if( block.CellPos.x == cell.x && block.CellPos.y < cell.y )
						retval |= 1;
					if( block.CellPos.y == cell.y && block.CellPos.x < cell.x )
						retval |= (1<<1);
				}
			} );

			return retval;
		}

	}
}
