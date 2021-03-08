using System;

namespace Stella.GameLogic.Environment
{
    public class Obstacle : Block
    {
        public override void SetSprite()
        {
            base.SetSprite();
            Set();
        }

        public void Set()
        {
            var subType = Id.Id;
            if (subType == 5)
            {
                gameObject.AddComponent<SpikeGenerator>();
            }
        }
    }
}