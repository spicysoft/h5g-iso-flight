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

			Entities.ForEach( ( ref GroundInfo grnd, ref WorldPosInfo info ) => {

				info.Wpos.z -= 60f * deltaTime;

				if( info.Wpos.z <= -600f ) {
					info.Wpos.z += 1500f;
				}
			} );

		}
	}
}
