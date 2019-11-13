using Unity.Entities;
using Unity.Mathematics;

namespace IsoFlight
{
	public struct BlockInfo : IComponentData
	{
		public bool IsActive;
		public bool Initialized;
		public float Timer;
		public float Vx;
		public float Vy;
		public int3 CellPos;
	}
}
