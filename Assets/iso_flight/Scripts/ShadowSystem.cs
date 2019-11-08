using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny.Core2D;

namespace IsoFlight
{
	[UpdateAfter( typeof( PlayerSystem ) )]
	public class ShadowSystem : ComponentSystem
	{
		protected override void OnUpdate()
		{
			float3 pcPos = float3.zero;

			Entities.ForEach( ( ref PlayerInfo player, ref WorldPosInfo info ) => {
				pcPos = info.Wpos;
			} );

			Entities.ForEach( ( ref ShadowInfo shadow, ref WorldPosInfo info, ref Sprite2DRenderer render ) => {
				info.Wpos = pcPos;
				info.Wpos.y = 0;

				int iy = (int)(pcPos.y / 50f);
				float alpha = 0.6f - 0.1f*iy;
				if( alpha < 0.2f )
					alpha = 0.2f;

				render.color.a = alpha;

			} );

		}
	}
}
