using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;

namespace IsoFlight
{
	public class GroundSystem : ComponentSystem
	{
		protected override void OnUpdate()
		{
			bool IsPause = false;
			Entities.ForEach( ( ref GameMngr mngr ) => {
				IsPause = mngr.IsPause;
			} );
			if( IsPause )
				return;

			var deltaTime = World.TinyEnvironment().frameDeltaTime;
			float scrollSpd = 0;

			Entities.ForEach( ( ref GameMngr mngr ) => {
				scrollSpd = mngr.ScrollSpd;
			} );

			Entities.ForEach( ( ref GroundInfo grnd, ref WorldPosInfo info ) => {
				if( !grnd.Initialized ) {
					grnd.Initialized = true;
					return;
				}

				info.Wpos.z -= scrollSpd * deltaTime;

				if( info.Wpos.z <= -900f ) {
					info.Wpos.z += 1800f;
				}
			} );

		}
	}
}
