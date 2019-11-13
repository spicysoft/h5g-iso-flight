using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
using Unity.Tiny.Debugging;

namespace IsoFlight
{
	public class BlockSystem : ComponentSystem
	{
		// セルサイス.
		public const float UnitX = 60f;
		public const float UnitY = 50f;
		public const float UnitZ = 60f;

		protected override void OnUpdate()
		{
			float dt = World.TinyEnvironment().frameDeltaTime;
			float scrollSpd = 0;

			Entities.ForEach( ( ref GameMngr mngr ) => {
				scrollSpd = mngr.ScrollSpd;
			} );

			Entities.ForEach( ( ref BlockInfo block, ref WorldPosInfo info ) => {

				info.Wpos.z -= scrollSpd * dt;

				// スクロールアウト.
				if( info.Wpos.z < -8f * UnitZ ) {
					// 消す.
				}

				block.CellPos.x = (int)(info.Wpos.x / BlockSystem.UnitX);
				block.CellPos.y = (int)(info.Wpos.y / BlockSystem.UnitY);
				block.CellPos.z = (int)(info.Wpos.z / BlockSystem.UnitZ);

			} );
		}

	}
}
