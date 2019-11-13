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
			var deltaTime = World.TinyEnvironment().frameDeltaTime;
			float scrollSpd = 0;

			Entities.ForEach( ( ref GameMngr mngr ) => {
				scrollSpd = mngr.ScrollSpd;
			} );

			Entities.ForEach( ( ref GroundInfo grnd, ref WorldPosInfo info ) => {

				info.Wpos.z -= scrollSpd * deltaTime;

				if( info.Wpos.z <= -600f ) {
					info.Wpos.z += 1500f;
				}
			} );

		}
	}
}
