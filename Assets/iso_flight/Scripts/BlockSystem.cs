using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
using Unity.Tiny.Debugging;

namespace IsoFlight
{
	public class BlockSystem : ComponentSystem
	{
		//private readonly float s_sn = math.sin( -45f * math.PI / 180f );
		//private readonly float s_cs = math.cos( -45f * math.PI / 180f );
		private const float s_xbias = 1f;
		private const float s_ybias = 9f / 16f;		// 帳尻合わせ.


		protected override void OnUpdate()
		{
			Entities.ForEach((ref BlockInfo block, ref Translation trans, ref NonUniformScale scl, ref LayerSorting layer) => {
				//float dt = World.TinyEnvironment().frameDeltaTime;
				
				//block.Wpos.z += 10f * dt;
/*
				float3 spos = CalcScreenPos( block.Wpos );

				//float order = 480 - spos.y;
				//order += block.Wpos.y;
				float order = 200f + block.Wpos.x - block.Wpos.z;
				order += block.Wpos.y * 2f;

				layer.order = (short)order;

				Debug.LogFormatAlways("blk {0} {1} {2}", order, spos.y, block.Wpos.y);

				trans.Value = spos;
				*/
			});
		}


		static public float3 CalcScreenPos( float3 wpos )
		{
			float s_sn = math.sin( -45f * math.PI / 180f );
			float s_cs = math.cos( -45f * math.PI / 180f );

			float x = s_xbias * (s_cs * wpos.x - s_sn * wpos.z);
			float y = s_ybias * (s_sn * wpos.x + s_cs * wpos.z);

			y += wpos.y;

			return new float3( x, y, 0 );
		}
	}
}
