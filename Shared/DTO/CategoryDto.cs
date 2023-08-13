namespace edu_institutional_management.Shared.DTO;

public class CategoryDto {
  public Guid Id { get; set; }
  public string Name { get; set; }
  public List<FieldDto> Fields { get; set; }
}