using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class ServiceDTO
{
    public int? Id { get; set; }
    [StringLength(100)]
    public required string Name { get; set; }
    public required double ServiceFee { get; set; }

}

public class ServiceOnlyDTO
{
    [StringLength(100)]
    public required string ServiceName { get; set; }

    public required double ServiceFee { get; set; }
}