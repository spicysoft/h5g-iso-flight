using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
using Unity.Tiny.Debugging;

namespace IsoFlight
{
	public class BlkShadowSystem : ComponentSystem
	{
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

			Entities.ForEach( ( ref BlkShadowInfo shadow, ref WorldPosInfo info ) => {
				if( !shadow.IsActive )
					return;
				if( !shadow.Initialized )
					return;

				//Debug.LogFormatAlways("pos {0}", info.Wpos);

				info.Wpos.z -= scrollSpd * dt;

				// スクロールアウト.
				if( info.Wpos.z < -11f * BlockSystem.UnitZ ) {
					// 消す.
					// 影は何もしない.
				}

			} );
		}

	}
}
