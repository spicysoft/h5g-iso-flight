using Unity.Entities;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
using Unity.Mathematics;
using Unity.Tiny.Scenes;
using Unity.Tiny.Debugging;

namespace IsoFlight
{
	public class PlayerSystem : ComponentSystem
	{
		// 半径.
		public const float PlayerR = 30f;
		public const float PlayerRsq = PlayerR * PlayerR;


		protected override void OnUpdate()
		{
			bool IsPause = false;
			/*Entities.ForEach( ( ref GameMngr mngr ) => {
				IsPause = mngr.IsPause;
			} );*/
			if( IsPause )
				return;

			var deltaTime = World.TinyEnvironment().frameDeltaTime;

			Entities.ForEach( ( ref PlayerInfo player, ref WorldPosInfo info, ref Translation trans, ref Rotation rot, ref LayerSorting layer ) => {
				if( !player.Initialized ) {
					player.Initialized = true;
					info.Wpos = new float3( 0f, 80f, -60f );
					return;
				}

				// 移動.
				info.Wpos.z += 40f*deltaTime;

			} );

		}

	}
}
