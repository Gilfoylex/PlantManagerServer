namespace PlantManagerServer.Models;

public class PlantInfoDisplay
{
    public long Id { get; set; } // 乔木编号

    public string CnName { get; set; } = ""; // 中文名称

    public string LatinName { get; set; } = ""; // 拉丁学名

    public string CommonName { get; set; } = ""; // 俗名土名

    public double Longitude { get; set; } // 经度

    public double Latitude { get; set; } // 纬度

    public string District { get; set; } = ""; // 城区

    public string StreetTown { get; set; } = "";// 街道乡镇

    public string Community { get; set; } = ""; // 社区

    public string RoadName { get; set; } = ""; // 道路名

    public string RoadStart { get; set; } = ""; // 路段起始位置

    public string RoadEnd { get; set; } = ""; // 路段截至位置

    public string GreenSpaceType { get; set; } = "";// 绿地类型

    public string LocatedInRoadDirection { get; set; } = ""; // 位于道路方向

    public int? Age { get; set; } // 树龄

    public string? AgeDeterminationMethod { get; set; } // 树龄确定方法

    public float[] ChestDiameters { get; set; } = Array.Empty<float>();// 胸径

    public float Height { get; set; } // 树高 (单位: 米)

    public float CrownSpreadEW { get; set; } // 冠幅 (东西) (单位: 米)

    public float CrownSpreadSN { get; set; } // 冠幅 (南北) (单位: 米)

    public string PoolShape { get; set; } = ""; // 树穴（树池）形状

    public string? CircleCave { get; set; } // 圆形树穴

    public float? SquareLength { get; set; } // 方形树穴长 (单位: 厘米)

    public float? SquareWidth { get; set; } // 方形树穴宽 (单位: 厘米)

    public string PestAndPathogenDamage { get; set; } = ""; // 病虫危害

    public string Soil { get; set; } = "";// 土壤

    public string GroundCondition { get; set; } = ""; // 地面状况

    public string Growth { get; set; } = ""; // 生长势

    public string RootStemLeafCondition { get; set; } = ""; // 根茎叶状况

    public float? Tilt { get; set; } // 倾斜度

    public int? DividedPlants { get; set; } // 胸径以下分株数

    public string ConservationStatus { get; set; } = ""; // 历史养护情况

    public string ExternalFactorsAffectingGrowth { get; set; } = "无"; // 影响树木生长的外部因素

    public string ExternalSecurityRisks { get; set; } = "无"; // 对外部环境的安全隐患

    public string ProtectionMeasures { get; set; } = "刷石灰";// 现状保护措施

    public string? Remarks { get; set; } // 备注
}