using PlantManagerServer.Models;
using Shared.Models;

namespace PlantManagerServer.Helpers;

public class Converts
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

}