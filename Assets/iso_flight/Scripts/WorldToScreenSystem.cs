using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
using Unity.Tiny.Debugging;

namespace IsoFlight
{
	[UpdateAfter( typeof( InitBlockSystem ) )]
	[UpdateAfter( typeof( PlayerSystem ) )]
	public class WorldToScreenSystem : ComponentSystem
	{
		private readonly float s_sn = math.sin( -45f * math.PI / 180f );
		private readonly float s_cs = math.cos( -45f * math.PI / 180f );
		private const float s_xbias = 1f;
		private const float s_ybias = 1f / 1.73205f; // 1f/sqrt(3f);


		protected override void OnUpdate()
		{
			Entities.ForEach( ( ref WorldPosInfo info, ref Translation trans, ref LayerSorting layer ) => {

				float3 spos = CalcScreenPos( info.Wpos );
				trans.Value = spos;

				// プライオリティ.
				if( !info.DontCalcOrder ) {
					float order = 500f + info.Wpos.x - info.Wpos.z;     // マイナスにならないように500たす.
					order += info.Wpos.y * 1.5f;  // yの重みあげる.
					layer.order = (short)order;
				}
			} );
		}


		public float3 CalcScreenPos( float3 wpos )
		{
			float x = s_xbias * (s_cs * wpos.x - s_sn * wpos.z);
			float y = s_ybias * (s_sn * wpos.x + s_cs * wpos.z);

			y += wpos.y;

			return new float3( x, y, 0 );
		}
	}
}
