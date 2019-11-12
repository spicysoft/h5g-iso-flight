using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
using Unity.Tiny.Debugging;

namespace IsoFlight
{
	public class BlockSystem : ComponentSystem
	{

		protected override void OnUpdate()
		{
			float dt = World.TinyEnvironment().frameDeltaTime;

			Entities.ForEach((ref BlockInfo block, ref WorldPosInfo info) => {
				
				info.Wpos.z -= 60f * dt;
			});
		}

	}
}
