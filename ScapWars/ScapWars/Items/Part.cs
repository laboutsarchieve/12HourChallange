using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrapWars.Items
{
    enum PartClass
    {
        ClassOne = 1,
        ClassTwo = 2,
        ClassThree = 3,
        ClassFour = 4,
        ClassFive = 5
    }

    abstract class Part
    {
        String partName;        
        PartClass partRank;

        public Part( String partName, PartClass partRank )
        {
            this.partName = partName;
            this.partRank = partRank;
        }

        public String PartName
        {
            get { return partName; }
            set { partName = value; }
        }

        public PartClass PartRank
        {
            get { return partRank; }
            set { partRank = value; }
        }
    }
}
