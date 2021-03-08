using System;
using Stella.Utils;
using UniRx;

namespace Stella.Data.Enums
{
    public enum ItemType
    {
        None,
        Floor,
        Water,
        Obstacles,
    }

    public enum MapThemeType
    {
        None,
        Common, // Dirt
        Autumn,
        Spring,
        Winter,
    }

    public enum MapType
    {
        None,
        Two, // 2d
        Two_Five, // 2.5d
    }

    public enum CommonBlockId
    {
        Asset40,
        Asset41,
        Column,
        ColumnDown,
        ColumnUp,
        Dirt,
        DirtDown,
        DirtLeft,
        DirtLeftCorner,
        DirtRight,
        DirtRightCorner,
    }

    public enum TileBlockId
    {
        Grass,
        GrassCliffLeft,
        GrassCliffMid,
        GrassCliffRight,
        GrassColumn,
        GrassHillLeft,
        GrassHillLeft2DownShadow,
        GrassHillLeft2,
        GrassHillRight,
        GrassHillRight2DownShadow,
        GrassHillRight2,
        GrassJoinHillLeft,
        GrassJoinHillLeft2DownShadow,
        GrassJoinHillLeft2,
        GrassJoinHillLeftAndRight,
        GrassJoinHillRight,
        GrassJoinHillRight2DownShadow,
        GrassJoinHillRight2AndLeft2DownShadow,
        GrassJoinHillRight2AndLeft2,
        GrassJoinHillRight2,
        GrassLeft,
        GrassMid,
        GrassRight,
    }

    public enum ObstacleType // 필요할까?
    {
        
    }

    public enum GameState
    {
        Loading,
        Ready,
        Play,
        Over,
        Clear,
    }

    public class GameStateRxProp : ReactiveProperty<GameState>
    {
        public GameStateRxProp(GameState initialValue) : base(initialValue)
        {
            
        }
    }

    public class TileBlockIdUtil : EnumUtil<TileBlockId> {}
    public class CommonBlockIdUtil : EnumUtil<CommonBlockId> {}
}
