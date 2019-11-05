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

			Entities.ForEach((Entity entity, ref BlockInfo block, ref Translation trans, ref NonUniformScale scl) =>	{
				if( !block.Initialized ) {
					block.Initialized = true;
					//block.Wpos = new float3( 0, 0, 0f );
					return;
				}

				//float dt = World.TinyEnvironment().frameDeltaTime;
				
				//block.Wpos.z += 10f * dt;

				float3 spos = calcScreenPos( block.Wpos );
				trans.Value = spos;

			});
		}



		float3 calcScreenPos( float3 wpos )
		{
			float s_xbias = 1f;
			float s_ybias = 9f/16f;
			float s_sn = math.sin( -45f*math.PI/180f );
			float s_cs = math.cos( -45f * math.PI / 180f );
			Debug.LogFormatAlways("s {0}, c {1}", s_sn, s_cs);


			float x = s_xbias * (s_cs * wpos.x - s_sn * wpos.z);
			float y = s_ybias * (s_sn * wpos.x + s_cs * wpos.z);


			// 画面の中心を原点にする
			//x += 540f * 0.5f;
			//y += 960f * 0.5f;

			return new float3( x, y, 0 );
		}
	}
}
