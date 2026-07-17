using DiarioAdestramento.Models;

namespace DiarioAdestramento.DTOs.Mappings;

public static class LocalDTOMappingExtensions
{
   
    public static LocalResponseDTO? ToLocalCreatedDTO(this Local local)
    {
        if(local is null) return null;

        return new LocalResponseDTO
        {
            Id = local.Id,
            Name = local.Nome,
            Latitude = local.Latitude,
            Longitude = local.Longitude,
          
        };
    }

    public static Local? ToLocal(this LocalCreatedDTO localCreatedDTO)
    {
        if(localCreatedDTO is null) return null;
        return new Local
        {
            Id = localCreatedDTO.Id,
            Nome = localCreatedDTO.Name,
            Latitude = localCreatedDTO.Latitude,
            Longitude = localCreatedDTO.Longitude
        };
    }

    public static Local? ToLocalUpdadte(this LocalUpdateRequestDTO localUpdateRequestDTO)
    {
        if(localUpdateRequestDTO is null) return null;
        return new Local
        {
            Id = localUpdateRequestDTO.Id,
            Nome = localUpdateRequestDTO.Name,
            Latitude = localUpdateRequestDTO.Latitude,
            Longitude = localUpdateRequestDTO.Longitude
        };
    }
    public static LocalResponseDTO? ToLocalResponseDTO(this Local local)
    {
        if(local is null) return null;
        return new LocalResponseDTO
        {
            Id = local.Id,
            Name = local.Nome,
            Latitude = local.Latitude,
            Longitude = local.Longitude,
          
        };
    }

    public static IEnumerable<LocalResponseDTO> ToLocalResponseDTOList(this IEnumerable<Local> locais)
    {
        if(locais is null || !locais.Any()) return new List<LocalResponseDTO>();
        return locais.Select(local => local.ToLocalResponseDTO()!).ToList();
    }

    public static LocalUpdateResponseDTO? ToLocalUpdateResponseDTO(this Local local)
    {
        if(local is null) return null;
        return new LocalUpdateResponseDTO
        {
            Id = local.Id,
            Name = local.Nome,
            Latitude = local.Latitude,
            Longitude = local.Longitude,
          
        };
    }

    public static LocalUpdateRequestDTO? ToLocalUpdateRequestDTO(this Local local)
    {
        if(local is null) return null;
        return new LocalUpdateRequestDTO
        {
            Name = local.Nome,
            Latitude = local.Latitude,
            Longitude = local.Longitude
        };
    }


}
