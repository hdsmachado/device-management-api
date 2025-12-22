namespace Device.Api.Models;

public record PagedResponse<T>(
    IReadOnlyList<T> Items,
    PaginationMetadata Pagination
);

public record PaginationMetadata(
    int CurrentPage,
    int PageSize,
    int TotalItems,
    int TotalPages
);
