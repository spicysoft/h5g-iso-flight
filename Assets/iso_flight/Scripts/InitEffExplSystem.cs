using Unity.Entities;
using Unity.Mathematics;

namespace IsoFlight
{
	public class InitEffExplSystem : ComponentSystem
	{
		protected override void OnUpdate()
		{
			Entities.ForEach( ( Entity entity, ref EffExplInfo eff, ref WorldPosInfo wpos ) => {

				if( !eff.Initialized ) {
					float3 explPos = float3.zero;
					Entities.ForEach( ( ref PlayerInfo player, ref WorldPosInfo pcWpos ) => {
						explPos = pcWpos.Wpos;
					} );
					wpos.Wpos = explPos;

					eff.Initialized = true;
				}
			} );
		}
	}
}
