using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models;

// 顺序要和excel中的顺序保持一直，要不然读取的时候会出错
[Table("plant_table", Schema = "public")]
public class PlantTable
{
    [Column("object_id")]
    public int ObjectId { get; set; } // OBJECTID

    [Key]
    [Column("id")]
    public long Id { get; set; } // 乔木编号

    [Column("cn_name")]
    public string CnName { get; set; } = ""; // 中文名称

    [Column("latin_name")]
    public string LatinName { get; set; } = ""; // 拉丁学名

    [Column("common_name")]
    public string CommonName { get; set; } = ""; // 俗名土名

    [Column("longitude")]
    public double Longitude { get; set; } // 经度

    [Column("latitude")]
    public double Latitude { get; set; } // 纬度

    [Column("district")]
    public string District { get; set; } = ""; // 城区

    [Column("street_town")]
    public string StreetTown { get; set; } = "";// 街道乡镇

    [Column("community")]
    public string Community { get; set; } = ""; // 社区

    [Column("road_name")]
    public string RoadName { get; set; } = ""; // 道路名

    [Column("road_start")]
    public string RoadStart { get; set; } = ""; // 路段起始位置

    [Column("road_end")]
    public string RoadEnd { get; set; } = ""; // 路段截至位置

    [Column("green_space_type")]
    public string GreenSpaceType { get; set; } = "";// 绿地类型

    [Column("located_in_road_direction")]
    public string LocatedInRoadDirection { get; set; } = ""; // 位于道路方向

    [Column("age")]
    public int? Age { get; set; } // 树龄

    [Column("age_determination_method")]
    public string? AgeDeterminationMethod { get; set; } // 树龄确定方法

    [Column("chest_diameters")]
    public float[] ChestDiameters { get; set; } = new float[0];// 胸径

    [Column("height")]
    public float Height { get; set; } // 树高 (单位: 米)

    [Column("crown_spread_e_w")]
    public float CrownSpreadEW { get; set; } // 冠幅 (东西) (单位: 米)

    [Column("crown_spread_s_n")]
    public float CrownSpreadSN { get; set; } // 冠幅 (南北) (单位: 米)

    [Column("pool_shape")]
    public string PoolShape { get; set; } = ""; // 树穴（树池）形状

    [Column("circle_cave")]
    public string? CircleCave { get; set; } // 圆形树穴

    [Column("square_length")]
    public float? SquareLength { get; set; } // 方形树穴长 (单位: 厘米)

    [Column("square_width")]
    public float? SquareWidth { get; set; } // 方形树穴宽 (单位: 厘米)

    [Column("pest_and_pathogen_damage")]
    public string PestAndPathogenDamage { get; set; } = ""; // 病虫危害

    [Column("soil")]
    public string Soil { get; set; } = "";// 土壤

    [Column("ground_condition")]
    public string GroundCondition { get; set; } = ""; // 地面状况

    [Column("growth")]
    public string Growth { get; set; } = ""; // 生长势

    [Column("root_stem_leaf_condition")]
    public string RootStemLeafCondition { get; set; } = ""; // 根茎叶状况

    [Column("tilt")]
    public float? Tilt { get; set; } // 倾斜度

    [Column("divided_plants")]
    public int? DividedPlants { get; set; } // 胸径以下分株数

    [Column("conservation_status")]
    public string ConservationStatus { get; set; } = ""; // 历史养护情况

    [Column("external_factors_affecting_growth")]
    public string ExternalFactorsAffectingGrowth { get; set; } = "无"; // 影响树木生长的外部因素

    [Column("external_security_risks")]
    public string ExternalSecurityRisks { get; set; } = "无"; // 对外部环境的安全隐患

    [Column("protection_measures")]
    public string ProtectionMeasures { get; set; } = "刷石灰";// 现状保护措施

    [Column("remarks")]
    public string? Remarks { get; set; } // 备注

    [Column("investigator")]
    public string Investigator { get; set; } = ""; // 调查员

    [Column("investigation_time")]
    public long InvestigationTime { get; set; } // 调查事件

    [Column("investigation_number")]
    public string InvestigationNumber { get; set; } = ""; // 调查编号

    [Column("listing_batch")]
    public int? ListingBatch { get; set; } // 挂牌批次

    [Column("plate_number")]
    public string? PlateNumber { get; set; } // 制牌编号

    [Column("tag_number2")]
    public string? TagNumber2 { get; set; } // 挂牌编号2

    [Column("taged")]
    public bool? Taged { get; set; } // 是否挂牌
}
