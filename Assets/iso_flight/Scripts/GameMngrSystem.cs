using Unity.Entities;
using Unity.Tiny.Debugging;
using Unity.Collections;
using Unity.Tiny.Core;
using Unity.Tiny.Input;
using Unity.Tiny.Scenes;
using Unity.Tiny.Text;
using Unity.Tiny.UILayout;
using Unity.Tiny.Core2D;
using Unity.Tiny.UIControls;

namespace IsoFlight
{
	public class GameMngrSystem : ComponentSystem
	{
		// 境界.
		public const float BorderUp = 800f;
		public const float BorderLow = -800f;
		public const float BorderLeft = -800f;
		public const float BorderRight = 800f;


		//public const float GameTimeLimit = 190f;        // ゲーム時間.
		//public const int MdTitle = 0;
		public const int MdGame = 0;
		public const int MdPreGameOver = 1;
		public const int MdGameOver = 2;
		public const int MdResult = 3;
		public const float DefScrSpd = 60f;


		protected override void OnUpdate()
		{
			int score = 0;
			bool isPause = false;
			bool reqGameOver = false;
			bool reqResult = false;
			bool btnDebOn = false;

			Entities.WithAll<BtnDebTag>().ForEach( ( ref PointerInteraction pointerInteraction ) => {
				if( pointerInteraction.clicked ) {
					btnDebOn = true;
				}
			} );

			Entities.ForEach( ( ref GameMngr mngr ) => {
				if( !mngr.Initialized ) {
					adjustCanvas();
					mngr.Initialized = true;
					mngr.ScrollSpd = DefScrSpd;
					mngr.Score = 0;
					mngr.GameTimer = 0;
					mngr.ModeTimer = 0;
					mngr.Mode = 0;
					mngr.IsPause = false;
					return;
				}

				if( btnDebOn ) {
					if( mngr.ScrollSpd > 0 )
						mngr.ScrollSpd = 0;
					else
						mngr.ScrollSpd = DefScrSpd;
				}

				float dt = World.TinyEnvironment().frameDeltaTime;

				switch( mngr.Mode ) {
				case MdGame:
					if( mngr.ReqGameOver ) {
						mngr.ReqGameOver = false;
						mngr.Mode = MdPreGameOver;
						mngr.ModeTimer = 0;
					}
					break;
				case MdPreGameOver:
					mngr.ModeTimer += dt;
					if( mngr.ModeTimer > 0.5f ) {
						reqGameOver = true;
						mngr.Mode = MdGameOver;
						mngr.ModeTimer = 0;
					}
					break;
				case MdGameOver:
					mngr.ModeTimer += dt;
					if( mngr.ModeTimer > 1.5f ) {
						mngr.Mode = MdResult;
						reqResult = true;
					}
					break;
				}

				isPause = mngr.IsPause;
				mngr.Score = (int)mngr.GameTimer;
				score = mngr.Score;

				if( mngr.IsPause ) {
					//isPause = true;
					return;
				}

				// タイマー.
				mngr.GameTimer += dt;
#if false
				timer = mngr.GameTimer;
				if( timer >= GameTimeLimit ) {
					mngr.IsPause = true;
				}
#endif
			} );


#if true
			// タイマー表示.
			if( !isPause ) {
				Entities.WithAll<TxtScoreTag>().ForEach( ( Entity entity ) => {
					EntityManager.SetBufferFromString<TextString>( entity, score.ToString() );
				} );
			}
#endif
			if( reqResult ) {
#if true
				// ゲームオーバーシーンアンロード.
				SceneReference gameoverScn = World.TinyEnvironment().GetConfigData<GameConfig>().GameOverScn;
				SceneService.UnloadAllSceneInstances( gameoverScn );
				// リザルト表示.
				SceneReference resultScn = World.TinyEnvironment().GetConfigData<GameConfig>().ResultScn;
				SceneService.LoadSceneAsync( resultScn );
#endif
			}
			else if( reqGameOver ) {
				// ゲームオーバー表示.
				SceneReference scn = new SceneReference();
				scn = World.TinyEnvironment().GetConfigData<GameConfig>().GameOverScn;
				SceneService.LoadSceneAsync( scn );
			}

		}


		void adjustCanvas()
		{
			Entities.WithAll<CanvasTag>().ForEach( ( ref UICanvas canvas ) => {
				// ディスプレイ情報.
				var displayInfo = World.TinyEnvironment().GetConfigData<DisplayInfo>();
				float frameW = displayInfo.frameWidth;
				float frameH = (float)displayInfo.frameHeight;

				// キャンバス情報.
				float matchval = 1f;
				if( frameH >= frameW )
					matchval = 0;

				canvas.matchWidthOrHeight = matchval;
			} );
		}
	}
}
