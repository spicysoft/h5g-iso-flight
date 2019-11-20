using Unity.Entities;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
using Unity.Mathematics;
using Unity.Tiny.Scenes;
using Unity.Tiny.Debugging;
using Unity.Tiny.UIControls;
using Unity.Tiny.Text;

namespace IsoFlight
{
	public class PlayerSystem : ComponentSystem
	{
		// 半径.
		//public const float PlayerR = 30f;
		//public const float PlayerRsq = PlayerR * PlayerR;


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
			bool isHit = false;

			Entities.ForEach( ( ref PlayerInfo player, ref WorldPosInfo info ) => {
				if( !player.Initialized ) {
					player.Initialized = true;
					info.Wpos = new float3( 0f, 75f, -BlockSystem.UnitZ );
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

				// 境界チェック.

				if( info.Wpos.x < -150f + 15f )
					info.Wpos.x = -150f + 15f;
				else if( info.Wpos.x > 150f - 10f )
					info.Wpos.x = 150f - 10f;

				if( info.Wpos.y < 5f )
					info.Wpos.y = 5f;
				else if( info.Wpos.y > 200f - 15f )
					info.Wpos.y = 200f - 15f;


				// コリジョンチェック.

				player.CellPos.x = (int)(info.Wpos.x / BlockSystem.UnitX);
				player.CellPos.y = (int)(info.Wpos.y / BlockSystem.UnitY);
				player.CellPos.z = (int)(info.Wpos.z / BlockSystem.UnitZ);

				int3 pcCell = player.CellPos;
				float3 pcCenter = info.Wpos;
				pcCenter.y += 10f;


				Entities.ForEach( ( ref BlockInfo block, ref WorldPosInfo blockW ) => {
					if( isNear( block.CellPos, pcCell ) ) {

						float pcRad = 5f;

						float maxY = blockW.Wpos.y + 25f + pcRad;
						float minY = blockW.Wpos.y - 25f - pcRad;

						if( pcCenter.y < minY || pcCenter.y > maxY ) {
							return;
						}

						float maxX = blockW.Wpos.x + 30f + pcRad;
						float minX = blockW.Wpos.x - 30f - pcRad;

						if( pcCenter.x < minX || pcCenter.x > maxX ) {
							return;
						}

						float maxZ = blockW.Wpos.z + 30f + pcRad;
						float minZ = blockW.Wpos.z - 30f - pcRad;

						if( pcCenter.z > minZ && pcCenter.z < maxZ ) {
							isHit = true;
						}

#if false
						float3 center = blockW.Wpos;
						center.y += 30f;
						float l = math.distancesq( pcCenter, center );
						if( l < 20f * 20f + 30f * 30f ) {
							isnear = true;
						}
#endif
					}
				} );

			} );


			if( isHit ) {
				Entities.ForEach( ( ref GameMngr mngr ) => {
					mngr.IsPause = true;
					mngr.ReqGameOver = true;
				} );
				SceneService.LoadSceneAsync( World.TinyEnvironment().GetConfigData<GameConfig>().EffExplScn );
			}


			Entities.WithAll<DebTextTag>().ForEach( ( Entity entity ) => {
				EntityManager.SetBufferFromString<TextString>( entity, isHit.ToString() );
			} );

		}


		bool isNear( int3 cell1, int3 cell2 )
		{
			//return true;
#if true
			if( math.abs( cell1.y - cell2.y ) < 2 &&
				math.abs( cell1.x - cell2.x ) < 2 &&
				math.abs( cell1.z - cell2.z ) < 2 ) {
				return true;
			}
			return false;
#endif
		}

	}
}
