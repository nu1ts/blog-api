using blog_api.Data;
using blog_api.Models;
using Microsoft.EntityFrameworkCore;

namespace blog_api.Services;

public class AddressService
{
    private readonly GarDbContext _garDbContext;

    public AddressService(GarDbContext garDbContext)
    {
        _garDbContext = garDbContext;
    }

    public async Task<List<SearchAddressModel>> Search(long parentObjectId, string? query)
    {
        var hierarchyQuery = _garDbContext.AsAdmHierarchies.AsQueryable();

        hierarchyQuery = hierarchyQuery
            .Where(obj => obj.Parentobjid == parentObjectId && obj.Isactive == 1);
        
        var result = new List<SearchAddressModel>();

        if (await hierarchyQuery.AnyAsync())
        {
            result = await SearchInAddrObj(hierarchyQuery);

            if (!result.Any())
            {
                result = await SearchInHouses(hierarchyQuery);
            }
        }
        
        if (query != null)
        {
            result = result
                .Where(x => x.Text != null 
                            && x.Text.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        
        return result;
    }

    private async Task<List<SearchAddressModel>> SearchInAddrObj(IQueryable<AsAdmHierarchy> hierarchyQuery)
    {
        var result = new List<SearchAddressModel>();

        var objectIds = await hierarchyQuery
            .Select(h => h.Objectid)
            .ToListAsync();
        
        var addrObjs = await _garDbContext.AsAddrObjs
            .Where(a => objectIds.Contains(a.Objectid) && a.Isactive == 1 && a.Isactual == 1)
            .Select(a => new SearchAddressModel
            {
                ObjectId = a.Objectid,
                ObjectGuid = a.Objectguid,
                Text = $"{a.Typename} {a.Name}",
                ObjectLevel = GetAddressLevel(Convert.ToInt32(a.Level)),
                ObjectLevelText = GetAddressName(Convert.ToInt32(a.Level))
            })
            .ToListAsync();

        result.AddRange(addrObjs);

        return result;
    }

    private async Task<List<SearchAddressModel>> SearchInHouses(IQueryable<AsAdmHierarchy> hierarchyQuery)
    {
        var result = new List<SearchAddressModel>();

        var objectIds = await hierarchyQuery
            .Select(h => h.Objectid)
            .ToListAsync();
        
        var houses = await _garDbContext.AsHouses
            .Where(h => objectIds.Contains(h.Objectid) && h.Isactive == 1 && h.Isactual == 1)
            .Select(h => new SearchAddressModel
            {
                ObjectId = h.Objectid,
                ObjectGuid = h.Objectguid,
                Text = BuildHouseName(h),
                ObjectLevel = GarAddressLevel.Building,
                ObjectLevelText = GetAddressName(10)
            })
            .ToListAsync();

        result.AddRange(houses);

        return result;
    }

    private static readonly Dictionary<int, GarAddressLevel> AddressLevelMapping = new()
    {
        { 1, GarAddressLevel.Region },
        { 2, GarAddressLevel.AdministrativeArea },
        { 3, GarAddressLevel.MunicipalArea },
        { 4, GarAddressLevel.RuralUrbanSettlement },
        { 5, GarAddressLevel.City },
        { 6, GarAddressLevel.Locality },
        { 7, GarAddressLevel.ElementOfPlanningStructure },
        { 8, GarAddressLevel.ElementOfRoadNetwork },
        { 9, GarAddressLevel.Land },
        { 10, GarAddressLevel.Building },
        { 11, GarAddressLevel.Room },
        { 12, GarAddressLevel.RoomInRooms },
        { 13, GarAddressLevel.AutonomousRegionLevel },
        { 14, GarAddressLevel.IntracityLevel },
        { 15, GarAddressLevel.AdditionalTerritoriesLevel },
        { 16, GarAddressLevel.LevelOfObjectsInAdditionalTerritories },
        { 17, GarAddressLevel.CarPlace }
    };

    private static readonly Dictionary<int, string> AddressNameMapping = new()
    {
        { 1, "Субъект РФ" },
        { 2, "Административный район" },
        { 3, "Муниципальный район" },
        { 4, "Сельское/городское поселение" },
        { 5, "Город" },
        { 6, "Населенный пункт" },
        { 7, "Элемент планировочной структуры" },
        { 8, "Элемент улично-дорожной сети" },
        { 9, "Земельный участок" },
        { 10, "Здание (сооружение)" },
        { 11, "Помещение" },
        { 12, "Помещения в пределах помещения" },
        { 13, "Уровень автономного округа" },
        { 14, "Уровень внутригородской территории" },
        { 15, "Уровень дополнительных территорий" },
        { 16, "Уровень объектов на дополнительных территориях" },
        { 17, "Машиноместо" }
    };

    private static readonly Dictionary<int, string> HouseTypeMapping = new()
    {
        { 1, "корпус" },
        { 2, "строение" },
        { 3, "сооружение" },
        { 4, "литера" }
    };

    private static GarAddressLevel GetAddressLevel(int? level)
    {
        return AddressLevelMapping.GetValueOrDefault(level ?? 0, GarAddressLevel.Region);
    }

    private static string GetAddressName(int? level)
    {
        return AddressNameMapping.GetValueOrDefault(level ?? 0, string.Empty);
    }

    private static string GetHouseType(int? type)
    {
        return HouseTypeMapping.GetValueOrDefault(type ?? 0, string.Empty);
    }


    private static string BuildHouseName(AsHouse house)
    {
        var parts = new List<string>();

        if (!string.IsNullOrEmpty(house.Housenum))
        {
            parts.Add(house.Housenum);
        }

        if (!string.IsNullOrEmpty(house.Addtype1.ToString()))
        {
            parts.Add(GetHouseType(house.Addtype1));
            parts.Add(house.Addnum1 ?? string.Empty);
        }
        
        if (string.IsNullOrEmpty(house.Addtype2.ToString()))
        {
            return string.Join(", ", parts).Trim();
        }

        parts.Add(GetHouseType(house.Addtype2));
        parts.Add(house.Addnum2 ?? string.Empty);

        return string.Join(", ", parts).Trim();
    }
}