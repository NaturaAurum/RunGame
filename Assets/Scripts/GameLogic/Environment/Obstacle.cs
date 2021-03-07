using System;

namespace Stella.GameLogic.Environment
{
    public class Obstacle : Block
    {
        public void Set()
        {
            var itemType = Id.Type;
            var subType = Id.Id;

            if (subType == 5)
            {
                gameObject.AddComponent<SpikeGenerator>();
            }
        }
    }
}