using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [CreateAssetMenu()]
    public class ColorRegionData : ScriptableObject
    {
        public List<SectionObject> sections;
        public int mode = 1;



        public ColorRegionData()
        {
            sections = new List<SectionObject>();
        }

        public List<SectionObject> LoadData()
        {
            return sections;
        }

    }
}
