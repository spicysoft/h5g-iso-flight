using Unity.Entities;

namespace IsoFlight
{
	public struct GameMngr : IComponentData
	{
		//public bool IsTitleFinished;// タイトル終了したか.
		public bool Initialized;
		public bool IsPause;        // ポーズするか.
		public bool ReqGameOver;    // ゲームオーバー.
		public int Mode;            // モード.
		public int Score;           // スコア.
		public float GameTimer;     // ゲームタイマー.
		public float ModeTimer;     // モードタイマー.
	}
}
