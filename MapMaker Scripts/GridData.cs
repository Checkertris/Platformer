using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Wrenji.Assets.Tools.Scripts;

namespace Wrenji.Assets.Tools.Scripts
{
    public class GridData
    {

        //fields
        public CellData[] cells;



        //Methods
        public static long CompressVector(Vector3Int vector)
        {
            return GetAsLong(vector.x, vector.y, vector.z);
        }

        public static long GetAsLong(int x, int y, int z)
        {
            return ((long)(x & 0x3FFFFFF) << 38) | ((long)(z & 0x3FFFFFF) << 12) | (long)(y & 0xFFF);
        }

        public static Vector3Int DecompressVector(long compressed)
        {
            int x = (int)(compressed >> 38);
            int y = (int)(compressed & 0xFFF);
            int z = (int)(compressed << 26 >> 38);
            return new Vector3Int(x, y, z);
        }

        private IDictionary<long, CellData> position2Data = new Dictionary<long, CellData>();

        public CellData GetDataForPosition(Vector3Int pos)
        {
            try
            {
                return this.position2Data[CompressVector(pos)];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public void SetCellData(Vector3Int pos, CellData data)
        {
            long compressed = CompressVector(pos);
            this.position2Data.Remove(compressed);
            this.position2Data.Add(compressed, data);
        }
    }
}