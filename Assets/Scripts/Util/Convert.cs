using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Util
{
    public static class Converter
    {

        public static float ConvertColor(float val)
        {
            return val / 255;
        }

        public static float ConvertToNormalColorValue(float val)
        {
            return val * 255f;
        }


        public static float ConvertToUnityColorValue(float val)
        {
            return val / 255;
        }
    }
}
