using PlantManagerServer.Models;
using Shared.Models;

namespace PlantManagerServer.Helpers;

public static class Converts
{
   public static PlantInfoDisplay ConvertFromPlantTableToPlantInfoDisplay(PlantTable plantTable)
    {
        PlantInfoDisplay plantInfoDisplay = new()
        {
            Id = plantTable.Id,
            CnName = plantTable.CnName,
            LatinName = plantTable.LatinName,
            CommonName = plantTable.CommonName,
            Longitude = plantTable.Longitude,
            Latitude = plantTable.Latitude,
            District = plantTable.District,
            StreetTown = plantTable.StreetTown,
            Community = plantTable.Community,
            RoadName = plantTable.RoadName,
            RoadStart = plantTable.RoadStart,
            RoadEnd = plantTable.RoadEnd,
            GreenSpaceType = plantTable.GreenSpaceType,
            LocatedInRoadDirection = plantTable.LocatedInRoadDirection,
            Age = plantTable.Age,
            AgeDeterminationMethod = plantTable.AgeDeterminationMethod,
            ChestDiameters = plantTable.ChestDiameters,
            Height = plantTable.Height,
            CrownSpreadEW = plantTable.CrownSpreadEW,
            CrownSpreadSN = plantTable.CrownSpreadSN,
            PoolShape = plantTable.PoolShape,
            CircleCave = plantTable.CircleCave,
            SquareLength = plantTable.SquareLength,
            SquareWidth = plantTable.SquareWidth,
            PestAndPathogenDamage = plantTable.PestAndPathogenDamage,
            Soil = plantTable.Soil,
            GroundCondition = plantTable.GroundCondition,
            Growth = plantTable.Growth,
            RootStemLeafCondition = plantTable.RootStemLeafCondition,
            Tilt = plantTable.Tilt,
            DividedPlants = plantTable.DividedPlants,
            ConservationStatus = plantTable.ConservationStatus,
            ExternalFactorsAffectingGrowth = plantTable.ExternalFactorsAffectingGrowth,
            ExternalSecurityRisks = plantTable.ExternalSecurityRisks,
            ProtectionMeasures = plantTable.ProtectionMeasures,
            Remarks = plantTable.Remarks
        };

        return plantInfoDisplay;
    }

   public static Size GetScaleSize(int srcWidth, int srcHeight, int destWidth, int destHeight)
   {
       if (destWidth == 0 && destHeight == 0) // 如果目标宽度和高度都为0，直接返回原始尺寸
       {
           return new Size(srcWidth, srcHeight);
       }
       else if (destWidth == 0 || destHeight == 0) // 如果目标宽度和高度有一个为0，按比例缩放
       {
           if (destWidth == 0) // 以目标高度为基准缩放
           {
               var scale = (double)destHeight / srcHeight;
               var width = (srcWidth * scale).ToInt();
               return new Size(width, destHeight);
           }
           else // 以目标宽度为基准缩放
           {
               var scale = (double)destWidth / srcWidth;
               var height = (srcHeight * scale).ToInt();
               return new Size(destWidth, height);
           }
       }
       else // 如果目标宽度和高度都不为0，直接使用产生新的尺寸
       {
           return new Size(destWidth, destHeight);
       }
   }

   public static int ToInt(this double value)
   {
       return Convert.ToInt32(Math.Round(value, MidpointRounding.AwayFromZero));
   }
}