using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiffO.Demo.Models
{
    public class DemoViewModel
    {
        public bool Reverse { get; set; }
        public ZooModel Bucharest { get; set; }
        public ZooModel Cluj { get; set; }
    }

    public class ZooModel : DiffObject
    {
        public LionModel Lion { get; set; }
        public BearModel Bear { get; set; }
        public List<BirdModel> Birds { get; set; }
    }

    public class LionModel : DiffObject
    {
        public string Name { get; set; }
        public LionModel Child { get; set; }
    }

    public class BearModel : DiffObject
    {
        public int Uid { get; set; }
        public string Color { get; set; }
        public string Name { get; set; }
    }

    public class BirdModel
    {
        public string Color { get; set; }
        public string Name { get; set; }
        public NestModel Nest { get; set; }

        public override bool Equals(Object obj)
        {
            var bird = obj as BirdModel;
            return bird != null && Equals(bird);
        }

        public bool Equals(BirdModel bird)
        {
            return String.CompareOrdinal(bird.Color, Color) == 0 &&
                   String.CompareOrdinal(bird.Name, Name) == 0 &&
                   String.CompareOrdinal(bird.Nest.Type, Nest.Type) == 0;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ 1337;
        }
    }

    public class NestModel
    {
        public string Type { get; set; }
    }
}