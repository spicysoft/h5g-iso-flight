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
				if( info.Wpos.z < -8f * BlockSystem.UnitZ ) {
					// 消す.
				}

			} );
		}

	}
}