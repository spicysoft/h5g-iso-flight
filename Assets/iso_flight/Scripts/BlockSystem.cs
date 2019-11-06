using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
using Unity.Tiny.Debugging;

namespace IsoFlight
{
	public class BlockSystem : ComponentSystem
	{
		private readonly float s_sn = math.sin( -45f * math.PI / 180f );
		private readonly float s_cs = math.cos( -45f * math.PI / 180f );
		private const float s_xbias = 1f;
		private const float s_ybias = 9f / 16f;		// 帳尻合わせ.


		protected override void OnUpdate()
		{
			Entities.ForEach((Entity entity, ref BlockInfo block, ref Translation trans, ref NonUniformScale scl, ref LayerSorting layer) => {
				/*if( !block.Initialized ) {
					block.Initialized = true;
					//block.Wpos = new float3( 0, 0, 0f );
					return;
				}*/

				//float dt = World.TinyEnvironment().frameDeltaTime;
				
				//block.Wpos.z += 10f * dt;

				float3 spos = calcScreenPos( block.Wpos );

				float order = 480f - spos.y;
				order += block.Wpos.y;

				layer.order = (short)order;

				trans.Value = spos;

			});
		}


		float3 calcScreenPos( float3 wpos )
		{
			float x = s_xbias * (s_cs * wpos.x - s_sn * wpos.z);
			float y = s_ybias * (s_sn * wpos.x + s_cs * wpos.z);

			y += wpos.y;

			return new float3( x, y, 0 );
		}
	}
}
