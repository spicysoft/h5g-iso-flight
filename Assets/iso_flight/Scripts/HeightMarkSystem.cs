using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny.Core2D;

namespace IsoFlight
{
	[UpdateAfter( typeof( PlayerSystem ) )]
	public class HeightMarkSystem : ComponentSystem
	{
		protected override void OnUpdate()
		{
			float3 pcPos = float3.zero;

			Entities.ForEach( ( ref PlayerInfo player, ref WorldPosInfo info ) => {
				pcPos = info.Wpos;
			} );

			Entities.ForEach( ( ref HeightMarkInfo mark, ref WorldPosInfo info ) => {
				info.Wpos = pcPos;
				info.Wpos.x = -150f;
			} );

		}
	}
}
