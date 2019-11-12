using Unity.Entities;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
using Unity.Mathematics;
using Unity.Tiny.Scenes;
using Unity.Tiny.Debugging;
using Unity.Tiny.UIControls;

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
			Entities.ForEach( ( ref GameMngr mngr ) => {
				IsPause = mngr.IsPause;
			} );
			if( IsPause )
				return;


			bool btnUpOn = false;
			bool btnDownOn = false;
			bool btnLeftOn = false;
			bool btnRightOn = false;
			Entities.WithAll<BtnUpTag>().ForEach( ( ref PointerInteraction pointerInteraction ) => {
				if( pointerInteraction.down ) {
					btnUpOn = true;
				}
			} );
			Entities.WithAll<BtnDownTag>().ForEach( ( ref PointerInteraction pointerInteraction ) => {
				if( pointerInteraction.down ) {
					btnDownOn = true;
				}
			} );
			Entities.WithAll<BtnLeftTag>().ForEach( ( ref PointerInteraction pointerInteraction ) => {
				if( pointerInteraction.down ) {
					btnLeftOn = true;
				}
			} );
			Entities.WithAll<BtnRightTag>().ForEach( ( ref PointerInteraction pointerInteraction ) => {
				if( pointerInteraction.down ) {
					btnRightOn = true;
				}
			} );




			var deltaTime = World.TinyEnvironment().frameDeltaTime;

			Entities.ForEach( ( ref PlayerInfo player, ref WorldPosInfo info ) => {
				if( !player.Initialized ) {
					player.Initialized = true;
					info.Wpos = new float3( 0f, 80f, -60f );
					return;
				}

				// 移動.
				//info.Wpos.z += 40f*deltaTime;

				float spd = 60f * deltaTime;

				if( btnUpOn ) {
					info.Wpos.y += spd;
				}
				else if( btnDownOn ) {
					info.Wpos.y -= spd;
				}
				if( btnLeftOn ) {
					info.Wpos.x -= spd;
				}
				else if( btnRightOn ) {
					info.Wpos.x += spd;
				}


			} );

		}

	}
}
