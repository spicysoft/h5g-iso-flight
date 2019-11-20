using Unity.Entities;
using Unity.Mathematics;

namespace IsoFlight
{
	public struct BlockInfo : IComponentData
	{
		public bool IsActive;
		public bool Initialized;
		public bool ShadowChecked;
		public float Timer;
		public int3 CellPos;
		public Entity shadowEntity;		// 下.
		public Entity shadowEntity2;	// 横.
	}
}
