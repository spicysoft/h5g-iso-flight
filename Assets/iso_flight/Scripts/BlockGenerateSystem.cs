using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny.Core;
using Unity.Tiny.Debugging;
using Unity.Tiny.Scenes;

namespace IsoFlight
{
	public class BlockGenerateSystem : ComponentSystem
	{
		private const int BallMax = 60;
		Random _random;

		protected override void OnUpdate()
		{
			bool reqGen = false;
			bool isPause = false;
			float gameTime = 0;


			Entities.ForEach( ( ref GameMngr mngr ) => {
				isPause = mngr.IsPause;
				gameTime = mngr.GameTimer;
			} );

			if( isPause )
				return;


			Entities.ForEach( ( Entity entity, ref BlockGenerateInfo gen ) => {
				if( !gen.Initialized ) {
					gen.Initialized = true;
					gen.Timer = 0;
					gen.Interval = 10f;

					int seed = World.TinyEnvironment().frameNum;
					_random.InitState( (uint)seed );

					reqGen = true;  // test
					return;
				}

				float dt = World.TinyEnvironment().frameDeltaTime;
				gen.Timer += dt;
				if( gen.Timer > gen.Interval ) {
					gen.Timer = 0;
					// インターバル減らす.
					if( gen.Interval > 5f )
						gen.Interval -= 0.25f;
					reqGen = true;
					//Debug.LogFormatAlways("intvl {0}", gen.Interval);
				}

			} );

			if( reqGen ) {

				int idx = _random.NextInt( 10 );
				int tblId = 0;
				if( gameTime > 60f ) {
					tblId = 1;
				}


				int[] ptn;
				if( tblId == 0 )
					ptn = BlockPattern.Tbl1[idx];
				else
					ptn = BlockPattern.Tbl2[idx];
				int num = 0;
				for( int i = 0; i < ptn.Length; ++i ) {
					if( ptn[i] != 0 )
						num++;
				}

				Entities.ForEach( ( ref InitBlockInfo info ) => {
					info.BlockCntr = 0;
					info.GeneretedNum = num;
					info.GeneretedTbl = tblId;
					info.GeneretedPtn = idx;
				} );

				//Debug.LogAlways( "--reqgen" );

				for( int i = 0; i < num; i++ ) {
					bool recycled = false;
					Entities.ForEach( ( Entity entity, ref BlockInfo block ) => {
						if( !recycled ) {
							if( !block.IsActive ) {
								block.IsActive = true;
								block.Initialized = false;
								recycled = true;
							}
						}
					} );

					//Debug.LogFormatAlways( "recycled {0}", recycled );

					if( !recycled ) {
						var env = World.TinyEnvironment();
						SceneService.LoadSceneAsync( env.GetConfigData<GameConfig>().BlockScn );
					}
				}
			}
		}
	}
}
